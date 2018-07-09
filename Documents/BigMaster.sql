IF EXISTS (SELECT * FROM sys.databases WHERE name = 'BigMaster')
   BEGIN
     RAISERROR('BigMaster数据库已存在，请检查后再确认是手动删除该数据库还是进行修改操作。',16,1);
     RETURN
   END

ELSE

--创建BigMaster数据库
CREATE DATABASE BigMaster

ON PRIMARY  --配置主数据文件
(
  NAME = 'BigMaster',  --主数据文件逻辑名称
  FILENAME = 'D:\DataBase\SQL2017\BigMaster\BigMaster.mdf',  --主数据文件保存路径
  SIZE = 10MB,  --主文件初始大小
  FILEGROWTH = 20%  --文件以10%扩容
)

LOG ON  --配置日志文件
(
  NAME = 'BigMaster_log',  --日志文件逻辑名称
  FILENAME = 'D:\DataBase\SQL2017\BigMaster\BigMaster_log.ldf',  --日志文件保存路径
  SIZE = 5MB,  --日志文件初始大小
  FILEGROWTH = 5MB  --增量
)
GO


USE BigMaster;
GO

/*
 * 用户表
 * 微信授权登录也会在这个表插入数据（此时只写入用户唯一标识信息），当已授权用户绑定手机号再重新写入其它信息。
*/
CREATE TABLE Users
(
  ID uniqueidentifier PRIMARY KEY,  --用户唯一标识、主键
  Phone varchar(50) NULL,  --手机号码
  Password varchar(50) NULL,  --登录密码
  Salt char(6) NULL,  --盐值
  SaltPassword char(40) NULL, --加密后密码
  RegisterTime datetime NULL,  --首次注册时间
  CreateTime datetime not NULL DEFAULT GETDATE(),  --首次授权或注册时间，插入数据库当时的时间
)
GO

--添加说明
DECLARE @CurrentUser sysname;
SELECT @CurrentUser = USER_NAME();
EXECUTE sp_addextendedproperty 'MS_Description', '用户表<br/>微信授权登录也会在这个表插入数据（此时只写入用户唯一标识信息），当已授权用户绑定手机号再重新写入其它信息。', 'user',@CurrentUser, 'table', 'Users'
EXECUTE sp_addextendedproperty 'MS_Description', '用户唯一标识', 'user', @CurrentUser, 'table', 'Users', 'column', 'ID'
EXECUTE sp_addextendedproperty 'MS_Description', '手机号码', 'user', @CurrentUser, 'table', 'Users', 'column', 'Phone'
EXECUTE sp_addextendedproperty 'MS_Description', '登录密码', 'user', @CurrentUser, 'table', 'Users', 'column', 'Password'
EXECUTE sp_addextendedproperty 'MS_Description', '盐值', 'user', @CurrentUser, 'table', 'Users', 'column', 'Salt'
EXECUTE sp_addextendedproperty 'MS_Description', '加密后密码', 'user', @CurrentUser, 'table', 'Users', 'column', 'SaltPassword'
EXECUTE sp_addextendedproperty 'MS_Description', '首次注册时间', 'user', @CurrentUser, 'table', 'Users', 'column', 'RegisterTime'
EXECUTE sp_addextendedproperty 'MS_Description', '首次授权或注册时间，插入数据库当时的时间', 'user', @CurrentUser, 'table', 'Users', 'column', 'CreateTime'
GO

/*
 * 用户微信信息表
 * 在用户同意微信授权登录时获取
*/
CREATE TABLE WXUserInfo
(
  ID uniqueidentifier PRIMARY KEY,  --用户唯一标识、主键
  Openid varchar(50) NULL,  --用户Openid
  NickName nvarchar(50) NULL,  --用户昵称
  Sex tinyint NULL,  --用户性别，值为1时是男性，为2时是女性，为0时是未知
  City nvarchar(50) NULL,  --用户所在城市
  Country nvarchar(50) NULL,  --用户所在国家
  Province nvarchar(50) NULL,  --用户所在省份
  Language nvarchar(50) NULL,  --用户的语言
  HeadImgUrl nvarchar(500) NULL,  --用户头像图片链接
)
GO

