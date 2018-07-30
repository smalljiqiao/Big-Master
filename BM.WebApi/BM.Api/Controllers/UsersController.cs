using BM.Api.Attributes;
using BM.Data.Domain;
using BM.Services.Common;
using BM.Services.ModelTransfer;
using BM.Services.ReturnServices;
using System;
using System.Collections.Generic;
using System.Web.Http;
using BM.Services.Data.Androids;
using BM.Services.Data.BurialPoint;
using BM.Services.Data.ShortMessages;
using BM.Services.Data.Users;
using Android = BM.Api.Models.Android;
using Sms = BM.Services.Data.ShortMessages.Sms;
using User = BM.Api.Models.User;


namespace BM.Api.Controllers
{

    /// <summary>
    /// 用户信息接口
    /// </summary>

    public class UsersController : ApiController
    {
        /// <summary>
        /// 写入安卓ID
        /// </summary>
        /// <param name="android">安卓信息对象</param>
        /// <returns></returns>
        [HttpPost]
        [ModelValid]
        [Route("api/user/android/id")]
        public object AndroidId(Android android)
        {
            Data.Domain.User userInfo = null; //返回的UserModel
            var isBind = false; //是否绑定手机号码
            User userInfoMap = null; //返回的前端UserModel
            Dictionary<string, object> returnDic = null; //返回的键值数组

            var returnCode = new ReturnCode();

            //查找Android Table 是否存在该Android
            var oldAndroid = AndroidService.GetByAndroidId(android.AndroidId, returnCode);

            //数据库执行过程中出错
            if (returnCode.Code != default(int))
            {
                return new Return { ReturnCode = returnCode };
            }

            //如果UserId已存在Android Table，则直接返回UserInfo
            if (oldAndroid != null)
            {
                userInfo = UserService.GetUserByUserId(oldAndroid.UserId.ToString(), returnCode);

                //模型转换
                userInfoMap = ModelTransfer.Mapper(userInfo, new User());

                isBind = !string.IsNullOrEmpty(userInfoMap.Phone);

                //忽略密码
                userInfoMap.Password = null;

                returnDic = new Dictionary<string, object> { { "IsBind", isBind }, { "User", userInfoMap } };

                return new Return { ReturnCode = returnCode, Content = returnDic };
            }

            //写入Android Table
            var flag = AndroidService.Insert(android.AndroidId);

            //数据库执行过程中出错
            if (!flag)
            {
                returnCode.Code = -1;
                return new Return { ReturnCode = returnCode };
            }

            //重新获取安卓信息
            var newAndroid = AndroidService.GetByAndroidId(android.AndroidId, returnCode);

            //数据库执行过程中出错 || 数据库并没有写入新数据
            if (returnCode.Code != default(int) || newAndroid == null)
            {
                return new Return { ReturnCode = returnCode };
            }

            //用户表的UserID为空，因为后续操作需要该ID，所以判定为系统错误。
            //其实这个应该不会触发的吧，因为数据库UserId设置为主键，如果写入过程中写入空值也是会报错的。
            if (string.IsNullOrEmpty(newAndroid.UserId.ToString()))
            {
                returnCode.Code = -1;
                return new Return { ReturnCode = returnCode };
            }

            //写入User Table
            //不检测User Table是否存在该UserId，因为这里都是将Android的UserId写入到User Table里。
            userInfo = UserService.Insert(newAndroid.UserId.ToString(), returnCode);

            //数据库执行过程中出错 当userInfo为null时也触发下面判断
            if (returnCode.Code != default(int))
            {
                return new Return { ReturnCode = returnCode };
            }

            //模型转换
            userInfoMap = ModelTransfer.Mapper(userInfo, new User());

            //是否绑定手机号码
            isBind = !string.IsNullOrEmpty(userInfoMap.Phone);

            //忽略密码
            userInfoMap.Password = null;

            returnDic = new Dictionary<string, object> { { "IsBind", isBind }, { "User", userInfoMap } };

            return new Return { ReturnCode = returnCode, Content = returnDic };
        }

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

                var flag = SmsService.InsertOrUpdate(smsModel);

                if (!flag)
                    returnCode.Code = -1;
            }
            else
            {
                if (smsResponse != null)
                {
                    //短信验证码 ：使用同一个签名，对同一个手机号码发送短信验证码，支持1条/分钟，5条/小时 ，累计10条/天。
                    if (smsResponse.Message.IndexOf("触发天级流控", StringComparison.Ordinal) != -1)
                        returnCode.Code = 1887;
                    else
                        returnCode.Code = 1992;

                    BpService.Use(smsResponse.Message);
                }

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

            //安卓ID为空
            if (string.IsNullOrEmpty(userModel.Android))
            {
                returnCode.Code = 1885;
                return new Return { ReturnCode = returnCode };
            }

            //密码为空
            if (string.IsNullOrEmpty(userModel.Password))
            {
                returnCode.Code = 1997;
                return new Return { ReturnCode = returnCode };
            }

            //验证码为空
            if (string.IsNullOrEmpty(userModel.VCode))
            {
                returnCode.Code = 1994;
                return new Return { ReturnCode = returnCode };
            }

            var sms = UserService.GetSmsByPhone(userModel.Phone, returnCode);

            //数据库执行出错
            if (returnCode.Code != default(int))
            {
                return new Return { ReturnCode = returnCode };
            }

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
            var flag = SmsService.InsertOrUpdate(newSms);

            //数据库执行错误
            if (!flag)
            {
                returnCode.Code = -1;
                return new Return { ReturnCode = returnCode };
            }

            var androidInfo = AndroidService.GetByAndroidId(userModel.Android, returnCode);

            //查询UserID过程中出现错误
            if (returnCode.Code != default(int))
            {
                returnCode.Code = -1;
                return new Return { ReturnCode = returnCode };
            }

            var userInfo = UserService.Register(androidInfo.UserId.ToString(), userModel.Phone, userModel.Password, returnCode);

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