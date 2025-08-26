using System.Text;
using BlogX;
using BlogX.Data;
using BlogX.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder
.Services
.AddControllers()
.ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddDbContext<BlogXDataContext>();
builder.Services.AddTransient<TokenService>(); //Sempre cria uma nova instância
// builder.Services.AddScoped(); //Por transação
// builder.Services.AddSingleton(); // Um por app (padrão de design)


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization(); //A ordem importa! Sempre Authentication antes de Authorization
app.MapControllers();

app.Run();
