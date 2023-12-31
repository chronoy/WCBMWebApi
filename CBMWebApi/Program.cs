using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Respository;
using Services;
using Services.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<SQLServerDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));
builder.Services.AddScoped<ICheckRespository, CheckRespository>();
builder.Services.AddScoped<ICheckService, CheckService>();
builder.Services.AddSingleton<IPDBRespository, PDBRespository>();

// Cors��������
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

//Yantao
builder.Services.AddSingleton<IPDBService, PDBService>();
builder.Services.AddSingleton<IOPCClientService, OPCClientService>();
builder.Services.AddHostedService<LifetimeEventsHostService>();
builder.Services.AddScoped<IStationRespository, StationRespository>();
builder.Services.AddScoped<IStationService, StationService>();
builder.Services.AddScoped<IStationLoopRespository, StationLoopRespository>();
builder.Services.AddScoped<IStationLoopService, StationLoopService>();
builder.Services.AddScoped<IStationEquipmentRespository, StationEquipmentRespository>();
builder.Services.AddScoped<IStationEquipmentService, StationEquipmentService>();
builder.Services.AddScoped<IKeyParameterRespository, KeyParameterRespository>();
builder.Services.AddScoped<IKeyParameterService, KeyParameterService>();

//by luoyuankan
builder.Services.AddScoped<IAlarmRespository, AlarmRespository>();
builder.Services.AddScoped<IAlarmService, AlarmService>();
builder.Services.AddScoped<IHistoricalTrendRespository, HistoricalTrendRespository>();
builder.Services.AddScoped<IHistoricalTrendService, HistoricalTrendService>();
builder.Services.AddScoped<IProductionReportRespository, ProductionReportRespository>();
builder.Services.AddScoped<IProductionReportService, ProductionReportService>();
builder.Services.AddScoped<IDiagnosisRespository, DiagnosisRespository>();
builder.Services.AddScoped<IDiagnosisService, DiagnosisService>();
builder.Services.AddScoped<IReferenceMaterialCertificateRespository, ReferenceMaterialCertificateRespository>();
builder.Services.AddScoped<IReferenceMaterialCertificateService, ReferenceMaterialCertificateService>();
builder.Services.AddScoped<IEquipmentMeteringCertificateRespository, EquipmentMeteringCertificateRespository>();
builder.Services.AddScoped<IEquipmentMeteringCertificateService, EquipmentMeteringCertificateService>();
builder.Services.AddScoped<IExcelExportHelper, ExcelExportHelper>();
//hhj
builder.Services.AddScoped<ILoopFlowContrastService, LoopFlowContrastService>();
builder.Services.AddScoped<ILoopFlowContrastRespository, LoopFlowContrastRespository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(); //����Cors����

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
