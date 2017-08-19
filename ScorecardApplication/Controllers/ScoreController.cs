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
            return View();
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
                     }
                if (item.Contains("add"))
                {
                    int groupid = Convert.ToInt32(item.Substring(18, item.IndexOf("]") - 18));
                    model.scorecardgroups[groupid].scorecarditems.Add(new ScorecardItem());
                }
            }


            if (submit == "Add Group")
            {
                model.scorecardgroups.Add(new ScorecardGroup { groupname = "New Group" });
                model.scorecardgroups[model.scorecardgroups.Count -1].scorecarditems.Add(new ScorecardItem());
            }


            if (submit == "Save")
            {

                dsScorecard ScorecardDataset = new dsScorecard();

                ScorecardTableAdapter ScorecardTA = new ScorecardTableAdapter();
                ScorecardItemGroupTableAdapter ScorecardGroupTA = new ScorecardItemGroupTableAdapter();
                ScorecardItemTableAdapter ScorecardItemTA = new ScorecardItemTableAdapter();

                int ScorecardID = ScorecardTA.Insert(model.scorecardname, model.scorecarddescription, model.passmark);

                for (int j = 0; j < model.scorecardgroups.Count; j++)
                {
                    ScorecardGroup group = model.scorecardgroups[j];
                    int GroupID = ScorecardGroupTA.Insert(ScorecardID, group.groupname, group.groupdescription, group.pasmark);

                    for (int i = 0; i < model.scorecardgroups[j].scorecarditems.Count; i++)
                    {
                        ScorecardItem item = model.scorecardgroups[j].scorecarditems[i];
                        ScorecardItemTA.Insert(ScorecardID, item.question, item.questiontype, item.possibleanswers, item.scoremodifier, false, GroupID);
                    }

                }
                Redirect("/home/index");
            }

                return View(model);
        }
         
 


    }
}