using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPN.CreaterTom.Services
{
    public interface IMessage
    {
        void ShowError(string message);
        void ShowInfo(string message);
    }
}
