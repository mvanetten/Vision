using System.ComponentModel.DataAnnotations;
namespace Vision.Domain.Entities
{
    public class Contact
    {
        public int contactID { get; set; }
        public string tenantID { get; set; }

        [Display(Name = "Bedrijfsnaam")]
        [Required(ErrorMessage = "Geef een bredrijfsnaam op")]
        public string companyname { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public string country { get; set; }

        [Display(Name = "Emailadres")]
        [EmailAddress(ErrorMessage = "Ongeldig emailadres")]
        [Required(ErrorMessage ="Emailadres is verplicht")]
        public string email { get; set; }

        [Display(Name = "Voornaam")]
        [Required(ErrorMessage = "Geef een voornaam op")]
        public string firstame { get; set; }

        [Display(Name = "Achternaam")]
        [Required(ErrorMessage = "Geef een achternaam op")]
        public string lastname { get; set; }

        [Display(Name = "Volledige naam")]
        public string name { get { return firstame + " " + lastname; } }

        [Phone(ErrorMessage = "Geen geldig telefoonnummer")]
        public string phone { get; set; }
        public SendMethod sendmethod { get; set; }
        public string taxnumber { get; set; }
        public string bankaccount { get; set; }
        public string chamberofcommerce { get; set; }
    }

    public enum SendMethod
    {
        EMAIL,MANUAL
    }
}
