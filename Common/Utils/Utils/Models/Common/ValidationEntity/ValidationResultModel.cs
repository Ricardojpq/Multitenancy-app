using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Utils.Models.Common.ValidationEntity
{
    public class ValidationResultModel
    {
        public ValidationResultModel(ModelStateDictionary modelState)
        {
            Message = "Validation Failed";
            Errors = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                .ToList();
        }

        public string Message
        {
            get;
        }

        public List<ValidationError> Errors
        {
            get;
        }
    }
}
