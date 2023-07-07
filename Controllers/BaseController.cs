using Microsoft.AspNetCore.Mvc;
using Net_6_使用_SqlSugar.ViewModels;

namespace Net_6_使用_SqlSugar.Controllers;

/// <summary>
/// 控制器基类
/// </summary>
 
[ApiController]
public class BaseController : ControllerBase
{
 
    #region 统一返回结构
    protected virtual IActionResult JsonView(object obj)
    {
        return Ok(new JsonView { Code = StatusCodes.Status200OK, Msg = "操作成功", Data = obj });
    }
    protected virtual IActionResult JsonView(object obj, int count)
    {
        return Ok(new JsonView { Code = StatusCodes.Status200OK, Msg = "操作成功", Data = obj, Count = count });
    }
    protected virtual IActionResult JsonView(string msg)
    {
        return Ok(new JsonView { Code = StatusCodes.Status400BadRequest, Msg = msg });
    }
    protected virtual IActionResult JsonView(bool s)
    {
        if (s)
        {
            return Ok(new JsonView { Code = StatusCodes.Status200OK, Msg = "操作成功" });
        }
        else
        {
            return Ok(new JsonView { Code = StatusCodes.Status400BadRequest, Msg = "操作失败" });
        }
    }
    protected virtual IActionResult JsonView(bool s, string msg)
    {
        if (s)
        {
            return Ok(new JsonView { Code = StatusCodes.Status200OK, Msg = msg });
        }
        else
        {
            return Ok(new JsonView { Code = StatusCodes.Status400BadRequest, Msg = msg });
        }
    }
    protected virtual IActionResult JsonView(bool s, object data)
    {
        if (s)
        {
            return Ok(new JsonView { Code = StatusCodes.Status200OK, Data = data, Msg = "操作成功" });
        }
        else
        {
            return Ok(new JsonView { Code = StatusCodes.Status400BadRequest, Data = data, Msg = "操作失败" });
        }
    }
    #endregion
}
