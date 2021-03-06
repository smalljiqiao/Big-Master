/*****
 ***** 创建数据库脚本
 *****
 ***** 注意：
 *****      1.不要随意使用GO语句.
 *****      2.需存在目录D:\DataBase\SQL2017\BigMaster.
 *****      3.若字段确定不存在中文，声明为varchar；若字段确定长度，声明为char；其它除开特殊字段全部声明为nvarchar
 *****
 ***** Author：MatueXL
 ***** UpdateTime：2018/07/09
******/

SET STATISTICS TIME OFF

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
DECLARE @CurrentUser sysname;  --我也不懂这俩是啥，用在编辑表说明和字段说明中。
SELECT @CurrentUser = USER_NAME();

/*
 * 用户表
 * 微信授权登录也会在这个表插入数据（此时只写入用户唯一标识信息），当已授权用户绑定手机号再重新写入其它信息。
*/
CREATE TABLE Users
(
  ID uniqueidentifier PRIMARY KEY,  --用户唯一标识、主键
  Phone varchar(50) NULL,  --手机号码
  NickName nvarchar(50) NULL,  --昵称，在个人中心处编辑
  Email varchar(50) NULL,  --邮箱，在个人中心处编辑
  Password varchar(50) NULL,  --登录密码
  Salt char(6) NULL,  --盐值
  SaltPassword char(40) NULL, --加密后密码
  RegisterTime datetime NULL,  --首次注册时间
  CreateTime datetime NOT NULL DEFAULT GETDATE(),  --首次授权或注册时间，插入数据库当时的时间
)


--添加说明
EXECUTE sp_addextendedproperty 'MS_Description', '用户表<br/>微信授权登录也会在这个表插入数据（此时只写入用户唯一标识信息），当已授权用户绑定手机号再重新写入其它信息。', 'user',@CurrentUser, 'table', 'Users'
EXECUTE sp_addextendedproperty 'MS_Description', '用户唯一标识', 'user', @CurrentUser, 'table', 'Users', 'column', 'ID'
EXECUTE sp_addextendedproperty 'MS_Description', '手机号码', 'user', @CurrentUser, 'table', 'Users', 'column', 'Phone'
EXECUTE sp_addextendedproperty 'MS_Description', '昵称，在个人中心处编辑', 'user', @CurrentUser, 'table', 'Users', 'column', 'NickName'
EXECUTE sp_addextendedproperty 'MS_Description', '邮箱，在个人中心处编辑', 'user', @CurrentUser, 'table', 'Users', 'column', 'Email'
EXECUTE sp_addextendedproperty 'MS_Description', '登录密码', 'user', @CurrentUser, 'table', 'Users', 'column', 'Password'
EXECUTE sp_addextendedproperty 'MS_Description', '盐值', 'user', @CurrentUser, 'table', 'Users', 'column', 'Salt'
EXECUTE sp_addextendedproperty 'MS_Description', '加密后密码', 'user', @CurrentUser, 'table', 'Users', 'column', 'SaltPassword'
EXECUTE sp_addextendedproperty 'MS_Description', '首次注册时间', 'user', @CurrentUser, 'table', 'Users', 'column', 'RegisterTime'
EXECUTE sp_addextendedproperty 'MS_Description', '首次授权或注册时间，插入数据库当时的时间', 'user', @CurrentUser, 'table', 'Users', 'column', 'CreateTime'


/*
 * 用户微信信息表
 * 在用户同意微信授权登录时获取
*/
CREATE TABLE WXUserInfo
(
  ID uniqueidentifier PRIMARY KEY,  --用户唯一标识、主键
  Openid varchar(50) NULL,  --用户Openid
  NickName nvarchar(50) NULL,  --用户昵称
  Sex tinyint NULL CHECK(Sex = 0 OR Sex = 1 OR Sex = 2),  --用户性别，值为1时是男性，为2时是女性，为0时是未知
  City nvarchar(50) NULL,  --用户所在城市
  Country nvarchar(50) NULL,  --用户所在国家
  Province nvarchar(50) NULL,  --用户所在省份
  Language nvarchar(50) NULL,  --用户的语言
  HeadImgUrl nvarchar(500) NULL,  --用户头像图片链接
  CreateTime datetime NOT NULL DEFAULT GETDATE(),  --用户首次授权时间，插入数据库当时时间
)


