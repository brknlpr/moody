using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Moody.Helper
{
    public class CommentHelper
    {
        public int id { get; set; }

        public string userName { get; set; }

        public string comment { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime createDate { get; set; }

        public int moodId { get; set; }

        public bool isValid { get; set; }
    }
}