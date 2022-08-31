using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text;

namespace ArkNeTransport.Application.Common.Utils
{
    /// <summary>
    /// Json帮助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 将json字符串转为Configuration
        /// </summary>
        /// <param name="jsonTxt">json字符串</param>
        /// <returns></returns>
        public static IConfiguration JsonConvertConfig(string jsonTxt)
        {
            //创建存储区为内存流
            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonTxt));
            var builder = new ConfigurationBuilder();
            IConfiguration config = builder.AddJsonStream(stream).Build();
            return config;
        }
    }
}