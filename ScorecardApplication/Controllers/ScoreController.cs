using ScorecardApplication.Datasets;
using ScorecardApplication.Datasets.dsScorecardTableAdapters;
using ScorecardApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScorecardApplication.Controllers
{
    public class ScoreController : Controller
    {
        // GET: Score
        public ActionResult Score()
        {
            Models.ScorecardModel model = new Models.ScorecardModel();
            dsScorecard ScorecardDataset = new dsScorecard();

            ScorecardTableAdapter ScorecardTA = new ScorecardTableAdapter();
            ScorecardTA.Fill(ScorecardDataset.Scorecard,null);

            List<string> ScorecardTemplates = new List<string>();
            foreach(dsScorecard.ScorecardRow row in ScorecardDataset.Scorecard)
            {
                //ScorecardTemplates.Add(row.ScorecardDescription);\         
                SelectListItem item = new SelectListItem();
                item.Text = row.ScorecardName;
                item.Value = row.ScorecardID.ToString();
                model.ScorecardList.Add(item);
            }
            //ViewData["ScorecardList"] = ScorecardTemplates;

            return View(model);
        }

        [HttpPost]
        public ActionResult Score(ScorecardModel model, string submit)
        {

            dsScorecard ScorecardDataset = new dsScorecard();

            ScorecardTableAdapter ScorecardTA = new ScorecardTableAdapter();
            ScorecardTA.Fill(ScorecardDataset.Scorecard, model.scorecardid);

            dsScorecard.ScorecardRow ScoreCardRow = ScorecardDataset.Scorecard.FindByScorecardID(model.scorecardid);
            model.scorecardname = ScoreCardRow.ScorecardName;


            if (submit == "Load")
            {
               

                RecordingSelectCommandTableAdapter RecordingTA = new RecordingSelectCommandTableAdapter();
                RecordingTA.Fill(ScorecardDataset.RecordingSelectCommand,model.callreference);

               foreach(dsScorecard.RecordingSelectCommandRow row in ScorecardDataset.RecordingSelectCommand.Rows)
                {

                    SelectListItem item = new SelectListItem();
                    item.Text = row.Description;
                    item.Value = row.CallDate.ToString("ddMMyyyy") + "\\" + row.FileName;
                    model.Callrecordinglist.Add(item);

                }
            }

            if(submit == "Load Call")
            {
                string recordingpath = ScorecardApplication.Properties.Settings.Default.CallRecordingPath + model.recording;
                model.recordingfilename = recordingpath.Replace(".vox", ".mp3");

                ScorecardItemGroupTableAdapter SCGroupTA = new ScorecardItemGroupTableAdapter();
                SCGroupTA.Fill(ScorecardDataset.ScorecardItemGroup, model.scorecardid);

                ScorecardItemTableAdapter SCItemTA = new ScorecardItemTableAdapter();
               
                if(ScorecardDataset.ScorecardItemGroup.Rows.Count > 0) { model.scorecardgroups = new List<ScorecardGroup>(); }
                foreach(dsScorecard.ScorecardItemGroupRow GroupRow in ScorecardDataset.ScorecardItemGroup.Rows)
                {
                    ScorecardGroup NewGroup = new ScorecardGroup();
                    NewGroup.groupdescription = GroupRow.Description;
                    NewGroup.groupname = GroupRow.GroupName;
                    NewGroup.pasmark = GroupRow.PassScore;
                    SCItemTA.Fill(ScorecardDataset.ScorecardItem, model.scorecardid,GroupRow.ScorecardItemGroupID);

                    if (ScorecardDataset.ScorecardItem.Rows.Count > 0) { NewGroup.scorecarditems = new List<ScorecardItem>(); }
                    foreach (dsScorecard.ScorecardItemRow ItemRow in ScorecardDataset.ScorecardItem.Rows)
                    {
                        ScorecardItem NewItem = new ScorecardItem();
                        NewItem.autofail = ItemRow.AutoFail.ToString();
                        NewItem.possibleanswers = ItemRow.PossibleAnswers;
                        NewItem.question = ItemRow.Question;
                        NewItem.questiontype = ItemRow.QuestionType;
                        NewGroup.scorecarditems.Add(NewItem);

                    }

                    model.scorecardgroups.Add(NewGroup);
                }


               
            }

            return View(model);
        }

        // GET: NewScorecard
        public ActionResult NewScorecard()
        {


            Models.ScorecardModel model = new Models.ScorecardModel();
            model.scorecardgroups = new List<ScorecardGroup>();
            model.scorecardgroups.Add(new ScorecardGroup { groupname = "Call Opening" });
            model.scorecardgroups[0].scorecarditems.Add(new ScorecardItem());
        
            return View(model);
        }
        [HttpPost]
        public ActionResult NewScorecard(Models.ScorecardModel model, string submit)
        {

            System.Collections.Specialized.NameObjectCollectionBase.KeysCollection keys = Request.Form.Keys;
            foreach(string item in keys)
            {
                if(item.Contains("delete"))
                    {
                    int groupid = Convert.ToInt32(item.Substring(18,item.IndexOf("]")-18));
                    int questionid = Convert.ToInt32(item.Replace(item.Substring(0, item.IndexOf("]")+1), "").Replace(".scorecarditems[", "").Replace("].delete",""));
                    model.scorecardgroups[groupid].scorecarditems.RemoveAt(questionid);
                    return View(model);
                }
                if (item.Contains("add"))
                {
                    int groupid = Convert.ToInt32(item.Substring(18, item.IndexOf("]") - 18));
                    model.scorecardgroups[groupid].scorecarditems.Add(new ScorecardItem());
                    return View(model);
                }
            }


            if (submit == "Add Group")
            {
                model.scorecardgroups.Add(new ScorecardGroup { groupname = "New Group" });
                model.scorecardgroups[model.scorecardgroups.Count -1].scorecarditems.Add(new ScorecardItem());
                return View(model);
            }


            if (submit == "Save")
            {

                dsScorecard ScorecardDataset = new dsScorecard();

                ScorecardTableAdapter ScorecardTA = new ScorecardTableAdapter();
                ScorecardItemGroupTableAdapter ScorecardGroupTA = new ScorecardItemGroupTableAdapter();
                ScorecardItemTableAdapter ScorecardItemTA = new ScorecardItemTableAdapter();

                int ScorecardID = Convert.ToInt32(ScorecardTA.ScorecardInsertCommand(model.scorecardname, model.scorecarddescription, model.passmark));

                for (int j = 0; j < model.scorecardgroups.Count; j++)
                {
                    ScorecardGroup group = model.scorecardgroups[j];
                    int GroupID = Convert.ToInt32(ScorecardGroupTA.ScorecardItemGroupInsertCommand(ScorecardID, group.groupname, group.groupdescription, group.pasmark));

                    for (int i = 0; i < model.scorecardgroups[j].scorecarditems.Count; i++)
                    {
                        ScorecardItem item = model.scorecardgroups[j].scorecarditems[i];
                        bool autofail;
                        if(item.autofail == "Yes") { autofail = true; } else { autofail = false; }
                        ScorecardItemTA.Insert(ScorecardID, item.question, item.questiontype, item.possibleanswers, item.scoremodifier, autofail, GroupID);
                    }

                }
                Redirect("/home/index");
            }
           
                return View(model);
            
      
        }
         
 


    }
}