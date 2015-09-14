using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.ModelBinding;
using OctoFX.Core.Model;

namespace OctoFX.TradingWebsite.Models.ModelBinders
{
    public class CurrencyBinder : IModelBinder
    {
        public Task<ModelBindingResult> BindModelAsync(ModelBindingContext bindingContext)
        {
            if(bindingContext.ModelType != typeof(Currency))
			{
				return ModelBindingResult.NoResultAsync;
			}
			
			var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			if(valueProviderResult == ValueProviderResult.None)
			{
				return ModelBindingResult.FailedAsync(bindingContext.ModelName);
			}
			
			bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);
			var value = valueProviderResult.FirstValue;
			if(string.IsNullOrEmpty(value))
			{
				return ModelBindingResult.FailedAsync(bindingContext.ModelName);
			}
			
			var model = (Currency)value;
			if(model == null)
			{
				return ModelBindingResult.FailedAsync(bindingContext.ModelName);
			}
			
			var validationNode = new ModelValidationNode(
				bindingContext.ModelName,
				bindingContext.ModelMetadata,
				model
			);
			
			return ModelBindingResult.SuccessAsync(bindingContext.ModelName, model, validationNode);
        }	
    }
}