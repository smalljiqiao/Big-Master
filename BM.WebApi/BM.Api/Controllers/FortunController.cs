using BM.Api.Attributes;
using BM.Api.Models;
using BM.Services.Common;
using BM.Services.ReturnServices;
using System;
using System.IO;
using System.Web.Http;
using BM.Services.Data.Logs;

namespace BM.Api.Controllers
{
    /// <summary>
    /// 算命信息接口与（Fortunetelling）
    /// </summary>
    public class FortunController : BaseController
    {
        /// <summary>
        /// 八字详批信息
        /// </summary>
        /// <param name="detBaModel">八字详批对象</param>
        /// <returns></returns>
        [ModelValid]
        [Route("api/fortun/detailbatch")]
        public Return DetailedBatch(DetailedBatch detBaModel)
        {
            var resultReturn = new Return();

            try
            {
                var dic = Services.WebData.DetailedBatch.Handler.GetHtml(detBaModel.UserName,
                    Convert.ToDateTime(detBaModel.BirthDay), detBaModel.IsMan);

                resultReturn.Content = dic;

                return resultReturn;
            }
            catch (Exception ex)
            {
                LogService.InsertLog(ex);
                resultReturn.SetWrong();
                return resultReturn;
            }
        }

        /// <summary>
        /// 宝宝起名信息
        /// </summary>
        /// <param name="babyName">宝宝对象</param>
        /// <returns></returns>
        [ModelValid]
        [Route("api/fortun/babyname")]
        public Return BabyName(BabyName babyName)
        {
            var resultReturn = new Return();

            try
            {
                var dic = Services.WebData.BabyName.Handler.GetHtml(babyName.Surname, babyName.BirthDay, babyName.IsMan,
                    babyName.Province, babyName.City, babyName.NameType);

                resultReturn.Content = dic;

                return resultReturn;
            }
            catch (Exception ex)
            {
                LogService.InsertLog(ex);
                resultReturn.SetWrong();
                return resultReturn;
            }
        }

        /// <summary>
        /// 八字合婚信息
        /// </summary>
        /// <param name="marriage">合婚对象</param>
        /// <returns></returns>
        [ModelValid]
        [Route("api/fortun/marriage")]
        public Return Marriage(Marriage marriage)
        {
            var resultReturn = new Return();

            try
            {
                var dic = Services.WebData.Marriage.Handler.GetHtml(marriage.ManName, marriage.ManBirthDay,
                    marriage.ManTime, marriage.WomanName, marriage.WomanBirthDay, marriage.WomanTime);

                resultReturn.Content = dic;

                return resultReturn;
            }
            catch (Exception ex)
            {
                LogService.InsertLog(ex);
                resultReturn.SetWrong();
                return resultReturn;
            }
        }

        /// <summary>
        /// 获取联动标题
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/fortun/dream/title")]
        public Return DreamTitle()
        {
            var resultReturn = new Return();

            try
            {
                var dic = Services.WebData.Dream.Handler.Title();

                resultReturn.Content = dic;

                return resultReturn;
            }
            catch (Exception ex)
            {
                LogService.InsertLog(ex);
                resultReturn.SetWrong();
                return resultReturn;
            }
        }

        /// <summary>
        /// 获取解梦详细内容
        /// </summary>
        /// <param name="dreamId">解梦ID，来源于 api/fortun/dream/title 里</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/fortun/dream/detail")]
        public Return DreamDetail(int dreamId)
        {
            var resultReturn = new Return();

            try
            {
                var detail = Services.WebData.Dream.Handler.Detail(dreamId);

                if (detail == "")
                {
                    resultReturn.ReturnCode.Code = 2999;
                    return resultReturn;
                }

                resultReturn.Content = detail;
            }
            catch (Exception ex)
            {
                LogService.InsertLog(ex);
                resultReturn.SetWrong();
                return resultReturn;
            }

            return resultReturn;
        }
    }
}
