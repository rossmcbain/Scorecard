using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ScorecardApplication.Models
{
    public class ScorecardModel
    {
        [Required]
        [Display(Name = "SCorecard Name")]
        public string scorecardname { get; set; }
        [Display(Name = "Scorecard Description")]
        public string scorecarddescription { get; set; }
        [Display(Name = "Pass Mark")]
        public int passmark { get; set; }
        public List<ScorecardGroup> scorecardgroups { get; set; }

        [Display(Name = "Date Scored")]
        public DateTime datescored { get; set; }
        [Display(Name = "Call Reference")]
        public string callreference { get; set; }
        [Display(Name = "Score")]
        public int score { get; set; }
        [Display(Name = "Comment")]
        public string comment { get; set; }
        [Display(Name = "Scored By")]
        public User scoredby { get; set; }

        public static List<SelectListItem> ScorecardQuestionTypeList { get => scorecardQuestionTypeList; set => scorecardQuestionTypeList = value; }

        private static List<SelectListItem> scorecardQuestionTypeList = new List<SelectListItem>()
    {
        new SelectListItem() {Text="Pass/Fail", Value="PassFail"},
        new SelectListItem() {Text="Multiple Choice", Value="MultipleChoice"}
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

    }


    public class ScorecardItem
    {

        [Display(Name = "Question")]
        public string question { get; set; }
        [Display(Name = "Question Type")]
        public ScorecardQuestionType questiontype { get; set; }
        [Display(Name = "Possible Answers")]
        public List<string> possibleanswers { get; set; }
        [Display(Name = "Score Modifier")]
        public int scoremodifier { get; set; }
        [Display(Name = "Auto Fail")]
        public bool autofail { get; set; }

        [Display(Name = "Answer")]
        public string answer { get; set; }
        [Display(Name = "Score")]
        public int score { get; set; }
        [Display(Name = "Comment")]
        public string comment { get; set; }
    }


    public enum ScorecardQuestionType
    {
       PassFail,
        MultipleChoice
    }

  
    
    public class User
    {
        public string username;
        public string firstname;
        public string surname;
        public string emailaddress;
        public UserLevel userlevel;

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

    public class PersonViewModel
    {
        public PersonViewModel()
        {
            ScorecardItems = new List<ScorecardItem>();
        }
        [Required]
        [Display(Name = "Name")]
        public string scorecardname { get; set; }
        public string callreference { get; set; }
        public string desription { get; set; }
        public List<ScorecardItem> ScorecardItems { get; set; }
    }

    public class PersonJobSplitViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string JobRole { get; set; }
        [Display(Name = "Percentage")]
        public decimal SplitPercentage { get; set; }
    }

}