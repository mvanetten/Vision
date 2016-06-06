using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vision.Domain.DAL;
using Vision.Domain.Entities;
using Microsoft.AspNet.Identity;
using Vision.WebUI.Models;
using System.Text;
using Vision.Domain.Abstract;
using System.Text.RegularExpressions;

namespace Vision.WebUI.Controllers
{
    public class DocumentController : Controller
    {
        private IDocumentRepository documentrepository { get; set; }
        private ITaxRepository taxrepository { get; set; }
        private IContactRepository contactrepository { get; set; }
        private ISettingRepository settingrepository { get; set; }
        private IMailSender mailsender { get; set; }

        private string TenantID { get { return User.Identity.IsAuthenticated ? User.Identity.GetUserId() : string.Empty; } }

        public DocumentController(IDocumentRepository documentrepo, ITaxRepository taxrepo, IContactRepository contactrepo, ISettingRepository settingrepo, IMailSender mails)
        {
            this.taxrepository = taxrepo;
            this.documentrepository = documentrepo;
            this.contactrepository = contactrepo;
            this.settingrepository = settingrepo;
            this.mailsender = mails;

        }

        // GET: Document
        [Authorize]
        public ActionResult Invoice()
        {
            return View("List", documentrepository.GetDocuments(TenantID).Where(x => x.documenttype == DocumentType.INVOICE));
        }

        [Authorize]
        public ActionResult Create()
        {
            int defaultTax = 1;
            Tax tx = this.taxrepository.GetTax(TenantID, defaultTax);
            Document doc = new Document();
            doc.invoice_date = DateTime.Now;          
            doc.documentstatus = DocumentStatus.DRAFT;
            doc.documenttype = DocumentType.INVOICE;
            doc.DocumentLine = new List<DocumentLine>();
            DocumentLine line = new DocumentLine { quantity = 0, discountpercentage = 0, price = 0.0M, description = "", taxID = defaultTax,tax = tx, documentID = doc.documentID};
            doc.DocumentLine.Add(line);
            ViewBag.taxID = taxrepository.GetTaxs(TenantID);
            ViewBag.contactID = contactrepository.GetContactsSelectList(this.TenantID, 0);
            ViewBag.TotalContacts = contactrepository.GetContacts(TenantID).Count;
            ViewBag.DocumentLineCalc = new DocumentCalc(doc);
            doc.footnote = "We verzoeken u vriendelijk het bovenstaande bedrag van {document.price} voor {document.invoice_duedate} te voldoen op onze bankrekening onder vermelding van het factuurnummer {document.documentnumber}. Voor vragen kunt u contact opnemen per e-mail.";
            return View(doc);
            
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Document doc, string button = "default")
        {
            int defaultTax = 1;
            Tax tx = this.taxrepository.GetTax(TenantID, defaultTax);

            switch (button.ToLower())
            {
                case "save":
                    if (ModelState.IsValid)
                    {
                        documentrepository.SaveDocument(doc, TenantID);
                        return RedirectToAction("Invoice");
                    }
                    break;
                case "saveandsend":
                    Contact con = contactrepository.GetContact(this.TenantID, doc.contactID);
                    SendDocument(doc, con);
                    break;

                case "appendline":
                    doc.DocumentLine.Add(new DocumentLine { quantity = 0, discountpercentage = 0, price = 0.0M, description = "", taxID = defaultTax, tax = tx, documentID = doc.documentID });
                    break;

                case "removeline":
                        doc.DocumentLine.RemoveAt(doc.DocumentLine.Count - 1);
                    break;

                default:
                    // Do nothing
                    break;
            }
            foreach (DocumentLine dl in doc.DocumentLine)
            {
                dl.tax = this.taxrepository.GetTax(TenantID, dl.taxID);
            }

            ViewBag.TotalContacts = contactrepository.GetContacts(TenantID).Count;
            ViewBag.DocumentLineCalc = new DocumentCalc(doc);
            ViewBag.contactID = contactrepository.GetContactsSelectList(this.TenantID, doc.contactID);
            ViewBag.taxID = taxrepository.GetTaxs(TenantID);
            return View(doc);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            int defaultTax = 1;
            Document doc = documentrepository.GetDocuments(TenantID).Where(x => x.documentID == id).FirstOrDefault();
            Tax tx = this.taxrepository.GetTax(TenantID, defaultTax);


            foreach (DocumentLine dl in doc.DocumentLine)
            {
                dl.tax = this.taxrepository.GetTax(TenantID, dl.taxID);
            }
            ViewBag.DocumentLineCalc = new DocumentCalc(doc);
            ViewBag.TotalContacts = contactrepository.GetContacts(TenantID).Count;
            ViewBag.contactID = contactrepository.GetContactsSelectList(this.TenantID, doc.contactID);
            ViewBag.taxID = taxrepository.GetTaxs(TenantID);
            return View(doc);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Document doc, string button = "default")
        {
            int defaultTax = 1;
            Tax tx = this.taxrepository.GetTax(TenantID, defaultTax);

            switch (button.ToLower())
            {
                case "save":
                    if (ModelState.IsValid)
                    {
                        documentrepository.SaveDocument(doc, TenantID);
                        return RedirectToAction("Invoice");
                    }
                    break;
                case "saveandsend":
                    Contact con = contactrepository.GetContact(this.TenantID, doc.contactID);
                    SendDocument(doc, con);
                    break;

                case "appendline":
                    doc.DocumentLine.Add(new DocumentLine { quantity = 0, discountpercentage = 0, price = 0.0M, description = "", taxID = defaultTax, tax = tx, documentID = doc.documentID });
                    break;

                case "removeline":
                    DocumentLine line = doc.DocumentLine.ElementAtOrDefault(doc.DocumentLine.Count - 1);
                    documentrepository.DeleteLine(line.documentlineID, TenantID);
                    doc.DocumentLine.RemoveAt(doc.DocumentLine.Count - 1);
                    break;

                default:
                    // Do nothing
                    break;
            }
            foreach (DocumentLine dl in doc.DocumentLine)
            {
                dl.tax = this.taxrepository.GetTax(TenantID, dl.taxID);
            }

            ViewBag.DocumentLineCalc = new DocumentCalc(doc);
            ViewBag.contactID = contactrepository.GetContactsSelectList(this.TenantID, doc.contactID);
            ViewBag.TotalContacts = contactrepository.GetContacts(TenantID).Count;
            ViewBag.taxID = taxrepository.GetTaxs(TenantID);
            return View(doc);
        }

        [Authorize]
        private ActionResult SendDocument(Document doc,Contact contact)
        {
            if (doc != null && contact != null)
            {
                mailsender.SubmitDocument(doc, contact, this.settingrepository.GetSettingEmail(TenantID), settingrepository.GetSetting(TenantID));
                TempData["message"] = "Document verzonden";
                return RedirectToAction("List");
            }
            TempData["message"] = "Document ongeldig";
            return RedirectToAction("Edit", new { id = doc.contactID });
        }
    }
}