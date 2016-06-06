using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Vision.Domain.Entities;
using Vision.Domain.Abstract;
using System.Text;

namespace Vision.Domain.Concrete
{
    public class MailSender : IMailSender
    {
        public void SubmitDocument(Document doc, Contact con, SettingEmail emailsettings, Setting settings)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailsettings.UseSSL;
                smtpClient.Host = emailsettings.Server;
                smtpClient.Port = emailsettings.Port;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential { UserName = emailsettings.Username, Password = emailsettings.Password };

                StringBuilder Body = new StringBuilder(System.IO.File.ReadAllText(@"C:\Users\Mark\Documents\Visual Studio 2015\Projects\Vision\Vision.Domain\Template\EmailInvoiceTemplate.html"));             
                Body.Replace("#CustomerName#", con.name);
                Body.Replace("#InvoiceDueDate#", doc.invoice_duedate.ToShortDateString());
                Body.Replace("#InvoiceNumber#", doc.documentID.ToString());
                Body.Replace("#InvoiceURL#", doc.documentID.ToString());
                Body.Replace("#MyCompany#", settings.companyname);

                try
                {
                    MailMessage mail = new MailMessage(emailsettings.From, con.email, doc.documenttype.ToString(), Body.ToString());
                    mail.IsBodyHtml = true;
                    smtpClient.Send(mail);
                }
                catch 
                {
                    throw new NotImplementedException("Sending Mail Failed");
                }

            }
        }
    }
}
