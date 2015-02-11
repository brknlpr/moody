using DataLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moody.Helper
{
    public class MyMoodWLikeHelper
    {
        public IEnumerable<int> _myLike { get; set; }

        public IEnumerable<MoodHelper> _myMood { get; set; }
    }
}