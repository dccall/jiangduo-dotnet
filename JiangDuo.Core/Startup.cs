using Furion;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace JiangDuo.Core;

[AppStartup(10)]
public class Startup : AppStartup
{

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRemoteRequest(options =>
        {
            // 配置高德api基本信息
            options.AddHttpClient("GaoDe", c =>
            {
                c.BaseAddress = new Uri(App.Configuration["Gaode:Address"]);
            });
            // 配置微信api基本信息
            options.AddHttpClient("WeXin", c =>
            {
                c.BaseAddress = new Uri(App.Configuration["WeiXin:Address"]);
            });
        });

       
    }
}
