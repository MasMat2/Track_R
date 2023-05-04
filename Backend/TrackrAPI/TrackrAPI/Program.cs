using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using TrackrAPI.Helpers;
using TrackrAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
// JwtSettings jwtSettings = new JwtSettings();
// builder.Configuration.GetSection(JwtSettings.SectionName).Bind(jwtSettings);

builder.Services.AddDbContext<TrackrContext>(options =>
{
    options.UseSqlServer(connection);
});

builder.Services.AddControllers().AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    opt.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
    opt.SerializerSettings.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;
});

builder.Services.AddCors();
builder.Services.AddSignalR();
builder.Services.AddScoped<SimpleAES>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<CorreoHelper>();
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

// IoC  - Inyeccion de dependencias para los Repositorys y Services
builder.Services.Scan(scan => scan
    .FromAssemblyOf<Program>().AddClasses(c => c.InNamespaces("TrackrAPI.Repositorys")).AsImplementedInterfaces().WithTransientLifetime()
);

builder.Services.Scan(scan => scan
    .FromAssemblyOf<Program>().AddClasses(c => c.InNamespaces("TrackrAPI.Services")).AsSelf().WithTransientLifetime()
);

// Crea una nueva instancia de JwtSettings y la configura con los valores del appsettings.json
var jwtSettings = new JwtSettings();
builder.Configuration.Bind(JwtSettings.SectionName, jwtSettings);

// Registra la instancia de JwtSettings como un servicio Singleton en el contenedor de inyecci�n de dependencias.
// Crea una �nica instancia de JwtSettings que se comparte entre todos los objetos que lo soliciten a trav�s de inyecci�n de dependencias.
builder.Services.AddSingleton(Options.Create(jwtSettings));

// Habilita la autenticaci�n y establece el esquema de autenticaci�n predeterminado como JWT Bearer.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime= true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(jwtSettings.Secret)),
        };
    });

// Se configura globalmente el Token de seguridad en todos los Controllers.
// Esto obliga a las rutas a recibir un Token.
builder.Services.AddMvc(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

var app = builder.Build();

app.UseExceptionHandler(builder =>
{
    builder.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var error = context.Features.Get<IExceptionHandlerFeature>();
        if (error != null)
        {
            var exceptionType = error.GetType();

            var exceptionHandlerPathFeature =
                    context.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error is CdisException)
            {
                bool unathorized = exceptionHandlerPathFeature?.Error is UnathorizedException;

                context.Response.StatusCode = unathorized ? 401 : 409;
                context.Response.AddApplicationError(((CdisException)error.Error).ErrorMessage);
                await context.Response.WriteAsync(((CdisException)error.Error).ErrorMessage);
            }
            else
            {
                string errorMessageDefault = "Ocurri� un error inesperado, favor de contactar al administrador del sistema";
                Logger.WriteError(exceptionHandlerPathFeature.Error, app.Environment);

                context.Response.AddApplicationError(errorMessageDefault);
                await context.Response.WriteAsync(errorMessageDefault);
            }
        }
    });
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyHeader()
    .WithMethods("GET", "POST", "PUT", "DELETE");
});
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

if (!app.Environment.IsDevelopment())
{
    app.UseDefaultFiles();
    app.UseStaticFiles();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapFallbackToController("Index", "Fallback");
    });
}

app.Run();
