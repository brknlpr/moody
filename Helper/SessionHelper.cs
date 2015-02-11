using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moody.Helper
{
    public class SessionHelper
    {
        public SessionHelper(ViewDataDictionary ViewData, String username, String permissionId)
        {
            ViewData["_userName"] = username;

            string _uPermissionId = permissionId;

            if (_uPermissionId == "1")
            {
                ViewData["_userPermission"] = "Admin";
            }
            if (_uPermissionId == "2")
            {
                ViewData["_userPermission"] = "User";
            }
        }
    }
}