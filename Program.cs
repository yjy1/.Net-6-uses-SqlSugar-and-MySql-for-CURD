using Autofac;
using Autofac.Extensions.DependencyInjection;
using SqlSugar;
using System.Reflection;

//.Net 6 ʹ�� SqlSugar ���� MySql ���м���ɾ�Ĳ� 
//�ο����ϣ� 
//���ߣ� ��ҹؼ�������  
//��Ŀ��ַ��https://gitee.com/mengke2815/net6-project/tree/master  ����.NET7ƽ̨WebApi��Ŀ
//��Դ��gitee  

var builder = WebApplication.CreateBuilder(args);

 
builder.Services.AddControllers();
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region ע�����ݿ� SqlSugar ORM
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
            //�����������ã�������������Ч       
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql);//���sql
            };
        }
    );
});
#endregion

#region ��ʼ��Autofac ע�����
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
var hostBuilder = builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    var assembly = Assembly.Load("Net 6 ʹ�� SqlSugar");
    builder.RegisterAssemblyTypes(assembly).Where(a => a.Name.EndsWith("Repository")).AsSelf();
});
#endregion

#region ��ʼ��AutoMapper �Զ�ӳ��
var serviceAssembly = Assembly.Load("Net 6 ʹ�� SqlSugar");
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
