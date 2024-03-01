using PayCorona.Data;
using PayCorona.Interface;
using PayCorona.Middleware;
using PayCorona.Repository;
using PayCorona.Servises;
using Microsoft.EntityFrameworkCore;

namespace PayCorona
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddLogging(x => x.AddConsole());
            builder.Services.AddTransient<Seed>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<IWalletRepository, WalletRepository>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IWalletService, WalletService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            //builder.Services.AddSingleton<SessionService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddHostedService<SessionService>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<DataContext>(options =>
            {
                // options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // microsoft sql server
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")); // postgresql
            });

            var app = builder.Build();

            MigrateDb(app);
            SeedData(app);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // app.UseAuthorization();
            app.UseMiddleware<LoggingMiddleware>();
            app.UseMiddleware<AuthMiddleware>();

            app.MapControllers();

            app.Run();
        }

        static void MigrateDb(IHost app)
        {
            var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopedFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<DataContext>();
                dbContext!.Database.Migrate();
            }
        }

        static void SeedData(IHost app)
        {
            var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopedFactory.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<Seed>();
                service.SeedDataContext();
            }
        }
    }
}