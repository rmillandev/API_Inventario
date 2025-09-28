using API_Inventario.Dtos;
using FluentValidation;

namespace API_Inventario.Validations
{
    public class CreateProductoDTOValidator : AbstractValidator<CreateProductoDTO>
    {
        public CreateProductoDTOValidator() {

            RuleFor(p => p.Codigo)
                .NotEmpty().WithMessage("El codigo no puede estar vacio.");

            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El nombre no puede estar vacio.");

            RuleFor(p => p.Descripcion)
                .NotEmpty().WithMessage("La descripcion no puede estar vacio.");

            RuleFor(p => p.Codigo)
                .NotEmpty().WithMessage("El codigo no puede estar vacio.");

            RuleFor(p => p.Precio)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0.");


            RuleFor(p => p.StockActual)
                .GreaterThanOrEqualTo(0).WithMessage("El stock actual no puede ser negativo");

            RuleFor(p => p.StockMinimo)
                .GreaterThanOrEqualTo(0).WithMessage("El stock minimo no puede ser negativo");

        }
    }
}
