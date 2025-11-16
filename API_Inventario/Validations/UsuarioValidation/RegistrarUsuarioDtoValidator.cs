using API_Inventario.Dtos.UsuarioDtos;
using FluentValidation;

namespace API_Inventario.Validations.UsuarioValidation
{
    public class RegistrarUsuarioDtoValidator : AbstractValidator<CreateUsuarioDTO>
    {
        public RegistrarUsuarioDtoValidator()
        {

            var roles = new List<string>
            {
                UsuarioRol.Admin.ToString(),
                UsuarioRol.Empleado.ToString(),
                UsuarioRol.Proveedor.ToString()
            };

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El campo Email es obligatorio.")
                .EmailAddress().WithMessage("El campo Email debe ser una dirección de correo electrónico válida.");
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El campo Username es obligatorio.")
                .MinimumLength(3).WithMessage("El campo Username debe tener al menos 3 caracteres.")
                .MaximumLength(20).WithMessage("El campo Username no debe exceder los 20 caracteres.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("El campo Password es obligatorio.")
                .MinimumLength(6).WithMessage("El campo Password debe tener al menos 6 caracteres.")
                .MaximumLength(16).WithMessage("El campo Password no debe exceder los 16 caracteres.");
            RuleFor(x => x.Rol)
                .NotEmpty().WithMessage("El campo Rol es obligatorio.")
                .Must(rol => roles.Contains(rol))
                .WithMessage("El rol ingresado no coincide con los predeterminados del sistema.");
        }
    }
}
