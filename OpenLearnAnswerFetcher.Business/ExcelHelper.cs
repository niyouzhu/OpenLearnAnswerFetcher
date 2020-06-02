using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace OpenLearnAnswerFetcher.Business
{
    public class ExcelHelper
    {

        public static Stream Export<T>(IEnumerable<T> result, string sheetName = "Default")
        {
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet(sheetName);
            var headerRow = sheet.CreateRow(0);
            Type resultType = typeof(T);
            var attributeInfos = GetPropertiesHaveAttribute(resultType).OrderBy(it => it.OrderNum);
            WriteHeader(headerRow, attributeInfos);
            var redFont = workbook.CreateFont();
            redFont.Color = HSSFColor.Red.Index;
            var redCellStyle = workbook.CreateCellStyle();
            redCellStyle.SetFont(redFont);
            redCellStyle.Alignment = HorizontalAlignment.Center;
            var normalCellStyle = workbook.CreateCellStyle();
            normalCellStyle.Alignment = HorizontalAlignment.Center;
            for (int i = 0; i < result.Count(); i++)
            {
                var line = result.ElementAt(i);
                var row = sheet.CreateRow(i + 1);
                WriteLine(row, attributeInfos, line);
                foreach (var cell in row.Cells)
                {
                    cell.CellStyle = normalCellStyle;
                }

            }

            var headerStyle = workbook.CreateCellStyle();
            var headerFont = workbook.CreateFont();
            headerFont.IsBold = true;
            headerStyle.Alignment = HorizontalAlignment.Center;
            headerStyle.SetFont(headerFont);

            for (int i = 0; i < headerRow.Count(); i++)
            {
                headerRow.ElementAt(i).CellStyle = headerStyle;
                sheet.AutoSizeColumn(i);
            }
            MemoryStream memoryStream = new MemoryStream();
            workbook.Write(memoryStream);
            memoryStream.Position = 0;
            memoryStream.Flush();
            return memoryStream;
        }

        private static IEnumerable<AttributeInfo> GetPropertiesHaveAttribute(Type type)
        {
            var allProperties = type.GetProperties();
            var attributedProperties = new List<AttributeInfo>();
            allProperties.ForEach(property =>
            {
                var attributes = property.GetCustomAttributes(true);

                var displayAttribute = attributes.FirstOrDefault(it => it.GetType() == typeof(DisplayNameAttribute));
                if (displayAttribute != null)
                {
                    var attributeInfo = new AttributeInfo();
                    attributeInfo.PropertyInfo = property;
                    attributedProperties.Add(attributeInfo);

                    attributeInfo.DisplayValue = (string)property.GetCustomAttributesData().First(it => it.Constructor.DeclaringType == typeof(DisplayNameAttribute)).ConstructorArguments.First().Value;

                    var orderAttribute = attributes.FirstOrDefault(it => it.GetType() == typeof(OrderAttribute));
                    if (orderAttribute != null)
                    {
                        attributeInfo.OrderNum = (int)property.GetCustomAttributesData().First(it => it.Constructor.DeclaringType == typeof(OrderAttribute)).ConstructorArguments.First().Value;
                    }
                }
            });
            return attributedProperties;
        }

        private class AttributeInfo
        {
            public string DisplayValue { get; set; }
            public PropertyInfo PropertyInfo { get; set; }

            public int OrderNum { get; set; }
        }

        private static void WriteHeader(IRow row, IEnumerable<AttributeInfo> attributeInfos)
        {
            for (int i = 0; i < attributeInfos.Count(); i++)
            {
                var cell = row.CreateCell(i, CellType.String);
                cell.SetCellValue(attributeInfos.ElementAt(i).DisplayValue);
            }
        }

        private static void WriteLine(IRow row, IEnumerable<AttributeInfo> attributeInfos, object value)
        {
            for (int i = 0; i < attributeInfos.Count(); i++)
            {
                var cell = row.CreateCell(i);
                var propertyValue = attributeInfos.ElementAt(i).PropertyInfo.GetValue(value, null);
                if (propertyValue != null)
                    cell.SetCellValue(propertyValue.ToString());
            }
        }
    }
}