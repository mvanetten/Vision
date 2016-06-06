using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vision.Domain.Entities;

namespace Vision.Domain.Abstract
{
    public interface ISettingRepository
    {
        SettingEmail GetSettingEmail(string TenantID);
        void SetSettingEmail(string TenantID, SettingEmail mailsettings);

        void SetSetting(string TenantID, Setting settings);
        Setting GetSetting(string TenantID);
    }
}
