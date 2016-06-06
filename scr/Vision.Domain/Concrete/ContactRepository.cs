using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vision.Domain.DAL;
using Vision.Domain.Entities;
using Vision.Domain.Abstract;
using System.Web.Mvc;
namespace Vision.Domain.Concrete
{
    public class ContactRepository : IContactRepository
    {
        private DBVisionContext db = new DBVisionContext();
        
        public IList<Contact> GetContacts(string TenantID)
        {
            if (TenantID != null)
            {
                return db.Contacts.Where(x => x.tenantID == TenantID).ToList();
            }
            return null; 
        }

        public SelectList GetContactsSelectList(string TenantID,int? select)
        {
            if (TenantID != null)
            {
                return new SelectList (db.Contacts.Where(x => x.tenantID == TenantID),"contactID", "companyname", select);
            }
            return null;
        }

        public Contact SaveContact(string TenantID,Contact ct)
        {
            Contact contactcontext = db.Contacts.Where(x => x.tenantID == TenantID && x.contactID == ct.contactID).FirstOrDefault();
            if (contactcontext != null)
            {
                contactcontext.address1 = ct.address1;
                contactcontext.address2 = ct.address2;
                contactcontext.bankaccount = ct.bankaccount;
                contactcontext.chamberofcommerce = ct.chamberofcommerce;
                contactcontext.city = ct.city;
                contactcontext.companyname = ct.companyname;
                contactcontext.country = ct.country;
                contactcontext.email = ct.email;
                contactcontext.firstame = ct.firstame;
                contactcontext.lastname = ct.lastname;
                contactcontext.phone = ct.phone;
                contactcontext.taxnumber = ct.taxnumber;
                contactcontext.sendmethod = ct.sendmethod;
                contactcontext.zipcode = ct.zipcode;
                
            }
            else
            {
                ct.tenantID = TenantID;
                db.Contacts.Add(ct);
            }
            db.SaveChanges();
            return ct;
        }
        public Contact GetContact(string TenantID,int id)
        {
            if (id > 0 && TenantID != null)
            {
                Contact contact = db.Contacts.Where(c => c.contactID == id && c.tenantID == TenantID).First();
                if (contact != null)
                {
                    return contact;
                }
            }
            return null;
        }

    }
}
