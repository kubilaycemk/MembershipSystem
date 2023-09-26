using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MembershipSystem.Extensions
{
    public static class ModelStateExtension
    {
        public static void AddModelErrorList(this ModelStateDictionary modelState, List<string> errors)
        {
            errors.ForEach(item =>
            {
                modelState.AddModelError(string.Empty, item);
            });
        }
    }
}
