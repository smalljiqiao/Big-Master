using System.Globalization;
using BM.Api.Attributes;
using BM.Api.Models;
using BM.Data.Domain;
using BM.Services.Common;
using System.Web.Http;
using BM.Services.Orders;
using BM.Services.ReturnServices;

namespace BM.Api.Controllers
{
    /// <summary>
    /// 订单接口
    /// </summary>
    public class OrdersController : ApiController
    {
        /// <summary>
        /// 八字详批
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ModelValid]
        [Route("api/orders/detailedbatch")]
        public object DetailedBatch(DetailedBatch batchModel)
        {
            var returnCode = new ReturnCode();

            //手机号码和安卓ID都为空
            if (string.IsNullOrEmpty(batchModel.Phone) && string.IsNullOrEmpty(batchModel.AndroidId))
            {
                returnCode.Code = 2889;
                return returnCode;
            }

            //检查是否输入价格参数
            //与前台约定好价格为零传输0.00
            //因为default(decimal) = 0 ,但它与0.00比较都是返回true，所以这里采用toString()后再进行比较
            if (batchModel.Price.ToString(CultureInfo.InvariantCulture) == default(decimal).ToString(CultureInfo.InvariantCulture))
            {
                returnCode.Code = 2888;
                return returnCode;
            }

            var orderModel = TransferToOrderModel(batchModel);

            var flag = OrderService.Insert(orderModel);

            if (!flag)
                returnCode.Code = -1;

            return new Return {ReturnCode = returnCode};
        }

        /// <summary>
        /// 将前端Model转换为OrderModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static Order TransferToOrderModel(object model)
        {
            Order order = null;

            //看不懂 case 里是判断 model is <ClassName>
            switch (model)
            {
                case DetailedBatch _:
                    var dBModel = (DetailedBatch)model;
                   
                    break;
            }

            return order;
        }
    }
}
