using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPN.CreaterTom
{
    public class CreatorTom
    {
        public static byte[] CreateTom(string[] files)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var packageTom = new ExcelPackage();

            foreach (var file in files)
            {
                var package = new ExcelPackage(file);
                var sheet = package.Workbook.Worksheets[0];
                packageTom.Workbook.Worksheets.Add(sheet.Name, sheet);
            }

            return packageTom.GetAsByteArray();
        }

        public static byte[] CreateTom(string[] files, int? numberList)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var packageTom = new ExcelPackage();

            foreach (var file in files)
            {
                var package = new ExcelPackage(file);
                var sheet = package.Workbook.Worksheets[(int)numberList];
                packageTom.Workbook.Worksheets.Add(sheet.Name, sheet);
            }

            return packageTom.GetAsByteArray();
        }

        public static byte[] CreateTom(string[] files, string nameList)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var packageTom = new ExcelPackage();

            foreach (var file in files)
            {
                var package = new ExcelPackage(file);
                var sheet = package.Workbook.Worksheets[nameList];
                packageTom.Workbook.Worksheets.Add(sheet.Name, sheet);
            }

            return packageTom.GetAsByteArray();
        }
    }
}
