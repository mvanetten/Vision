using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vision.Domain.Entities
{
    public class DocumentManager
    {
        public Document document {get; set;}
        public Contact contacts { get; set; }
        public List<Tax> Tax { get; set; }
        public List<DocumentLine> lines { get; set; }


        public void Load(Document docParam)
        {
            this.document = docParam;
        }

        public void Create(DocumentType type)
        {
            Document doc = new Document();
            doc.invoice_date = DateTime.Now;
            doc.documentstatus = DocumentStatus.DRAFT;
            doc.documenttype = type;
            doc.DocumentLine = new List<DocumentLine>();
        }

        public void AddNewLine()
        {
            //lines.Add(new DocumentLine { quantity = 0, discountpercentage = 0, price = 0.0M, description = "", taxID = defaultTax, tax = tx, documentID = document.documentID };)
        }
        

    }
}
