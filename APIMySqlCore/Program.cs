using WebApiCore.Data;
using WebApiCore.Data.Repositories;
using WebApiCore.Data.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MySqlConnectioConfig = new MySqlConfig(builder.Configuration.GetConnectionString("DevMySqlConnection"));
builder.Services.AddSingleton(MySqlConnectioConfig);

builder.Services.AddScoped<IPatientRepository, PatientRepository>();

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
