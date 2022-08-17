using Furion.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Core.TaskScheduler
{
    public class JobWorker : ISpareTimeWorker
    {
        /// <summary>
        /// 每分钟执行
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="count"></param>
        [SpareTime("* * * * * *", "CheckServiceStatus", DoOnce = true, StartNow = true)]
        public async Task CheckServiceStatus(SpareTimer timer, long count)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            await Task.CompletedTask;
        }
    }
}
