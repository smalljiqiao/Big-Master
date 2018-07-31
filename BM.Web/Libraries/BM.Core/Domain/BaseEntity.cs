namespace BM.Core.Domain
{
    /// <summary>
    /// 表基类
    /// </summary>
    public abstract partial class BaseEntity
    {
        //这里应该定义数据库通用的ID，但是目前这个项目手机号码是主键，但是后台管理的表又不需要。
        //声明这个基类也是为了反射时能用得上
       
    }
}
