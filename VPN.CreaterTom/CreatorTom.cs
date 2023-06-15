using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
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

            var packageTom = new ExcelPackage(files[0]);
            DeleteAllLists(packageTom);

            for (int i = 0; i < files.Length; i++)
            {
                var package = new ExcelPackage(files[i]);

                foreach (var sheet in package.Workbook.Worksheets)
                {
                    packageTom.Workbook.Worksheets.Add($"{sheet.Name}({i})", sheet);
                }
            }

            return packageTom.GetAsByteArray();
        }

        public static byte[] CreateTom(string[] files, int? numberList)
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
                packageTom.Workbook.Worksheets.Add($"{sheet.Name}({i})", sheet);
            }

            return packageTom.GetAsByteArray();
        }

        public static byte[] CreateTom(string[] files, string nameList)
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
                packageTom.Workbook.Worksheets.Add($"{sheet.Name}({i})", sheet);
            }

            return packageTom.GetAsByteArray();
        }

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
    }
}
