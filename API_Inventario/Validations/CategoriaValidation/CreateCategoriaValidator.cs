using API_Inventario.Dtos.CategoriaDtos;
using API_Inventario.Models;
using FluentValidation;

namespace API_Inventario.Validations.CategoriaValidation
{
    public class CreateCategoriaValidator : AbstractValidator<CreateCategoriaDTO>
    {

        public CreateCategoriaValidator()
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
