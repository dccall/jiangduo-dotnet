using Furion;
using Furion.DependencyInjection;
using Furion.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiangDuo.Core;
using Furion.Logging;

namespace JiangDuo.Jobs.TaskScheduler;

public class SpareTimeListener : ISpareTimeListener, ISingleton
{
    /// <summary>
    /// 监听所有任务
    /// </summary>
    /// <param name="executer"></param>
    /// <returns></returns>
    public Task OnListener(SpareTimerExecuter executer)
    {
        switch (executer.Status)
        {
            // 执行开始通知
            case 0:
                Log.Information($"{executer.Timer.WorkerName} 任务开始通知");
                break;
            // 任务执行之前通知
            case 1:
                Log.Information($"{executer.Timer.WorkerName} 执行之前通知");
                break;
            // 执行成功通知
            case 2:
                Log.Information($"{executer.Timer.WorkerName} 执行成功通知");
                break;
            // 任务执行失败通知
            case 3:
                Log.Information($"{executer.Timer.WorkerName} 执行失败通知");
                break;
            // 任务执行停止通知
            case -1:
                Log.Information($"{executer.Timer.WorkerName} 执行停止通知");
                break;
            // 任务执行取消通知
            case -2:
                Log.Information($"{executer.Timer.WorkerName} 执行取消通知");
                break;
            default:
                break;
        }
        return Task.CompletedTask;
    }
}

