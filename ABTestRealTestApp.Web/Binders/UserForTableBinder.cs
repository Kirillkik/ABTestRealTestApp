using ABTestRealTestApp.Web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABTestRealTestApp.Web.Binders
{
    public class UserForTableBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var jsonString = ((string)bindingContext.ValueProvider.GetValue(bindingContext.ModelName));
            UserForTable[] result = JsonConvert.DeserializeObject<UserForTable[]>(jsonString);

            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }
}
