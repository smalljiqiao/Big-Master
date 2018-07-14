namespace BM.Core.Infrastructure
{
    /// <summary>
    /// 单例模式泛型类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Singleton<T>
    {
        public static T Instance { get; set; }
    }
}
