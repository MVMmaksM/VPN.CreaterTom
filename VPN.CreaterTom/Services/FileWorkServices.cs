using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPN.CreaterTom.Model;

namespace VPN.CreaterTom.Services
{
    public class FileWorkServices
    {
        private IWorkFile _workFile;
        public FileWorkServices(IWorkFile workFileSetting)
        {
            _workFile = workFileSetting;
        }

        public ISetting GetSetting(string pathFile)
        {
            var settingsString = _workFile.ReadFile(pathFile);
            return JsonConvert.DeserializeObject<SettingModel>(settingsString);
        }

        public void SaveFile(string pathFileSave, byte[] bytesSettings)
        {           
            var parhDirectory = Path.GetDirectoryName(pathFileSave);

            if (!Directory.Exists(parhDirectory)) 
            {
                Directory.CreateDirectory(parhDirectory);
            }
            _workFile.SaveFile(pathFileSave, bytesSettings);
        }

        public void SaveSetting(string pathFileSave, byte[] bytesSettings) 
        {
            _workFile.SaveFile(pathFileSave, bytesSettings);
        }
    }
}
