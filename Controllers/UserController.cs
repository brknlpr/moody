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
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            ViewData.Clear();

            System.Web.Security.FormsAuthentication.SignOut();

            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            try
            {
                Repository<User> _ru = new Repository<User>();
                User _user = _ru.Find(w => w.username == username && w.password == password);
                bool _userStatus = _ru.Find(u => u.username == username).status;

                int _userId = 0;

                if (_user != null && _userStatus.Equals(true))
                {
                    _user.lastTime = DateTime.Now;
                    _ru.Save();

                    string _userType = _ru.Find(w => w.username == username && w.password == password).permissionType.permissionType;
                    int _userTypeId = _ru.Find(w => w.username == username && w.password == password).permissionTypeId;
                    _userId = _ru.Find(w => w.username == username && w.password == password).id;

                    Session["_userName"] = username;
                    Session["_userId"] = _userId;
                    Session["_userPermission"] = _userType;
                    Session["_userPermissionId"] = _userTypeId;

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("kullaniciHata", "Lütfen girdiğiniz bilgileri kontrol ediniz");
            }

            return RedirectToAction("Index", "User");
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
