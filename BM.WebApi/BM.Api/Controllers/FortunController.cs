using BM.Api.Models;
using BM.Services.Common;
using System.Web.Http;

namespace BM.Api.Controllers
{
    /// <summary>
    /// 算命信息接口与（Fortunetelling）
    /// </summary>
    public class FortunController : ApiController
    {
        /// <summary>
        /// 八字详批信息
        /// </summary>
        /// <param name="detBaModel">八字详批对象</param>
        /// <returns></returns>
        [Route("api/Fortun/DetailedBatch")]
        public object DetailedBatch(DetailedBatch detBaModel)
        {
            var returnCode = new ReturnCode();

            if (!ModelState.IsValid)
            {
                returnCode.Code = 9999;
                //获取所有不合法的字段说明
                //returnCode.Remark = ModelState.Values.SelectMany(error => error.Errors).Aggregate("", (current, e) => current + e.ErrorMessage + "; ");

                foreach (var item in ModelState.Values)
                {
                    foreach (var e in item.Errors)
                    {
                        if (string.IsNullOrEmpty(e.ErrorMessage))
                            returnCode.Remark += e.Exception.Message;
                        else
                            returnCode.Remark += e.ErrorMessage;
                    }
                }

                return returnCode;
            }

            //var userInfo = UserService.GetUserByPhone(detBaModel.Phone, returnCode);

            ////获取用户信息过程中系统错误
            //if (returnCode.Code != default(int))
            //    return returnCode;

            //if (userInfo == null)
            //{ returnCode.Code = 1996; return returnCode; }

            //var html = Services.WebData.DetailedBatch.Handler.Get(detBaModel.UserName, Convert.ToDateTime(detBaModel.BirthDay), detBaModel.IsMan);


            return null;
        }
    }
}
