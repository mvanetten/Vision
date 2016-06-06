using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace Vision.WebUI.Helpers
{
    public static class BootstrapAlerts
    {
        public static MvcHtmlString AlertBox(this HtmlHelper htmlHelper, string title, string message, string alert = "info")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format("<div class='alert alert-{0}'>", alert.ToString()))
              .AppendLine(String.Format("<strong>{0}</strong> {1}", title, message))
              .AppendLine("</div>");
            return new MvcHtmlString(sb.ToString());
        }
    }
}