﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ScorecardApplication.Models
{
    public class ScorecardModel
    {
        public int scorecardid { get; set; }
        [Required]
        [Display(Name = "Scorecard Name")]
        public string scorecardname { get; set; }
        [Display(Name = "Scorecard Description")]
        public string scorecarddescription { get; set; }
        [Display(Name = "Pass Mark")]
        public int passmark { get; set; }
        public List<ScorecardGroup> scorecardgroups { get; set; }

        [Display(Name = "Date Scored")]
        public DateTime datescored { get; set; }
        [Required]
        [Display(Name = "Call Reference")]
        public int callreference { get; set; }
        public string recording { get; set; }
        public string recordingfilename { get; set; }
        [Display(Name = "Score")]
        public int score { get; set; }
        [Display(Name = "Comment")]
        public string comment { get; set; }
        [Display(Name = "Scored By")]
        public User scoredby { get; set; }
        [Required]
        [Display(Name = "Agent Scored")]
        public User agentscored { get; set; }


        public  List<SelectListItem> Callrecordinglist { get; set; }

        public List<SelectListItem> ScorecardList { get; set; }

        public List<SelectListItem> UserList { get; set; }

        public static List<SelectListItem> ScorecardQuestionTypeList { get => scorecardQuestionTypeList; set => scorecardQuestionTypeList = value; }

        private static List<SelectListItem> scorecardQuestionTypeList = new List<SelectListItem>()
    {
        new SelectListItem() {Text="Pass/Fail", Value="PassFail"},
        new SelectListItem() {Text="Multiple Choice", Value="MultipleChoice"}
    };


        public static List<SelectListItem> AutoFailList { get => autoFailList; set => autoFailList = value; }

        private static List<SelectListItem> autoFailList = new List<SelectListItem>()
    {
        new SelectListItem() {Text="Yes", Value="Yes"},
        new SelectListItem() {Text="No", Value="No"}
    };

    }


    public class ScorecardGroup
    {
        public ScorecardGroup()
        {
            scorecarditems = new List<ScorecardItem>();
        }

        [Required]
        [Display(Name = "Group Name")]
        public string groupname { get; set; }
        [Display(Name = "Group Description")]
        public string groupdescription { get; set; }
        [Display(Name = "Group Pass Mark")]
        public int pasmark { get; set; }
        public List<ScorecardItem> scorecarditems { get; set; }

        [Display(Name = "Group Score")]
        public int score { get; set; }
        [Display(Name = "Group Comment")]
        public string comment { get; set; }
        public int groupid { get; set; }
    }


    public class ScorecardItem
    {
        [Required]
        [Display(Name = "Question")]
        public string question { get; set; }
        [Display(Name = "Question Type")]
        public string questiontype { get; set; }
        [Display(Name = "Possible Answers")]
        public string possibleanswers { get; set; }
        [Display(Name = "Score Modifier")]
        public int scoremodifier { get; set; }
        [Display(Name = "Auto Fail")]
        public string autofail { get; set; }

        [Display(Name = "Answer")]
        public string answer { get; set; }
        [Display(Name = "Score")]
        public int score { get; set; }
        [Display(Name = "Comment")]
        public string comment { get; set; }

        public List<SelectListItem> possibleanswerslist { get; set; }
        public int itemid { get; set; }
    }


    public enum ScorecardQuestionType
    {
       PassFail,
        MultipleChoice
    }

    public class UserAccessRights
    {
        public  List<User> UserList;
    }
    
    public class User
    {

        public int userid;
        [Required]
        [Display(Name = "Username")]
        public string username;
        [Required]
        [Display(Name = "Firstname")]
        public string firstname;
        [Required]
        [Display(Name = "Surname")]
        public string surname;
        public string fullname { get => firstname + " " + surname; }
        [Required]
        [Display(Name = "Email Address")]
        public string emailaddress;
        public UserLevel userlevel;

        public List<SelectListItem> UserLevelList { get; set; }

    }

    public class UserLevel
    {
        public string decription;
        public List<string> pagepermissions;
    }



    public class Report
    {
        public List<string> ReportList;
    }




    public class Results
    {
        [Display(Name = "Scorecard")]
        public int ScorecardID { get; set; }
        public List<SelectListItem> ListOfTemplates { get; set; }
        public Dictionary <int, ScorecardModel> ListOfResults { get; set; }
    }

}