--添加外键约束
ALTER TABLE WXUserInfo ADD CONSTRAINT FK_WXUserInfo_Users FOREIGN KEY(ID)
REFERENCES dbo.Users


--添加说明
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


/*
 * 用户安卓信息表
 * 只在首次微信授权登录或绑定手机号时获取
 *
 * 考虑用户可能在不同机器上登录，于是将用户唯一标识和安卓ID同时设置为主键。
 * 当检测到AndroidID更换时，再次插入数据
*/
CREATE TABLE AndroidUserInfo
(
  ID uniqueidentifier,  --用户唯一标识、主键
  AndroidID varchar(50),  --安卓ID、主键
  CreateTime datetime NOT NULL DEFAULT GETDATE(),  --获取时间，插入数据库当时的时间

  PRIMARY KEY(ID,AndroidID)  --将用户标识和安卓ID同时设置为主键
)

--添加外键约束
ALTER TABLE AndroidUserInfo ADD CONSTRAINT FK_AndroidUserInfo_Users FOREIGN KEY(ID)
REFERENCES dbo.Users

--添加说明
EXECUTE sp_addextendedproperty 'MS_Description', '用户安卓信息表<br/>只在首次微信授权登录或绑定手机号时获取。<br/><br/>考虑用户可能在不同机器上登录，于是将用户唯一标识和安卓ID同时设置为主键。<br/>当检测到AndroidID更换时，再次插入数据', 'user',@CurrentUser, 'table', 'AndroidUserInfo'
EXECUTE sp_addextendedproperty 'MS_Description', '用户唯一标识', 'user', @CurrentUser, 'table', 'AndroidUserInfo', 'column', 'ID'
EXECUTE sp_addextendedproperty 'MS_Description', '用户Openid', 'user', @CurrentUser, 'table', 'AndroidUserInfo', 'column', 'AndroidID'
EXECUTE sp_addextendedproperty 'MS_Description', '用户昵称', 'user', @CurrentUser, 'table', 'AndroidUserInfo', 'column', 'CreateTime'


/*
 * 订单表
 * 记录用户订单信息
*/
CREATE TABLE Orders
(
  ID uniqueidentifier,  --用户唯一标识、主键
  OrderID char(16),  --订单唯一标识、主键:随机生成16位字符
  OrderType varchar(20) NOT NULL,  --订单业务类型 *** 之后需补充约束  ***
  Price decimal(10,2) NOT NULL,  --订单价格,精确两位小数
  PayState varchar(10) NOT NULL,  --支付状态  *** 之后需补充约束 ***
  CreateTime datetime NOT NULL DEFAULT GETDATE(),  --订单生成时间，插入数据库当时的时间

  PRIMARY KEY(ID,OrderID)  --将用户标识和安卓ID同时设置为主键
)

--添加外键约束
ALTER TABLE Orders ADD CONSTRAINT FK_Orders_Users FOREIGN KEY(ID)
REFERENCES dbo.Users



--添加说明
EXECUTE sp_addextendedproperty 'MS_Description', '订单表<br/>记录用户订单信息<br/>将用户ID和订单ID同时设置为主键','user',@CurrentUser, 'table', 'Orders';
EXECUTE sp_addextendedproperty 'MS_Description', '用户唯一标识', 'user', @CurrentUser, 'table', 'Orders', 'column', 'ID';
EXECUTE sp_addextendedproperty 'MS_Description', '订单唯一标识', 'user', @CurrentUser, 'table', 'Orders', 'column', 'OrderID';
EXECUTE sp_addextendedproperty 'MS_Description', '订单业务类型*** 之后需补充约束  ***', 'user', @CurrentUser, 'table', 'Orders', 'column', 'OrderType';
EXECUTE sp_addextendedproperty 'MS_Description', '订单价格,精确两位小数', 'user', @CurrentUser, 'table', 'Orders', 'column', 'Price';
EXECUTE sp_addextendedproperty 'MS_Description', '支付状态  *** 之后需补充约束 ***  ***', 'user', @CurrentUser, 'table', 'Orders', 'column', 'PayState';
EXECUTE sp_addextendedproperty 'MS_Description', '订单生成时间，插入数据库当时的时间', 'user', @CurrentUser, 'table', 'Orders', 'column', 'CreateTime';

