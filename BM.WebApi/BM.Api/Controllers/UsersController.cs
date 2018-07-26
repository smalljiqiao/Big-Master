using System;
using System.Collections.Generic;
using BM.Api.Attributes;
using BM.Services.Common;
using BM.Services.ReturnServices;
using BM.Services.Users;
using System.Web.Http;
using System.Web.UI;
using BM.Data.Domain;
using BM.Services.BurialPoint;
using BM.Services.ModelTransfer;
using BM.Services.ShortMessages;
using Sms = BM.Services.ShortMessages.Sms;
using User = BM.Api.Models.User;

namespace BM.Api.Controllers
{

    /// <summary>
    /// 用户信息接口
    /// </summary>

    public class UsersController : ApiController
    {
        /// <summary>
        /// 获取短信验证码
        /// </summary>
        /// <param name="userModel">用户对象</param>
        /// <returns></returns>
        [HttpPost]
        [ModelValid]
        [Route("api/user/sms")]
        public object Sms(User userModel)
        {
            var returnCode = new ReturnCode();

            var vCode = CommonHelper.CreateCode();
            var sms = new Sms(vCode);
            var smsResponse = sms.SendSmsToPhone(userModel.Phone);


            if (smsResponse?.Code != null && smsResponse.Code == "OK")
            {

                var smsModel = new Data.Domain.Sms
                {
                    Code = vCode,
                    Phone = userModel.Phone,
                    UpdateTime = DateTime.Now
                };

                SmsService.InsertOrUpdate(smsModel);
            }
            else
            {
                //短信验证码 ：使用同一个签名，对同一个手机号码发送短信验证码，支持1条/分钟，5条/小时 ，累计10条/天。
                if (smsResponse != null && smsResponse.Message.IndexOf("触发天级流控", StringComparison.Ordinal) != -1)
                    returnCode.Code = 1887;
                else
                    returnCode.Code = 1992;

                BpService.Use(returnCode.Message);
            }

            return new Return
            {
                ReturnCode = returnCode
            };
        }


        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="userModel">用户对象</param>
        /// <returns>用户对象或返回码对象</returns>
        [HttpPost]
        [ModelValid]
        [Route("api/user/register")]
        public object Register(User userModel)
        {
            var returnCode = new ReturnCode();

            if (string.IsNullOrEmpty(userModel.Password))
            {
                returnCode.Code = 1997;
                return new Return { ReturnCode = returnCode };
            }

            if (string.IsNullOrEmpty(userModel.VCode))
            {
                returnCode.Code = 1994;
                return new Return { ReturnCode = returnCode };
            }

            var sms = UserService.GetSmsByPhone(userModel.Phone, returnCode);

            //还没发送短信
            if (sms == null)
            {
                returnCode.Code = 1991;
                return new Return { ReturnCode = returnCode };
            }

            //验证码已过期
            if (Convert.ToDateTime(sms.UpdateTime).AddMinutes(5) < DateTime.Now)
            {
                returnCode.Code = 1990;
                return new Return { ReturnCode = returnCode };
            }

            //已使用
            if (sms.IsUse)
            {
                returnCode.Code = 1888;
                return new Return { ReturnCode = returnCode };
            }

            //验证码不正确
            if (sms.Code != userModel.VCode)
            {
                returnCode.Code = 1889;
                return new Return { ReturnCode = returnCode };
            }

            //避免不同数据库上下文操作出现的错误
            var newSms = new Data.Domain.Sms { Code = sms.Code, IsUse = true, Phone = sms.Phone, UpdateTime = DateTime.Now };

            //更新Sms IsUse 为 true
            SmsService.InsertOrUpdate(newSms);

            var userInfo = UserService.Register(userModel.Phone, userModel.Password, returnCode);

            //注册过程中出现错误
            if (returnCode.Code != default(int))
            {
                return new Return { ReturnCode = returnCode };
            }

            //将User数据库模型类转换为页面模型类
            var user = ModelTransfer.Mapper(userInfo, new User());
            user.Password = "";

            return new Return
            {
                ReturnCode = returnCode,
                Content = user
            };
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userModel">用户对象</param>
        /// <returns>用户对象或返回码对象</returns>
        [HttpPost]
        [ModelValid]
        [Route("api/user/login")]
        public object Login(User userModel)
        {
            var returnCode = new ReturnCode();

            if (string.IsNullOrEmpty(userModel.Password))
            {
                returnCode.Code = 1997;
                return new Return { ReturnCode = returnCode };
            }

            var userInfo = UserService.GetUserByPhone(userModel.Phone, returnCode, new DbEntities());

            //注册过程中出现错误
            if (returnCode.Code != default(int))
            {
                return new Return { ReturnCode = returnCode };
            }

            //该手机号码还没有注册
            if (userInfo == null)
            {
                returnCode.Code = 1996;
                return new Return { ReturnCode = returnCode };
            }

            //密码错误
            if (userInfo.Password != userModel.Password)
            {
                returnCode.Code = 1999;
                return new Return { ReturnCode = returnCode };
            }

            //将User数据库模型类转换为页面模型类
            var user = ModelTransfer.Mapper(userInfo, new User());
            user.Password = "";

            return new Return
            {
                ReturnCode = returnCode,
                Content = user
            };
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userModel">用户对象</param>
        /// <returns>用户对象或返回码对象</returns>
        [HttpPost]
        [ModelValid]
        [Route("api/user/changepassword")]
        public object ChangePassword(User userModel)
        {
            var returnCode = new ReturnCode();

            if (string.IsNullOrEmpty(userModel.Password))
            {
                returnCode.Code = 1997;
                return new Return { ReturnCode = returnCode };
            }

            var userInfo = UserService.ChangePassword(userModel.Phone, userModel.Password, returnCode);

            //修改密码过程中出现错误
            if (returnCode.Code != default(int))
            {
                return new Return { ReturnCode = returnCode };
            }

            //将User数据库模型类转换为页面模型类
            var user = ModelTransfer.Mapper(userInfo, new User());
            user.Password = "";

            return new Return
            {
                ReturnCode = returnCode,
                Content = user
            };
        }

        /// <summary>
        /// 修改用户昵称和邮箱
        /// </summary>
        /// <param name="userModel">用户对象</param>
        /// <returns>用户对象或返回码对象</returns>
        [HttpPost]
        [ModelValid]
        [Route("api/user/changenameoemail")]
        public object ChangeNameOEmail(User userModel)
        {
            var returnCode = new ReturnCode();

            if (userModel.Nickname == "" && userModel.Email == "")
            {
                returnCode.Code = 1995;
                return new Return { ReturnCode = returnCode };
            }

            var userInfo = UserService.ChangeN0E(userModel.Phone, userModel.Email, userModel.Nickname, returnCode);

            //修改密码过程中出现错误
            if (returnCode.Code != default(int))
            {
                return new Return { ReturnCode = returnCode };
            }

            //将User数据库模型类转换为页面模型类
            var user = ModelTransfer.Mapper(userInfo, new User());
            user.Password = "";

            return new Return
            {
                ReturnCode = returnCode,
                Content = user
            };
        }
    }
}