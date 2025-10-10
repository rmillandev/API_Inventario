using API_Inventario.Dtos.MovimientoInventarioDtos;
using FluentValidation;

namespace API_Inventario.Validations.MovimientoInventarioValidation
{
    public class CreateMovimientoDtoValidator : AbstractValidator<CreateMovimientoDto>
    {
        public CreateMovimientoDtoValidator() {

            RuleFor(m => m.Cantidad)
                .NotNull().WithMessage("La cantidad no puede ser nula.")
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad no puede ser 0 o menor que 0.");

            RuleFor(m => m.TipoMovimiento)
                .NotNull().WithMessage("El tipo de movimiento no puede ser nulo.")
                .NotEmpty().WithMessage("El tipo de movimiento no puede estar vacion.");

            RuleFor(m => m.UsuarioResponsable)
                .NotNull().WithMessage("El usuario no puede estar nulo.")
                .NotEmpty().WithMessage("El usuario no puede estar vacio.");

            RuleFor(m => m.ProductoId)
                .NotNull().WithMessage("El producto no puede ser nulo.")
                .NotEmpty().WithMessage("El producto no puede estar vacio.");

        }
    }
}
