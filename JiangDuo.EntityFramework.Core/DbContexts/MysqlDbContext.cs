using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace JiangDuo.EntityFramework.Core.DbContexts;

[AppDbContext("MySqlDbConnectionString", $"{DbProvider.MySql}@8.0.28")]
public class MysqlDbContext : AppDbContext<MysqlDbContext>  // 继承 AppDbContext<> 类
{
    /// <summary>
    /// 继承父类构造函数
    /// </summary>
    /// <param name="options"></param>
    public MysqlDbContext(DbContextOptions<MysqlDbContext> options) : base(options)
    {
        InsertOrUpdateIgnoreNullValues = true;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }



}

