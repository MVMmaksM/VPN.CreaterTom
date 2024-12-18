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
            if (!File.Exists(pathFile))
            {
                var setting = new SettingModel();
                setting.PathLoadFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Файлы для загрузки");
                setting.PathSaveFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Том");
                var settingString = JsonConvert.SerializeObject(setting);
                var settingBytes = Encoding.UTF8.GetBytes(settingString);
                SaveSetting(pathFile, settingBytes);
                return setting;
            }
            var settingsString = _workFile.ReadFile(pathFile);
            return JsonConvert.DeserializeObject<SettingModel>(settingsString);
        }

        public void SaveFile(string pathFileSave, byte[] bytesSettings)
        {
            var pathDirectory = Path.GetDirectoryName(pathFileSave);

            if (!Directory.Exists(pathDirectory))
            {
                Directory.CreateDirectory(pathDirectory);
            }
            _workFile.SaveFile(pathFileSave, bytesSettings);
        }

        public void SaveSetting(string pathFileSave, byte[] bytesSettings)
        {
            _workFile.SaveFile(pathFileSave, bytesSettings);
        }
        public void DeleteFile(string pathFile) => _workFile.DeleteFile(pathFile);
        public int DeleteFiles(string[] pathFiles) => _workFile.DeleteFile(pathFiles);

        public int RenameFiles(string[] files) 
        {
            for (int i = 0; i < files.Length; i++) 
            {
                var splitNameFile = files[i].Split("_");

                if (splitNameFile.Length > 0) 
                {
                    var terr = splitNameFile
                        .Where(n => n.StartsWith("Терр="))
                        .FirstOrDefault();                 

                    if (terr.Length < 16)
                    {
                        var newNameFile = files[i].Replace(terr, terr + "000");
                        _workFile.RenameFile(files[i], newNameFile);
                    }
                }
            }
            return 0;
        }
    }
}
