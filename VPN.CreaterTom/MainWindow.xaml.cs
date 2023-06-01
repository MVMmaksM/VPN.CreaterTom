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

namespace VPN.CreaterTom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        InputModel model;
        public MainWindow()
        {
            InitializeComponent();
            model = new InputModel();
            this.DataContext = model;
        }

        private void MenuOpenLogFile(object sender, RoutedEventArgs e)
        {

        }

        private void MenuOpenSettings(object sender, RoutedEventArgs e)
        {

        }

        private void RdBtnListNumber_Checked(object sender, RoutedEventArgs e)
        {
            TxtListName.IsEnabled = false;
            TxtListNumber.IsEnabled = true;
            model.RbtnListNumber = true;

            if (Validation.GetHasError(TxtListName))
            {
                model.ListName = default;
            }
        }

        private void RdBtnListName_Checked(object sender, RoutedEventArgs e)
        {
            TxtListName.IsEnabled = true;
            TxtListNumber.IsEnabled = false;
            model.RbtnListName = true;

            if (Validation.GetHasError(TxtListNumber))
            {
                model.ListNumber = default;
            }
        }

        private void RdBtnAllList_Checked(object sender, RoutedEventArgs e)
        {
            TxtListName.IsEnabled = false;
            TxtListNumber.IsEnabled = false;
            model.RbtnAllList = true;

            if ((Validation.GetHasError(TxtListNumber) || Validation.GetHasError(TxtListName)) || (Validation.GetHasError(TxtListNumber) && Validation.GetHasError(TxtListName)))
            {
                model.ListName = default;
                model.ListNumber = default;
            }
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
