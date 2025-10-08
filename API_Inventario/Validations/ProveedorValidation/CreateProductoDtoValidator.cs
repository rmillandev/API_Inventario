using API_Inventario.Dtos.ProveedorDTO;
using FluentValidation;

namespace API_Inventario.Validations.ProveedorValidation
{
    public class CreateProductoDtoValidator : AbstractValidator<CreateProveedorDTO>
    {
        public CreateProductoDtoValidator()
        {

            RuleFor(p => p.Nit)
                .NotEmpty().WithMessage("El nit no puede estar vacio.")
                .NotNull().WithMessage("El nit no puede ser nulo.");

            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El nombre no puede estar vacion.")
                .NotNull().WithMessage("El nombre no puede ser nulo.");

            RuleFor(p => p.Telefono)
                .NotEmpty().WithMessage("El telefono no puede estar vacio.")
                .NotNull().WithMessage("El telefono no puede ser nulo.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("El email no puede estar vacio.")
                .NotNull().WithMessage("El email no puede ser nulo.")
                .EmailAddress().WithMessage("El email no tiene un formato válido.")
                .When(p => !string.IsNullOrEmpty(p.Email));
        }
    }
}
