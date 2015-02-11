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
    public class LikeController : Controller
    {
        //
        // GET: /Like/

        public void Index(int? id)
        {
            //if (Session["_userName"] == null)
            //{
            //    return RedirectToAction("Index", "User");
            //}

            int moodId = Convert.ToInt32(id);

            SessionHelper _sHelper = new SessionHelper(ViewData, Session["_userName"].ToString(), Session["_userPermissionId"].ToString());

            int _userId = Convert.ToInt32(Session["_userId"]);

            Repository<Like> _rl = new Repository<Like>();
            Like _like = new Like { userId = _userId, moodId = Convert.ToInt32(moodId), isValid = true };

            _rl.Add(_like);
            _rl.Save();

        }

    }
}
