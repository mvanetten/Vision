using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vision.Domain.Concrete;
using Vision.Domain.Entities;

namespace Vision.Domain.Abstract
{
    public interface IDocumentRepository
    {
        IList<Document> GetDocuments(string TenantID);
        void SaveDocument(Document document, string TenantID);
        void DeleteLine(int? id, string TenantId);
    }
}
