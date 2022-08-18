using Furion.DatabaseAccessor;
using Furion.TaskScheduler;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace JiangDuo.Jobs.TaskScheduler
{
    public class JobWorker : ISpareTimeWorker
    {
        /// <summary>
        /// 检查活动/服务状态(每分钟执行)
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="count"></param>
        [SpareTime("* * * * *", "检查活动/服务状态", DoOnce = true, StartNow = true)]
        public async Task CheckServiceStatus(SpareTimer timer, long count)
        {
            var current = DateTime.Now;
            // 获取仓储
            var serviceRespository = Db.GetRepository<Service>();
            //将所有服务结束时间超出当前时间的，状态全部改为已结束
            var result = await serviceRespository.Context.BatchUpdate<Service>()
                .Where(x => !x.IsDeleted && current > x.PlanEndTime)
                .Set(x => x.Status, x => ServiceStatusEnum.Ended)
                .ExecuteAsync();
            await Task.CompletedTask;
        }
    }
}
