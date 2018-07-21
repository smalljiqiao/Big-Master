using System.IO;
using BM.Core;
using BM.Core.Data;
using BM.Core.Domain.Users;
using BM.Core.Infrastructure;
using BM.Core.Installation;
using BM.Core.Localization;
using BM.Data;

namespace BM.Services.Installation
{
    public partial class CodeFirstInstallationService : IInstallationService
    {
        private readonly IDbContext _idbContext;
        private readonly IRepository<User> _useRepository;

        public CodeFirstInstallationService(IRepository<User> userRepository,
            IDbContext dbContext)
        {
            this._useRepository = userRepository;
            this._idbContext = dbContext;
        }

        public virtual void InstallData()
        {
            AddDefault();
            AddConstraint();
            AddDescrition();

            InstallCodeMessage();
        }

        protected virtual void InstallCodeMessage()
        {
            foreach (var filePath in Directory.EnumerateFiles(CommonHelper.MapPath("~/App_Data/CodeMessage/"), "*.xml",
                SearchOption.TopDirectoryOnly))
            {
                var codeMessageXml = File.ReadAllText(filePath);
                var localService = EngineContext.Current.Resolve<ILocalizationService>();
                localService.ImportCodeMessageFromXml(codeMessageXml);
            }
        }


        /// <summary>
        /// 添加字段默认值
        /// </summary>
        protected virtual void AddDefault()
        {
            var cmd = "ALTER TABLE [Log] ADD CONSTRAINT DF_CreateTime_Log DEFAULT(GETDATE()) FOR CreateTime;" +
                      "ALTER TABLE [Search] ADD CONSTRAINT DF_CreateTime_Search DEFAULT(GETDATE()) FOR CreateTime;" +
                      "ALTER TABLE [Order] ADD CONSTRAINT DF_CreateTime_Order DEFAULT(GETDATE()) FOR CreateTime;" +
                      "ALTER TABLE [User] ADD CONSTRAINT DF_CreateTime_User DEFAULT(GETDATE()) FOR CreateTime;" +
                      "ALTER TABLE [AndroidInfo] ADD CONSTRAINT DF_CreateTime_AndroidInfo DEFAULT(GETDATE()) FOR CreateTime;" +
                      "ALTER TABLE [BurialPoint] ADD CONSTRAINT DF_CreateTime_BurialPoint DEFAULT(GETDATE()) FOR CreateTime;" +
                      "ALTER TABLE [DreamTitle] ADD CONSTRAINT DF_CreateTime_BurialPoint DEFAULT(GETDATE()) FOR CreateTime;" +
                      "ALTER TABLE [DreamDetail] ADD CONSTRAINT DF_CreateTime_BurialPoint DEFAULT(GETDATE()) FOR CreateTime;";

            _idbContext.ExecuteSqlCommand(cmd);
        }

        /// <summary>
        /// 添加字段约束
        /// </summary>
        protected virtual void AddConstraint()
        {
            var cmd = "ALTER TABLE [OrderSearch] ADD CONSTRAINT [CK_OrderSearch_Sex] CHECK (Sex = 0 OR Sex = 1 OR Sex = 2);" +
                      "ALTER TABLE [Search] ADD CONSTRAINT [CK_Search_Sex] CHECK (Sex = 0 OR Sex = 1 OR Sex = 2);";

            _idbContext.ExecuteSqlCommand(cmd);
        }

