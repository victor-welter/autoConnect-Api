
using auto_connect_api.Data;
using auto_connect_api.interfaces;
using auto_connect_api.repositories;
using Microsoft.EntityFrameworkCore;

namespace auto_connect_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Adicione serviços ao contêiner.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin() // Permitir todas as origens
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure o DbContext com PostgreSQL
            builder.Services.AddDbContext<ConnectionContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddTransient<IVeiculoRepository, VeiculoRepository>();
            builder.Services.AddTransient<IMarcaRepository, MarcaRepository>();
            builder.Services.AddTransient<IModeloRepository, ModeloRepository>();
            builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            builder.Services.AddTransient<ILocalRepository, LocalRepository>();
            builder.Services.AddTransient<ITipoCombustivelRepository, TipoCombustivelRepository>();
            builder.Services.AddTransient<ITipoDespesaRepository, TipoDespesaRepository>();
            builder.Services.AddTransient<ITipoProblemaRepository, TipoProblemaRepository>();
            builder.Services.AddTransient<IDespesaRepository, DespesaRepository>();
            builder.Services.AddTransient<INotificacaoRepository, NotificacaoRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Middleware CORS
            app.Use((context, next) =>
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
                context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");

                // Se a requisição for OPTIONS, retorne 204 imediatamente
                if (context.Request.Method == HttpMethods.Options)
                {
                    context.Response.StatusCode = StatusCodes.Status204NoContent;
                    return Task.CompletedTask;
                }

                return next();
            });

            app.UseCors("AllowAllOrigins"); // Aplica a política CORS definida

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}