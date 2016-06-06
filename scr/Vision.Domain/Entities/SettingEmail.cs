using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Vision.Domain.Entities
{
    public class SettingEmail
    {
        public int    SettingEmailID { get; set; }
        public string tenantID { get; set; }
        public bool   UseMyOwnEmailServer { get; set; }

        [EmailAddress(ErrorMessage = "Ongeldig email adres")]
        public string From  { get; set; }
        public bool   UseSSL  { get; set; }
        public string Username  { get; set; }

        [DataType(DataType.Password)]
        public string Password  { get; set; }

        public string Server  { get; set; }

        [Range(1, 65535,ErrorMessage = "Geef een waarde tussen 1 en 65535 op")]
        public int    Port { get; set; }
    }
}
