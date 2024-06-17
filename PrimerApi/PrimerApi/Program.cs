using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PrimerApi.Data;
using PrimerApi.Interfaces;
using PrimerApi.Interfaces.Services;
using PrimerApi.Mappings;
using PrimerApi.Models;
using PrimerApi.Repos;
using PrimerApi.Services;
using PrimerApi.Services.Usuario;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//-------------------------------------
//asi es originalmente, pero si metemos autenticacion hay que a�adir configuraciones 
//builder.Services.AddSwaggerGen();
//-------------------------------------
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Authentication with Bearer scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    // options.OperationFilter<SecurityRequirementOperationFilter>(); aca al profe le falto una config. No se cual es todavia
});



builder.Services.AddDbContext<ContextDb>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("ConexionDatabase"));
});

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAvionRepository, AvionRepository>();
builder.Services.AddScoped<IAvionService, AvionService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});


//---------------------
//Esto tambi�n es por la autenticacion

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("nicoClase@Tup2024ll12wnicoClase@Tup2024ll12wnicoClase@Tup2024ll12wnicoClase@Tup2024ll12wnicoClase@Tup2024ll12w")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


//---------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
