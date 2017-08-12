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
            return View(model);
        }
        [HttpPost]
        public ActionResult NewScorecard(Models.ScorecardModel model)
        {

            //foreach (var key in formCollection.AllKeys)
            //{
            //    var value = formCollection[key];
            //}

            return View(model);
        }

    }
}