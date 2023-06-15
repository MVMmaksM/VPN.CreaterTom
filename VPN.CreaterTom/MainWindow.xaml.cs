using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VPN.CreaterTom.Model;
using VPN.CreaterTom.Services;
using VPN.CreaterTom.View;

namespace VPN.CreaterTom
{
    public partial class MainWindow : Window
    {
        private Facade _facade;
        private static ILogger _logger = LogManager.GetCurrentClassLogger();
        private static string pathFolderLog = $"{Environment.CurrentDirectory}\\logs";
        private static string pathCurrentLog = $"{pathFolderLog}\\{DateTime.Now:yyyy-MM-dd}.log";
        private IMessage _messageSrvices = new MessageService();
        public MainWindow()
        {
            _logger.Info("Запуск приложения");

            InitializeComponent();
            _facade = new Facade(_messageSrvices, _logger);
            this.Title = _facade.GetVersionAssembly();
            _facade.OnHandlerInfoShow += ShowInfo;
            this.DataContext = _facade.GetInputData();
        }
        private void ShowInfo(string messageInfo) 
        {
            TxtBlockShow.Text += messageInfo;
        }
        private void MenuOpenLogFile(object sender, RoutedEventArgs e)
        {
            _facade.Open(pathCurrentLog);
        }

        private void MenuOpenSettings(object sender, RoutedEventArgs e)
        {
            _facade.CreateWindowSetting(this);         
        }

        private void RdBtnListNumber_Checked(object sender, RoutedEventArgs e)
        {
            TxtListName.IsEnabled = false;
            TxtListNumber.IsEnabled = true;

            if (Validation.GetHasError(TxtListName))
            {
                _facade.SetListNameDefault();
            }
        }

        private void RdBtnListName_Checked(object sender, RoutedEventArgs e)
        {
            TxtListName.IsEnabled = true;
            TxtListNumber.IsEnabled = false;

            if (Validation.GetHasError(TxtListNumber))
            {
                _facade.SetListNumberDefault();
            }
        }

        private void RdBtnAllList_Checked(object sender, RoutedEventArgs e)
        {
            TxtListName.IsEnabled = false;
            TxtListNumber.IsEnabled = false;

            if ((Validation.GetHasError(TxtListNumber) || Validation.GetHasError(TxtListName)) || (Validation.GetHasError(TxtListNumber) && Validation.GetHasError(TxtListName)))
            {
                _facade.SetListNameDefault();
                _facade.SetListNumberDefault();
            }
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!Validation.GetHasError(TxtListNumber) && !Validation.GetHasError(TxtListName) && !Validation.GetHasError(TxtNameTom))
                _facade.CreateTom();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _logger.Info("Завершение приложения");
        }

        private void MenuOpenLogFolder(object sender, RoutedEventArgs e)
        {
            _facade.Open(pathFolderLog);
        }

        private void MenuDeleteAllLogFile(object sender, RoutedEventArgs e)
        {
            _facade.DeleteAllLog();
        }

        private void MenuOpenAbout(object sender, RoutedEventArgs e)
        {
            _facade.OpenAboutFile();
        }
    }
}
