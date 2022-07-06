using JiangDuo.EntityFramework.Core.DbContexts;
using Furion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JiangDuo.EntityFramework.Core;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDatabaseAccessor(options =>
        {
            options.AddDbPool<MysqlDbContext>(providerName: default, optionBuilder: opt =>
            {
                //批量操作
                opt.UseBatchEF_MySQLPomelo();
            });
        }, "JiangDuo.Database.Migrations");
    }
}
