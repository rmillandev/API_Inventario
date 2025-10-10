using API_Inventario.Db;
using API_Inventario.Models;
using API_Inventario.Repositorys.Interfaces;

namespace API_Inventario.Repositorys
{
    public class MovimientoInventarioRepository : GenericRepository<MovimientoInventario>, IMovimientoInventarioRepository
    {
        private readonly Context context;
        public MovimientoInventarioRepository(Context context) : base(context)
        {
            this.context = context; 
        }
    }
}
