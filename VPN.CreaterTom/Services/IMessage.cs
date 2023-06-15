using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VPN.CreaterTom.Services
{
    public interface IMessage
    {
        MessageBoxResult ShowError(string message);
        MessageBoxResult ShowInfo(string message);
        MessageBoxResult ShowQuestion(string message);
    }
}
