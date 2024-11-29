
using BookStoreAPI.Models;
using BookStoreAPI.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookStoreAPI
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
            builder.Services.AddSwaggerGen(op=>op.EnableAnnotations());
            builder.Services.AddDbContext<BookStoreContext>(op => op.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("bookcon")));
            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<BookStoreContext>();
            builder.Services.AddAuthentication(option => {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
    .AddJwtBearer(
    //validate token
    op =>
    {
        op.SaveToken = true;
        #region secret key
        string key = "welcome to my secret key mohamed elshafie";
        var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        #endregion
        op.TokenValidationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = secertkey,
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

            builder.Services.AddScoped<UnitOFWork>();






            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
