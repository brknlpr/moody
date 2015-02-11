using DataLibrary.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Moody.Helper
{
    public class MoodHelper
    {
        public int id { get; set; }

        public string userName { get; set; }

        public string moodType { get; set; }

        public string moodReason { get; set; }

        public int day { get; set; }

        [DisplayFormat(DataFormatString = "MMMM")]
        public DateTime month { get; set; }

        public int likeCount { get; set; }

        public int commentCount { get; set; }

        
    }
}