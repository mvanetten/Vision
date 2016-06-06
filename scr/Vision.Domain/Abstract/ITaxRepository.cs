using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vision.Domain.Entities;
using System.Web.Mvc;

namespace Vision.Domain.Abstract
{
    public interface ITaxRepository
    {
        List<Tax> GetTaxs(string TenantID);
        SelectList GetTaxsSelectList(string TenantID, int? select);
        Tax GetTax(string TenantID, int taxID);
    }
}