/*
 * 订单查询表
 * 记录订单所查询的关键字信息
*/
CREATE TABLE OrderSearch
(
  OrderID char(16) PRIMARY KEY,  --订单唯一标识、主键
  UserName nvarchar(20) NULL,  --查询姓名 对应八字详批和宝宝取名
  Sex tinyint NULL CHECK(Sex = 0 OR Sex = 1 OR Sex = 2),  --性别 值为1时是男性，为2时是女性，为0时是未知 对应八字详批和宝宝取名
  BirthDay datetime NULL,  --出生日期 对应八字详批和宝宝取名
  ManName nvarchar(20) NULL,  --八字合婚男方姓名
  ManBirthDay datetime NULL,  --八字合婚男方出生日期
  WomanName nvarchar(20) NULL,  --八字合婚女方姓名
  WomanBirthDay datetime NULL,  --八字合婚女方出生日期
)

--ALTER TABLE Orders ADD CONSTRAINT FK_Orders_OrderSearch FOREIGN KEY(OrderID)
--REFERENCES dbo.OrderSearch(OrderID)
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_OrderSearch] FOREIGN KEY([OrderID])
REFERENCES [dbo].[OrderSearch] ([OrderID])


--添加说明
EXECUTE sp_addextendedproperty 'MS_Description', 'OrderSearch<br/>记录订单所查询的关键字信息','user',@CurrentUser, 'table', 'OrderSearch';
EXECUTE sp_addextendedproperty 'MS_Description', '订单唯一标识', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'OrderID';
EXECUTE sp_addextendedproperty 'MS_Description', '查询姓名 对应八字详批和宝宝取名', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'UserName';
EXECUTE sp_addextendedproperty 'MS_Description', '性别 值为1时是男性，为2时是女性，为0时是未知 对应八字详批和宝宝取名', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'Sex';
EXECUTE sp_addextendedproperty 'MS_Description', '出生日期 对应八字详批和宝宝取名', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'BirthDay';
EXECUTE sp_addextendedproperty 'MS_Description', '八字合婚男方姓名', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'ManName';
EXECUTE sp_addextendedproperty 'MS_Description', '八字合婚男方出生日期', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'ManBirthDay';
EXECUTE sp_addextendedproperty 'MS_Description', '八字合婚女方姓名', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'WomanName';
EXECUTE sp_addextendedproperty 'MS_Description', '八字合婚女方出生日期', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'WomanBirthDay';

/*
 * 搜索记录表
 * 记录用户所有搜索信息
*/
CREATE TABLE Searchs
(
  ID uniqueidentifier PRIMARY KEY,  --用户唯一标识、主键
  UserName nvarchar(20) NULL,  --查询姓名 对应八字详批和宝宝取名
  Sex tinyint NULL CHECK(Sex = 0 OR Sex = 1 OR Sex = 2),  --性别 值为1时是男性，为2时是女性，为0时是未知 对应八字详批和宝宝取名
  BirthDay datetime NULL,  --出生日期 对应八字详批和宝宝取名
  ManName nvarchar(20) NULL,  --八字合婚男方姓名
  ManBirthDay datetime NULL,  --八字合婚男方出生日期
  WomanName nvarchar(20) NULL,  --八字合婚女方姓名
  WomanBirthDay datetime NULL,  --八字合婚女方出生日期
  ZhouWord nvarchar(50) NULL,  --周公解梦搜索词
)

