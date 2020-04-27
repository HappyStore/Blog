using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public static class ControllerExtensions
    {
        public static IActionResult ProduceEnumResult<TEnum>(
            this ControllerBase controller,
            TEnum value,
            params (TEnum, Func<IActionResult>)[] funcs)
            where TEnum : Enum
        {
            if (funcs == null) throw new ArgumentNullException(nameof(funcs));

            var (_, func) = funcs.FirstOrDefault(f => f.Item1.Equals(value));

            if (func == null) throw new ArgumentOutOfRangeException(nameof(value));

            return func();
        }


        public static ActionResult<T> OkOrNotFound<T>(
            this ControllerBase controller,
            T model)
        {
            if (model == null) return controller.NotFound();

            return controller.Ok(model);
        }
    }
}