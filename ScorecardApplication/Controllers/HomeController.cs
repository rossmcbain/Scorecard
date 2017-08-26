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
                    
                };
            }

            return View(model);
        }
    }
}