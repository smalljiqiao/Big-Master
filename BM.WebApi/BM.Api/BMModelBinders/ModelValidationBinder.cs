using System;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using IModelBinder = System.Web.Http.ModelBinding.IModelBinder;
using ModelBindingContext = System.Web.Http.ModelBinding.ModelBindingContext;

namespace BM.Api.BMModelBinders
{
    /// <summary>
    /// 模型验证提供类 不能用
    /// 只能验证系统自带的字段，例如string、int等。自己定义的对象不行
    /// </summary>
    public sealed class ModelValidationBinder : ModelBinderProvider
    {
        readonly ModelBinder _binder = new ModelBinder();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="modelType"></param>
        /// <returns></returns>
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            return _binder;
        }
    }

    /// <summary>
    /// 模型验证类
    /// </summary>
    public class ModelBinder : IModelBinder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            bindingContext.Model = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName)
                .ConvertTo(bindingContext.ModelType, Thread.CurrentThread.CurrentCulture);

            bindingContext.ValidationNode.ValidateAllProperties = true;

            return true;
        }
    }
}