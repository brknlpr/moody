using DataLibrary;
using DataLibrary.Model;
using Moody.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace Moody.Controllers
{
    public class HomeController : MultiBaseController
    {
        public ActionResult Index()
        {
            if (Session["_userName"] == null)
            {
                return RedirectToAction("Index", "User");
            }

            SessionHelper _sHelper = new SessionHelper(ViewData, Session["_userName"].ToString(), Session["_userPermissionId"].ToString());
            int _userId = Convert.ToInt32(Session["_userId"]);

            Repository<Like> _rl = new Repository<Like>();
            int _c = 0;

            IEnumerable<int> _rLike = _rl.All().Where(o => o.userId == _userId && o.isValid == true).Select(s => s.moodId);

            Repository<Mood> _rm = new Repository<Mood>();

            IEnumerable<Mood> _m = _rm.All().ToList();

            foreach (Mood item in _m)
            {
                _c = _rl.All().Where(o => o.moodId == item.id && o.isValid == true).Count();
                item.likeCount = _c;
                _rm.Save();
            }

            IEnumerable<MoodHelper> _moods = _rm.All().Select(s => new MoodHelper { id = s.id, moodReason = s.moodReason, moodType = s.moodType.moodType, userName = s.user.username, day = s.createDate.Day, month = s.createDate, likeCount = s.likeCount, commentCount=s.commentCount});

            MyMoodWLikeHelper _myMoodHelper = new MyMoodWLikeHelper { _myMood = _moods, _myLike = _rLike };

            return View(_myMoodHelper);
        }

        [HttpPost]
        public ActionResult Index(int? dd, string txtReason)
        {
            if (Session["_userName"] == null)
            {
                return RedirectToAction("Index", "User");
            }

            SessionHelper _sHelper = new SessionHelper(ViewData, Session["_userName"].ToString(), Session["_userPermissionId"].ToString());

            var ddChosen = dd;
            if (ddChosen == null)
            {
                ModelState.AddModelError("feelEmpty", "How you feel ?");
                return RedirectToAction("Index");
            }
            ddChosen = Convert.ToInt32(dd);

            Repository<Mood> _rm = new Repository<Mood>();
            Mood _mood = new Mood { createDate = DateTime.Now,
                                    moodTypeId = (int)ddChosen + 1,
                                    moodReason = txtReason,
                                    userId = Convert.ToInt32(Session["_userId"]),
                                    commentCount = 0
            };

            _rm.Add(_mood);
            _rm.Save();

            return RedirectToAction("Index");
        }

        public ActionResult Liked(int? id)
        {
            if (Session["_userName"] == null)
            {
                return RedirectToAction("Index", "User");
            }

            int moodId = Convert.ToInt32(id);

            SessionHelper _sHelper = new SessionHelper(ViewData, Session["_userName"].ToString(), Session["_userPermissionId"].ToString());

            int _userId = Convert.ToInt32(Session["_userId"]);

            Repository<Like> _rl = new Repository<Like>();

            Like isLike = _rl.Find(o => o.userId == _userId && o.moodId == moodId && o.isValid == true);

            if (isLike == null)
            {
                Like _like = new Like { userId = _userId, moodId = Convert.ToInt32(moodId), isValid = true };

                _rl.Add(_like);
                _rl.Save();
            }
            else
            {
                isLike.isValid = false;
                _rl.Save();
            }

            return RedirectToAction("Index");
        }

        public ActionResult ChangeCurrentCulture(int id)
        {
            //
            // Change the current culture for this user.
            //
            SessionManager.CurrentCulture = id;
            //
            // Cache the new current culture into the user HTTP session. 
            //
            Session["CurrentCulture"] = id;
            //
            // Redirect to the same page from where the request was made! 
            //
            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}
