using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Services;
using AlkemyWallet.DataAccess;
using Microsoft.EntityFrameworkCore;
using AlkemyWallet.DataAccess.DataSeed;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using AlkemyWallet.Repositories.Interfaces;
using AlkemyWallet.Repositories;
using AlkemyWallet.Entities;

var allowAnyOrigins = "allowAnyOrigins";
var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("WalletDbConn");

// Add services to the container.
builder.Services.AddDbContext<WalletDbContext>(o => o.UseSqlServer(connString));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("JWTBearer", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme(\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});


builder.Services.AddScoped<IRolesServices, RolesService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IFixedTermDepositServices, FixedTermDepositService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddCors(options => {
    options.AddPolicy(name: allowAnyOrigins, builder =>
    {
        builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    });
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
        //.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("My clave personalizada para la app")),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