        /// <summary>
        /// 添加数据说明
        /// </summary>
        protected virtual void AddDescrition()
        {
            var cmd = "USE BigMaster; \r\n" +
                      "DECLARE @CurrentUser sysname; \r\n" +
                      "SELECT @CurrentUser = USER_NAME(); \r\n" +
                      "-- User Table \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '用户信息类', 'user',@CurrentUser, 'table', 'User'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '手机号码', 'user', @CurrentUser, 'table', 'User', 'column', 'Phone'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '昵称', 'user', @CurrentUser, 'table', 'User', 'column', 'NickName'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '邮箱', 'user', @CurrentUser, 'table', 'User', 'column', 'Email'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '密码明文', 'user', @CurrentUser, 'table', 'User', 'column', 'Password'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '密码盐值', 'user', @CurrentUser, 'table', 'User', 'column', 'Salt'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '密码密文', 'user', @CurrentUser, 'table', 'User', 'column', 'SaltPassword'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '注册时间', 'user', @CurrentUser, 'table', 'User', 'column', 'CreateTime'; \r\n" +
                      "-- AndroidInfo Table \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '用户安卓信息类', 'user',@CurrentUser, 'table', 'AndroidInfo'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '手机号码', 'user', @CurrentUser, 'table', 'AndroidInfo', 'column', 'Phone'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '安卓ID', 'user', @CurrentUser, 'table', 'AndroidInfo', 'column', 'AndroidId'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '生成时间', 'user', @CurrentUser, 'table', 'AndroidInfo', 'column', 'CreateTime'; \r\n" +
                      "-- Order Table \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '订单类', 'user',@CurrentUser, 'table', 'Order'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '手机号码', 'user', @CurrentUser, 'table', 'Order', 'column', 'Phone'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '订单ID', 'user', @CurrentUser, 'table', 'Order', 'column', 'OrderId'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '订单类型', 'user', @CurrentUser, 'table', 'Order', 'column', 'OrderType'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '订单价格，精确两位小数', 'user', @CurrentUser, 'table', 'Order', 'column', 'Price'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '订单支付状态', 'user', @CurrentUser, 'table', 'Order', 'column', 'PayState'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '生成时间', 'user', @CurrentUser, 'table', 'Order', 'column', 'CreateTime'; \r\n" +
                      "-- OrderSearch Table \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '订单查询类<br/>记录订单所有查询的关键字信息', 'user',@CurrentUser, 'table', 'OrderSearch'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '手机号码', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'Phone'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '订单ID', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'OrderId'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '查询姓名 对应八字详批和宝宝取名', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'UserName'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '性别 值为1时是男性，为2时是女性，为0时是未知 对应八字详批和宝宝取名', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'Sex'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '出生日期 对应八字详批和宝宝取名', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'BirthDay'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '八字合婚男方姓名', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'ManName'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '八字合婚男方出生日期', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'ManBirthDay'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '八字合婚女方姓名', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'WomanName'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '八字合婚女方出生日期', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'WomanBirthDay'; \r\n" +
                      "-- Log Table \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '错误日志类', 'user',@CurrentUser, 'table', 'Log'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '日志ID', 'user', @CurrentUser, 'table', 'Log', 'column', 'LogId'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '错误信息', 'user', @CurrentUser, 'table', 'Log', 'column', 'Mes'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '堆栈轨迹', 'user', @CurrentUser, 'table', 'Log', 'column', 'StackTrace'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '生成时间', 'user', @CurrentUser, 'table', 'Log', 'column', 'CreateTime'; \r\n" +
                      "-- Search Table \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '搜索记录类', 'user',@CurrentUser, 'table', 'Search'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '手机号码', 'user', @CurrentUser, 'table', 'Search', 'column', 'Phone'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '搜索ID', 'user', @CurrentUser, 'table', 'Search', 'column', 'SearchId'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '查询姓名 对应八字详批和宝宝取名', 'user', @CurrentUser, 'table', 'Search', 'column', 'UserName'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '性别 值为1时是男性，为2时是女性，为0时是未知 对应八字详批和宝宝取名', 'user', @CurrentUser, 'table', 'Search', 'column', 'Sex'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '出生日期 对应八字详批和宝宝取名', 'user', @CurrentUser, 'table', 'Search', 'column', 'BirthDay'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '八字合婚男方姓名', 'user', @CurrentUser, 'table', 'Search', 'column', 'ManName'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '八字合婚男方出生日期', 'user', @CurrentUser, 'table', 'Search', 'column', 'ManBirthDay'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '八字合婚女方姓名', 'user', @CurrentUser, 'table', 'Search', 'column', 'WomanName'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '八字合婚女方出生日期', 'user', @CurrentUser, 'table', 'Search', 'column', 'WomanBirthDay'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '周公解梦搜索词', 'user', @CurrentUser, 'table', 'Search', 'column', 'ZhouWord'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '生成时间', 'user', @CurrentUser, 'table', 'Search', 'column', 'CreateTime'; \r\n" +
                      "-- BPrice Table \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '业务价格类', 'user',@CurrentUser, 'table', 'BPrice'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '业务价格类ID', 'user', @CurrentUser, 'table', 'BPrice', 'column', 'PriceId'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '业务种类ID，外键关联BType表', 'user', @CurrentUser, 'table', 'BPrice', 'column', 'TypeId'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '业务原价', 'user', @CurrentUser, 'table', 'BPrice', 'column', 'OriginalCost'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '业务优惠价', 'user', @CurrentUser, 'table', 'BPrice', 'column', 'PreferentialCost'; \r\n" +
                      "-- BType Table \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '业务种类类', 'user',@CurrentUser, 'table', 'BType'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '业务种类ID', 'user', @CurrentUser, 'table', 'BType', 'column', 'TypeId'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '业务名称', 'user', @CurrentUser, 'table', 'BType', 'column', 'TypeName'; \r\n" +
                      "-- BurialPoint Table \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '埋点信息', 'user',@CurrentUser, 'table', 'BurialPoint'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '埋点ID', 'user', @CurrentUser, 'table', 'BurialPoint', 'column', 'BpId'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '手机号码', 'user', @CurrentUser, 'table', 'BurialPoint', 'column', 'Phone'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '一级类型', 'user', @CurrentUser, 'table', 'BurialPoint', 'column', 'FType'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '二级类型', 'user', @CurrentUser, 'table', 'BurialPoint', 'column', 'SType'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '补充说明，当二类类型不能满足埋点操作时，将额外信息写入此列', 'user', @CurrentUser, 'table', 'BurialPoint', 'column', 'Remark'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '生成时间', 'user', @CurrentUser, 'table', 'BurialPoint', 'column', 'CreateTime'; \r\n" +
                      "-- CodeMessage Table \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '返回码信息表', 'user',@CurrentUser, 'table', 'CodeMessage'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '返回码ID', 'user', @CurrentUser, 'table', 'CodeMessage', 'column', 'CmId'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '返回码', 'user', @CurrentUser, 'table', 'CodeMessage', 'column', 'Code'; \r\n" +
                      "EXECUTE sp_addextendedproperty 'MS_Description', '返回码说明', 'user', @CurrentUser, 'table', 'CodeMessage', 'column', 'Message'; \r\n" +
                      "USE master; \r\n";

            _idbContext.ExecuteSqlCommand(cmd);
        }
    }
}
