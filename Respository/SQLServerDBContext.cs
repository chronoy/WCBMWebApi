using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Text;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Respository
{
    public class SQLServerDBContext : DbContext
    {
        public SQLServerDBContext(DbContextOptions<SQLServerDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistoricalDanielCheckDataVOSChartData>().HasKey(c => new { c.ReportID, c.Datetime });
            modelBuilder.Entity<HistoricalSickCheckDataVOSChartData>().HasKey(c => new { c.ReportID, c.Datetime });
            modelBuilder.Entity<HistoricalElsterCheckDataVOSChartData>().HasKey(c => new { c.ReportID, c.Datetime });
            modelBuilder.Entity<HistoricalWeiseCheckDataVOSChartData>().HasKey(c => new { c.ReportID, c.Datetime });
            modelBuilder.Entity<HistoricalRMGCheckDataVOSChartData>().HasKey(c => new { c.ReportID, c.Datetime });
        }
        public DbSet<Collector> Collectors { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Company> companies { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<StationEquipment> StationEquipments { get; set; }
        public DbSet<StationLoop> StationLoops { get; set; }

        public DbSet<StationDeviceCollectDataType> StationDeviceCollectDataTypes { get; set; }

        //Trend
        public DbSet<Trend> Trends { get; set; }
        public DbSet<TrendTag> TrendTags { get; set; }

       //Diagnostic
        public DbSet<DiagnosticResultDescription> DiagnosticResultDescriptions { get; set; }
        public DbSet<DiagnosticTTResultDescription> DiagnosticTTResultDescriptions { get; set; }
        public DbSet<DiagnosticPTResultDescription> DiagnosticPTResultDescriptions { get; set; }
        public DbSet<DiagnosticFMResultDescription> DiagnosticFMResultDescriptions { get; set; }
        public DbSet<DiagnosticFCResultDescription> DiagnosticFCResultDescriptions { get; set; }
        public DbSet<DiagnosticVOSResultDescription> DiagnosticVOSResultDescriptions { get; set; }
        public DbSet<DiagnosticGCResultDescription> DiagnosticGCResultDescriptions { get; set; }
        public DbSet<DiagnosticStatusDescription> DiagnosticStatusDescriptions { get; set; }
        public DbSet<StationLoopDiagnosticData> StationLoopDiagnosticDatas { get; set; }
        public DbSet<StationEquipmentDiagnosticData> StationEquipmentDiagnosticDatas { get; set; }
        public DbSet<DanielTTDiagnosticDataDetail> DanielTTDiagnosticDataDetails { get; set; }
        public DbSet<DanielPTDiagnosticDataDetail> DanielPTDiagnosticDataDetails { get; set; }
        public DbSet<DanielFMDiagnosticDataDetail> DanielFMDiagnosticDataDetails { get; set; }
        public DbSet<DanielFCDiagnosticDataDetail> DanielFCDiagnosticDataDetails { get; set; }
        public DbSet<DanielVOSDiagnosticDataDetail> DanielVOSDiagnosticDataDetails { get; set; }
        public DbSet<ElsterTTDiagnosticDataDetail> ElsterTTDiagnosticDataDetails { get; set; }
        public DbSet<ElsterPTDiagnosticDataDetail> ElsterPTDiagnosticDataDetails { get; set; }
        public DbSet<ElsterFMDiagnosticDataDetail> ElsterFMDiagnosticDataDetails { get; set; }
        public DbSet<ElsterFCDiagnosticDataDetail> ElsterFCDiagnosticDataDetails { get; set; }
        public DbSet<ElsterVOSDiagnosticDataDetail> ElsterVOSDiagnosticDataDetails { get; set; }
        public DbSet<SickTTDiagnosticDataDetail> SickTTDiagnosticDataDetails { get; set; }
        public DbSet<SickPTDiagnosticDataDetail> SickPTDiagnosticDataDetails { get; set; }
        public DbSet<SickFMDiagnosticDataDetail> SickFMDiagnosticDataDetails { get; set; }
        public DbSet<SickFCDiagnosticDataDetail> SickFCDiagnosticDataDetails { get; set; }
        public DbSet<SickVOSDiagnosticDataDetail> SickVOSDiagnosticDataDetails { get; set; }
        public DbSet<WeiseTTDiagnosticDataDetail> WeiseTTDiagnosticDataDetails { get; set; }
        public DbSet<WeisePTDiagnosticDataDetail> WeisePTDiagnosticDataDetails { get; set; }
        public DbSet<WeiseFMDiagnosticDataDetail> WeiseFMDiagnosticDataDetails { get; set; }
        public DbSet<WeiseFCDiagnosticDataDetail> WeiseFCDiagnosticDataDetails { get; set; }
        public DbSet<WeiseVOSDiagnosticDataDetail> WeiseVOSDiagnosticDataDetails { get; set; }
        public DbSet<RMGTTDiagnosticDataDetail> RMGTTDiagnosticDataDetails { get; set; }
        public DbSet<RMGPTDiagnosticDataDetail> RMGPTDiagnosticDataDetails { get; set; }
        public DbSet<RMGFMDiagnosticDataDetail> RMGFMDiagnosticDataDetails { get; set; }
        public DbSet<RMGFCDiagnosticDataDetail> RMGFCDiagnosticDataDetails { get; set; }
        public DbSet<RMGVOSDiagnosticDataDetail> RMGVOSDiagnosticDataDetails { get; set; }
        public DbSet<ABBGCDiagnosticDataDetail> ABBGCDiagnosticDataDetails { get; set; }
        public DbSet<DanielGCDiagnosticDataDetail> DanielGCDiagnosticDataDetails { get; set; }
        public DbSet<ElsterGCDiagnosticDataDetail> ElsterGCDiagnosticDataDetails { get; set; }

        //Alarm
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<RealtimeAlarm> RealtimeAlarms { get; set; }
        public DbSet<HistoricalAlarm> HistoricalAlarms { get; set; }
        public DbSet<DiagnosticAlarm> DiagnosticAlarms { get; set; }
        public DbSet<AlarmKPI> AlarmKPIs { get; set; }
        
        //Loop Check Data
        public DbSet<HistoricalDanielVOSCheckData> HistoricalDanielVOSCheckDatas { get; set; }
        public DbSet<HistoricalDanielCheckDataVOSChartData> HistoricalCheckDataDanielVOSChartDatas { get; set; }
        public DbSet<HistoricalDanielFRCheckData> HistoricalDanielFRCheckDatas { get; set; }
        public DbSet<HistoricalDanielLoopCheckData> HistoricalDanielLoopCheckDatas { get; set; }
        public DbSet<HistoricalElsterVOSCheckData> HistoricalElsterVOSCheckDatas { get; set; }
        public DbSet<HistoricalElsterCheckDataVOSChartData> HistoricalCheckDataElsterVOSChartDatas { get; set; }
        public DbSet<HistoricalElsterFRCheckData> HistoricalElsterFRCheckDatas { get; set; }
        public DbSet<HistoricalElsterLoopCheckData> HistoricalElsterLoopCheckDatas { get; set; }
        public DbSet<HistoricalSickVOSCheckData> HistoricalSickVOSCheckDatas { get; set; }
        public DbSet<HistoricalSickCheckDataVOSChartData> HistoricalCheckDataSickVOSChartDatas { get; set; }
        public DbSet<HistoricalSickFRCheckData> HistoricalSickFRCheckDatas { get; set; }
        public DbSet<HistoricalSickLoopCheckData> HistoricalSickLoopCheckDatas { get; set; }
        public DbSet<HistoricalWeiseVOSCheckData> HistoricalWeiseVOSCheckDatas { get; set; }
        public DbSet<HistoricalWeiseCheckDataVOSChartData> HistoricalCheckDataWeiseVOSChartDatas { get; set; }
        public DbSet<HistoricalWeiseFRCheckData> HistoricalWeiseFRCheckDatas { get; set; }
        public DbSet<HistoricalWeiseLoopCheckData> HistoricalWeiseLoopCheckDatas { get; set; }
        public DbSet<HistoricalRMGVOSCheckData> HistoricalRMGVOSCheckDatas { get; set; }
        public DbSet<HistoricalRMGCheckDataVOSChartData> HistoricalCheckDataRMGVOSChartDatas { get; set; }
        public DbSet<HistoricalRMGFRCheckData> HistoricalRMGFRCheckDatas { get; set; }
        public DbSet<HistoricalRMGLoopCheckData> HistoricalRMGLoopCheckDatas { get; set; }

        public DbSet<RealtimeDanielVOSCheckData> RealtimeDanielVOSCheckDatas { get; set; }
        public DbSet<RealtimeDanielFRCheckData> RealtimeDanielFRCheckDatas { get; set; }
        public DbSet<RealtimeDanielLoopCheckData> RealtimeDanielLoopCheckDatas { get; set; }
        public DbSet<RealtimeDanielCheckDataVOSChartData> RealtimeCheckDataDanielVOSChartDatas { get; set; }

        public DbSet<RealtimeElsterVOSCheckData> RealtimeElsterVOSCheckDatas { get; set; }
        public DbSet<RealtimeElsterFRCheckData> RealtimeElsterFRCheckDatas { get; set; }
        public DbSet<RealtimeElsterLoopCheckData> RealtimeElsterLoopCheckDatas { get; set; }
        public DbSet<RealtimeElsterCheckDataVOSChartData> RealtimeCheckDataElsterVOSChartDatas { get; set; }

        public DbSet<RealtimeSickVOSCheckData> RealtimeSickVOSCheckDatas { get; set; }
        public DbSet<RealtimeSickFRCheckData> RealtimeSickFRCheckDatas { get; set; }
        public DbSet<RealtimeSickLoopCheckData> RealtimeSickLoopCheckDatas { get; set; }
        public DbSet<RealtimeSickCheckDataVOSChartData> RealtimeCheckDataSickVOSChartDatas { get; set; }

        public DbSet<RealtimeWeiseVOSCheckData> RealtimeWeiseVOSCheckDatas { get; set; }
        public DbSet<RealtimeWeiseFRCheckData> RealtimeWeiseFRCheckDatas { get; set; }
        public DbSet<RealtimeWeiseLoopCheckData> RealtimeWeiseLoopCheckDatas { get; set; }
        public DbSet<RealtimeWeiseCheckDataVOSChartData> RealtimeCheckDataWeiseVOSChartDatas { get; set; }

        public DbSet<RealtimeRMGVOSCheckData> RealtimeRMGVOSCheckDatas { get; set; }
        public DbSet<RealtimeRMGFRCheckData> RealtimeRMGFRCheckDatas { get; set; }
        public DbSet<RealtimeRMGLoopCheckData> RealtimeRMGLoopCheckDatas { get; set; }
        public DbSet<RealtimeRMGCheckDataVOSChartData> RealtimeCheckDataRMGVOSChartDatas { get; set; }


        //Equipment Check Data
        public DbSet<HistoricalABBGCCheckData> HistoricalABBGCCheckDatas { get; set; }
        public DbSet<HistoricalDanielGCCheckData> HistoricalDanielGCCheckDatas { get; set; }
        public DbSet<HistoricalElsterGCCheckData> HistoricalElsterGCCheckDatas { get; set; }

        public DbSet<GCRepeatabilityCheckData> HistoricalGCRepeatabilityCheckDatas { get; set; }


        public DbSet<CheckDescriptionStatus> CheckDescriptionStatuses { get; set; }
        //public DbSet<VOSKeyCheckData> VOSKeyCheckDatas { get; set; }
        // public DbSet<LoopUncertain> LoopUncertains { get; set; }
        // public DbSet<DataItem> FlowrateData { get; set; }

        //User
        public DbSet<User> Users { get; set; }
        public DbSet<UserStation> UserStations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserLogRecord> UserLogRecords { get; set; }

        //public DbSet<DataItem> DataItems { get; set; }

        //Product Report
        public DbSet<HistoricalProductionDailyReport> HistoricalProductionDailyReports { get; set; }
        //public DbSet<ExpertKnowledge> ExpertKnowledges { get; set; }

        //Equipment
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentCategory> EquipmentCategories { get; set; }
        public DbSet<EquipmentCompany> EquipmentCompanies { get; set; }
        public DbSet<EquipmentLine> EquipmentLines { get; set; }
        public DbSet<EquipmentStation> EquipmentStations { get; set; }
        public DbSet<EquipmentModel> EquipmentModels { get; set; }
        public DbSet<EquipmentAccuracy> EquipmentAccuracies { get; set; }
        public DbSet<EquipmentPressureClass> EquipmentPressureClasses { get; set; }
        public DbSet<EquipmentManufacturer> EquipmentManufacturers { get; set; }

        // Equipment Metering Certificate
        public DbSet<EquipmentMeteringCertificate> EquipmentMeteringCertificates { get; set; }
        public DbSet<EquipmentMeteringCheckedData> EquipmentMeteringCheckedDatas { get; set; }
        public DbSet<EquipmentMeteringDetectingEquipment> EquipmentMeteringDetectingEquipment { get; set; }
        public DbSet<EquipmentMeteringResultData> EquipmentMeteringResultDatas { get; set; }

        // Reference Material Certificate
        public DbSet<ReferenceMaterialCertificate> ReferenceMaterialCertificates { get; set; }

        //Loop Flow Contrast
        public DbSet<LoopFlowContrastConfig> LoopFlowContrastConfigs { get; set; }

    }
}

