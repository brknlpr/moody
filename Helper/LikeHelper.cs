using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moody.Helper
{
    public class LikeHelper
    {
        public int id { get; set; }

        public int userId { get; set; }
        public string userName { get; set; }

        public int moodId { get; set; }

        public bool isValid { get; set; }
    }
}