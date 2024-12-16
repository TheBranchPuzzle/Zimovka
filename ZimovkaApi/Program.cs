using System.Data;
using Zimovka.Core.Data;
using Zimovka.Data;
using Zimovka.Model;
using Zimovka.Presenter;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add services to the container.
builder.Services.AddScoped<RegionOutput>();
builder.Services.AddScoped(s => new DBconnection(builder.Configuration.GetValue<string>("DBstring:ConnectionString")));
builder.Services.AddScoped<IStorage, DBStorage>();
builder.Services.AddScoped<ISaveManager, DBSaveManager>();
builder.Services.AddScoped<IBlogic, BLogic>();
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
