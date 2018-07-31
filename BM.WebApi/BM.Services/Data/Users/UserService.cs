﻿using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using BM.Data.Domain;
using BM.Services.Common;
using BM.Services.Data.Logs;
using BM.Services.Security;

namespace BM.Services.Data.Users
{
    public static class UserService
    {
        /// <summary>
        /// 初次更新用户表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="returnCode">返回码对象</param>
        /// <returns></returns>
        public static User Insert(string userId, ReturnCode returnCode)
        {
            //用户默认名称
            var defaultBame = CommonHelper.CreateCode();

            try
            {
                var db = new DbEntities();
                var userInfo = new User { UserId = Guid.Parse(userId), DefaultName = defaultBame };

                db.User.Add(userInfo);
                db.SaveChanges();

                return userInfo;
            }
            catch (Exception ex)
            {
                returnCode.Code = -1;
                LogService.InsertLog(ex);
                return null;
            }
        }

        /// <summary>
        /// 用户注册（根据UserId补充信息）
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="phone">手机号码</param>
        /// <param name="password">登录密码</param>
        /// <param name="returnCode">返回码对象</param>
        /// <returns>user对象，根据codeMessage是否为空判断系统是否出错</returns>
        public static User Register(string userId, string phone, string password, ReturnCode returnCode)
        {
            var db = new DbEntities();

            var userInfo = GetUserByUserId(userId, returnCode, db);
            if (returnCode.Code != default(int))
            {
                return null;
            }
            else
            {
                try
                {
                    var salt = EncryptionService.CreateSaltKey(6);
                    var saltPassword = EncryptionService.CreatePasswordHash(password, salt);

                    userInfo.Phone = phone;
                    userInfo.Password = password;
                    userInfo.Salt = salt;
                    userInfo.SaltPassword = saltPassword;
                    userInfo.RegisterTime = DateTime.Now;

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
        /// 修改用户ID
        /// </summary>
        /// <param name="userId">新用户ID</param>
        /// <param name="userIdReplaced">需要被替换的用户ID</param>
        /// <param name="returnCode">返回码对象</param>
        /// <returns></returns>
        public static User ChangeUesrId(string userId, string userIdReplaced, ReturnCode returnCode)
        {
            var db = new DbEntities();

            var userInfo = GetUserByUserId(userIdReplaced, returnCode, db);

            if (returnCode.Code != default(int))
            {
                return null;
            }
            else
            {
                try
                {
                    userInfo.UserId = Guid.Parse(userId);
                    db.Entry<User>(userInfo).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    returnCode.Code = -1;
                    LogService.InsertLog(ex);
                    return null;
                }
            }

            return userInfo;
        }

        /// <summary>
        /// 删除User
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="returnCode">返回码对象</param>
        /// <returns></returns>
        public static bool DeleteByUserId(string userId, ReturnCode returnCode)
        {
            var db = new DbEntities();

            var userInfo = GetUserByUserId(userId, returnCode);

            if (returnCode.Code != default(int))
            {
                return false;
            }

            if (userInfo != null)
            {
                try
                {
                    db.User.Remove(userInfo);
                }
                catch (Exception ex)
                {
                    //这里错误并不抛出，不影响后续操作
                    LogService.InsertLog(ex);
                }
            }

            return true;
        }

        /// <summary>
        /// 根据手机号码获取用户信息
        /// 因为Phone字段添加了唯一性约束，所以也能保证查询数据的唯一性。
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="returnCode">返回码对象</param>
        /// <param name="db">数据库上下文，因为使用不同数据库上下文操作会出错，所以增加这个参数</param>
        /// <returns>User对象，根据returnCode是否为空判断系统是否出错</returns>
        public static User GetUserByPhone(string phone, ReturnCode returnCode, DbEntities db = null)
        {
            User user;

            try
            {
                if (db == null)
                    db = new DbEntities();

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
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="returnCode"></param>
        /// <returns></returns>
        public static User GetUserByUserId(string userId, ReturnCode returnCode, DbEntities db = null)
        {
            User user;

            try
            {
                var userIdGuid = Guid.Parse(userId);

                if (db == null)
                    db = new DbEntities();

                var query = from d in db.User
                            where d.UserId == userIdGuid
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
