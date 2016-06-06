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
    public class TaxRepository : ITaxRepository
    {
        private DBVisionContext db = new DBVisionContext();

        public List<Tax> GetTaxs(string TenantID)
        {
            if (TenantID != null)
            {
                return db.Taxs.Where(x => x.TenantID == TenantID || x.TenantID == "-1").ToList();
            }
            return null;
        }

        public Tax GetTax(string TenantID, int taxID)
        {
            return db.Taxs.Where(x => x.TenantID == TenantID || x.TenantID == "-1" && x.taxID == taxID).FirstOrDefault();
        }

        public SelectList GetTaxsSelectList(string TenantID, int? select)
        {
            if (TenantID != null)
            {
                return new SelectList(db.Taxs.Where(x => x.TenantID == TenantID || x.TenantID == "-1"),"taxID","displayname", select);
            }
            return null;
        }
    }
}
