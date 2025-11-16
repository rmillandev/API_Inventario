namespace API_Inventario.Dtos.UsuarioDtos
{
    public class AuthResponseDTO
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string Rol { get; set; }
        public string Email { get; set; }
    }
}
