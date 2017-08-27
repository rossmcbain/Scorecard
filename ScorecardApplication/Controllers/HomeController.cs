using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult UserAccessRights()
        {
            ScorecardApplication.Models.UserAccessRights model = new ScorecardApplication.Models.UserAccessRights();
            UserTableAdapter UserTA = new UserTableAdapter();
            dsScorecard ScorecardDataset = new dsScorecard();
            UserTA.Fill(ScorecardDataset.User);
            model.UserList = new List<Models.User>();
            UserLevelTableAdapter UserLevelTA = new UserLevelTableAdapter();
            UserLevelTA.Fill(ScorecardDataset.UserLevel);

            
            foreach(dsScorecard.UserRow UserRow in ScorecardDataset.User)
            {
                dsScorecard.UserLevelRow UserLevelRow = ScorecardDataset.UserLevel.FindByUserLevelID(UserRow.UserLevelID);
                UserLevel UserLevelItem = new UserLevel { decription = UserLevelRow.Description };

                User UserItem = new User
                {
                    firstname = UserRow.FirstName,
                    surname = UserRow.Surname,
                    username = UserRow.Username,
                    emailaddress = UserRow.EmailAddress,
                    userlevel = UserLevelItem
                    
                    
                }; ;
                model.UserList.Add(UserItem);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult UserAccessRights(Models.User model)
        {
            return (RedirectToAction("EditUser", "Home"));
     
        }
        public ActionResult EditUser()
        {
            Models.User model = new Models.User();
            dsScorecard ScorecardDataset = new dsScorecard();
            UserLevelTableAdapter UserLevelTA = new UserLevelTableAdapter();
            UserLevelTA.Fill(ScorecardDataset.UserLevel);
            model.UserLevelList = new List<SelectListItem>();

            foreach(dsScorecard.UserLevelRow Row in ScorecardDataset.UserLevel)
            {
                model.UserLevelList.Add(new SelectListItem { Text = Row.Description, Value = Row.UserLevelID.ToString() });
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult EditUser(Models.User model)
        {
            UserTableAdapter UserTA = new UserTableAdapter();
            UserTA.Insert(model.username, model.firstname, model.surname, model.emailaddress, model.userlevel.userlevelid);


            return (RedirectToAction("UserAccessRights", "Home"));
        }

    }
}