using AirBnb_for_campers.Data;
using AirBnb_for_campers.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace AirBnb_for_campers
{
    public class Program
    {
        public static void Main(string[] args)
        {
           
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.ConfigureKestrel(options => {
                options.ListenAnyIP(5156);
                options.ListenAnyIP(7156, listenOptions => {
                    listenOptions.UseHttps();
                });
            });
            // Add Core services to the container.
            builder.Services.AddCors(s => s.AddPolicy("MyPolicy", builder => builder.AllowAnyOrigin()
                                                    .AllowAnyMethod()
                                                    .AllowAnyHeader())); 
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            /*
                        Rex: adding singletong
             */
            builder.Services.AddSingleton(typeof(ICampingSpot), typeof(CampingSpotData));
            builder.Services.AddSingleton(typeof(IUsers), typeof(UserData));
            builder.Services.AddSingleton(typeof(IOwners), typeof(OwnerDatabase));
            builder.Services.AddSingleton(typeof(IBooking), typeof(BookingData));
            builder.Services.AddSingleton(typeof(IRatingsAndComments), typeof(RateAndCommentData));      


            var app = builder.Build();
            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");
            app.UseStaticFiles();
            app.UseRouting();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

        }
    }
}