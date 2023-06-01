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
    public class WorkFile : IWorkFileSetting
    {
        public ISetting ReadFileSetting(string pathFileSetting)
        {            
            var stringSetting = string.Empty;

            if (!File.Exists(pathFileSetting))
            {
                ISetting setting = new SettingModel();
                SaveFileSetting(pathFileSetting, setting);
                return setting;
            }

            using (var streamReader = new StreamReader(pathFileSetting))
            {
               stringSetting =  streamReader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<SettingModel>(stringSetting);
        }

        public void SaveFileSetting(string pathFileSetting, ISetting settings)
        {
            var settingString = JsonConvert.SerializeObject(settings);
            using (var streamWriter = new StreamWriter(pathFileSetting))
            {
                streamWriter.WriteLine(settingString);
            }
        }      
    }
}
