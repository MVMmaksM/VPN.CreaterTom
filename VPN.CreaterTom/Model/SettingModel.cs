using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VPN.CreaterTom.Model
{
    public class SettingModel :INotifyPropertyChanged, ISetting
    {
        private string _pathLoadFile;
        private string _pathSaveFile;

        public string PathSaveFile
        {
            get { return _pathSaveFile; }
            set 
            { 
                _pathSaveFile = value; 
                OnPropertyChanged("PathSaveFile");
            }
        }

        public string PathLoadFile
        {
            get { return _pathLoadFile; }
            set 
            { 
                _pathLoadFile = value; 
                OnPropertyChanged("PathLoadFile");
            }
        }       

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
