using System.Collections.Generic;
using System.Linq;

namespace Vision.Domain.Entities
{
    public class DocumentCalc
    {
        private Document doc { get; set; }
        private IList<TaxCalc> tc { get; set; }

        public DocumentCalc (Document docParam)
        {
            this.doc = docParam;
            this.tc = new List<TaxCalc>();
        }

        public decimal SubTotal()
        {
            return doc.DocumentLine.Sum(x => x.total);
        }

        public decimal DiscountTotal()
        {
            return doc.DocumentLine.Sum(x => x.discountprice);
        }

        public decimal Total()
        {
            return doc.DocumentLine.Sum(x => x.totaltax + x.total);
        }

        public IList<TaxCalc> GetTaxLines()
        { 
            if (doc.DocumentLine.First().tax != null)
            {
                var FoundTaxes = this.doc.DocumentLine.GroupBy(e => e.tax.taxrate).ToList().Select(x => new { Total = x.Sum(y => y.total), TotalTax = x.Sum(y => y.totaltax), TaxRate = x.Key }).ToArray();

                foreach (var p in FoundTaxes)
                {
                    tc.Add(new TaxCalc { TaxRate = p.TaxRate, Total = p.Total, TotalTax = p.TotalTax });
                };
            }
            return tc;
        }




    }

    public class TaxCalc
    {
        public int TaxRate { get;set;}
        public decimal Total { get; set; }
        public decimal TotalTax { get; set; }
    }
}
