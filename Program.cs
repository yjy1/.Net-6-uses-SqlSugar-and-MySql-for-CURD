using Autofac;
using Autofac.Extensions.DependencyInjection;
using SqlSugar;
using System.Reflection;

//.Net 6 使用 SqlSugar 连接 MySql 进行简单增删改查 
//参考资料： 
//作者： 昨夜丶雨疏风骤  
//项目地址：https://gitee.com/mengke2815/net6-project/tree/master  基于.NET7平台WebApi项目
//来源：gitee  

var builder = WebApplication.CreateBuilder(args);

 
builder.Services.AddControllers();
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region 注入数据库 SqlSugar ORM
builder.Services.AddSingleton(options =>
{
    return new SqlSugarScope(new List<ConnectionConfig>()
        {
            new ConnectionConfig() {  
                DbType = SqlSugar.DbType.MySql,
                ConnectionString = builder.Configuration["ConnectString"],
                IsAutoCloseConnection = true,
            }
        },
        db =>
        {
            //单例参数配置，所有上下文生效       
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql);//输出sql
            };
        }
    );
});
#endregion

#region 初始化Autofac 注入程序集
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
var hostBuilder = builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    var assembly = Assembly.Load("Net 6 使用 SqlSugar");
    builder.RegisterAssemblyTypes(assembly).Where(a => a.Name.EndsWith("Repository")).AsSelf();
});
#endregion

#region 初始化AutoMapper 自动映射
var serviceAssembly = Assembly.Load("Net 6 使用 SqlSugar");
builder.Services.AddAutoMapper(serviceAssembly);
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
