using API_Inventario.Dtos.UsuarioDtos;
using API_Inventario.Models;
using API_Inventario.Repositorys.Interfaces;
using API_Inventario.Services.Interfaces;
using API_Inventario.Utils.Objects;

namespace API_Inventario.Services
{
    public class UsuarioService : GenericService<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository repository;
        private readonly IPasswordHasherService passwordHasher;
        public UsuarioService(IUsuarioRepository repository, IPasswordHasherService passwordHasher) : base(repository)
        {
            this.repository = repository;
            this.passwordHasher = passwordHasher;
        }

        public async Task<ShowSuccessCreateUserDTO> CreateUsuario(CreateUsuarioDTO usuarioDto)
        {
            var userAlreadyExists = repository.UsernameAlreadyExist(usuarioDto.Username);

            if (userAlreadyExists) throw new InvalidOperationException("El nombre de usuario ya existe.");

            Usuario usuario = new Usuario()
            {
                Username = usuarioDto.Username,
                Password = passwordHasher.HashPassword(usuarioDto.Password),
                Email = usuarioDto.Email,
                Rol = usuarioDto.Rol
            };

            var data = await repository.Create(usuario);

            return new ShowSuccessCreateUserDTO
            {
                Success = true,
                Message = "Usuario registrado exitosamente."
            };
        }

        public Task UpdateUsuario(int id, UpdateUsuarioDTO usuarioDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> ValidateUser(LoginDTO dto)
        {
            var user = await repository.GetByUsername(dto.Username);

            if (user == null) throw new UnauthorizedAccessException("Credenciales invalidas.");

            bool verifiedPassword = passwordHasher.VerifyPassword(dto.Password, user.Password);

            if (!verifiedPassword) throw new UnauthorizedAccessException("Credenciales invalidas.");

            return user;
        }
    }
}
