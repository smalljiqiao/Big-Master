using BM.Data.Domain;
using BM.Services.Common;
using System;
using System.Data.Entity;
using System.Linq;
using EncryptionService = BM.Services.Security.EncryptionService;

namespace BM.Services.Users
{
    public static class UserService
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="password">登录密码</param>
        /// <param name="returnCode">返回码对象</param>
        /// <returns>user对象，根据codeMessage是否为空判断系统是否出错</returns>
        public static User Register(string phone, string password, ReturnCode returnCode)
        {
            var userInfo = GetUserByPhone(phone, returnCode);
            if (returnCode.Code != default(int))
            {
                return null;
            }
            else
            {
                //该手机号码已注册
                if (userInfo != null)
                {
                    returnCode.Code = 1998;
                    return null;
                }

                var salt = EncryptionService.CreateSaltKey(6);
                var saltPassword = EncryptionService.CreatePasswordHash(password, salt);

                userInfo = new User { Phone = phone, Password = password, Salt = salt, SaltPassword = saltPassword };

                var db = new DbEntities();
                db.User.Add(userInfo);
                db.SaveChanges();
            }

            return userInfo;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="password">新密码</param>
        /// <param name="returnCode">返回码对象</param>
        /// <returns>user对象，根据codeMessage是否为空判断系统是否出错</returns>
        public static User ChangePassword(string phone, string password, ReturnCode returnCode)
        {
            var userInfo = GetUserByPhone(phone, returnCode);
            if (returnCode.Code != default(int))
            {
                return null;
            }
            else
            {
                if (userInfo == null)
                {
                    returnCode.Code = 1996;
                    return null;
                }

                var salt = EncryptionService.CreateSaltKey(6);
                var saltPassword = EncryptionService.CreatePasswordHash(password, salt);

                userInfo.Password = password;
                userInfo.SaltPassword = saltPassword;
                userInfo.Salt = salt;

                var db = new DbEntities();
                db.Entry<User>(userInfo).State = EntityState.Modified;
                db.SaveChanges();
            }

            return userInfo;
        }

        /// <summary>
        /// 修改用户昵称或电子邮箱
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="email">电子邮箱</param>
        /// <param name="nickname">用户昵称</param>
        /// <param name="returnCode">返回码对象</param>
        /// <returns>user对象，根据codeMessage是否为空判断系统是否出错</returns>
        public static User ChangeN0E(string phone, string email, string nickname, ReturnCode returnCode)
        {
            var userInfo = GetUserByPhone(phone, returnCode);
            if (returnCode.Code != default(int))
            {
                return null;
            }
            else
            {
                if (userInfo == null)
                {
                    returnCode.Code = 1996;
                    return null;
                }

                userInfo.Email = email;
                userInfo.Nickname = nickname;
                var db = new DbEntities();
                db.Entry<User>(userInfo).State = EntityState.Modified;
                db.SaveChanges();
            }

            return userInfo;
        }

        /// <summary>
        /// 根据手机号码获取用户信息
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="returnCode">返回码对象</param>
        /// <returns>User对象，根据returnCode是否为空判断系统是否出错</returns>
        public static User GetUserByPhone(string phone, ReturnCode returnCode)
        {
            User user;

            try
            {
                var db = new DbEntities();

                var query = from d in db.User
                            where d.Phone == phone
                            select d;

                user = query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                returnCode.Code = 99999;
                return null;
            }

            return user;
        }
    }
}