--添加外键约束
ALTER TABLE Searchs ADD CONSTRAINT FK_Searchs_Users FOREIGN KEY(ID)
REFERENCES dbo.Users

--添加说明
EXECUTE sp_addextendedproperty 'MS_Description', '搜索记录表<br/>记录用户所有搜索信息','user',@CurrentUser, 'table', 'Searchs';
EXECUTE sp_addextendedproperty 'MS_Description', '用户唯一标识', 'user', @CurrentUser, 'table', 'Searchs', 'column', 'ID';
EXECUTE sp_addextendedproperty 'MS_Description', '查询姓名 对应八字详批和宝宝取名', 'user', @CurrentUser, 'table', 'Searchs', 'column', 'UserName';
EXECUTE sp_addextendedproperty 'MS_Description', '性别 值为1时是男性，为2时是女性，为0时是未知 对应八字详批和宝宝取名', 'user', @CurrentUser, 'table', 'Searchs', 'column', 'Sex';
EXECUTE sp_addextendedproperty 'MS_Description', '出生日期 对应八字详批和宝宝取名', 'user', @CurrentUser, 'table', 'Searchs', 'column', 'BirthDay';
EXECUTE sp_addextendedproperty 'MS_Description', '八字合婚男方姓名', 'user', @CurrentUser, 'table', 'Searchs', 'column', 'ManName';
EXECUTE sp_addextendedproperty 'MS_Description', '八字合婚男方出生日期', 'user', @CurrentUser, 'table', 'Searchs', 'column', 'ManBirthDay';
EXECUTE sp_addextendedproperty 'MS_Description', '八字合婚女方姓名', 'user', @CurrentUser, 'table', 'Searchs', 'column', 'WomanName';
EXECUTE sp_addextendedproperty 'MS_Description', '八字合婚女方出生日期', 'user', @CurrentUser, 'table', 'Searchs', 'column', 'WomanBirthDay';
EXECUTE sp_addextendedproperty 'MS_Description', '周公解梦搜索词', 'user', @CurrentUser, 'table', 'Searchs', 'column', 'ZhouWord';

/*
 * 埋点表
 * 记录用户操作记录
*/
CREATE TABLE BurialPoint
(
  UUID uniqueidentifier PRIMARY KEY,  --唯一标识、主键
  AndroidID varchar(50) NULL,  --安卓ID 
  ID uniqueidentifier NULL,  --用户ID
  FType nvarchar(20) NULL,  --埋点一级类型
  SType nvarchar(20) NULL,  --埋点二级类型
  Bak nvarchar(100) NULL,  --埋点特殊说明 当二类类型不能满足埋点操作时，将额外信息写入此列
  CreateTime datetime NOT NULL DEFAULT GETDATE(),  --生成时间，插入数据库当时的时间
)

--添加说明
EXECUTE sp_addextendedproperty 'MS_Description', '埋点表<br/>记录用户操作记录','user',@CurrentUser, 'table', 'BurialPoint';
EXECUTE sp_addextendedproperty 'MS_Description', '唯一标识', 'user', @CurrentUser, 'table', 'BurialPoint', 'column', 'UUID';
EXECUTE sp_addextendedproperty 'MS_Description', '安卓ID', 'user', @CurrentUser, 'table', 'BurialPoint', 'column', 'AndroidID';
EXECUTE sp_addextendedproperty 'MS_Description', '用户ID', 'user', @CurrentUser, 'table', 'BurialPoint', 'column', 'ID';
EXECUTE sp_addextendedproperty 'MS_Description', '埋点一级类型', 'user', @CurrentUser, 'table', 'BurialPoint', 'column', 'FType';
EXECUTE sp_addextendedproperty 'MS_Description', '埋点二级类型', 'user', @CurrentUser, 'table', 'BurialPoint', 'column', 'SType';
EXECUTE sp_addextendedproperty 'MS_Description', '埋点特殊说明，当二类类型不能满足埋点操作时，将额外信息写入此列', 'user', @CurrentUser, 'table', 'BurialPoint', 'column', 'Bak';
EXECUTE sp_addextendedproperty 'MS_Description', '生成时间，插入数据库当时的时间', 'user', @CurrentUser, 'table', 'BurialPoint', 'column', 'CreateTime';

