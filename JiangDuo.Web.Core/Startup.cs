using JiangDuo.SignalR;
using JiangDuo.Web.Core.Handlers;
using Furion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace JiangDuo.Web.Core;

public class Startup : AppStartup
{
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddLogging(cfg =>
		{
			cfg.AddLog4Net();
		});
		services.AddJwt<JwtHandler>(enableGlobalAuthorize: true);
		services.AddCorsAccessor();
		// 添加即时通讯
		services.AddSignalR();
		services.AddControllers().AddDynamicApiControllers().AddInjectWithUnifyResult()
			.AddNewtonsoftJson(option =>
		   //忽略循环引用
		   option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
		);
		
		services.AddStackExchangeRedisCache(options =>
		{
			// 连接字符串，这里也可以读取配置文件
			options.Configuration = App.Configuration["ConnectionStrings:redisConnectionString"];
			// 键名前缀
			options.InstanceName = string.Empty;
		});
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}
		app.UseHttpsRedirection();
		app.UseCors(builder =>
		{
			//设置同源地址，不然有跨域问题
			builder.WithOrigins("https://localhost:7079", "同源地址")
				.AllowAnyHeader()
				.WithMethods("GET", "POST")
				.AllowCredentials();
		});
		app.UseRouting();
		app.UseCorsAccessor();
		app.UseAuthentication();
		app.UseAuthorization();
		app.UseInject(string.Empty);

		app.UseEndpoints(endpoints => {

			endpoints.MapHub<ChatHub>("/hubs/chathub");
			endpoints.MapControllers(); 
		});
	}
}