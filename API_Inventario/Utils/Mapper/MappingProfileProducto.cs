using API_Inventario.Dtos.ProductoDtos;
using API_Inventario.Models;
using AutoMapper;

namespace API_Inventario.Utils.Mapper
{
    public class MappingProfileProducto : Profile
    {

        public MappingProfileProducto() {

            CreateMap<UpdateProductoDTO, Producto>().ForAllMembers(opt => 
                opt.Condition((objOrigen, objDestino, member) => member != null)
            );

        }

    }
}
