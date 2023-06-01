using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPN.CreaterTom.Model;

namespace VPN.CreaterTom.Services
{
    class SettingServices
    {       
        private IWorkFileSetting _workFile;
        public SettingServices(IWorkFileSetting workFileSetting)
        {           
            _workFile = workFileSetting;
        }

        public ISetting GetSetting(string pathFile) 
        {
            return _workFile.ReadFileSetting(pathFile);
        }

        public void SaveSetting(string pathFileSave, ISetting setting) 
        {
            _workFile.SaveFileSetting(pathFileSave, setting);
        }
    }
}
