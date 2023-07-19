
using Microsoft.EntityFrameworkCore;
using PaymentProcessorServer.Data;

namespace PaymentProcessorServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<PaymentsContext>(options => 
                options.UseInMemoryDatabase("PaymentsDb"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                try
                {
                    using (var scope = app.Services.CreateScope())
                    {
                        var salesContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
                        salesContext.Database.EnsureCreated();
                        salesContext.Seed();
                    }
                }
                catch (Exception ex)
                {

                }
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}