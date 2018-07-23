using System.Collections.Generic;
using BM.Api.Models;
using BM.Services.Common;
using System.Web.Http;
using BM.Api.Attributes;

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
        [ModelValid]
        [Route("api/fortun/detailbatch")]
        public object DetailedBatch(DetailedBatch detBaModel)
        {
            var returnCode = new ReturnCode();

            //var userInfo = UserService.GetUserByPhone(detBaModel.Phone, returnCode);

            ////获取用户信息过程中系统错误
            //if (returnCode.Code != default(int))
            //    return returnCode;

            //if (userInfo == null)
            //{ returnCode.Code = 1996; return returnCode; }

            //var html = Services.WebData.DetailedBatch.Handler.Get(detBaModel.UserName, Convert.ToDateTime(detBaModel.BirthDay), detBaModel.IsMan);


            return returnCode;
        }

        /// <summary>
        /// 宝宝起名信息
        /// </summary>
        /// <param name="babyName">宝宝对象</param>
        /// <returns></returns>
        [ModelValid]
        [Route("api/fortun/babyname")]
        public object BabyName(BabyName babyName)
        {
            var returnCode = new ReturnCode();

            return returnCode;
        }

        /// <summary>
        /// 八字合婚信息
        /// </summary>
        /// <param name="marriage">合婚对象</param>
        /// <returns></returns>
        [ModelValid]
        [Route("api/fortun/marriage")]
        public object Marriage(Marriage marriage)
        {
            var returnCode = new ReturnCode();

            return returnCode;
        }

        /// <summary>
        /// 获取联动标题
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/fortun/dream/title")]
        public object DreamTitle()
        {
            var returnCode = new ReturnCode();

            var dic = Services.WebData.Dream.Handler.Title();

            return new List<object>
            {
                returnCode,
                dic
            };
        }

        /// <summary>
        /// 获取解梦详细内容
        /// </summary>
        /// <param name="dreamId">解梦ID，来源于 api/fortun/dream/title 里</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/fortun/dream/detail")]
        public object DreamDetail(int dreamId)
        {
            var returnCode = new ReturnCode();

            var detail = Services.WebData.Dream.Handler.Detail(dreamId);

            if (detail == "")
            {
                returnCode.Code = 2999;
                return returnCode;
            }

            return new List<object>
            {
                returnCode,
                detail
            };
        }
    }
}
