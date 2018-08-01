namespace BM.Data.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tb_Product")]
    public partial class Product
    {
        /// <summary>
        /// 产品Id
        /// </summary>
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        [Required]
        [StringLength(50)]
        public string ProductType{get; set; }

        /// <summary>
        /// 市场价
        /// </summary>
        public decimal MarketPrice { get; set; }

        /// <summary>
        /// 折扣价
        /// </summary>
        public decimal DiscountPrice { get; set; }

        /// <summary>
        /// 产品是否处于折扣状态
        /// </summary>
        public bool IsDiscount { get; set; }

        /// <summary>
        /// 是否已经删除
        /// </summary
        [Required]
        public bool IsDelete { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        [Required]
        [StringLength(50)]
        public string CreateUserId { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreateTime { get; set; }

    }
}
