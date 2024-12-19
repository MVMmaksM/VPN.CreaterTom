using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VPN.CreaterTom.Model
{
    public class InputModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private bool _isDelPrintArea;       
        private string? _rangeRepeatRows = "$4:$9";
        private bool _txtBxIsEnabledRangeRepeatRows;       
        private bool _isPrintHeader;      
        private bool _isAddZeroTersonInNameFile;      
        private bool _isNameFileAsNameSheet;     
        private int _fitToWidth;
        private int _fitToHeight;
        private int? listNumber;
        private string? listName = "Название листа";
        private string nameTom = "Том";
        private bool rbtnAllList = true;
        private bool rbtnListName;
        private bool rbtnListNumber;
        private decimal leftMargin = 1.8M;
        private decimal rightMargin = 1.8M;
        private decimal topMargin = 2.0M;
        private decimal bottomMargin = 2.0M;
        private ExcelSheetOrientation _selectedValueOrientation;
        private List<ExcelSheetOrientation> _listOrientation = new List<ExcelSheetOrientation>()
        {
            new ExcelSheetOrientation()
            {
                nameOrientation = "Книжная",
                orientation = eOrientation.Portrait
            },
            new ExcelSheetOrientation()
            {
                nameOrientation = "Альбомная",
                orientation = eOrientation.Landscape
            }
        };

        /// <summary>
        /// убрать области печати
        /// </summary>
        public bool IsDelPrintArea
        {
            get => _isDelPrintArea;
            set => _isDelPrintArea = value;
        }

        /// <summary>
        /// тестовое поле диапазога сквозных строк
        /// </summary>
        public string? RangeRepeatRows
        {
            get => _rangeRepeatRows;
            set => _rangeRepeatRows = value;
        }

        /// <summary>
        /// устаналивает свойство IsEnabled для текствого поля
        /// диапазона строк при печати заголовков
        /// </summary>
        public bool TxtBxIsEnabledRangeRepeatRows
        {
            get { return _txtBxIsEnabledRangeRepeatRows; }
            set
            {
                _txtBxIsEnabledRangeRepeatRows = value;
            }
        }

        /// <summary>
        /// галочка печатать заголовки
        /// </summary>
        public bool IsPrintHeader
        {
            get => _isPrintHeader;
            set
            {
                _isPrintHeader = value;
                _txtBxIsEnabledRangeRepeatRows = value;
                OnPropertyChanged("TxtBxIsEnabledRangeRepeatRows");
            }
        }

        public ExcelSheetOrientation SelectedValueOrientation
        {
            get { return _selectedValueOrientation ?? _listOrientation[1]; }
            set { _selectedValueOrientation = value; }
        }

        public List<ExcelSheetOrientation> SheetOrientation { get => _listOrientation; }

        public string Error => throw new NotImplementedException();
        public string this[string columnName] 
        {
            get 
            {
                string error = string.Empty;

                switch (columnName)
                {
                    case "RangeRepeatRows":
                        if (string.IsNullOrWhiteSpace(RangeRepeatRows))
                            error = "Диапазон должен быть заполнен";
                       
                        if(!Regex.IsMatch(RangeRepeatRows, @"\$\d\:\$\d"))
                            error = "Строка диапазона должна быть в формате $1:$1";

                        if (!RangeRepeatRows.StartsWith("$"))
                            error = "Строка диапазона должна быть в формате $1:$1";
                        
                        if(RangeRepeatRows.StartsWith("$") && RangeRepeatRows[1] == 0)
                            error = "Диапазон не может начинаться с 0";

                        var splitStr = RangeRepeatRows.Split(":");
                        
                        var lastValue = splitStr[1]
                            .Split("$")
                            .Where(s => s != "")
                            .ToArray();

                        if (lastValue[0] == "0")
                            error = "Диапазон не может заканчиваться 0";
                        break;
                    default:
                        break;
                }

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
        public bool IsAddZeroTersonInNameFile
        {
            get =>_isAddZeroTersonInNameFile; 
            set => _isAddZeroTersonInNameFile = value; 
        }

        public bool IsNameFileAsNameSheet
        {
            get => _isNameFileAsNameSheet; 
            set => _isNameFileAsNameSheet = value; 
        }
        public int FitToWidth
        {
            get { return _fitToWidth; }
            set
            {
                _fitToWidth = value;
                OnPropertyChanged("FitToWidth");
            }
        }

        public int FitToHeight
        {
            get { return _fitToHeight; }
            set
            {
                _fitToHeight = value;
                OnPropertyChanged("FitToHeight");
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
