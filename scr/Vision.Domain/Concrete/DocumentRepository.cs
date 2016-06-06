using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vision.Domain.DAL;
using Vision.Domain.Entities;
using Vision.Domain.Abstract;

namespace Vision.Domain.Concrete
{
    public class DocumentRepository : IDocumentRepository
    {
        private DBVisionContext db = new DBVisionContext();
        
        public IList<Document> GetDocuments (string TenantID)
        {
            if (TenantID != null)
            {
                return db.Documents.Where(x => x.TenantID == TenantID).ToList();
            }
            return null;    
        }

        public int GetDocumentNumber(string TenantID, DocumentType type)
        {
            if (TenantID != null)
            {
                int total = db.Documents.Where(x => x.TenantID == TenantID && x.documenttype == type).Count() + 1;
            }
            return 0;
        }

        public void DeleteLine(int? id, string TenantId)
        {
            DocumentLine line = db.DocumentLines.Where(x => x.documentlineID == id && x.TenantID == TenantId).FirstOrDefault();
            if (line != null)
            {
                db.DocumentLines.Remove(line);
                db.SaveChanges();
            }
            
        }


        public void SaveDocument(Document document, string TenantId)
        {
            
            if (document.DocumentLine.Count > 0)
            {
                for (int c = 0; c < document.DocumentLine.Count;c++)
                {
                    document.DocumentLine[c].TenantID = TenantId;
                }
            }
            
            if (document.documentnumber == 0)
            {
                document.documentnumber = db.Documents.Where(x => x.TenantID == TenantId && x.documenttype == document.documenttype).OrderByDescending(x => x.documentnumber).Select(x => x.documentnumber).FirstOrDefault() + 1;
            }

            if (document.documentID == 0)
            {
                document.TenantID = TenantId;
                db.Documents.Add(document);
            }
            else
            {
                Document documentcontext = db.Documents.Where(x => x.documentID == document.documentID && x.TenantID == TenantId).FirstOrDefault();                
                if (documentcontext != null)
                {
                    documentcontext.contactID = document.contactID;
                    for (int i = 0; i < document.DocumentLine.Count; i++)
                    {
                        if (document.DocumentLine[i].documentlineID != 0)
                        {
                            documentcontext.DocumentLine[i].description = document.DocumentLine[i].description;
                            documentcontext.DocumentLine[i].discountpercentage = document.DocumentLine[i].discountpercentage;
                            documentcontext.DocumentLine[i].price = document.DocumentLine[i].price;
                            documentcontext.DocumentLine[i].quantity = document.DocumentLine[i].quantity;
                            documentcontext.DocumentLine[i].taxID = document.DocumentLine[i].taxID;
                        }
                        else
                        {
                            documentcontext.DocumentLine.Add(document.DocumentLine[i]);
                        }                       
                    }
                    
                    documentcontext.documentstatus = document.documentstatus;
                    documentcontext.documenttype = document.documenttype;
                    documentcontext.invoice_date = document.invoice_date;
                    documentcontext.footnote = document.footnote;
                    documentcontext.subject = document.subject;
                }
            }
            db.SaveChanges();
        }
    }
}
