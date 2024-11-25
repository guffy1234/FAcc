using Microsoft.AspNetCore.Components.Forms;

namespace FuelAcc.Client
{
    // This class lets us modify the CSS class that will be added to the input elements
    // when the properties they are bound to are invalid
    // This way we can add the "is-invalid" to the input elements
    // See: https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation?view=aspnetcore-5.0#custom-validation-class-attributes
    internal class BootstrapFieldClassProvider : FieldCssClassProvider
    {
        public override string GetFieldCssClass(EditContext editContext, in FieldIdentifier fieldIdentifier)
        {
            var isValid = !editContext.GetValidationMessages(fieldIdentifier).Any();
            return isValid ? "" : "is-invalid";
        }
    }
}