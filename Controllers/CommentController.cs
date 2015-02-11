using DataLibrary;
using DataLibrary.Model;
using Moody.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moody.Controllers
{
    public class CommentController : MultiBaseController
    {
        //
        // GET: /Comment/

        public ActionResult Index(int id)
        {
            if (Session["_userName"] == null)
            {
                return RedirectToAction("Index", "User");
            }

            SessionHelper _sHelper = new SessionHelper(ViewData, Session["_userName"].ToString(), Session["_userPermissionId"].ToString());

            int _userId = Convert.ToInt32(Session["_userId"]);

            Repository<Like> _rl = new Repository<Like>();
            Repository<Mood> _rm = new Repository<Mood>();

            bool amLike = false;
            Like _rLike = _rl.Find(o => o.userId == _userId && o.moodId == id  && o.isValid == true);

            if (_rLike == null)
            {
                amLike = false;
            }
            else
            {
                amLike = true;
            }

            Mood _mood = _rm.Find(o => o.id == id);

            Repository<Comment> _rc = new Repository<Comment>();
            IEnumerable<CommentHelper> _comments = _rc.All().Where(o => o.moodId == id).Select(s => new CommentHelper { comment= s.comment, createDate = s.createDate, id= s.id, isValid= s.isValid, moodId=s.moodId, userName=s.user.username });

            cPageHelper _commentPage = new cPageHelper { commentHelper= _comments, moodHelper = _mood , _myLİke = amLike };


            return View(_commentPage);
        }

        [HttpPost]
        public ActionResult Index(int dd, string txtReason)
        {
            if (Session["_userName"] == null)
            {
                return RedirectToAction("Index", "User");
            }

            SessionHelper _sHelper = new SessionHelper(ViewData, Session["_userName"].ToString(), Session["_userPermissionId"].ToString());

            Repository<Mood> _rm = new Repository<Mood>();
            Mood _mood = new Mood
            {
                createDate = DateTime.Now,
                moodTypeId = dd + 1,
                moodReason = txtReason,
                userId = Convert.ToInt32(Session["_userId"])
            };

            _rm.Add(_mood);
            _rm.Save();

            return RedirectToAction("Index");
        }

        public ActionResult sendComment(string comm,int hidid)
        {
            if (Session["_userName"] == null)
            {
                return RedirectToAction("Index", "User");
            }

            SessionHelper _sHelper = new SessionHelper(ViewData, Session["_userName"].ToString(), Session["_userPermissionId"].ToString());

            int _userId = Convert.ToInt32(Session["_userId"]);

            Repository<Mood> _rm = new Repository<Mood>();
            Mood _mood = _rm.Find(o => o.id == hidid);

            Repository<Comment> _rc = new Repository<Comment>();
            Comment _comment = null;
            IEnumerable<CommentHelper> _comments = null;
            cPageHelper _commentPage = null;

            bool amLike = false;
            Repository<Like> _rl = new Repository<Like>();
            Like _rLike = _rl.Find(o => o.userId == _userId && o.moodId == hidid && o.isValid == true);

            if (_rLike == null)
            {
                amLike = false;
            }
            else
            {
                amLike = true;
            }

            try
            {
                if (comm != "")
                {
                    _comment = new Comment { userId = _userId, moodId = hidid, createDate = DateTime.Now, comment = comm, isValid = true };
                    _rc.Add(_comment);
                    _rc.Save();

                    _mood.commentCount++;
                    _rm.Save();
                }
                else
                {
                    ModelState.AddModelError("nullComment", "You should enter your comment");

                    _comments = _rc.All().Where(o => o.moodId == hidid).Select(s => new CommentHelper { comment = s.comment, createDate = s.createDate, id = s.id, isValid = s.isValid, moodId = s.moodId, userName = s.user.username });

                    _commentPage = new cPageHelper { commentHelper = _comments, moodHelper = _mood, _myLİke = amLike };

                    return View("Index", _commentPage);
                }
            }
            catch
            {
                _comments = _rc.All().Where(o => o.moodId == hidid).Select(s => new CommentHelper { comment = s.comment, createDate = s.createDate, id = s.id, isValid = s.isValid, moodId = s.moodId, userName = s.user.username });

                _commentPage = new cPageHelper { commentHelper = _comments, moodHelper = _mood, _myLİke = amLike };
                ModelState.AddModelError("aProblem", "Something went wrong..Please try again");

                return View("Index", _commentPage);
                
            }

            

           _comments=  _rc.All().Where(o => o.moodId == hidid).Select(s => new CommentHelper { comment = s.comment, createDate = s.createDate, id = s.id, isValid = s.isValid, moodId = s.moodId, userName = s.user.username });

           _commentPage = new cPageHelper { commentHelper = _comments, moodHelper = _mood, _myLİke = amLike };

            return View("Index", _commentPage);
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
