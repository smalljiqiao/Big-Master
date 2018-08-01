using BM.Api.Attributes;
using BM.Data.Domain;
using BM.Services.Common;
using BM.Services.ModelTransfer;
using BM.Services.ReturnServices;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Http;
using BM.Services.Data.Androids;
using BM.Services.Data.BurialPoint;
using BM.Services.Data.Logs;
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
        public Return AndroidId(Android android)
        {
            var resultReturn = new Return();

            try
            {
                //查找Android Table 是否存在该Android
                var oldAndroid = AndroidService.GetByAndroidId(android.AndroidId);

                //如果UserId已存在Android Table，则直接返回UserInfo

                Data.Domain.User userInfo = null; //返回的UserModel

                if (oldAndroid != null)
                {
                    userInfo = UserService.GetUserByUserId(oldAndroid.UserId.ToString());
                }
                else
                {
                    //写入Android Table
                    AndroidService.Insert(android.AndroidId);

                    //重新获取安卓信息
                    var newAndroid = AndroidService.GetByAndroidId(android.AndroidId);

                    //写入User Table
                    userInfo = UserService.Insert(newAndroid.UserId.ToString());
                }

                //模型转换
                var userInfoMap = ModelTransfer.Mapper(userInfo, new User());

                //是否绑定手机号码
                var isBind = !string.IsNullOrEmpty(userInfoMap.Phone);

                //忽略密码和用户ID
                userInfoMap.Password = null;
                userInfoMap.UserId = null;

                var returnDic = new Dictionary<string, object> {{"IsBind", isBind}, {"User", userInfoMap}};

                resultReturn.Content = returnDic;
                return resultReturn;
            }
            catch (Exception ex)
            {
                LogService.InsertLog(ex);
                resultReturn.ReturnCode.Code = -1;
                resultReturn.Content = null;
                return resultReturn;
            }
        }

        /// <summary>
        /// 获取短信验证码
        /// </summary>
        /// <param name="userModel">用户对象</param>
        /// <returns></returns>
        [HttpPost]
        [ModelValid]
        [Route("api/user/sms")]
        public Return Sms(User userModel)
        {
            var resultReturn = new Return();

            try
            {
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
                        resultReturn.ReturnCode.Code = -1;
                }
                else
                {
                    if (smsResponse != null)
                    {
                        //短信验证码 ：使用同一个签名，对同一个手机号码发送短信验证码，支持1条/分钟，5条/小时 ，累计10条/天。
                        if (smsResponse.Message.IndexOf("触发天级流控", StringComparison.Ordinal) != -1)
                            resultReturn.ReturnCode.Code = 1887;
                        else
                            resultReturn.ReturnCode.Code = 1992;

                        BpService.Use(smsResponse.Message);
                    }

                    BpService.Use("发送短信验证码返回的response为空");
                }

                return resultReturn;
            }
            catch (Exception ex)
            {
                LogService.InsertLog(ex);
                resultReturn.ReturnCode.Code = -1;
                resultReturn.Content = null;
                return resultReturn;
            }
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="userModel">用户对象</param>
        /// <returns>用户对象或返回码对象</returns>
        [HttpPost]
        [ModelValid]
        [Route("api/user/register")]
        public Return Register(User userModel)
        {
            var resultReturn = new Return();

            try
            {

                #region 检查必须字段是否为空

                //安卓ID为空
                if (string.IsNullOrEmpty(userModel.Android))
                {
                    resultReturn.ReturnCode.Code = 1885;
                    return resultReturn;
                }

                //密码为空
                if (string.IsNullOrEmpty(userModel.Password))
                {
                    resultReturn.ReturnCode.Code = 1997;
                    return resultReturn;
                }

                //验证码为空
                if (string.IsNullOrEmpty(userModel.VCode))
                {
                    resultReturn.ReturnCode.Code = 1994;
                    return resultReturn;
                }

                #endregion

                #region 验证该安卓ID是否被记录

                var androidInfo = AndroidService.GetByAndroidId(userModel.Android);

                //该安卓ID不存在Android表中
                if (androidInfo == null)
                {
                    resultReturn.ReturnCode.Code = 1884;
                    return resultReturn;
                }

                #endregion

                #region 短信验证

                var sms = SmsService.GetSmsByPhone(userModel.Phone);

                //还没发送短信
                if (sms == null)
                {
                    resultReturn.ReturnCode.Code = 1991;
                    return resultReturn;
                }

                //验证码已过期
                if (Convert.ToDateTime(sms.UpdateTime).AddMinutes(5) < DateTime.Now)
                {
                    resultReturn.ReturnCode.Code = 1990;
                    return resultReturn;
                }

                //已使用
                if (sms.IsUse)
                {
                    resultReturn.ReturnCode.Code = 1888;
                    return resultReturn;
                }

                //验证码不正确
                if (sms.Code != userModel.VCode)
                {
                    resultReturn.ReturnCode.Code = 1889;
                    return resultReturn;
                }

                //避免不同数据库上下文操作出现的错误
                var newSms = new Data.Domain.Sms
                {
                    Code = sms.Code,
                    IsUse = true,
                    Phone = sms.Phone,
                    UpdateTime = DateTime.Now
                };

                //更新Sms IsUse 为 true  //若出错，不抛出错误，只做错误记录
                SmsService.InsertOrUpdate(newSms);

                #endregion

                #region 验证手机号码是否已注册

                var userInfo = UserService.GetUserByPhone(userModel.Phone);

                //该手机号码已注册
                if (userInfo != null)
                {
                    resultReturn.ReturnCode.Code = 1998;
                    return resultReturn;
                }

                #endregion

                resultReturn = UserService.Register(androidInfo.UserId.ToString(), userModel.Phone, userModel.Password);

                //将User数据库模型类转换为页面模型类
                var user = ModelTransfer.Mapper(resultReturn.Content, new User());
                user.Password = "";

                resultReturn.Content = user;
                return resultReturn;
            }
            catch (Exception ex)
            {
                LogService.InsertLog(ex);
                resultReturn.ReturnCode.Code = -1;
                resultReturn.Content = null;
                return resultReturn;
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userModel">用户对象</param>
        /// <returns>用户对象或返回码对象</returns>
        [HttpPost]
        [ModelValid]
        [Route("api/user/login")]
        public Return Login(User userModel)
        {
            var resultReturn = new Return();

            try
            {
                #region 检查必需字段是否为空

                //密码为空
                if (string.IsNullOrEmpty(userModel.Password))
                {
                    resultReturn.ReturnCode.Code = 1997;
                    return resultReturn;
                }

                //安卓ID为空
                if (string.IsNullOrEmpty(userModel.Android))
                {
                    resultReturn.ReturnCode.Code = 1885;
                    return resultReturn;
                }

                #endregion

                #region 检查用户登录信息是否正确

                var userInfo = UserService.GetUserByPhone(userModel.Phone);

                //该手机号码还没有注册
                if (userInfo == null)
                {
                    resultReturn.ReturnCode.Code = 1996;
                    return resultReturn;
                }

                //密码错误
                if (userInfo.Password != userModel.Password)
                {
                    resultReturn.ReturnCode.Code = 1999;
                    return resultReturn;
                }

                #endregion

                #region 检查安卓ID是否更换，若更换了，使用旧UserId替换新UserId

                var nowUserId = userInfo.UserId.ToString();

                //获取登录时安卓ID对应的安卓信息
                var androidInfo = AndroidService.GetByAndroidId(userModel.Android);

                var oldUserId = androidInfo.UserId.ToString();

                //新旧UserId不一致，使用旧UserId替换新UserId
                if (nowUserId != oldUserId)
                {
                    userInfo.UserId = Guid.Parse(oldUserId);
                    UserService.ChangeUesrId(oldUserId, nowUserId); //替换UserID
                    UserService.DeleteByUserId(nowUserId); //删除User
                }

                #endregion

                //将User数据库模型类转换为页面模型类
                var user = ModelTransfer.Mapper(userInfo, new User());
                user.Password = "";

                resultReturn.Content = user;

                return resultReturn;
            }
            catch (Exception ex)
            {
                LogService.InsertLog(ex);
                resultReturn.ReturnCode.Code = -1;
                resultReturn.Content = null;
                return resultReturn;
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userModel">用户对象</param>
        /// <returns>用户对象或返回码对象</returns>
        [HttpPost]
        [ModelValid]
        [Route("api/user/changepassword")]
        public Return ChangePassword(User userModel)
        {
            var resultReturn = new Return();

            try
            {
                if (string.IsNullOrEmpty(userModel.Password))
                {
                    resultReturn.ReturnCode.Code = 1997;
                    return resultReturn;
                }

                resultReturn = UserService.ChangePassword(userModel.Phone, userModel.Password);

                if (resultReturn.ReturnCode.Code != default(int))
                {
                    return resultReturn;
                }

                //将User数据库模型类转换为页面模型类
                var user = ModelTransfer.Mapper(resultReturn.Content, new User());
                user.Password = "";

                resultReturn.Content = user;

                return resultReturn;
            }
            catch (Exception ex)
            {
                LogService.InsertLog(ex);
                resultReturn.ReturnCode.Code = -1;
                resultReturn.Content = null;
                return resultReturn;
            }
        }

        /// <summary>
        /// 修改用户昵称和邮箱
        /// </summary>
        /// <param name="userModel">用户对象</param>
        /// <returns>用户对象或返回码对象</returns>
        [HttpPost]
        [ModelValid]
        [Route("api/user/changenameoemail")]
        public Return ChangeNameOEmail(User userModel)
        {
            var resultReturn = new Return();

            try
            {

                if (userModel.Nickname == "" && userModel.Email == "")
                {
                    resultReturn.ReturnCode.Code = 1995;
                    return resultReturn;
                }

                resultReturn = UserService.ChangeN0E(userModel.Phone, userModel.Email, userModel.Nickname);

                //将User数据库模型类转换为页面模型类
                var user = ModelTransfer.Mapper(resultReturn.Content, new User());
                user.Password = "";

                resultReturn.Content = user;

                return resultReturn;
            }
            catch (Exception ex)
            {
                LogService.InsertLog(ex);
                resultReturn.ReturnCode.Code = -1;
                resultReturn.Content = null;
                return resultReturn;
            }
        }
    }
}