--添加外键约束
ALTER TABLE WXUserInfo ADD CONSTRAINT FK_WXUserInfo_Users FOREIGN KEY(ID)
REFERENCES dbo.Users
GO

--添加说明
DECLARE @CurrentUser sysname;
SELECT @CurrentUser = USER_NAME();
EXECUTE sp_addextendedproperty 'MS_Description', '用户微信信息表<br/>在用户同意微信授权登录时获取。', 'user',@CurrentUser, 'table', 'WXUserInfo'
EXECUTE sp_addextendedproperty 'MS_Description', '用户唯一标识', 'user', @CurrentUser, 'table', 'WXUserInfo', 'column', 'ID'
EXECUTE sp_addextendedproperty 'MS_Description', '用户Openid', 'user', @CurrentUser, 'table', 'WXUserInfo', 'column', 'Openid'
EXECUTE sp_addextendedproperty 'MS_Description', '用户昵称', 'user', @CurrentUser, 'table', 'WXUserInfo', 'column', 'NickName'
EXECUTE sp_addextendedproperty 'MS_Description', '用户性别，值为1时是男性，为2时是女性，为0时是未知', 'user', @CurrentUser, 'table', 'WXUserInfo', 'column', 'Sex'
EXECUTE sp_addextendedproperty 'MS_Description', '用户所在城市', 'user', @CurrentUser, 'table', 'WXUserInfo', 'column', 'City'
EXECUTE sp_addextendedproperty 'MS_Description', '用户所在国家', 'user', @CurrentUser, 'table', 'WXUserInfo', 'column', 'Country'
EXECUTE sp_addextendedproperty 'MS_Description', '用户所在省份', 'user', @CurrentUser, 'table', 'WXUserInfo', 'column', 'Province'
EXECUTE sp_addextendedproperty 'MS_Description', '用户的语言', 'user', @CurrentUser, 'table', 'WXUserInfo', 'column', 'Language'
EXECUTE sp_addextendedproperty 'MS_Description', '用户头像图片链接', 'user', @CurrentUser, 'table', 'WXUserInfo', 'column', 'HeadImgUrl'
GO

/*
 * 用户安卓信息表
 * 只在首次微信授权登录或绑定手机号时获取
 *
 * 考虑用户可能在不同机器上登录，于是将用户唯一标识和安卓ID同时设置为主键。
 * 当检测到AndroidID更换时，再次插入数据
*/
CREATE TABLE AndroidUserInfo
(
  ID uniqueidentifier not NULL,  --用户唯一标识、主键
  AndroidID varchar(50) not NULL,  --安卓ID
  CreateTime datetime not NULL DEFAULT GETDATE(),  --获取时间，插入数据库当时的时间

  PRIMARY KEY(ID,AndroidID)  --将用户标识和安卓ID同时设置为主键
)
GO

--添加外键约束
ALTER TABLE AndroidUserInfo ADD CONSTRAINT FK_AndroidUserInfo_Users FOREIGN KEY(ID)
REFERENCES dbo.Users
GO

--添加说明
DECLARE @CurrentUser sysname;
SELECT @CurrentUser = USER_NAME();
EXECUTE sp_addextendedproperty 'MS_Description', '用户安卓信息表<br/>只在首次微信授权登录或绑定手机号时获取。<br/><br/>考虑用户可能在不同机器上登录，于是将用户唯一标识和安卓ID同时设置为主键。<br/>当检测到AndroidID更换时，再次插入数据', 'user',@CurrentUser, 'table', 'AndroidUserInfo'
EXECUTE sp_addextendedproperty 'MS_Description', '用户唯一标识', 'user', @CurrentUser, 'table', 'AndroidUserInfo', 'column', 'ID'
EXECUTE sp_addextendedproperty 'MS_Description', '用户Openid', 'user', @CurrentUser, 'table', 'AndroidUserInfo', 'column', 'AndroidID'
EXECUTE sp_addextendedproperty 'MS_Description', '用户昵称', 'user', @CurrentUser, 'table', 'AndroidUserInfo', 'column', 'CreateTime'
GO

USE master; --退出占用