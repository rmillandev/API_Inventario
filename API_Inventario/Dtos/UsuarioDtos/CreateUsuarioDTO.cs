namespace API_Inventario.Dtos.UsuarioDtos
{
    public class CreateUsuarioDTO
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
    }
}
