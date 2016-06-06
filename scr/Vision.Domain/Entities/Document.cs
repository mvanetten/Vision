using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vision.Domain.Entities
{
    public enum DocumentType
    {
        INVOICE, ESTIMATE, PURCHASEINVOICE
    }
    public enum DocumentStatus
    {
        DRAFT, SEND
    }

    public enum PaymentStatus
    {
        NOTPAYED,PARTIALPAYED,PAYED
    }
    public class Document
    {
        public int documentID { get; set; }
        public string TenantID { get; set; }

        [Required(ErrorMessage = "Selecteer een contactpersoon")]
        [Range(1,9999999,ErrorMessage = "Selecteer een contactpersoon")]
        public int contactID { get; set; }
        public int documentnumber { get; set; }
        public DocumentType documenttype { get; set; }
        public DocumentStatus documentstatus { get; set; }

        [Display(Name = "Referentie")]
        [MaxLength(500,ErrorMessage = "Onderwerp kan niet langer zijn dan 500 karakters.")]
        public string subject { get; set; }

        [Display(Name = "Voettekst")]
        [MaxLength(2000, ErrorMessage = "Voettekst kan niet langer zijn dan 2000 karakters.")]
        [DataType(DataType.MultilineText)]
        public string footnote { get; set; }

        [Required]
        [Display(Name = "Factuurdatum")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime invoice_date { get; set; }


        public DateTime invoice_duedate {
            get {
                return invoice_date.AddDays(15);
            }
        }

        private DateTime Created { get; set; }
        private DateTime Modified { get; set; }

        public virtual IList<DocumentLine> DocumentLine { get; set; }
        public virtual Contact Contact {get;set;}


    }
}
