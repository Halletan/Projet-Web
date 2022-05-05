using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SpaceAdventures.MVC.Models
{
    public class GridModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            //if (bindingContext == null)
            //{
            //    throw new ArgumentNullException(nameof(bindingContext));
            //}

            //var draw = Convert.ToInt32(bindingContext.ActionContext.HttpContext.Request.Form["draw"].FirstOrDefault());
            //var start = Convert.ToInt32(bindingContext.ActionContext.HttpContext.Request.Form["start"].FirstOrDefault());
            //var length = Convert.ToInt32(bindingContext.ActionContext.HttpContext.Request.Form["length"].FirstOrDefault());

            //var sortColumn =
            //    bindingContext.ActionContext.HttpContext.Request.Form[
            //        "columns[" + bindingContext.ActionContext.HttpContext.Request.Form.FirstOrDefault();


        }
    }
}
