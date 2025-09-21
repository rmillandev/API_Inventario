using API_Inventario.Dtos;
using FluentValidation;

namespace API_Inventario.Validations
{
    public class ProveedorDTOValidator : AbstractValidator<ProveedorDTO>
    {
        public ProveedorDTOValidator() {
            RuleFor(p => p.Email)
            .EmailAddress().WithMessage("El email no tiene un formato válido.")
            .When(p => !string.IsNullOrEmpty(p.Email));
        }
    }
}
