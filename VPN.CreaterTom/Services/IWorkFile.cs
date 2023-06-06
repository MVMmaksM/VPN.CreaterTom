using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPN.CreaterTom.Model;

namespace VPN.CreaterTom.Services
{
    public interface IWorkFile 
    {
        string ReadFile(string pathFile);
        void SaveFile(string pathFile, byte[] files);
    }
}
