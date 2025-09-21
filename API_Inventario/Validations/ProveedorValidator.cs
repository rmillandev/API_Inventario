using API_Inventario.Models;
using FluentValidation;

namespace API_Inventario.Validations
{
    public class ProveedorValidator : AbstractValidator<Proveedor>
    {

        public ProveedorValidator() 
        {

            RuleFor(p => p.Nit)
                .NotEmpty().WithMessage("El nit del proveedor no puede estar vacio.")
                .NotNull().WithMessage("El nit del proveedor no puede estar nulo.");

            RuleFor(p => p.Nombre)
                  .NotEmpty().WithMessage("El nombre del proveedor no puede estar vacio.")
                .NotNull().WithMessage("El nombre del proveedor no puede estar nulo.");


            RuleFor(p => p.Telefono)
                .NotEmpty().WithMessage("El telefono del proveedor no puede estar vacio.")
                .NotNull().WithMessage("El telefono del proveedor no puede estar nulo.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("El email del proveedor no puede estar vacio.")
                .NotNull().WithMessage("El email del proveedor no puede estar nulo.")
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("El email del proveedor no tiene un formato valido.");


        }

    }
}
