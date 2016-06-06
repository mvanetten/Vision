using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vision.Domain.Abstract;
using Vision.Domain.DAL;
using Vision.Domain.Entities;

namespace Vision.Domain.Concrete
{
    public class SettingRepository : ISettingRepository
    {
        private DBVisionContext db = new DBVisionContext();


        public Setting GetSetting(string TenantID)
        {
            if (TenantID != null)
            {
                return db.Setting.Where(x => x.tenantID == TenantID).FirstOrDefault();
            }
            return null;
        }

        public void SetSetting(string TenantID, Setting settings)
        {
            Setting s = db.Setting.Where(x => x.tenantID == TenantID).FirstOrDefault();
            if (s != null)
            {
                s.address1 = settings.address1;
                s.address2 = settings.address2;
                s.bankaccount = settings.bankaccount;
                s.chamberofcommerce = settings.chamberofcommerce;
                s.city = settings.city;
                s.companyname = settings.companyname;
                s.country = settings.country;
                s.defaultduedateindays = settings.defaultduedateindays;
                s.defaulttax = settings.defaulttax;
                s.taxnumber = settings.taxnumber;
            }
            else
            {
                settings.tenantID = TenantID;
                db.Setting.Add(settings);
            }
            db.SaveChanges();
        }


        public SettingEmail GetSettingEmail(string TenantID)
        {
            if (TenantID != null)
            {
                return db.SettingEmail.Where(x => x.tenantID == TenantID).FirstOrDefault();
            }
            return null;
        }

        public void SetSettingEmail(string TenantID, SettingEmail mailsettings)
        {
            SettingEmail se = db.SettingEmail.Where(x => x.tenantID == TenantID).FirstOrDefault();
            if (se != null)
            {
                se.From = mailsettings.From;
                se.Password = mailsettings.Password;
                se.Port = mailsettings.Port;
                se.Server = mailsettings.Server;
                se.UseMyOwnEmailServer = mailsettings.UseMyOwnEmailServer;
                se.Username = mailsettings.Username;
                se.UseSSL = mailsettings.UseSSL;
            }
            else
            {
                mailsettings.tenantID = TenantID;
                db.SettingEmail.Add(mailsettings);
            }
            db.SaveChanges();
        }
    }
}
