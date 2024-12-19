using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPN.CreaterTom.Model;

namespace VPN.CreaterTom
{
    public class CreatorTom
    {
        public static byte[] CreateTom(string[] files, InputModel inputData)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var packageTom = new ExcelPackage(files[0]);
            DeleteAllLists(packageTom);

            for (int i = 0; i < files.Length; i++)
            {
                var package = new ExcelPackage(files[i]);

                for (int j = 0; j < package.Workbook.Worksheets.Count; j++)
                {
                    //устанавливаем настройки листа
                    SetSettingsSheet(package.Workbook.Worksheets[j], inputData);                    
                 
                    if (inputData.IsNameFileAsNameSheet)
                    {
                        var nameFile = Path
                            .GetFileNameWithoutExtension(files[i])
                            .Split("_")
                            .Where(n => n.StartsWith("Терр="))
                            .FirstOrDefault();

                        if (nameFile is null)
                            throw new Exception($"В названии файла: {files[i]} отсутствует подстрока вида Терр=");

                        var terson = nameFile.Split("=");

                        if (package.Workbook.Worksheets.Count > 1)                       
                        {
                            packageTom.Workbook.Worksheets.Add($"{terson[1] ?? "Неизвестно"}({j})", package.Workbook.Worksheets[j]);
                        }
                        else
                        {
                            packageTom.Workbook.Worksheets.Add($"{terson[1] ?? "Неизвестно"}", package.Workbook.Worksheets[j]);
                        }
                    }
                    else 
                    {
                        packageTom.Workbook.Worksheets.Add($"{package.Workbook.Worksheets[j].Name}({i})", package.Workbook.Worksheets[j]);
                    }                    
                }                
            }

            return packageTom.GetAsByteArray();
        }

        public static byte[] CreateTom(string[] files, int? numberList, InputModel inputData)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var packageTom = new ExcelPackage(files[0]);
            packageTom.Workbook.Worksheets.Delete(0);
            packageTom.Workbook.Worksheets.Delete("GenParams");

            for (int i = 0; i < files.Length; i++)
            {
                var package = new ExcelPackage(files[i]);

                if (package.Workbook.Worksheets.ElementAtOrDefault((Index)numberList) is null)
                {
                    throw new Exception($"Лист с номером \"{numberList}\" отсутствует в книге: {Path.GetFileName(files[i])}");
                }

                var sheet = package.Workbook.Worksheets[(int)numberList];

                //устанавливаем настройки листа
                SetSettingsSheet(sheet, inputData);              

                //если используется название файла в названии листа
                if (inputData.IsNameFileAsNameSheet)
                {
                    var nameFile = Path
                            .GetFileNameWithoutExtension(files[i])
                            .Split("_")
                            .Where(n => n.StartsWith("Терр="))
                            .FirstOrDefault();

                    if (nameFile is null)
                        throw new Exception($"В названии файла: {files[i]} отсутствует подстрока вида Терр=");

                    var terson = nameFile.Split("=");

                    packageTom.Workbook.Worksheets.Add($"{terson[1]}", sheet);
                }
                else 
                {
                    //добавляем лист в книгу
                    packageTom.Workbook.Worksheets.Add($"{sheet.Name}({i})", sheet);
                }
            }

            return packageTom.GetAsByteArray();
        }

        public static byte[] CreateTom(string[] files, string nameList, InputModel inputData)
        {
            var packageTom = new ExcelPackage(files[0]);
            packageTom.Workbook.Worksheets.Delete(0);
            packageTom.Workbook.Worksheets.Delete("GenParams");

            for (int i = 0; i < files.Length; i++)
            {
                var package = new ExcelPackage(files[i]);

                if (!package.Workbook.Worksheets.Any(s => s.Name.Equals(nameList)))
                {
                    throw new Exception($"Лист с именем \"{nameList}\" отсутствует в книге: {Path.GetFileName(files[i])}");
                }

                var sheet = package.Workbook.Worksheets[nameList];

                //устанавливаем настройки листа
                SetSettingsSheet(sheet, inputData);

                //если используется название файла в названии листа
                if (inputData.IsNameFileAsNameSheet)
                {
                    var nameFile = Path
                            .GetFileNameWithoutExtension(files[i])
                            .Split("_")
                            .Where(n => n.StartsWith("Терр="))
                            .FirstOrDefault();

                    if (nameFile is null)
                        throw new Exception($"В названии файла: {files[i]} отсутствует подстрока вида Терр=");

                    var terson = nameFile.Split("=");

                    packageTom.Workbook.Worksheets.Add($"{terson[1]}", sheet);
                }
                else
                {
                    //добавляем лист в книгу
                    packageTom.Workbook.Worksheets.Add($"{sheet.Name}({i})", sheet);
                }
            }

            return packageTom.GetAsByteArray();
        }

        /// <summary>
        /// устанавливает настройки листа
        /// </summary>
        private static void SetSettingsSheet(ExcelWorksheet sheet, InputModel inputData) 
        {
            //убрать области печати
            if(inputData.IsDelPrintArea)
                sheet.PrinterSettings.PrintArea = null;

            //сквозные строки
            if (inputData.IsPrintHeader)
                sheet.PrinterSettings.RepeatRows = new ExcelAddress(inputData.RangeRepeatRows);
            
            //поля
            sheet.PrinterSettings.LeftMargin = inputData.LeftMargin / 2.54M;
            sheet.PrinterSettings.RightMargin = inputData.RightMargin / 2.54M;
            sheet.PrinterSettings.TopMargin = inputData.TopMargin / 2.54M;
            sheet.PrinterSettings.BottomMargin = inputData.BottomMargin / 2.54M;
            //ориентация
            sheet.PrinterSettings.Orientation = inputData.SelectedValueOrientation.orientation;
            //вписать в кол-во страниц
            sheet.PrinterSettings.FitToPage = true;
            sheet.PrinterSettings.FitToWidth = inputData.FitToWidth;
            sheet.PrinterSettings.FitToHeight = inputData.FitToHeight;
        }

        /// <summary>
        /// удаление всех листов из книги
        /// </summary>
        private static void DeleteAllLists(ExcelPackage package)
        {
            if (package.Workbook.Worksheets.ElementAtOrDefault(0) is not null)
            {
                for (int i = package.Workbook.Worksheets.Count; i > 0; i--)
                {
                    package.Workbook.Worksheets.Delete(0);
                }
            }
        }

        /// <summary>
        /// удаляет столбцы, где все строки содержат прочерк 
        /// в таблицах по населению
        /// </summary>     
        private static void DeleteColumnsContainsHyphen(ExcelWorksheet sheet) 
        {
            const int indexColumnStartTable = 6;
            const int indexRowStartTable = 10;
            var countRows = sheet.Dimension.End.Row;
            var countColumns = sheet.Dimension.End.Row;
            var indexRowsContainsHyphen = new List<int>();

            for (int row = 1; row <= countRows; row++)
            {
                var flagContains = 0;

                for (int col = 1; col <= countColumns; col++)
                {
                    if (sheet.Cells[row, col].Value?.ToString() != "-")
                    {
                        flagContains = 0;
                        break;
                    }

                    flagContains = 1;
                }

                if (flagContains == 1)
                    indexRowsContainsHyphen.Add(row);
            }

            if (indexRowsContainsHyphen.Count > 0) 
            {
                for (int i = 0; i < indexRowsContainsHyphen.Count; i++)
                {
                    sheet.DeleteRow(indexRowsContainsHyphen[i] - i);
                }
            }           
        }
    }
}
