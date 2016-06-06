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
    public class ContactController : Controller
    {
        private IContactRepository contactrepository { get; set; }
        private string TenantID { get { return User.Identity.IsAuthenticated ? User.Identity.GetUserId() : string.Empty; } }

        public ContactController (IContactRepository contactparam)
        {
            this.contactrepository = contactparam;

        }

        public ActionResult List()
        {
            IList<Contact> ct = contactrepository.GetContacts(this.TenantID);
            {
                return View(ct);
            }
        }

        public ActionResult Create()
        {
            Contact ct = new Contact();
            return View(ct);
        }

        [HttpPost]
        public ActionResult Create(Contact ct)
        {
            if (ModelState.IsValid)
            {
                contactrepository.SaveContact(this.TenantID,ct);
                TempData["message"] = String.Format("Contact {0} is aangemaakt", ct.companyname);
                TempData["title"] = "Gelukt";
                TempData["status"] = "success";
                return RedirectToAction("List");
            }
            return View(ct);
        }

        [HttpPost]
        public ActionResult Edit(Contact ct)
        {
            if (ModelState.IsValid)
            {
                contactrepository.SaveContact(this.TenantID, ct);
                return RedirectToAction("List");
            }
            return View(ct);
        }

        public ActionResult Edit(int id)
        {
            Contact ct = contactrepository.GetContact(this.TenantID, id);
            if (ct != null)
            {
                return View(ct);
            }
            return RedirectToAction("List");
        }

    }
}