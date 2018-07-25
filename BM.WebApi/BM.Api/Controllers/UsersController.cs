using BM.Api.Attributes;
using BM.Api.Models;
using BM.Services.Common;
using BM.Services.ReturnServices;
using BM.Services.Users;
using System.Web.Http;

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
        /// <param name="phone">手机号码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/user/vcode")]
        public string VCode(string phone)
        {
            //需要添加Session
            return "";
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
                return returnCode;
            }

            if (string.IsNullOrEmpty(userModel.VCode))
            {
                returnCode.Code = 1994;
                return returnCode;
            }

            //TODO Check VCode is valid

            var userInfo = UserService.Register(userModel.Phone, userModel.Password, returnCode);

            //注册过程中出现错误
            if (returnCode.Code != default(int))
            {
                return returnCode;
            }

            return new Return
            {
                ReturnCode = returnCode,
                Content = userInfo
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

            return new Return
            {
                ReturnCode = returnCode,
                Content = userInfo
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
                return returnCode;
            }

            var userInfo = UserService.ChangePassword(userModel.Phone, userModel.Password, returnCode);

            //修改密码过程中出现错误
            if (returnCode.Code != default(int))
            {
                return returnCode;
            }

            return new Return
            {
                ReturnCode = returnCode,
                Content = userInfo
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
                return returnCode;
            }

            var userInfo = UserService.ChangeN0E(userModel.Phone, userModel.Email, userModel.Nickname, returnCode);

            //修改密码过程中出现错误
            if (returnCode.Code != default(int))
            {
                return returnCode;
            }

            return new Return
            {
                ReturnCode = returnCode,
                Content = userInfo
            };
        }
    }
}