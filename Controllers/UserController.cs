using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace Net_6_使用_SqlSugar.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISqlSugarClient db;

        public UserController(ISqlSugarClient db)
        {
            this.db = db;
        }

        [HttpGet]
        public void Get()
        {
            var userInfos = db.Queryable<UserInfo>().ToList();  //查询所有
            var userInfos1 = db.Queryable<UserInfo>().Where(a => a.Id == 1).ToList();  //根据条件查询  
            var single = db.Queryable<UserInfo>().Where(a => a.Id == 1).Single().ToList();  //根据条件查询一条  
        }

        public void SearchDemo()
        {
             
            var p = new PageModel()
            {
                PageIndex = 1,
                PageSize = 2
            };
            // 分页查询
            var data5 = db.Queryable<UserInfo>(it => it.用户 == "xx", p);
            Console.Write(p.TotalCount); //返回总数
                                         // 分页查询加排序
            var data6 = db.GetPageList(it => it.用户 == "xx", p, it => it.用户, OrderByType.Asc);
            Console.Write(p.TotalCount); //返回总数
                                         //组装条件查询作为条件实现 分页查询加排序
            List<IConditionalModel> conModels = new List<IConditionalModel>
            {
                new ConditionalModel()
                {
                    FieldName = "id",
                    ConditionalType = ConditionalType.Equal,
                    FieldValue = "1"
                } //id=1
            };
            var data7 = db.GetPageList(conModels, p, it => it.用户, OrderByType.Asc);
            //4.9.7.5支持了转换成queryable,我们可以用queryable实现复杂功能
            db.AsQueryable()
                .Where(x => x.id == 1)
                .ToList();
        }

        //多表查询
        public void JoinDemo()
        {
            var list = Db.Queryable<User
                , User>((st, sc) => new object[]
                {
                  JoinType.Left
                      , st.用户 == sc.用户
                })
                .Select<User>()
                .ToList();
        }

        //使用事务的例子
        public void TranDemo()
        {
            var result = Db.Ado.UseTran(() =>
            {
                //这里写你的逻辑
            });
            if (result.IsSuccess)
            {
                //成功
            }
            else
            {
                Console.WriteLine(result.ErrorMessage);
            }
        }
        //更新例子
        public void UpdateDemo()
        {
            var student = new User()
            {
                id = 1,
                用户 = "jack"
            };
            var studentArray = new User[]
            {
              student
            };
            db.Update(student); //根据实体更新
            db.UpdateRange(studentArray); //批量更新
                                          // db.Update(it => new User() { 用户 = "a", CreateTime = DateTime.Now }, it => it.Id == 1);// 只更新Name列和CreateTime列，其它列不更新，条件id=1
                                          //支持db.AsUpdateable(student)
        }

        //新增数据
        //插入例子
        public void InsertDemo()
        {
            var student = new User()
            {
                用户 = "jack"
            };
            var studentArray = new User[]
            {
              student
            };
            db.Insert(student); //插入
            db.InsertRange(studentArray); //批量插入
            var id = db.InsertReturnIdentity(student); //插入返回自增列
                                                       //4.9.7.5我们可以转成 Insertable实现复杂插入
                                                       // db.AsInsertable(insertObj).ExecuteCommand();
        }

        //删除数据
        //删除例子
        public void DeleteDemo()
        {
            var student = new User()
            {
                id = 1,
                用户 = "jack"
            };
            db.Delete(student); //根据实体删除
            db.DeleteById(1); //根据主键删除
            db.DeleteById(new int[]
            {
              1, 2

            }); //根据主键数组删除
            db.Delete(it => it.id == 1); //根据条件删除
                                         //支持db.AsDeleteable()
        }
    }
}
