using System;
using System.Reflection;
using BM.Services.Logs;

namespace BM.Services.ModelTransfer
{
    public static class ModelTransfer
    {
        /// <summary>
        /// 将S转换为D（新实例）
        /// </summary>
        /// <typeparam name="D"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static D Mapper<D, S>(S s)
        {
            try
            {
                D d = Activator.CreateInstance<D>();
                var sType = s.GetType();
                var dType = typeof(D);
                foreach (PropertyInfo sP in sType.GetProperties())
                {
                    foreach (PropertyInfo dP in dType.GetProperties())
                    {
                        if (dP.Name == sP.Name)
                        {
                            dP.SetValue(d, sP.GetValue(s));
                            break;
                        }
                    }
                }
                return d;
            }
            catch (Exception ex)
            {
                LogService.InsertLog(ex);
                return default(D);
            }
        }

        /// <summary>
        /// 将S转换为D（对象不变）
        /// </summary>
        /// <typeparam name="D"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="s"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static D Mapper<D, S>(S s, D d)
        {
            try
            {
                var sType = s.GetType();
                var dType = typeof(D);
                foreach (PropertyInfo sP in sType.GetProperties())
                {
                    foreach (PropertyInfo dP in dType.GetProperties())
                    {
                        if (dP.Name == sP.Name)
                        {
                            dP.SetValue(d, sP.GetValue(s));
                            break;
                        }
                    }
                }
                return d;
            }
            catch (Exception ex)
            {
                LogService.InsertLog(ex);
                return default(D);
            }
        }
    }
}
