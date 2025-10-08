using API_Inventario.Dtos.ProveedorDtos;
using FluentValidation;

namespace API_Inventario.Validations.ProveedorValidation
{
    public class UpdateProveedorDtoValidator :AbstractValidator<UpdateProveedorDTO>
    {

        public UpdateProveedorDtoValidator()
        {
            RuleFor(p => p.Email)
                .EmailAddress().WithMessage("El email no tiene un formato válido.")
                .When(p => !string.IsNullOrEmpty(p.Email));
        }

    }
}
