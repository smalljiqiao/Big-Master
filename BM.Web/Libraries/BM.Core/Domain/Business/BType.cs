namespace BM.Core.Domain.Business
{
    /// <summary>
    /// 业务种类类
    /// </summary>
    public class BType : BaseEntity
    {
        /// <summary>
        /// 业务种类ID
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 业务名称
        /// </summary>
        public string TypeName { get; set; }
    }
}
