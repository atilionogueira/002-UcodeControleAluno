using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ucode.Api.Data;
using Ucode.Api.Handlers;
using Ucode.Api.Models;
using Ucode.Core;
using Ucode.Core.Handlers;

namespace Ucode.Api.Common.Api
{
    public static class BuilderExtension
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            Configuration.ConnectionString =
                builder
                    .Configuration
                    .GetConnectionString("DefaultConnection")
                    ?? string.Empty;
            Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
            Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
        }
        public static void AddDocumentation(this WebApplicationBuilder builder) 
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.CustomSchemaIds(n => n.FullName);
            });
        }

        public static void AddSecurity(this WebApplicationBuilder builder) 
        {
            builder.Services
                .AddAuthentication(IdentityConstants.ApplicationScheme)
                .AddIdentityCookies(); // JWT Bearer
            builder.Services.AddAuthorization();
        }

        public static void AddDataContexts(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(
                x => { x.UseSqlServer(Configuration.ConnectionString); });          

            builder.Services
                .AddIdentityCore<User>()
                .AddRoles<IdentityRole<long>>()
                .AddEntityFrameworkStores<AppDbContext>() // Adicionar as tabelas no BD
                .AddApiEndpoints();
        }

    
        public static void AddCrossOrigin(this WebApplicationBuilder builder)
        {
            // Configura politica de permissão e restrição do api
            builder.Services.AddCors(
           options => options.AddPolicy(
               ApiConfiguration.CorsPolicyName,
               policy => policy
                   .WithOrigins([
                       Configuration.BackendUrl,
                       Configuration.FrontendUrl
                   ])
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials()
           ));
        }
        public static void AddSerives(this WebApplicationBuilder builder) 
        {
            // Injeção de Dependência
            builder.Services.AddTransient<IAlunoHandler, AlunoHandler>(); 
            builder.Services.AddTransient<ICursoHandler, CursoHandler>();
            builder.Services.AddTransient<IModuloHandler, ModuloHandler>();
            builder.Services.AddTransient<IControleAlunoHandler, ControleAlunoHandler>();
            builder.Services.AddTransient<IReportHandler, ReportHandler>();
        }
    }
}
