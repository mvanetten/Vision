using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vision.Domain.Entities;
using System.Web.Mvc;

namespace Vision.Domain.Abstract
{
    public interface IContactRepository
    {
        IList<Contact> GetContacts(string TenantID);
        SelectList GetContactsSelectList(string TenantID,int? select);
        Contact SaveContact(string TenantID,Contact ct);
        Contact GetContact(string TenantID,int id );
    }

}
