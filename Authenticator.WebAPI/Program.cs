using Authenticador.AppService.AppServices;
using Authenticador.AppService.Interfaces;
using Authenticador.Domain.Interfaces;
using Authenticador.Domain.Services;
using Authenticador.Infra.Data.Interfaces.Usuario;
using Authenticador.Infra.Data.Repositories.Base;
using Authenticador.Infra.Data.Repositories.Usuario;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var key = Encoding.ASCII.GetBytes("ASDASD12312312312312312ASDASDASD12312312ASDAS");
builder.Services.AddAuthentication(x => { x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; }).AddJwtBearer(x => { x.RequireHttpsMetadata = false; x.SaveToken = true; x.TokenValidationParameters = new TokenValidationParameters { ValidateIssuerSigningKey = true, IssuerSigningKey = new SymmetricSecurityKey(key), ValidateIssuer = false, ValidateAudience = true }; });
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

#region Injeção

builder.Services.AddScoped<IAutenticarUsuarioAppService, AutenticarUsuarioAppService>();

builder.Services.AddScoped<IAutenticacaoService, AutenticacaoService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

#endregion

//Auto Mapper Configurations
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddMaps(new Assembly[]
    {
        Assembly.Load("Authenticador.Domain"),
        Assembly.Load("Authenticator.WebAPI")
    });

});
var mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
