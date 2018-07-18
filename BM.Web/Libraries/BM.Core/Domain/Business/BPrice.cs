using System;

namespace BM.Core.Domain.Business
{
    /// <summary>
    /// 业务价格类
    /// </summary>
    public class BPrice
    {
        /// <summary>
        /// 业务价格类ID
        /// </summary>
        public int PriceId { get; set; }

        /// <summary>
        /// 业务种类ID，外键关联BType表
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 业务原价
        /// </summary>
        public decimal OriginalCost { get; set; }

        /// <summary>
        /// 业务优惠价
        /// </summary>
        public decimal PreferentialCost { get; set; }

        /// <summary>
        /// 外键关联BType表
        /// </summary>
        public BType BType { get; set; }
    }
}
