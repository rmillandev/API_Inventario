using API_Inventario.Dtos.ProveedorDtos;
using API_Inventario.Dtos.UsuarioDtos;
using API_Inventario.Models;
using API_Inventario.Repositorys.Interfaces;
using API_Inventario.Services.Interfaces;
using API_Inventario.Utils;
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

        public async Task<PagedResult<ReadUsuarioDTO>> GetAllDto(int? pageNumber, int? pageSize)
        {
            var query = repository.GetAllQuery()
                .Select(u => new ReadUsuarioDTO
                {
                    Id = u.Id,
                    UserName = u.Username,
                    Email = u.Email,
                    Rol = u.Rol
                });
            return await query.ToPagedResultAsync(pageNumber, pageSize);
        }

        public async Task<ShowSuccessCreateUserDTO> CreateUsuario(CreateUsuarioDTO usuarioDto)
        {
            var userAlreadyExists = repository.UsernameAlreadyExist(usuarioDto.Username);

            if (userAlreadyExists) throw new InvalidOperationException("No se creo el usuario porque el usuario ya existe.");

            var emailAlreadyExists = repository.EmailAlreadyExist(usuarioDto.Email);
            if (emailAlreadyExists) throw new InvalidOperationException("No se creo el usuario porque el email ya existe.");

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

        public async Task UpdateUsuario(int id, UpdateUsuarioDTO usuarioDto)
        {
            var usuario = await repository.GetById(id);

            if (usuario == null) throw new KeyNotFoundException("Usuario no encontrado.");

            if (!string.IsNullOrEmpty(usuarioDto.Username))
            {
                var userAlreadyExists = repository.UsernameAlreadyExist(usuarioDto.Username);
                if (userAlreadyExists) throw new InvalidOperationException("No se pudo modificar porque el nombre de usuario ya existe.");
            }

            if (!string.IsNullOrEmpty(usuarioDto.Email))
            {
                var emailAlreadyExists = repository.EmailAlreadyExist(usuarioDto.Email);
                if (emailAlreadyExists) throw new InvalidOperationException("No se pudo modificar porque el email ya existe.");
            }

            usuario.Username = usuarioDto.Username ?? usuario.Username;
            usuario.Email = usuarioDto.Email ?? usuario.Email;
            usuario.Rol = usuarioDto.Rol ?? usuario.Rol;

            await repository.Update(id, usuario);

        }

        public override async Task Delete(int id)
        {
            var usuario = await repository.GetById(id);
            if (usuario == null) throw new KeyNotFoundException("Usuario no encontrado.");
            await repository.Delete(id);
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
