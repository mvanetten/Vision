using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vision.Domain.Entities;
using Vision.Domain.Abstract;
using Microsoft.AspNet.Identity;

namespace Vision.WebUI.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        
        private ISettingRepository settings { get; set; }
        private IMailSender sender { get; set; }
        private string TenantID { get { return User.Identity.IsAuthenticated ? User.Identity.GetUserId() : string.Empty; } }

        public SettingController(ISettingRepository setrepo, IMailSender mailsender)
        {
            this.settings = setrepo;
            this.sender = mailsender;
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Company()
        {
            Setting s = settings.GetSetting(TenantID);
            if (s != null)
            {
                return View(settings.GetSetting(TenantID));
            }
            else
            {
                return View(new Setting { defaultduedateindays = 14});
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Company(Setting s)
        {
            if (ModelState.IsValid)
            {
                settings.SetSetting(TenantID,s);
                TempData["message"] = "Bedrijfs instellingen zijn bewaard";
                return RedirectToAction("List");
            }
            return View(s);
        }

        public ActionResult Email()
        {
            return View(settings.GetSettingEmail(TenantID));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Email(SettingEmail email,string button = "default")
        {
            switch (button.ToLower())
            {
                case "save":
                    if (ModelState.IsValid)
                    {
                        settings.SetSettingEmail(TenantID, email);
                        TempData["message"] = "Email instellingen zijn bewaard";
                        return RedirectToAction("List");
                    }
                    break;
                case "testemail":
                    if (ModelState.IsValid)
                    {
                        sender.SubmitDocument(new Document { documentID = 0001, invoice_date = DateTime.Today, contactID = 0 },
                            new Contact { companyname = "Test bedrijf",firstame = "John", lastname = "Do", email = User.Identity.GetUserName() },
                            this.settings.GetSettingEmail(TenantID), settings.GetSetting(TenantID));
                        TempData["message"] = String.Format("Test email verzonden naar {0}", User.Identity.GetUserName());
                    }
                    break;
                default:
                    break;
                    
            }
            return View(email);


        }
    }
}