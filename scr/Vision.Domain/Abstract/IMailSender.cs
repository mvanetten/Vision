using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vision.Domain.Entities;

namespace Vision.Domain.Abstract
{
    public interface IMailSender
    {
        void SubmitDocument(Document doc, Contact contact, SettingEmail emailsettings, Setting settings);
    }
}
