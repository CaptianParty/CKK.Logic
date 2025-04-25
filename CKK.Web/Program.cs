using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using CKK.DB.Interfaces;
using CKK.DB.UOW;

namespace CKK.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<DatabaseConnectionFactory>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(sp => 
            {
                var facotry = sp.GetRequiredService<DatabaseConnectionFactory>();
                return new UnitOfWork(facotry);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }


            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
