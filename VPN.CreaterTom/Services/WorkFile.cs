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
        public void DeleteFile(string pathFile)
        {
            File.Delete(pathFile);
        }

        public int DeleteFile(string[] pathFiles)
        {
            int deleteFileCount = 0;

            for (int i = 0; i < pathFiles.Length; i++)
            {
                File.Delete(pathFiles[i]);
                deleteFileCount++;
            }

            return deleteFileCount;
        }

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
