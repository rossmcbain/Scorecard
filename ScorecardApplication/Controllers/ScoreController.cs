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
            Models.PersonViewModel model = new Models.PersonViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult NewScorecard(Models.PersonViewModel model)
        {

            //foreach (var key in formCollection.AllKeys)
            //{
            //    var value = formCollection[key];
            //}

            return View(model);
        }

    }
}