using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VPN.CreaterTom.Model
{
    public class InputModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private int? listNumber;
        private string? listName = "Название листа";
        private string nameTom = "Том";
        private bool rbtnAllList = true;
        private bool rbtnListName;
        private bool rbtnListNumber;
        private decimal leftMargin = 1.8M;
        private decimal rightMargin = 1.8M;
        private decimal topMargin = 1.8M;
        private decimal bottomMargin = 1.8M;

        public string Error => throw new NotImplementedException();
        public string this[string columnName] 
        {
            get 
            {
                string error = string.Empty;

                switch (columnName)
                {
                    case "NameTom":
                        if (string.IsNullOrWhiteSpace(NameTom))
                        {
                            error = "Название тома должно быть заполнено!";
                        }
                        break;                    
                    default:
                        break;
                }

                switch (columnName)
                {
                    case "ListName":
                        if (string.IsNullOrWhiteSpace(ListName))
                        {
                            error = "Название листа должно быть заполнено!";
                        }
                        break;
                    default:
                        break;
                }               

                return error;
            }
        }

        public bool RbtnListNumber
        {
            get { return rbtnListNumber; }
            set 
            { 
                rbtnListNumber = value;
                OnPropertyChanged("RbtnListNumber");
            }
        }


        public bool RbtnListName
        {
            get { return rbtnListName; }
            set 
            { 
                rbtnListName = value;
                OnPropertyChanged("RbtnListName");
            }
        }


        public bool RbtnAllList
        {
            get { return rbtnAllList; }
            set
            {
                rbtnAllList = value;
                OnPropertyChanged("RbtnAllList");
            }
        }


        public int? ListNumber
        {
            get { return listNumber; }
            set
            {
                listNumber = value;
                OnPropertyChanged("ListNumber");
            }
        }

        public string? ListName
        {
            get { return listName; }
            set
            {
                listName = value;
                OnPropertyChanged("ListName");
            }
        }

        public string NameTom
        {
            get { return nameTom; }
            set
            {
                nameTom = value;
                OnPropertyChanged("NameTom");
            }
        }
        
        public decimal LeftMargin
        {
            get => leftMargin;
            set 
            {
                leftMargin = value;
                OnPropertyChanged("LeftMargin");
            }
        }

        public decimal RightMargin
        {
            get => rightMargin;
            set
            {
                rightMargin = value;
                OnPropertyChanged("RightMargin");
            }
        }

        public decimal TopMargin
        {
            get => topMargin;
            set
            {
                topMargin = value;
                OnPropertyChanged("TopMargin");
            }
        }

        public decimal BottomMargin
        {
            get => bottomMargin;
            set
            {
                bottomMargin = value;
                OnPropertyChanged("BottomMargin");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
