using SqlSugar;

namespace Net_6_使用_SqlSugar.Setup
{
    public static class SqlsugarSetup
    {
        public static void AddSqlsugarSetup(this IServiceCollection services, IConfiguration configuration,
        string dbName = "ConnectString")
        {
            SqlSugarScope sqlSugar = new SqlSugarScope(
                new ConnectionConfig()
                {
                    DbType = SqlSugar.DbType.MySql,
                    ConnectionString = configuration[dbName],
                    IsAutoCloseConnection = true,
                },
                db =>
                {
                    //单例参数配置，所有上下文生效       
                    db.Aop.OnLogExecuting = (sql, pars) =>
                    {
                        Console.WriteLine(sql);//输出sql
                    };

                    //技巧：拿到非ORM注入对象
                    //services.GetService<注入对象>();
                }
            );
            services.AddSingleton<ISqlSugarClient>(sqlSugar);//这边是SqlSugarScope用AddSingleton
        }
    }
}
