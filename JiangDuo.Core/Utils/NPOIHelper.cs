using JiangDuo.Core.Attributes;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace JiangDuo.Core.Utils
{
    public class NPOIHelper
    {
        private static HSSFWorkbook CreateHSSFWorkbook(string title)
        {
            var workbook = new HSSFWorkbook();
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "ChinaCustoms";
            workbook.DocumentSummaryInformation = dsi;

            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = title;
            workbook.SummaryInformation = si;
            return workbook;
        }

        public static List<HeadValueInfoExcel> GetHeadValue<T>(List<T> list)
        {
            List<HeadValueInfoExcel> headList = new List<HeadValueInfoExcel>();
            int index = 0;
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(typeof(ExportPropertyAttribute), true);
                if (attributes.Length == 0)
                {
                    continue;
                }
                var attribute = attributes[0] as ExportPropertyAttribute;
                if (attribute.CanExport)
                {
                    headList.Add(new HeadValueInfoExcel
                    {
                        HeadName = attribute.Name,
                        Order = index++,
                        ValueName = property.Name
                    });
                }
            }
            return headList;
        }

        public static byte[] ExportXlsFile(DataTable data, List<HeadValueInfoExcel> headValueList, string title)
        {
            var hssfworkbook = CreateHSSFWorkbook(title);
            ISheet sheet1 = hssfworkbook.CreateSheet("Sheet1");
            sheet1.CreateRow(0).CreateCell(0).SetCellValue(title);
            int listCount = data.Rows.Count;
            int headCount = headValueList.Count;
            ICellStyle cellstyle = hssfworkbook.CreateCellStyle();
            cellstyle.VerticalAlignment = VerticalAlignment.Center;
            cellstyle.Alignment = HorizontalAlignment.Center;

            //设置表字段头
            IRow headRow = sheet1.CreateRow(1);
            //headRow.Height = 2500;
            var headlst = headValueList.OrderByDescending(p => p.Order).ToList();
            for (int j = 0; j < headCount; j++)
            {
                var headInfo = headlst[j];
                ICell cell = headRow.CreateCell(j);
                cell.CellStyle = cellstyle;
                cell.SetCellValue(headInfo.HeadName);
            }

            //填充数据
            for (int i = 0; i < listCount; i++)
            {
                IRow row = sheet1.CreateRow(i + 2);

                var cellValue = "";
                for (int j = 0; j < headCount; j++)
                {
                    var headInfo = headlst[j];
                    var v = data.Rows[i][headInfo.ValueName];
                    if (v != null)
                    {
                        cellValue = v.ToString();
                    }
                    ICell cell = row.CreateCell(j);
                    cell.CellStyle = cellstyle;
                    cell.SetCellValue(cellValue);
                }
            }
            MemoryStream ms = new MemoryStream();
            hssfworkbook.Write(ms);
            var reuslt = ms.ToArray();
            hssfworkbook.Close();
            ms.Close();
            return ms.ToArray();
        }

        public static byte[] ExportXlsFile<T>(List<T> list, string title)
        {
            //获取属性的别名作为表头
            var headValueList = GetHeadValue(list);
            var hssfworkbook = CreateHSSFWorkbook(title);
            ISheet sheet1 = hssfworkbook.CreateSheet("Sheet1");
            sheet1.CreateRow(0).CreateCell(0).SetCellValue(title);
            int listCount = list.Count;
            int headCount = headValueList.Count;
            ICellStyle cellstyle = hssfworkbook.CreateCellStyle();
            cellstyle.VerticalAlignment = VerticalAlignment.Center;
            cellstyle.Alignment = HorizontalAlignment.Center;

            //设置表字段头
            IRow headRow = sheet1.CreateRow(1);
            //headRow.Height = 2500;

            //IEnumerable<HeadValueInfoExcel> headValueOrderList = headValueList.OrderBy(i=>i.Order);
            int index = 0;
            foreach (HeadValueInfoExcel headInfo in headValueList)
            {
                //HeadValueInfoExcel headInfo = headValueList[j];
                ICell cell = headRow.CreateCell(index++);
                cell.CellStyle = cellstyle;
                cell.SetCellValue(headInfo.HeadName);
            }

            //填充数据
            for (int i = 0; i < listCount; i++)
            {
                IRow row = sheet1.CreateRow(i + 2);
                T value = list[i];
                var cellValue = "";
                int j = 0;
                foreach (HeadValueInfoExcel headInfo in headValueList)
                {
                    if (value is Hashtable)
                    {
                        object properotyValue = null; // 属性的值
                        System.Reflection.PropertyInfo properotyInfo = null; // 属性的信息
                        properotyInfo = value.GetType().GetProperty(headInfo.ValueName);
                        if (properotyInfo != null)
                        {
                            properotyValue = properotyInfo.GetValue(value, null);
                        }
                        if (properotyValue != null)
                        {
                            cellValue = properotyValue.ToString();
                        }
                        else//如果字段值为NULL，设置cellValue值为空。解决cellValue是上一个字段值的问题
                        {
                            cellValue = "";
                        }

                        object tmpObj = (object)value;
                        Hashtable aaa = (Hashtable)tmpObj;
                        cellValue = null == aaa[headInfo.ValueName] ? "" : aaa[headInfo.ValueName].ToString();// value[headInfo.ValueName];
                        ICell cell = row.CreateCell(j++);
                        cell.CellStyle = cellstyle;
                        cell.SetCellValue(cellValue);
                    }
                    else
                    {
                        //var headInfo = headValueList.FirstOrDefault(h => h.Order == j);
                        object properotyValue = null; // 属性的值
                        System.Reflection.PropertyInfo properotyInfo = null; // 属性的信息
                        properotyInfo = value.GetType().GetProperty(headInfo.ValueName);
                        if (properotyInfo != null)
                        {
                            properotyValue = properotyInfo.GetValue(value, null);
                        }
                        if (properotyValue != null)
                        {
                            cellValue = properotyValue.ToString();
                        }
                        else//如果字段值为NULL，设置cellValue值为空。解决cellValue是上一个字段值的问题
                        {
                            cellValue = "";
                        }
                        ICell cell = row.CreateCell(j++);
                        cell.CellStyle = cellstyle;
                        cell.SetCellValue(cellValue);
                    }
                }
            }
            MemoryStream ms = new MemoryStream();
            hssfworkbook.Write(ms);
            var reuslt = ms.ToArray();
            hssfworkbook.Close();
            ms.Close();
            return reuslt;
        }
    }

    public class HeadValueInfoExcel
    {
        public string HeadName { get; set; }
        public string ValueName { get; set; }
        public int Order { get; set; }
    }
}