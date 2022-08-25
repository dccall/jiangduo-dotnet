using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.Logging;
using Furion.TaskScheduler;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace JiangDuo.Jobs.TaskScheduler
{
    /// <summary>
    /// 任务调度
    /// </summary>
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

            //任务中解析对象/服务，需要增加独立作用域，不然连接可能会被释放

            Scoped.Create((_, scope) =>
            {
                var current = DateTime.Now;
                var services = scope.ServiceProvider;
                // 获取数据库上下文
                var dbContext = Db.GetDbContext(services);
                // 获取仓储
                //var serviceRepository = Db.GetRepository<Service>(services);
                //将所有服务结束时间超出当前时间的，状态全部改为已结束
                var resultCount = dbContext.BatchUpdate<Service>()
                    .Where(x => !x.IsDeleted && x.Status != ServiceStatusEnum.Ended && current > x.PlanEndTime)
                    .Set(x => x.Status, x => ServiceStatusEnum.Ended)
                    .Execute();
                Log.Information($"{timer.WorkerName},执行数量:" + resultCount);
            });
            await Task.CompletedTask;
        }

        /// <summary>
        /// 检查预约占用状态(每分钟执行)
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="count"></param>
        [SpareTime("* * * * *", "检查活动预约占用状态", DoOnce = true, StartNow = true)]
        public async Task CheckServiceOccupancyStatus(SpareTimer timer, long count)
        {
            //任务中解析对象/服务，需要增加独立作用域，不然连接可能会被释放
            Scoped.Create((_, scope) =>
            {
                var current = DateTime.Now;
                double minutes = 5; //可改为从系统配置里取
                var services = scope.ServiceProvider;
                // 获取数据库上下文
                var dbContext = Db.GetDbContext(services);
                // 获取仓储
                //var participantRepository = Db.GetRepository<Participant>();
                //批量删除所有超时的占用预约
                var resultCount = dbContext.DeleteRange<Participant>(x => x.Status == ParticipantStatus.Occupancy && current > x.CreatedTime.AddMinutes(minutes));
                Log.Information($"{timer.WorkerName},执行数量:" + resultCount);
            });
            await Task.CompletedTask;
            
        }

    }
}
