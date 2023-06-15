using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VPN.CreaterTom.Model;
using VPN.CreaterTom.Services;
using VPN.CreaterTom.View;

namespace VPN.CreaterTom
{
    public class Facade
    {
        private ILogger _logger;
        private ISetting _setting;
        private InputModel _inputData;
        private FileWorkServices _fileServices;
        private IMessage _message;
        private string pathFileSetting = Path.Combine(Environment.CurrentDirectory, "appsettings.json");
        private static string pathFolderLog = $"{Environment.CurrentDirectory}\\logs";
        private static string pathCurrentLog = $"{pathFolderLog}\\{DateTime.Now:yyyy-MM-dd}.log";
        public delegate void HandlerInfoShow(string messageInfo);
        public event HandlerInfoShow OnHandlerInfoShow;

        public Facade(IMessage message, ILogger logger)
        {
            _logger = logger;
            _message = message;
            _inputData = new InputModel();
            _fileServices = new FileWorkServices(new WorkFile());

            _logger.Info($"Получение настроек: {pathFileSetting}");
            _setting = _fileServices.GetSetting(pathFileSetting);

            _logger.Info($"Проверка директории: {_setting.PathSaveFile}");
            CreateDirectory(_setting.PathSaveFile);

            _logger.Info($"Проверка директории: {_setting.PathLoadFile}");
            CreateDirectory(_setting.PathLoadFile);
        }
        public string GetVersionAssembly()
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName();
            return $"{assemblyName.Name} ver. {assemblyName?.Version?.Major}.{assemblyName?.Version?.Minor} build:{assemblyName?.Version?.Build}";
        }
        public void CreateWindowSetting(MainWindow mainWindow)
        {
            Settings settingsWindow = new Settings(_setting, _message);
            settingsWindow.SaveSetting += SaveSettings;
            settingsWindow.Owner = mainWindow;
            settingsWindow.Show();

            _logger.Info("Открытие настроек");
        }
        private void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                _logger.Info($"Создана директория: {path}");
            }
        }

        public InputModel GetInputData() => _inputData;

        public void SaveSettings()
        {
            var settingsString = JsonConvert.SerializeObject(_setting);
            var bytesSettings = Encoding.UTF8.GetBytes(settingsString);
            _fileServices.SaveFile(pathFileSetting, bytesSettings);
            _logger.Info($"Сохранение настроек в файл {pathFileSetting}");
        }

        public ISetting GetSettings() => _setting;
        public void SetListNameDefault() => _inputData.ListName = "Название листа";
        public void SetListNumberDefault() => _inputData.ListNumber = default;

        public async void CreateTom()
        {
            try
            {
                OnHandlerInfoShow?.Invoke($"\n[{DateTime.Now.ToShortTimeString()}] получение файлов из {_setting.PathLoadFile}");
                _logger.Info($"Получение файлов из {_setting.PathLoadFile}");

                var files = Directory.GetFiles(_setting.PathLoadFile);

                OnHandlerInfoShow?.Invoke($"\n[{DateTime.Now.ToShortTimeString()}] получено файлов: {files.Count()}");

                byte[] resultTomBytes = default;

                if (files.Length == 0)
                {
                    _message.ShowError($"Не найдены файлы в: {_setting.PathLoadFile}");
                    return;
                }

                if (_inputData.RbtnListName)
                {
                    OnHandlerInfoShow?.Invoke($"\n[{DateTime.Now.ToShortTimeString()}] выполнение...");
                    resultTomBytes = await Task.Run(() => CreatorTom.CreateTom(files, _inputData.ListName));
                    OnHandlerInfoShow?.Invoke($"\n[{DateTime.Now.ToShortTimeString()}] выполнено!");
                }
                else if (_inputData.RbtnListNumber)
                {
                    OnHandlerInfoShow?.Invoke($"\n[{DateTime.Now.ToShortTimeString()}] выполнение...");
                    resultTomBytes = await Task.Run(() => CreatorTom.CreateTom(files, _inputData.ListNumber));
                    OnHandlerInfoShow?.Invoke($"\n[{DateTime.Now.ToShortTimeString()}] выполнено!");
                }
                else
                {
                    OnHandlerInfoShow?.Invoke($"\n[{DateTime.Now.ToShortTimeString()}] выполнение...");
                    resultTomBytes = await Task.Run(() => CreatorTom.CreateTom(files));
                    OnHandlerInfoShow?.Invoke($"\n[{DateTime.Now.ToShortTimeString()}] выполнено!");
                }

                var pathSaveFileTom = Path.Combine(_setting.PathSaveFile, _inputData.NameTom + ".xlsx");
                _fileServices.SaveFile(pathSaveFileTom, resultTomBytes);

                OnHandlerInfoShow?.Invoke($"\n[{DateTime.Now.ToShortTimeString()}] файл сохранен: {pathSaveFileTom}");
                _logger.Info($"Файл сохранен: {pathSaveFileTom}");

                if (_message.ShowInfo($"Файл сохранен: {pathSaveFileTom}\nПоказать в папке?") == System.Windows.MessageBoxResult.OK)
                {
                    Open(_setting.PathSaveFile);
                }

            }
            catch (Exception ex)
            {
                OnHandlerInfoShow?.Invoke($"\n[{DateTime.Now.ToShortTimeString()}] ошибка");
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");

                if (_message.ShowError(ex.Message + "\nПодробная информация в log-файле\n\nОткрыть log?") == System.Windows.MessageBoxResult.OK)
                {
                    Open(pathCurrentLog);
                }
            }
        }

        public void Open(string path)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.UseShellExecute = true;
            processStartInfo.FileName = path;
            Process.Start(processStartInfo);
        }

        public void DeleteAllLog()
        {
            if (_message.ShowQuestion("Хотите удалить все log-файлы?") == System.Windows.MessageBoxResult.OK)
            {
                var files = Directory.GetFiles(pathFolderLog);
                var deleteFileCount = _fileServices.DeleteFiles(files);
                _message.ShowInfo("Логи удалены!");
                OnHandlerInfoShow?.Invoke($"\n[{DateTime.Now.ToShortTimeString()}] удалено файлов: {deleteFileCount} из {pathFolderLog}");
            }
        }
    }
}
