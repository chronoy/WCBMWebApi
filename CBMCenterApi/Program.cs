using Microsoft.EntityFrameworkCore;
using Respository;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<SQLServerDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
    builder =>
    {
        builder.SetIsOriginAllowed(origin => true)
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials();
    });
});

builder.Services.AddScoped<IUserRespository, UserRespository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleRespository, RoleRespository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IStationRespository, StationRespository>();
builder.Services.AddScoped<IStationService, StationService>();
builder.Services.AddScoped<IEquipmentRespository, EquipmentRespository>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IEquipmentParameterRespository, EquipmentParameterRespository>();
builder.Services.AddScoped<IEquipmentParameterService, EquipmentParameterService>();
builder.Services.AddScoped<IExcelExportHelper, ExcelExportHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
