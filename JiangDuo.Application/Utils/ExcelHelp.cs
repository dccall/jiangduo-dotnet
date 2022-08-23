using JiangDuo.Core;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using Furion;
using Furion.DatabaseAccessor;
using Furion.FriendlyException;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npoi.Mapper;
using Npoi.Mapper.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yitter.IdGenerator;
using JiangDuo.Core.Utils;
using JiangDuo.Core.Attributes;

namespace JiangDuo.Application.Tools
{
    public static class ExcelHelp
    {

        /// <summary>
        /// List转Excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">数据</param>
        /// <param name="sheetName">表名</param>
        /// <param name="overwrite">true,覆盖单元格，false追加内容(list和创建的excel或excel模板)</param>
        /// <param name="xlsx">true-xlsx，false-xls</param>
        /// <returns>返回文件</returns>
        public static MemoryStream ParseListToExcel<T>(List<T> list, string sheetName = "sheet1", bool overwrite = true, bool xlsx = true) where T : class
        {
            var mapper = new Mapper();
            MemoryStream ms = new MemoryStream();
            mapper.Save<T>(ms, list, sheetName, overwrite, xlsx);
            return ms;
        }

        /// <summary>
        /// Excel转为List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileStream"></param>
        /// <param name="sheetname">要读取的sheet名称</param>
        /// <returns></returns>
        public static List<T> ParseExcelToList<T>(Stream fileStream, string sheetname = "") where T : class
        {
            List<T> ModelList = new List<T>();
            var mapper = new Mapper(fileStream);
            List<RowInfo<T>> DataList = new List<RowInfo<T>>();
            if (!string.IsNullOrEmpty(sheetname))
            {
                DataList = mapper.Take<T>(sheetname).ToList();
            }
            else
            {
                DataList = mapper.Take<T>().ToList();
            }

            if (DataList != null && DataList.Count > 0)
            {
                foreach (var item in DataList)
                {
                    ModelList.Add(item.Value);
                }
            }
            return ModelList;
        }
        /// <summary>
        /// 导出到excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static IActionResult ExportExcel<T>(string fileName,List<T> list) where T : class
        {
            var file= ParseListToExcel(list);
            return new FileStreamResult(new MemoryStream(file.ToArray()), "application/octet-stream")
            {
                FileDownloadName = fileName // 配置文件下载显示名
            };
        }
    }
    public class Test
    {
        [ExportProperty("编号")]
        public int Id { get; set; }

        [ExportProperty("名称")]
        public string Name { get; set; }
    }

   
}
