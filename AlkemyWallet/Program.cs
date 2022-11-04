using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Services;
using AlkemyWallet.DataAccess;
using Microsoft.EntityFrameworkCore;
using AlkemyWallet.DataAccess.DataSeed;
using AlkemyWallet.Repositories.Interfaces;
using AlkemyWallet.Repositories;
using AlkemyWallet.Entities;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("WalletDbConn");

// Add services to the container.
builder.Services.AddDbContext<WalletDbContext>(o => o.UseSqlServer(connString));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRolesServices, RolesService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IFixedTermDepositServices, FixedTermDepositService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

SeedData(app);

app.MapControllers();

app.Run();


void SeedData(IApplicationBuilder app)
{
    var scopedFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<CatalogueDataSeeder>();
        service.Seed();
    }
}