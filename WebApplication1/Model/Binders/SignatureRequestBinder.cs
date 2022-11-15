using Microsoft.AspNetCore.Mvc.ModelBinding;
using Signarutes.Domain.Contracts.models;
using Signarutes.Domain.Contracts.models.Request;

namespace Sigtatures.Web.ModelBinders
{
    public class SignatureRequestBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var form = bindingContext.HttpContext.Request.Form;
            var file = form.Files.FirstOrDefault();

            // TODO: pass serialized request (except file)
            // TODO: check for null file value

            using (var stream = new MemoryStream())
            {
                if (file != null)
                {
                    await file.CopyToAsync(stream);
                }

                var result = new SignatureRequest(
                    new Recipient(form["recipientName"],
                    form["recipientEmail"]),
                    form["message"],
                    file != null
                    ? new SignatureRequestFile(file.FileName, file.ContentType, stream.ToArray())
                    : default);

                bindingContext.Result = ModelBindingResult.Success(result);
            }
        }
    }
}