/*
 * 错误日志表
 * 记录程序的错误信息
*/
CREATE TABLE Logs
(
 UUID uniqueidentifier PRIMARY KEY,  --唯一标识、主键
 msg nvarchar(1000) NULL,  --错误信息
 StackTrace nvarchar(MAX) NULL,  --堆栈轨迹
 CreateTime datetime NOT NULL DEFAULT GETDATE(),  --生成时间，插入数据库当时的时间
)

--添加说明
EXECUTE sp_addextendedproperty 'MS_Description', '错误日志表<br/>记录程序的错误信息','user',@CurrentUser, 'table', 'Logs';
EXECUTE sp_addextendedproperty 'MS_Description', '唯一标识', 'user', @CurrentUser, 'table', 'Logs', 'column', 'UUID';
EXECUTE sp_addextendedproperty 'MS_Description', '错误信息', 'user', @CurrentUser, 'table', 'Logs', 'column', 'msg';
EXECUTE sp_addextendedproperty 'MS_Description', '堆栈轨迹', 'user', @CurrentUser, 'table', 'Logs', 'column', 'StackTrace';
EXECUTE sp_addextendedproperty 'MS_Description', '生成时间，插入数据库当时的时间', 'user', @CurrentUser, 'table', 'Logs', 'column', 'CreateTime';

USE master; --退出占用



--/*
-- * 登录日志表
-- * 记录用户登录操作
--*/
--CREATE TABLE Logins
--(
--  ID uniqueidentifier PRIMARY KEY,  --用户唯一标识、主键
--  LoginType tinyint NOT NULL CHECK(LoginType = 0 OR LoginType = 1),  --登录类型  0代表账号密码登录、1代表微信授权登录（方便以后扩展所以声明为tinyint）
--  CreateTime datetime NOT NULL  --登录时间，插入数据库当时的时间
--)

----添加外键约束
--ALTER TABLE Logins ADD CONSTRAINT FK_Logins_Users FOREIGN KEY(ID)
--REFERENCES dbo.Users

----添加说明
--EXECUTE sp_addextendedproperty 'MS_Description', '登录日志表<br/>记录用户登录操作','user',@CurrentUser, 'table', 'Logins';
--EXECUTE sp_addextendedproperty 'MS_Description', '用户唯一标识', 'user', @CurrentUser, 'table', 'Logins', 'column', 'ID';
--EXECUTE sp_addextendedproperty 'MS_Description', '登录类型  0代表账号密码登录、1代表微信授权登录（方便以后扩展所以声明为tinyint）', 'user', @CurrentUser, 'table', 'Logins', 'column', 'LoginType';
--EXECUTE sp_addextendedproperty 'MS_Description', '登录时间，插入数据库当时的时间', 'user', @CurrentUser, 'table', 'Logins', 'column', 'CreateTime';

--/*
-- * 注销日志表
-- * 记录用户注销操作
--*/
--CREATE TABLE UnLogins
--(
--  ID uniqueidentifier PRIMARY KEY,  --用户唯一标识、主键
--  CreateTime datetime NOT NULL  --注销时间，插入数据库当时的时间
--)

----添加外键约束
--ALTER TABLE UnLogins ADD CONSTRAINT FK_UnLogins_Users FOREIGN KEY(ID)
--REFERENCES dbo.Users

----添加说明
--EXECUTE sp_addextendedproperty 'MS_Description', '注销日志表<br/>记录用户注销操作','user',@CurrentUser, 'table', 'UnLogins';
--EXECUTE sp_addextendedproperty 'MS_Description', '用户唯一标识', 'user', @CurrentUser, 'table', 'UnLogins', 'column', 'ID';
--EXECUTE sp_addextendedproperty 'MS_Description', '注销时间，插入数据库当时的时间', 'user', @CurrentUser, 'table', 'UnLogins', 'column', 'CreateTime';