using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Vision.Domain.Entities
{
    public class Setting
    {
        public int settingID { get; set; }
        public string tenantID { get; set; }

        [Display(Name = "Bedrijfsnaam")]
        [Required(ErrorMessage = "Geef een bredrijfsnaam op")]
        public string companyname { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public string country { get; set; }
        public string taxnumber { get; set; }
        public string bankaccount { get; set; }
        public string chamberofcommerce { get; set; }
        public int defaultduedateindays { get; set; }
        public int defaulttax { get; set; }
        public string prefixinvoice { get; set; }
        public string prefixestimate { get; set; }
    }
}
