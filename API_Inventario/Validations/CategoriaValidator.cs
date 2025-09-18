using API_Inventario.Models;
using FluentValidation;

namespace API_Inventario.Validations
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {

        public CategoriaValidator() 
        {
            RuleFor(c => c.Nombre)
                .NotEmpty()
                .NotNull()
                .WithMessage("El nombre de la categoria es obligatorio.");

            RuleFor(c => c.Descripcion)
                .NotEmpty()
                .NotNull()
                .WithMessage("La descripcion de la categoria es obligatoria.");
        }

    }
}
