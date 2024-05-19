using AirBnb_for_campers.Data;
using AirBnb_for_campers.Models;
using Microsoft.Extensions.FileProviders;

namespace AirBnb_for_campers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /* This is an Integrated API with Login within the Same Application */

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
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
            //builder.Services.AddSingleton<IUsers, UserData>();

            var app = builder.Build();
            app.UseCors("MyPolicy");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //userprofile
            /*app.UseHttpsRedirection();
            app.UseStaticFiles();*/ // Enable serving static files (for profile pictures)

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

        }
    }
}