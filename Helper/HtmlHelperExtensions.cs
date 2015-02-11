using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moody.Helper
{
    public static class HtmlHelperExtensions
    {
        public static string ActivePage(this HtmlHelper htmlHelper, string controller, string action)
        {
            string _classValue = "";

            string _currentController = htmlHelper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();

            string _currentAction = htmlHelper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString();

            if (_currentController == controller && _currentAction == action)
            {
                _classValue = "active";
            }
            else if (_currentController == controller && action.Equals(""))
            {
                _classValue = "has-sub active";
            }
            else
            {
                if (action.Equals(""))
                {
                    _classValue = "has-sub";
                }
            }

            return _classValue;
        }
    }
}