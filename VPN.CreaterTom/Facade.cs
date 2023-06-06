using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPN.CreaterTom.Model;
using VPN.CreaterTom.Services;
using VPN.CreaterTom.View;

namespace VPN.CreaterTom
{
    public class Facade
    {
        private ISetting _setting;
        private InputModel _inputData;
        private FileWorkServices _fileServices;
        private IMessage _message;
        private string pathFileSetting = Path.Combine(Environment.CurrentDirectory, "appsettings.json");
        public delegate void HandlerInfoShow(string messageInfo);
        public event HandlerInfoShow OnHandlerInfoShow;

        public Facade(IMessage message)
        {
            _message = message;
            _inputData = new InputModel();
            _fileServices = new FileWorkServices(new WorkFile());
            _setting = _fileServices.GetSetting(pathFileSetting);
        }

        public InputModel GetInputData() => _inputData;

        public void SaveSettings()
        {
            var settingsString = JsonConvert.SerializeObject(_setting);
            var bytesSettings = Encoding.UTF8.GetBytes(settingsString);
            _fileServices.SaveFile(pathFileSetting, bytesSettings);
        }

        public ISetting GetSettings() => _setting;
        public void SetListNameDefault() => _inputData.ListName = default;
        public void SetListNumberDefault() => _inputData.ListNumber = default;

        public void CreateTom()
        {
            try
            {
                OnHandlerInfoShow?.Invoke($"Получение файлов из {_setting.PathLoadFile}");
                var files = Directory.GetFiles(_setting.PathLoadFile);
                
                OnHandlerInfoShow?.Invoke($"\nПолучено файлов: {files.Count()}");

                byte[] resultTomBytes = default;

                if (files.Length == 0)
                {
                    _message.ShowError($"Не найдены файлы по пути: {_setting.PathLoadFile}");
                    return;
                }

                if (_inputData.RbtnListName)
                {
                    OnHandlerInfoShow?.Invoke($"\nВыполнение");
                    resultTomBytes = CreatorTom.CreateTom(files, _inputData.ListName);
                }
                else if (_inputData.RbtnListNumber)
                {
                    OnHandlerInfoShow?.Invoke($"\nВыполнение");
                    resultTomBytes = CreatorTom.CreateTom(files, _inputData.ListNumber);
                }
                else
                {
                    OnHandlerInfoShow?.Invoke($"\nВыполнение");
                    resultTomBytes = CreatorTom.CreateTom(files);
                }

                var pathSaveFileTom = Path.Combine(_setting.PathSaveFile, _inputData.NameTom + ".xlsx");
                _fileServices.SaveFile(pathSaveFileTom, resultTomBytes);

                _message.ShowInfo($"Файл сохранен: {pathSaveFileTom}");
            }
            catch (Exception ex)
            {
                _message.ShowError(ex.Message + "\nПодробная информация в log-файле");
            }           
        }
    }
}
