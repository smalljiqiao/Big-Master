using BM.Services.Common;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BM.Api.Attributes
{
    /// <summary>
    /// 模型验证特性
    /// </summary>
    public class ModelValidAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// action执行中进行检查model是否valid
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var returnCode = new ReturnCode();

            if (!actionContext.ModelState.IsValid)
            {
                returnCode.Code = 9999;
                //获取所有不合法的字段说明
                //returnCode.Remark = ModelState.Values.SelectMany(error => error.Errors).Aggregate("", (current, e) => current + e.ErrorMessage + "; ");

                foreach (var item in actionContext.ModelState.Values)
                {
                    foreach (var e in item.Errors)
                    {
                        if (string.IsNullOrEmpty(e.ErrorMessage))
                            returnCode.Remark += e.Exception.Message;
                        else
                            returnCode.Remark += e.ErrorMessage;
                    }
                }


                actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.OK,
                    new { returnCode },
                    actionContext.ControllerContext.Configuration.Formatters.JsonFormatter
                );
            }
        }
    }
}