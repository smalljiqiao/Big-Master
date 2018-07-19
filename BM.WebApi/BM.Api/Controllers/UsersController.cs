﻿using BM.Api.Models;
using BM.Services.Common;
using BM.Services.Users;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BM.Api.Controllers
{
    public class UsersController : ApiController
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="userModel">用户对象</param>
        /// <returns>用户对象或返回码对象</returns>
        [Route("api/user/register")]
        public object Register(User userModel)
        {
            var returnCode = new ReturnCode();

            if (!ModelState.IsValid)
            {
                returnCode.Code = 1999;
                //获取所有不合法的字段说明
                returnCode.Remark = ModelState.Values.SelectMany(error => error.Errors).Aggregate("", (current, e) => current + e.ErrorMessage);
                return returnCode;
            }


            if (string.IsNullOrEmpty(userModel.Password))
            {
                returnCode.Code = 1997;
                return returnCode;
            }

            var userInfo = UserService.Register(userModel.Phone, userModel.Password, returnCode);

            //注册过程中出现错误
            if (returnCode.Code != default(int))
            {
                return returnCode;
            }

            //忽略敏感信息
            userInfo.SaltPassword = "";
            userInfo.Salt = "";

            var returnLis = new List<object>
            {
                returnCode,
                userInfo
            };

            return returnLis;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userModel">用户对象</param>
        /// <returns>用户对象或返回码对象</returns>
        [Route("api/user/login")]
        public object Login(User userModel)
        {
            var returnCode = new ReturnCode();

            if (!ModelState.IsValid)
            {
                returnCode.Code = 1999;
                //获取所有不合法的字段说明
                returnCode.Remark = ModelState.Values.SelectMany(error => error.Errors).Aggregate("", (current, e) => current + e.ErrorMessage);
                return returnCode;
            }

            if (string.IsNullOrEmpty(userModel.Password))
            {
                returnCode.Code = 1997;
                return returnCode;
            }

            var userInfo = UserService.GetUserByPhone(userModel.Phone, returnCode);

            //注册过程中出现错误
            if (returnCode.Code != default(int))
            {
                return returnCode;
            }

            //该手机号码还没有注册
            if (userInfo == null)
            {
                returnCode.Code = 1996;
                return returnCode;
            }

            //忽略敏感信息
            userInfo.SaltPassword = "";
            userInfo.Salt = "";

            var returnLis = new List<object>
            {
                returnCode,
                userInfo
            };

            return returnLis;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [Route("api/user/changepassword")]
        public object ChangePassword(User userModel)
        {
            var returnCode = new ReturnCode();

            if (!ModelState.IsValid)
            {
                returnCode.Code = 1999;
                //获取所有不合法的字段说明
                returnCode.Remark = ModelState.Values.SelectMany(error => error.Errors).Aggregate("", (current, e) => current + e.ErrorMessage);
                return returnCode;
            }

            if (string.IsNullOrEmpty(userModel.Password))
            {
                returnCode.Code = 1997;
                return returnCode;
            }

            var userInfo = UserService.ChangePassword(userModel.Phone, userModel.Password, returnCode);

            //修改密码过程中出现错误
            if (returnCode.Code != default(int))
            {
                return returnCode;
            }

            //忽略敏感信息
            userInfo.SaltPassword = "";
            userInfo.Salt = "";

            var returnLis = new List<object>
            {
                returnCode,
                userInfo
            };

            return returnLis;
        }

        /// <summary>
        /// 修改用户昵称和邮箱
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [Route("api/user/changenameoemail")]
        public object ChangeNameOEmail(User userModel)
        {
            var returnCode = new ReturnCode();

            if (!ModelState.IsValid)
            {
                returnCode.Code = 1999;
                //获取所有不合法的字段说明
                returnCode.Remark = ModelState.Values.SelectMany(error => error.Errors).Aggregate("", (current, e) => current + e.ErrorMessage);
                return returnCode;
            }

            if (userModel.Nickname == "" && userModel.Email == "")
            {
                returnCode.Code = 1995;
                return returnCode;
            }

            var userInfo = UserService.ChangeN0E(userModel.Phone, userModel.Email, userModel.Nickname, returnCode);

            //修改密码过程中出现错误
            if (returnCode.Code != default(int))
            {
                return returnCode;
            }

            //忽略敏感信息
            userInfo.SaltPassword = "";
            userInfo.Salt = "";

            var returnLis = new List<object>
            {
                returnCode,
                userInfo
            };

            return returnLis;
        }
    }
}