using API_Inventario.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Inventario.Db
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options): base (options) { }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<MovimientoInventario> MovimientoInventario { get; set; }
    }
}
