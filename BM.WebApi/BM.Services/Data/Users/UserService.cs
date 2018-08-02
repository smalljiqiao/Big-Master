using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using BM.Data.Domain;
using BM.Services.Common;
using BM.Services.Data.Logs;
using BM.Services.Infrastructure;
using BM.Services.ReturnServices;
using BM.Services.Security;

namespace BM.Services.Data.Users
{
    /// <summary>
    /// 用户服务类
    /// </summary>
    public class UserService : BaseDataService
    {
        /// <summary>
        /// 初次更新用户表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="returnCode">返回码对象</param>
        /// <returns></returns>
        public User Insert(string userId)
        {
            //用户默认名称
            var defaultBame = CommonHelper.CreateCode();

            var userInfo = new User { UserId = Guid.Parse(userId), DefaultName = defaultBame };

            db.User.Add(userInfo);
            db.SaveChanges();

            return userInfo;
        }

        /// <summary>
        /// 用户注册（根据UserId补充信息）
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="phone">手机号码</param>
        /// <param name="password">登录密码</param>
        /// <param name="returnCode">返回码对象</param>
        /// <returns>user对象，根据codeMessage是否为空判断系统是否出错</returns>
        public Return Register(string userId, string phone, string password)
        {
            var resultReturn = new Return();

            var userInfo = GetUserByUserId(userId);

            var salt = EncryptionService.CreateSaltKey(6);
            var saltPassword = EncryptionService.CreatePasswordHash(password, salt);

            userInfo.Phone = phone;
            userInfo.Password = password;
            userInfo.Salt = salt;
            userInfo.SaltPassword = saltPassword;
            userInfo.RegisterTime = DateTime.Now;

            db.Entry<User>(userInfo).State = EntityState.Modified;
            db.SaveChanges();

            resultReturn.Content = userInfo;
            return resultReturn;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="password">新密码</param>
        /// <param name="returnCode">返回码对象</param>
        /// <returns>user对象，根据codeMessage是否为空判断系统是否出错</returns>
        public Return ChangePassword(string phone, string password)
        {
            var resultReturn = new Return();

            var userInfo = GetUserByPhone(phone);

            //该手机号码还没注册
            if (userInfo == null)
            {
                resultReturn.ReturnCode.Code = 1996;
                return resultReturn;
            }

            var salt = EncryptionService.CreateSaltKey(6);
            var saltPassword = EncryptionService.CreatePasswordHash(password, salt);

            userInfo.Password = password;
            userInfo.SaltPassword = saltPassword;
            userInfo.Salt = salt;

            db.Entry<User>(userInfo).State = EntityState.Modified;
            db.SaveChanges();

            resultReturn.Content = userInfo;

            return resultReturn;
        }

        /// <summary>
        /// 修改用户昵称或电子邮箱
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="email">电子邮箱</param>
        /// <param name="nickname">用户昵称</param>
        /// <param name="returnCode">返回码对象</param>
        /// <returns>user对象，根据codeMessage是否为空判断系统是否出错</returns>
        public Return ChangeN0E(string phone, string email, string nickname)
        {
            var resultReturn = new Return();

            var userInfo = GetUserByPhone(phone);

            //该手机号码还没注册
            if (userInfo == null)
            {
                resultReturn.ReturnCode.Code = 1996;
                return resultReturn;
            }

            userInfo.Email = email;
            userInfo.NickName = nickname;
            db.Entry<User>(userInfo).State = EntityState.Modified;
            db.SaveChanges();

            resultReturn.Content = userInfo;

            return resultReturn;
        }

        /// <summary>
        /// 修改用户ID
        /// </summary>
        /// <param name="userId">新用户ID</param>
        /// <param name="userIdReplaced">需要被替换的用户ID</param>
        /// <param name="returnCode">返回码对象</param>
        /// <returns></returns>
        public Return ChangeUesrId(string userId, string userIdReplaced)
        {
            var resultReturn = new Return();

            var userInfo = GetUserByUserId(userIdReplaced);


            userInfo.UserId = Guid.Parse(userId);
            db.Entry<User>(userInfo).State = EntityState.Modified;
            db.SaveChanges();

            resultReturn.Content = userInfo;

            return resultReturn;
        }

        /// <summary>
        /// 删除User
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public bool DeleteByUserId(string userId)
        {
            var userInfo = GetUserByUserId(userId);

            if (userInfo != null)
            {
                db.User.Remove(userInfo);
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
        public User GetUserByPhone(string phone)
        {
            var query = from d in db.User
                        where d.Phone == phone
                        select d;

            var user = query.FirstOrDefault();

            return user;
        }

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="returnCode"></param>
        /// <returns></returns>
        public User GetUserByUserId(string userId)
        {
            var userIdGuid = Guid.Parse(userId);

            var query = from d in db.User
                        where d.UserId == userIdGuid
                        select d;

            var user = query.FirstOrDefault();

            return user;
        }


    }
}
