using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPN.CreaterTom.Model;

namespace VPN.CreaterTom.Services
{
    public interface IWorkFileSetting 
    {
        ISetting ReadFileSetting(string pathFileSetting);
        void SaveFileSetting(string pathFileSetting, ISetting settings);
    }
}
