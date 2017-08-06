using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace ScorecardApplication.Models
{
    public class ScorecardModel
    {
        public string scorecardname;
        public string scorecarddescription;
        public int passmark;
        public List <ScorecardGroup> scorecardgroups;

        public DateTime datescored;
        public string callreference;
        public int score;
        public string comment;
        public User scoredby;
    }


    public class ScorecardGroup
    {
        public string groupname;
        public string groupdescription;
        public int pasmark;
        public List<ScorecardItem> scorecarditems;

        public int score;
        public string comment;

    }


    public class ScorecardItem
    {
        
        [Display(Name = "Question")]
        public string question { get; set; }
        public ScorecardQuestionType questiontype;
        public List<string> possibleanswers;
        public int scoremodifier;
        public bool autofail;

        [Display(Name = "Answer")]
        public string answer { get; set; }
        public int score;
        public string comment;
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