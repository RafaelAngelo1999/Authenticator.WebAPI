using Authenticador.AppService.AppServices;
using Authenticador.AppService.Interfaces;
using Authenticador.Domain.Interfaces;
using Authenticador.Domain.Services;
using Authenticador.Infra.Data.Interfaces.Usuario;
using Authenticador.Infra.Data.Repositories.Base;
using Authenticador.Infra.Data.Repositories.Usuario;
using Authenticador.WebApi.Filters;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;
// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add(typeof (HttpGlobalExceptionFilter)));
builder.Services.AddEndpointsApiExplorer();

var key = Encoding.ASCII.GetBytes("d41d8cd98f00b204e9800998ecf8427e");
builder.Services.AddAuthentication(x => { x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; }).AddJwtBearer(x => { x.RequireHttpsMetadata = false; x.SaveToken = true; x.TokenValidationParameters = new TokenValidationParameters { ValidateIssuerSigningKey = true, IssuerSigningKey = new SymmetricSecurityKey(key), ValidateIssuer = false, ValidateAudience = false }; });

var serverVersion = new MySqlServerVersion(new Version(10, 4, 22));
var host = Environment.GetEnvironmentVariable("DBHOST") ?? configuration.GetConnectionString("DBHOST");
var password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? configuration.GetConnectionString("MYSQL_PASSWORD");
var userid = Environment.GetEnvironmentVariable("MYSQL_USER") ?? configuration.GetConnectionString("MYSQL_USER");
var usersDataBase = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? configuration.GetConnectionString("MYSQL_DATABASE");

var connString = $"Server={host};DataBase={usersDataBase};Uid={userid};Pwd={password}";
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connString, serverVersion));
builder.Services.AddSwaggerGen(c =>
  {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "API DE AUTENTICAÇÃO", Version = "v1" });

      c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
      {
        Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
         Name = "Authorization",
         In = ParameterLocation.Header,
         Type = SecuritySchemeType.ApiKey,
         Scheme = "Bearer"
       });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });
});

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
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
