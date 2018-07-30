using System;

namespace BM.Core.Domain.Orders
{
    /// <summary>
    /// 订单查询类
    /// 记录订单所有查询的关键字信息
    /// </summary>
    public class OrderSearch : BaseEntity
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { get; set; }

        #region 八字详批

        /// <summary>
        /// 查询姓名
        /// </summary>
        public string DName { get; set; }

        /// <summary>
        /// 性别 值为1时是男性，为2时是女性，为0时是未知 对应八字详批和宝宝取名
        /// </summary>

        public byte DSex { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime DBirthDay { get; set; }

        #endregion

        #region 宝宝取名

        /// <summary>
        /// 姓氏
        /// </summary>
        public string BSurname { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public byte BSex { get; set; }

        /// <summary>
        /// 出生日期 yyyy-MM-dd HH-mm-ss
        /// </summary>
        public DateTime BBirthDay { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string BProvince { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string BCity { get; set; }

        #endregion

        #region 八字合婚

        /// <summary>
        /// 八字合婚男方姓名
        /// </summary>
        public string MManName { get; set; }

        /// <summary>
        /// 八字合婚男方出生日期 yyyy-MM-dd 
        /// </summary>
        public DateTime MManBirthDay { get; set; }

        /// <summary>
        /// 八字合婚男方时辰
        /// </summary>
        public string MManTime { get; set; }

        /// <summary>
        /// 八字合婚女方姓名
        /// </summary>
        public string MWomanName { get; set; }

        /// <summary>
        /// 八字合婚女方出生日期 yyyy-MM-dd 
        /// </summary>
        public DateTime MWomanBirthDay { get; set; }

        /// <summary>
        /// 八字合婚女方时辰
        /// </summary>
        public string MWomanTime { get; set; }

        #endregion

        /// <summary>
        /// 外键关联Order表
        /// </summary>
        public Order Order { get; set; }
    }
}
