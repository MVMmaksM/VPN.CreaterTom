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
        public MessageBoxResult ShowError(string message) => MessageBox.Show(message, "Ошибка", MessageBoxButton.OKCancel, MessageBoxImage.Error);
        public MessageBoxResult ShowInfo(string message) => MessageBox.Show(message, "Информация", MessageBoxButton.OKCancel, MessageBoxImage.Information);
        public MessageBoxResult ShowQuestion(string message) => MessageBox.Show(message, "Вопрос", MessageBoxButton.OKCancel, MessageBoxImage.Question);
    }
}
