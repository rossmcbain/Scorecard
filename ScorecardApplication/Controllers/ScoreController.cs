using ScorecardApplication.Datasets;
using ScorecardApplication.Datasets.dsScorecardTableAdapters;
using ScorecardApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

            UserTableAdapter UserTA = new UserTableAdapter();
            UserTA.Fill(ScorecardDataset.User);

            model.agentscored = new User();
            model.UserList = new List<SelectListItem>();
            foreach(dsScorecard.UserRow UserRow in ScorecardDataset.User)
            {
                model.UserList.Add(new SelectListItem
                {
                    Text = UserRow.FirstName + " " + UserRow.Surname,
                    Value = UserRow.UserID.ToString()
                });
            }

            List<string> ScorecardTemplates = new List<string>();
            model.ScorecardList = new List<SelectListItem>();
            foreach (dsScorecard.ScorecardRow row in ScorecardDataset.Scorecard)
            {
                //ScorecardTemplates.Add(row.ScorecardDescription);\         
                SelectListItem item = new SelectListItem
                {
                    Text = row.ScorecardName,
                    Value = row.ScorecardID.ToString()
                };
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
                RecordingTA.Fill(ScorecardDataset.RecordingSelectCommand,model.callreference.ToString());
                model.Callrecordinglist = new List<SelectListItem>();
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
                foreach (dsScorecard.ScorecardItemGroupRow GroupRow in ScorecardDataset.ScorecardItemGroup.Rows)
                {
                    ScorecardGroup NewGroup = new ScorecardGroup() {
                    groupid = GroupRow.ScorecardItemGroupID,
                    groupdescription = GroupRow.Description,
                    groupname = GroupRow.GroupName,
                    pasmark = GroupRow.PassScore};
                    SCItemTA.Fill(ScorecardDataset.ScorecardItem, model.scorecardid,GroupRow.ScorecardItemGroupID);

                    if (ScorecardDataset.ScorecardItem.Rows.Count > 0) { NewGroup.scorecarditems = new List<ScorecardItem>(); }
                    foreach (dsScorecard.ScorecardItemRow ItemRow in ScorecardDataset.ScorecardItem.Rows)
                    {
                        ScorecardItem NewItem = new ScorecardItem();
                        NewItem.autofail = ItemRow.AutoFail.ToString();
                        String[] PossibleAnswerArray = ItemRow.PossibleAnswers.Split("|".ToCharArray());
                        NewItem.possibleanswerslist = new List<SelectListItem>();
                        foreach(String possibleanswer in PossibleAnswerArray)
                        {
                            SelectListItem SelectItem = new SelectListItem
                            {
                                Text = possibleanswer,
                                Value = possibleanswer
                            };
                            NewItem.possibleanswerslist.Add(SelectItem);
                        };
                        NewItem.question = ItemRow.Question;
                        NewItem.questiontype = ItemRow.QuestionType;
                        NewItem.itemid = ItemRow.ScorecardItemID;
                        NewGroup.scorecarditems.Add(NewItem);
                        

                    }

                    model.scorecardgroups.Add(NewGroup);
                }


               
            }

            if(submit == "Save Scorecard")
            {
                ResultTableAdapter ResultTA = new ResultTableAdapter();
                ResultItemTableAdapter ResultItemTA = new ResultItemTableAdapter();
                ResultGroupTableAdapter ResultGroupTA = new ResultGroupTableAdapter();
                UserTableAdapter UserTA = new UserTableAdapter();
                UserTA.Fill(ScorecardDataset.User);
                int ScorerID  = 0;
                foreach(dsScorecard.UserRow row in ScorecardDataset.User)
                {
                    if (row.Username.ToLower() == User.Identity.Name.ToLower())
                    {
                        ScorerID = row.UserID;
                    }
                }


                int ResultID = Convert.ToInt32(ResultTA.ResultInsertCommand(model.agentscored.userid, ScorerID, model.scorecardid, DateTime.Now, model.callreference, 0, model.comment));

               foreach(ScorecardGroup Group in model.scorecardgroups)
                {
                    int GroupID = 0;
                    GroupID = Convert.ToInt32(ResultGroupTA.ResultGroupInsertCommand(ResultID, Group.groupid, Group.comment, 0));
                    
                    foreach(ScorecardItem Item in Group.scorecarditems)
                    {
                        ResultItemTA.ResultItemInsertCommand(ResultID, Item.itemid, Item.answer, 0, Item.comment, GroupID);
                    }


                }

                return (Redirect("/home/index"));


            }

            return View(model);
        }

        // GET: NewScorecard
        public ActionResult NewScorecard()
        {


            Models.ScorecardModel model = new Models.ScorecardModel();
            model.scorecardgroups = new List<ScorecardGroup>();
            //model.scorecardgroups.Add(new ScorecardGroup { groupname = "Call Opening" });
            //model.scorecardgroups[0].scorecarditems.Add(new ScorecardItem());
            
        
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
                    int questionid = Convert.ToInt32(item.Replace(item.Substring(0, item.IndexOf("]") + 1), "").Replace(".scorecarditems[", "").Replace("].delete", ""));
                    ScorecardItem SCItem = model.scorecardgroups[groupid].scorecarditems[questionid];
                    model.scorecardgroups[groupid].scorecarditems.Remove(SCItem);
                    //return View(model);
                }
                if (item.Contains("addpf"))
                {
                  
                    int groupid = Convert.ToInt32(item.Substring(18, item.IndexOf("]") - 18));
                    List<SelectListItem> PossibleAnswersList = new List<SelectListItem>();
                    PossibleAnswersList.Add(new SelectListItem { Text = "Pass", Value = "Pass" });
                    PossibleAnswersList.Add(new SelectListItem { Text = "Fail", Value = "Fail" });
                    if (model.scorecardgroups[groupid].scorecarditems == null) { model.scorecardgroups[groupid].scorecarditems = new List<ScorecardItem>(); }
                                      ScorecardItem SCitem = new ScorecardItem()
                    {
                        questiontype = "Pass/Fail",
                        possibleanswers = "Pass|Fail",
                        possibleanswerslist = PossibleAnswersList
                    };
                    model.scorecardgroups[groupid].scorecarditems.Add(SCitem);
                    ViewData[$"m.scorecardgroups[{groupid}].scorecarditems[{(model.scorecardgroups[groupid].scorecarditems.Count-1).ToString()}].possibleanswers"] = "Pass|Fail";
                    //return View(model);
                }
                if (item.Contains("addmc"))
                {
                    int groupid = Convert.ToInt32(item.Substring(18, item.IndexOf("]") - 18));
                    if (model.scorecardgroups[groupid].scorecarditems == null) { model.scorecardgroups[groupid].scorecarditems = new List<ScorecardItem>(); }
                    model.scorecardgroups[groupid].scorecarditems.Add(new ScorecardItem() { questiontype = "Multiple Choice" });
                    //return View(model);
                }
                if (item.Contains("newitem"))
                {
                    string key = item.Replace("newitem", "itemvalue");
                    string[] value = Request.Form.GetValues(key);
                    int groupid = Convert.ToInt32(item.Substring(18, item.IndexOf("]") - 18));
                    int questionid = Convert.ToInt32(item.Replace(item.Substring(0, item.IndexOf("]") + 1), "").Replace(".scorecarditems[", "").Replace("].newitem", ""));

                    string[] existinganswers = new String[]{ };
                    string existinganswer = Request.Form.GetValues($"m.scorecardgroups[{groupid}].scorecarditems[{questionid}].possibleanswers")[0];
                    if (existinganswer != "")
                    { 
                    existinganswers = existinganswer.Split(Convert.ToChar("|"));
                    }
                  

                    if (model.scorecardgroups[groupid].scorecarditems[questionid].possibleanswerslist == null)
                    {
                        model.scorecardgroups[groupid].scorecarditems[questionid].possibleanswerslist = new List<SelectListItem>();
                        if (existinganswers != null)
                        {
                            foreach (string answer in existinganswers)
                            {
                                model.scorecardgroups[groupid].scorecarditems[questionid].possibleanswerslist.Add(new SelectListItem { Text = answer, Value = answer });
                            }
                        }
                    }
                    model.scorecardgroups[groupid].scorecarditems[questionid].possibleanswerslist.Add(new SelectListItem { Text = value[0], Value = value[0] });

                    String possibleanswers = "";
                    foreach (SelectListItem answer in model.scorecardgroups[groupid].scorecarditems[questionid].possibleanswerslist)
                        {
                        if(possibleanswers != "")
                        {
                            possibleanswers = possibleanswers+ "|";
                        }
                        possibleanswers = possibleanswers + answer.Text;
                        }
                    model.scorecardgroups[groupid].scorecarditems[questionid].possibleanswers = possibleanswers;
                    ViewData[$"m.scorecardgroups[{groupid}].scorecarditems[{questionid}].possibleanswers"] = possibleanswers;

                   // return View(model);
                }

            }


            if (submit == "Add Group")
            {
                if(model.scorecardgroups == null) { model.scorecardgroups = new List<ScorecardGroup>(); }
                model.scorecardgroups.Add(new ScorecardGroup { groupname = "New Group" });
                model.scorecardgroups[model.scorecardgroups.Count - 1].scorecarditems = new List<ScorecardItem>();
              

               // return View(model);
            }



            for (int j = 0; j < model.scorecardgroups.Count; j++)
            {
                ScorecardGroup Group = model.scorecardgroups[j];

                for (int i = 0; i < model.scorecardgroups[j].scorecarditems.Count; i++)
                {
                    ScorecardItem Item = model.scorecardgroups[j].scorecarditems[i];
                    String PreviousAnswers = "";

                    PreviousAnswers = Item.possibleanswers;
                    if(PreviousAnswers == null || PreviousAnswers == "")
                    { 
                        try
                        {
                            PreviousAnswers = Request.Form.GetValues($"m.scorecardgroups[{j}].scorecarditems[{i}].possibleanswers")[0];
                        }
                        catch (Exception e)
                        {
                            PreviousAnswers = Item.possibleanswers;
                        }                    

                    }


                    if (PreviousAnswers != null)
                    { 
                    String[] PossibleAnswers = PreviousAnswers.Split(Convert.ToChar("|"));

                    ViewData[$"m.scorecardgroups[{j}].scorecarditems[{i}].possibleanswers"] = PreviousAnswers;
                    Item.possibleanswerslist = new List<SelectListItem>();
                    foreach (String Answer in PossibleAnswers)
                    {
                        Item.possibleanswerslist.Add(new SelectListItem { Text = Answer, Value = Answer });
                    }
                }
                    
                }

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

                        string existinganswer = Request.Form.GetValues($"m.scorecardgroups[{j}].scorecarditems[{i}].possibleanswers")[0];

                        ScorecardItemTA.Insert(ScorecardID, item.question, item.questiontype, existinganswer, item.scoremodifier, autofail, GroupID,item.answer);
                    }

                }
                return(Redirect("/home/index"));
            }
           
                return View(model);
            
      
        }
         

        public ActionResult Results()
        {
            Models.Results model = new Models.Results();
            dsScorecard ScorecardDataset = new dsScorecard();
            ScorecardTableAdapter ScorecardTA = new ScorecardTableAdapter();

            ScorecardTA.Fill(ScorecardDataset.Scorecard,null);

            model.ListOfTemplates = new List<SelectListItem>();

            foreach(dsScorecard.ScorecardRow Row in ScorecardDataset.Scorecard)
            {
                model.ListOfTemplates.Add(new SelectListItem { Value = Row.ScorecardID.ToString(), Text = Row.ScorecardName });
            }


            return View(model);
        }


        [HttpPost]
        public ActionResult Results(Models.Results model)
        {

            UserTableAdapter UserTA = new UserTableAdapter();
            ResultTableAdapter ResultTA = new ResultTableAdapter();
            dsScorecard ScorecardDataset = new dsScorecard();
            ScorecardTableAdapter ScorecardTA = new ScorecardTableAdapter();
            ScorecardTA.Fill(ScorecardDataset.Scorecard, model.ScorecardID);

            model.ScorecardName = ((dsScorecard.ScorecardRow)ScorecardDataset.Scorecard.Rows[0]).ScorecardName;
            ResultTA.Fill(ScorecardDataset.Result,Convert.ToInt32(model.ScorecardID),null);
            UserTA.Fill(ScorecardDataset.User);

            model.ListOfResults = new Dictionary<int, ScorecardModel>();
            foreach(dsScorecard.ResultRow Row in ScorecardDataset.Result)
            {
                dsScorecard.UserRow UserRow = ScorecardDataset.User.FindByUserID(Row.AgentID);
                dsScorecard.UserRow ScorerRow = ScorecardDataset.User.FindByUserID(Row.ScorerID);
                ScorecardModel Scorecard = new ScorecardModel
                {
                    score = Row.Score,
                    datescored = Row.DateScored,
                    agentscored = new User
                    {
                        firstname = UserRow.FirstName,
                        surname = UserRow.Surname
                    },
                    scoredby = new User
                    {
                        firstname = ScorerRow.FirstName,
                        surname = ScorerRow.Surname
                    }
                };
                model.ListOfResults.Add(Row.ResultID, Scorecard);
            }


            return View(model);
        }


        public ActionResult ScorecardResult(int ResultID)
        {
            ScorecardModel model = new ScorecardModel();
            dsScorecard ScorecardDataset = new dsScorecard();
            ResultTableAdapter ResultTA = new ResultTableAdapter();
            ResultGroupTableAdapter ResultGroupTA = new ResultGroupTableAdapter();
            ResultItemTableAdapter ResultItemTA = new ResultItemTableAdapter();

            ScorecardTableAdapter ScorecardTA = new ScorecardTableAdapter();
            ScorecardItemGroupTableAdapter ScorecardGroupTA = new ScorecardItemGroupTableAdapter();
            ScorecardItemTableAdapter ScorecardItemTA = new ScorecardItemTableAdapter();

        

            ResultTA.Fill(ScorecardDataset.Result, null, ResultID);
            ResultGroupTA.Fill(ScorecardDataset.ResultGroup, ResultID);
            ResultItemTA.Fill(ScorecardDataset.ResultItem, ResultID);
            dsScorecard.ResultRow ResultRow = ((dsScorecard.ResultRow)ScorecardDataset.Result.Rows[0]);

            UserTableAdapter UserTA = new UserTableAdapter();
            UserTA.Fill(ScorecardDataset.User);

            dsScorecard.UserRow AgentScoredRow = ScorecardDataset.User.FindByUserID(ResultRow.AgentID);
            dsScorecard.UserRow ScoredByRow = ScorecardDataset.User.FindByUserID(ResultRow.ScorerID);

            ScorecardTA.Fill(ScorecardDataset.Scorecard, ResultRow.ScorecardID);
            ScorecardGroupTA.Fill(ScorecardDataset.ScorecardItemGroup, ResultRow.ScorecardID);
            
            dsScorecard.ScorecardRow ScorecardRow = ((dsScorecard.ScorecardRow)ScorecardDataset.Scorecard.Rows[0]);

            model.scorecardname = ScorecardRow.ScorecardName;
            model.scorecarddescription = ScorecardRow.ScorecardDescription;
            model.scorecardgroups = new List<ScorecardGroup>();
            model.agentscored = new User {
                emailaddress = AgentScoredRow.EmailAddress,
                firstname = AgentScoredRow.FirstName,
                surname = AgentScoredRow.Surname
                };
            model.scoredby = new User
            {
                emailaddress = ScoredByRow.EmailAddress,
                firstname = ScoredByRow.FirstName,
                surname = ScoredByRow.Surname
            };
            model.comment = ResultRow.Comment;
            model.datescored = ResultRow.DateScored;
            model.callreference = ResultRow.CallReference;
            model.score = ResultRow.Score;

            foreach (dsScorecard.ScorecardItemGroupRow GroupRow in ScorecardDataset.ScorecardItemGroup)
            {
                DataRow[] ResultGroupRows = ScorecardDataset.ResultGroup.Select("ScorecardItemGroupID="+GroupRow.ScorecardItemGroupID.ToString());
                ScorecardGroup GroupItem = new ScorecardGroup
                {
                    groupname = GroupRow.GroupName,
                    pasmark = GroupRow.PassScore,
                    groupdescription = GroupRow.Description,
                    comment = ((dsScorecard.ResultGroupRow)ResultGroupRows[0]).Comment,
                    score = ((dsScorecard.ResultGroupRow)ResultGroupRows[0]).Score

                };

                GroupItem.scorecarditems = new List<ScorecardItem>();
                ScorecardDataset.ScorecardItem.Clear();
                ScorecardItemTA.Fill(ScorecardDataset.ScorecardItem, ResultRow.ScorecardID, GroupRow.ScorecardItemGroupID);
                foreach (dsScorecard.ScorecardItemRow ItemRow in ScorecardDataset.ScorecardItem)
                {
                    DataRow[] ResultItemRows = ScorecardDataset.ResultItem.Select("QuestionID=" + ItemRow.ScorecardItemID.ToString());
                    ScorecardItem Item = new ScorecardItem
                    {
                        question = ItemRow.Question,
                        autofail = ItemRow.AutoFail.ToString(),
                        questiontype = ItemRow.QuestionType,
                        scoremodifier = ItemRow.ScoreModifier,
                        answer = ((dsScorecard.ResultItemRow)ResultItemRows[0]).Answer,
                        comment = ((dsScorecard.ResultItemRow)ResultItemRows[0]).Comment,
                        score = ((dsScorecard.ResultItemRow)ResultItemRows[0]).Score
                    };
                    GroupItem.scorecarditems.Add(Item);
                }


                model.scorecardgroups.Add(GroupItem);


            }
           
            return View(model);
        }


    }
}