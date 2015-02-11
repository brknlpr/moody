using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.SessionState;

namespace Moody.Helper
{
    public class SessionManager
    {
        protected HttpSessionState session;

        public SessionManager(HttpSessionState httpSessionState)
        {
            session = httpSessionState;
        }

        public static int CurrentCulture
        {
            get
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    return 0;
                else if (Thread.CurrentThread.CurrentUICulture.Name == "tr-TR")
                    return 1;
                else
                    return 0;
            }
            set
            {
                //
                // Set the thread's CurrentUICulture.
                //
                if (value == 0)
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                else if (value == 1)
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("tr-TR");
                else
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                //
                // Set the thread's CurrentCulture the same as CurrentUICulture.
                //
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
            }
        }
    }
}