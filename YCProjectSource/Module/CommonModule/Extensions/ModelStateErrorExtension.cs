using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace CommonModule.Extensions
{
    public static class ModelStateErrorExtension
    {
        public static Dictionary<string, List<string>> AllErrors(this ModelStateDictionary modelState)
        {
            Dictionary<string, List<string>> errorObjects = new Dictionary<string, List<string>>();
            var erroneousFields = modelState.Where(ms => ms.Value.Errors.Any())
                                        .Select(x => new { x.Key, x.Value.Errors });
            foreach (var erroneousField in erroneousFields)
            {
                var fieldKey = erroneousField.Key;

                var fieldErrors = erroneousField.Errors
                                   .Select(error => error.ErrorMessage).ToList();
                errorObjects.Add(fieldKey, fieldErrors);
            }
            return errorObjects;
        }
    }
}