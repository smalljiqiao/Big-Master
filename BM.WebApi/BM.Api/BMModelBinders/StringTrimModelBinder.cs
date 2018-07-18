using System.Web.Mvc;


namespace BM.Api.BMModelBinders
{
    public sealed class StringTrimModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = base.BindModel(controllerContext, bindingContext);
            return value is string s ? s.Trim() : value;
        }
    }
}