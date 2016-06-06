using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Vision.Domain.Entities
{
    public class Tax
    {
        public int taxID { get; set; }
        public string TenantID { get; set; }

        [Required(ErrorMessage = "Geef een BTW Percentage op")]
        [Range(0.00, 100.00, ErrorMessage = "Geef een BTW Percentage op van 0.00 t/m 100.00")]
        public int taxrate { get; set; }

        public string displayname { get; set; }
        public string description { get; set; }
        public bool show { get; set; }
    }
}
