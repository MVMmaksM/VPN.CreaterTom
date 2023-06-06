using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VPN.CreaterTom.Services
{
    public class MessageService : IMessage
    {      
        public void ShowError(string message)
        {
           MessageBox.Show(message, "Ошибка", MessageBoxButton.OKCancel, MessageBoxImage.Error);
        }

        public void ShowInfo(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButton.OKCancel, MessageBoxImage.Information);
        }
    }
}
