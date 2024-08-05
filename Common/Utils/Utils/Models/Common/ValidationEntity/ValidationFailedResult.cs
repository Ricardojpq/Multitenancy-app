using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Utils.Models.Common.ValidationEntity
{
    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState) : base((object)new ValidationResultModel(modelState))
        {
            this.StatusCode = new int?(422);
        }
    }
}
