
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace jwt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //step2: regidter the auth schema
            builder.Services.AddAuthentication(op => op.DefaultAuthenticateScheme="myschema")
                .AddJwtBearer(
                "myschema", //same name as the name of default auth shema
                option =>
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret key secret key secret key secret key secret key secret key secret key"));

                    //validate token
                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        IssuerSigningKey = secretKey, // assigning the validation key to be same as the secret key used in hashing
                        ValidateIssuer = false, // its default is true and its used when we use server to authenticate
                        ValidateAudience = false, // its default is true and its used when we use server to authenticate
                        ValidateLifetime = true
                    };
                }
                );

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
