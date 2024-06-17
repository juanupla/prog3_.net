using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PrimerApi.Dto;
using PrimerApi.Interfaces;
using PrimerApi.Interfaces.Services;
using PrimerApi.Repos;
using PrimerApi.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PrimerApi.Services.Usuario;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<UsuarioDto>>> GetAll()
    {
        var response = new ApiResponse<List<UsuarioDto>>();
        var usuarios = await _usuarioRepository.GetAll();
        if (usuarios != null && usuarios.Count > 0)
        {
            response.Data = _mapper.Map<List<UsuarioDto>>(usuarios);
            // var result = new List<UsuarioDto>();
            //
            // foreach (var usu in usuarios)
            // {
            //     var usuDto = new UsuarioDto
            //     {
            //         NombreUsuario = usu.NombreUsuario,
            //         Email = usu.Email
            //     };
            //     
            //     result.Add(usuDto);
            // }
            //
            // return new ApiResponse<List<UsuarioDto>>
            // {
            //     Data = result
            // };
        }

        return response;
    }

    public async Task<ApiResponse<UsuarioDto>> GetById(int id)
    {
        var usuario = await _usuarioRepository.GetById(id);
        if (usuario != null)
        {

            var usuDto = new UsuarioDto
            {
                NombreUsuario = usuario.NombreUsuario,
                Email = usuario.Email
            };
            return new ApiResponse<UsuarioDto>
            {
                Data = _mapper.Map<UsuarioDto>(usuario)
            };
        }

        return new ApiResponse<UsuarioDto>();
    }

    public async Task<ApiResponse<LoginDto>> LoginUsuario(string nombreUsuario, string email)
    {
        var usuario = await _usuarioRepository.GetByNombreUsuarioAndEmail(nombreUsuario, email);

        var token = GenerateToken(usuario);

        var result = new ApiResponse<LoginDto>();
        result.Data = new LoginDto
        {
            Email = usuario.Email,
            NombreUsuario = usuario.NombreUsuario,
            Token = token
        };

        return result;
    }

    private string GenerateToken(Models.Usuario usu)
    {
        var claim = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usu.Id.ToString()),
            new Claim(ClaimTypes.Name, usu.NombreUsuario)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("nicoClase@Tup2024ll12wnicoClase@Tup2024ll12wnicoClase@Tup2024ll12wnicoClase@Tup2024ll12wnicoClase@Tup2024ll12w"));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var securityToken = new JwtSecurityToken(
            claims: claim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return token;
    }
}