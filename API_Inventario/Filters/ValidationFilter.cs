using API_Inventario.Dtos.CategoriaDtos;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API_Inventario.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.SingleOrDefault(p =>
                p.Value is CreateCategoriaDTO || p.Value is UpdateCategoriaDTO
            );

            if (param.Value == null)
            {
                await next();
                return;
            }

            var validator = (IValidator) context.HttpContext.RequestServices.GetService(typeof(IValidator<>).MakeGenericType(param.Value.GetType()));
            if (validator == null)
            {
                await next();
                return;
            }

            var validationContext = new ValidationContext<object>(param.Value);
            var validationResult = await validator.ValidateAsync(validationContext);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(error => new
                    {
                        Field = error.PropertyName,
                        Message = error.ErrorMessage
                    });

                context.Result = new BadRequestObjectResult(new { statusCode = 400, validationErrorsMessage = errors });
                return;
            }

            await next();
        }
    }
}
