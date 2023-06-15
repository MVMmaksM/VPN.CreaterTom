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
using System.Windows.Shapes;
using VPN.CreaterTom.Model;
using VPN.CreaterTom.Services;

namespace VPN.CreaterTom.View
{   
    public partial class Settings : Window
    {
        private ISetting _setting;
        private IMessage _message;
        public delegate void HadlerSaveSetting();
        public event HadlerSaveSetting SaveSetting;

        public Settings(ISetting settings, IMessage message)
        {
            InitializeComponent();
            
            _message = message;
            _setting = settings;
            this.DataContext = settings;
        }

        private void BtnSaveSettings_Click(object sender, RoutedEventArgs e)
        {          
            SaveSetting?.Invoke();
            _message.ShowInfo("Настройки успешно сохранены!");
        }
    }
}
