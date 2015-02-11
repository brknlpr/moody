using Moody.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Moody.Controllers
{
    public class MultiBaseController : Controller
    {
       protected override void ExecuteCore()
        {
            //string cultureName = RouteData.Values["culture"] as string;

            //if (cultureName == null)
            //    cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null; // obtain it from HTTP header AcceptLanguage

            //cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            //if (RouteData.Values["culture"] as string != cultureName)
            //{

            //    // Force a valid culture in the URL
            //    RouteData.Values["culture"] = cultureName.ToLowerInvariant(); // lower case too

            //    // Redirect user
            //    Response.RedirectToRoute(RouteData.Values);
            //}


            //string cultureName = null;
            //string lang = null;
            //HttpCookie cultureCookie = Request.Cookies["Arsenal"];
            //if (cultureCookie != null)
            //{
            //    lang = cultureCookie.Value;
            //    switch (lang)
            //    {
            //        case "English":
            //            cultureName = "en-us";
            //            break;
            //        case "Turkish":
            //            cultureName = "tr-TR";
            //            break;
            //        default:
            //            cultureName = "en-us";
            //            break;
            //    }
            //}
            //else
            //{
            //    cultureName = "en-us";
            //}

            int culture = 0;
            if (this.Session == null || this.Session["CurrentCulture"] == null)
            {
                int.TryParse(System.Configuration.ConfigurationManager.AppSettings["Culture"], out culture);
                this.Session["CurrentCulture"] = culture;
            }
            else
            {
                culture = (int)this.Session["CurrentCulture"];
            }
            //
            SessionManager.CurrentCulture = culture;
            //
            // Invokes the action in the current controller context.
            //
            
            //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            //Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            base.ExecuteCore();
        }

       protected override bool DisableAsyncSupport
       {
           get { return true; }
       }
    }

    }

