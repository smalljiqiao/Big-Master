using BM.Data.Domain;
using BM.Services.Common;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using BM.Services.Logs;
using BM.Services.Security;

namespace BM.Services.Users
{
    public static class UserService
    {
        /// <summary>
        /// 插入或更新Android信息，更新时间
        /// </summary>
        /// <param name="androidId">安卓ID</param>
        /// <returns>安卓信息对象或null</returns>
        public static Android AndroidIdInsertOrUpdate(string androidId)
        {
            var androidInfo = new Android
            {
                AndroidId = androidId
            };

            try
            {
                var db = new DbEntities();
                db.Android.AddOrUpdate(androidInfo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogService.InsertLog(ex);
                return null;
            }

            return androidInfo;
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="password">登录密码</param>
        /// <param name="returnCode">返回码对象</param>
        /// <returns>user对象，根据codeMessage是否为空判断系统是否出错</returns>
        public static User Register(string phone, string password, ReturnCode returnCode)
        {
            var db = new DbEntities();

            var userInfo = GetUserByPhone(phone, returnCode, db);
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

                try
                {
                    var salt = EncryptionService.CreateSaltKey(6);
                    var saltPassword = EncryptionService.CreatePasswordHash(password, salt);

                    userInfo = new User { Phone = phone, Password = password, Salt = salt, SaltPassword = saltPassword };

                    db.User.Add(userInfo);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    LogService.InsertLog(ex);
                    returnCode.Code = -1;
                    return null;
                }
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
            var db = new DbEntities();

            var userInfo = GetUserByPhone(phone, returnCode, db);
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

                try
                {
                    var salt = EncryptionService.CreateSaltKey(6);
                    var saltPassword = EncryptionService.CreatePasswordHash(password, salt);

                    userInfo.Password = password;
                    userInfo.SaltPassword = saltPassword;
                    userInfo.Salt = salt;

                    db.Entry<User>(userInfo).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    LogService.InsertLog(ex);
                    returnCode.Code = -1;
                    return null;
                }
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
            var db = new DbEntities();

            var userInfo = GetUserByPhone(phone, returnCode, db);
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

                try
                {
                    userInfo.Email = email;
                    userInfo.NickName = nickname;
                    db.Entry<User>(userInfo).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    LogService.InsertLog(ex);
                    returnCode.Code = -1;
                    return null;
                }
            }

            return userInfo;
        }

        /// <summary>
        /// 根据手机号码获取用户信息
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="returnCode">返回码对象</param>
        /// <param name="db">数据库上下文，因为使用不同数据库上下文操作会出错，所以增加这个参数</param>
        /// <returns>User对象，根据returnCode是否为空判断系统是否出错</returns>
        public static User GetUserByPhone(string phone, ReturnCode returnCode, DbEntities db)
        {
            User user;

            try
            {
                var query = from d in db.User
                            where d.Phone == phone
                            select d;

                user = query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                returnCode.Code = -1;
                LogService.InsertLog(ex);
                return null;
            }

            return user;
        }

        /// <summary>
        /// 获取短信验证码
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="returnCode">返回码对象</param>
        /// <returns></returns>
        public static Sms GetSmsByPhone(string phone, ReturnCode returnCode)
        {
            try
            {
                var db = new DbEntities();

                var query = from d in db.Sms
                            where d.Phone == phone
                            select d;

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                returnCode.Code = -1;
                LogService.InsertLog(ex);
                return null;
            }
        }
    }
}
