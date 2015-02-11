using DataLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moody.Helper
{
    public class cPageHelper
    {
        public Mood moodHelper { get; set; }

        public bool _myLİke { get; set; }

        public IEnumerable<CommentHelper> commentHelper { get; set; }
    }
}