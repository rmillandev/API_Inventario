using API_Inventario.Dtos.ProductoDtos;
using API_Inventario.Models;
using AutoMapper;

namespace API_Inventario.Utils.Mapper
{
    public class MappingProfileProducto : Profile
    {

        public MappingProfileProducto() {

            CreateMap<UpdateProductoDTO, Producto>()
    .ForMember(dest => dest.CategoriaId, opt => opt.Ignore())
    .ForMember(dest => dest.ProveedorId, opt => opt.Ignore())
    .ForAllMembers(opt =>
        opt.Condition((src, dest, srcMember) => srcMember != null));


        }

    }
}
