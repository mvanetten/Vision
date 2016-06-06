using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Vision.Domain.Entities;

namespace Vision.Domain.DAL
{
    public class DBVisionInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DBVisionContext>
    {
        protected override void Seed(DBVisionContext context)
        {
            var tax = new List<Tax>
            {
                new Tax {displayname="21 %",taxrate=21,description="Hoog btw tarief",show=true,TenantID = "-1" },
                new Tax {displayname="6 %",taxrate=6,description="Laag btw tarief",show=true,TenantID = "-1" },
                new Tax {displayname="0 %",taxrate=0,description="Geen btw tarief",show=true,TenantID = "-1" }
            };
            tax.ForEach(s => context.Taxs.Add(s));
            context.SaveChanges();

            context.Contacts.Add(new Contact { address1 = "Adresstraat 1", address2 = "11a", companyname = "sunnus", contactID = 1, firstame = "mark", lastname = "van etten", email = "mark@sunnus.nl" });
            context.SaveChanges();

            var document = new List<Document>
            {
                new Document {documenttype=DocumentType.INVOICE, invoice_date=DateTime.Now, contactID = 1 },
                new Document {documenttype=DocumentType.INVOICE, invoice_date=DateTime.Now, contactID = 1},
                new Document {documenttype=DocumentType.INVOICE, invoice_date=DateTime.Now, contactID = 1},
                new Document {documenttype=DocumentType.INVOICE, invoice_date=DateTime.Now, contactID = 1}
            };

            document.ForEach(s => context.Documents.Add(s));
            context.SaveChanges();

            var documentline = new List<DocumentLine>
            {
                new DocumentLine {quantity=2,price=50,description="A Factuur regel 1",taxID=1,documentID=1 },
                new DocumentLine {quantity=23,price=0.99M,description="A Factuur regel 2",taxID=1,documentID=1 },
                new DocumentLine {quantity=4,price=0.23M,description="A Factuur regel 3",taxID=1,documentID=1 },
                new DocumentLine {quantity=10,price=120.10M,description="A Factuur regel 4",taxID=1,documentID=1 },

                new DocumentLine {quantity=2,price=50,description="B Factuur regel 1",taxID=1,documentID=2 },
                new DocumentLine {quantity=23,price=0.99M,description="B Factuur regel 2",taxID=1,documentID=2 },
                new DocumentLine {quantity=4,price=0.23M,description="B Factuur regel 3",taxID=1,documentID=2 },
                new DocumentLine {quantity=10,price=120.10M,description="B Factuur regel 4",taxID=2,documentID=2 },

                new DocumentLine {quantity=2,price=50, description="Discount 5% Factuur regel 1",taxID=1,documentID=3 },
                new DocumentLine {quantity=23,price=0.99M,description="Discount 5% Factuur regel 2",taxID=1,documentID=3 },
                new DocumentLine {quantity=4,price=0.23M,description="Discount 5% Factuur regel 3",taxID=1,documentID=3 },
                new DocumentLine {quantity=10,price=120.10M,description="Discount 5% Factuur regel 4",taxID=1,documentID=3 },

                new DocumentLine {quantity=1,price=100, description="test2",taxID=1,documentID=4,discountpercentage=5 },
                new DocumentLine {quantity=2,price=100,description="test3",taxID=2,documentID=4,discountpercentage=5 },
                new DocumentLine {quantity=1,price=23,description="test",taxID=1,documentID=4,discountpercentage=5 },
            };
            documentline.ForEach(s => context.DocumentLines.Add(s));
            context.SaveChanges();



        }
    }
}
