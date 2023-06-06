using System;
using System.Collections.Generic;
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
        private Facade facade;
        public MainWindow()
        {
            InitializeComponent();
            facade = new Facade(new MessageService());
            facade.OnHandlerInfoShow += ShowInfo;
            this.DataContext = facade.GetInputData();
        }
        private void ShowInfo(string messageInfo) 
        {
            TxtBlockShow.Text += messageInfo;
        }
        private void MenuOpenLogFile(object sender, RoutedEventArgs e)
        {

        }

        private void MenuOpenSettings(object sender, RoutedEventArgs e)
        {
            Settings settingsWindow = new Settings(facade.GetSettings());
            settingsWindow.SaveSetting += facade.SaveSettings;
            settingsWindow.Owner = this;
            settingsWindow.Show();
        }

        private void RdBtnListNumber_Checked(object sender, RoutedEventArgs e)
        {
            TxtListName.IsEnabled = false;
            TxtListNumber.IsEnabled = true;

            if (Validation.GetHasError(TxtListName))
            {
                facade.SetListNameDefault();
            }
        }

        private void RdBtnListName_Checked(object sender, RoutedEventArgs e)
        {
            TxtListName.IsEnabled = true;
            TxtListNumber.IsEnabled = false;

            if (Validation.GetHasError(TxtListNumber))
            {
                facade.SetListNumberDefault();
            }
        }

        private void RdBtnAllList_Checked(object sender, RoutedEventArgs e)
        {
            TxtListName.IsEnabled = false;
            TxtListNumber.IsEnabled = false;

            if ((Validation.GetHasError(TxtListNumber) || Validation.GetHasError(TxtListName)) || (Validation.GetHasError(TxtListNumber) && Validation.GetHasError(TxtListName)))
            {
                facade.SetListNameDefault();
                facade.SetListNumberDefault();
            }
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            facade.CreateTom();
        }
    }
}
