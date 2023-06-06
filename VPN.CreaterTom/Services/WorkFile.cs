using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPN.CreaterTom.Model;

namespace VPN.CreaterTom.Services
{
    public class WorkFile : IWorkFile
    {
        public string ReadFile(string pathFile)
        {
            var stringReading = string.Empty;

            using (var streamReader = new StreamReader(pathFile))
            {
                stringReading = streamReader.ReadToEnd();
            }

            return stringReading;
        }
        public void SaveFile(string pathFile, byte[] files)
        {          
            File.WriteAllBytes(pathFile, files);
        }
    }
}
