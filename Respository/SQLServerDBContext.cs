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
            modelBuilder.Entity<HistoricalCheckDataDanielVOSChartData>().HasKey(c => new { c.ReportID, c.Datetime });
            modelBuilder.Entity<HistoricalCheckDataSickVOSChartData>().HasKey(c => new { c.ReportID, c.Datetime });
            modelBuilder.Entity<HistoricalCheckDataElsterVOSChartData>().HasKey(c => new { c.ReportID, c.Datetime });
            modelBuilder.Entity<HistoricalCheckDataWeiseVOSChartData>().HasKey(c => new { c.ReportID, c.Datetime });
            modelBuilder.Entity<HistoricalCheckDataRMGVOSChartData>().HasKey(c => new { c.ReportID, c.Datetime });
        }
        public DbSet<Collector> Collectors { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Company> companies { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<StationEquipment> StationEquipments { get; set; }
        public DbSet<StationEquipmentDiagnosticData> StationEquipmentDiagnosticDatas { get; set; }

        public DbSet<StationLoop> StationLoops { get; set; }
        public DbSet<StationLoopDiagnosticData> StationLoopDiagnosticDatas { get; set; }
        public DbSet<StationDeviceCollectDataType> StationDeviceCollectDataTypes { get; set; }

        public DbSet<Trend> Trends { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<RealtimeAlarm> RealtimeAlarms { get; set; }
        public DbSet<HistoricalAlarm> HistoricalAlarms { get; set; }
        public DbSet<DiagnosticDataDetail> DiagnosticDataDetails { get; set; }
        public DbSet<DiagnosticAlarm> DiagnosticAlarms { get; set; }
        public DbSet<DiagnosticResultDescription> DiagnosticResultDescriptions { get; set; }
        public DbSet<DiagnosticStatusDescription> DiagnosticStatusDescriptions { get; set; }
        public DbSet<RealtimeDiagnosticAlarm> RealtimeDiagnosticAlarms { get; set; }
        public DbSet<AlarmKPI> AlarmKPIs { get; set; }
        //Loop Check Data
        public DbSet<HistoricalDanielVOSCheckData> HistoricalDanielVOSCheckDatas { get; set; }
        public DbSet<HistoricalCheckDataDanielVOSChartData> HistoricalCheckDataDanielVOSChartDatas { get; set; }
        public DbSet<HistoricalDanielFRCheckData> HistoricalDanielFRCheckDatas { get; set; }
        public DbSet<HistoricalDanielLoopCheckData> HistoricalDanielLoopCheckDatas { get; set; }
        public DbSet<HistoricalElsterVOSCheckData> HistoricalElsterVOSCheckDatas { get; set; }
        public DbSet<HistoricalCheckDataElsterVOSChartData> HistoricalCheckDataElsterVOSChartDatas { get; set; }
        public DbSet<HistoricalElsterFRCheckData> HistoricalElsterFRCheckDatas { get; set; }
        public DbSet<HistoricalElsterLoopCheckData> HistoricalElsterLoopCheckDatas { get; set; }
        public DbSet<HistoricalSickVOSCheckData> HistoricalSickVOSCheckDatas { get; set; }
        public DbSet<HistoricalCheckDataSickVOSChartData> HistoricalCheckDataSickVOSChartDatas { get; set; }
        public DbSet<HistoricalSickFRCheckData> HistoricalSickFRCheckDatas { get; set; }
        public DbSet<HistoricalSickLoopCheckData> HistoricalSickLoopCheckDatas { get; set; }
        public DbSet<HistoricalWeiseVOSCheckData> HistoricalWeiseVOSCheckDatas { get; set; }
        public DbSet<HistoricalCheckDataWeiseVOSChartData> HistoricalCheckDataWeiseVOSChartDatas { get; set; }
        public DbSet<HistoricalWeiseFRCheckData> HistoricalWeiseFRCheckDatas { get; set; }
        public DbSet<HistoricalWeiseLoopCheckData> HistoricalWeiseLoopCheckDatas { get; set; }
        public DbSet<HistoricalRMGVOSCheckData> HistoricalRMGVOSCheckDatas { get; set; }
        public DbSet<HistoricalCheckDataRMGVOSChartData> HistoricalCheckDataRMGVOSChartDatas { get; set; }
        public DbSet<HistoricalRMGFRCheckData> HistoricalRMGFRCheckDatas { get; set; }
        public DbSet<HistoricalRMGLoopCheckData> HistoricalRMGLoopCheckDatas { get; set; }

        public DbSet<RealtimeDanielVOSCheckData> RealtimeDanielVOSCheckDatas { get; set; }
        public DbSet<RealtimeDanielFRCheckData> RealtimeDanielFRCheckDatas { get; set; }
        public DbSet<RealtimeDanielLoopCheckData> RealtimeDanielLoopCheckDatas { get; set; }
        public DbSet<RealtimeCheckDataDanielVOSChartData> RealtimeCheckDataDanielVOSChartDatas { get; set; }

        public DbSet<RealtimeElsterVOSCheckData> RealtimeElsterVOSCheckDatas { get; set; }
        public DbSet<RealtimeElsterFRCheckData> RealtimeElsterFRCheckDatas { get; set; }
        public DbSet<RealtimeElsterLoopCheckData> RealtimeElsterLoopCheckDatas { get; set; }
        public DbSet<RealtimeCheckDataElsterVOSChartData> RealtimeCheckDataElsterVOSChartDatas { get; set; }

        public DbSet<RealtimeSickVOSCheckData> RealtimeSickVOSCheckDatas { get; set; }
        public DbSet<RealtimeSickFRCheckData> RealtimeSickFRCheckDatas { get; set; }
        public DbSet<RealtimeSickLoopCheckData> RealtimeSickLoopCheckDatas { get; set; }
        public DbSet<RealtimeCheckDataSickVOSChartData> RealtimeCheckDataSickVOSChartDatas { get; set; }

        public DbSet<RealtimeWeiseVOSCheckData> RealtimeWeiseVOSCheckDatas { get; set; }
        public DbSet<RealtimeWeiseFRCheckData> RealtimeWeiseFRCheckDatas { get; set; }
        public DbSet<RealtimeWeiseLoopCheckData> RealtimeWeiseLoopCheckDatas { get; set; }
        public DbSet<RealtimeCheckDataWeiseVOSChartData> RealtimeCheckDataWeiseVOSChartDatas { get; set; }

        public DbSet<RealtimeRMGVOSCheckData> RealtimeRMGVOSCheckDatas { get; set; }
        public DbSet<RealtimeRMGFRCheckData> RealtimeRMGFRCheckDatas { get; set; }
        public DbSet<RealtimeRMGLoopCheckData> RealtimeRMGLoopCheckDatas { get; set; }
        public DbSet<RealtimeCheckDataRMGVOSChartData> RealtimeCheckDataRMGVOSChartDatas { get; set; }

        //Equipment Check Data
        public DbSet<HistoricalABBGCCheckData> HistoricalABBGCCheckDatas { get; set; }
        public DbSet<HistoricalDanielGCCheckData> HistoricalDanielGCCheckDatas { get; set; }
        public DbSet<HistoricalElsterGCCheckData> HistoricalElsterGCCheckDatas { get; set; }

        public DbSet<CheckDescriptionStatus> CheckDescriptionStatuses { get; set; }
        //public DbSet<VOSKeyCheckData> VOSKeyCheckDatas { get; set; }
        // public DbSet<LoopUncertain> LoopUncertains { get; set; }
        // public DbSet<DataItem> FlowrateData { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<UserStation> UserStations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TrendGroup> TrendGroups { get; set; }
        public DbSet<TrendTag> TrendTags { get; set; }
        //public DbSet<DataItem> DataItems { get; set; }
        public DbSet<HistoricalProductionDailyReport> HistoricalProductionDailyReports { get; set; }
        //public DbSet<ExpertKnowledge> ExpertKnowledges { get; set; }

        public DbSet<UserLogRecord> UserLogRecords { get; set; }

        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentCategory> EquipmentCategories { get; set; }
        public DbSet<FlowmeterType> FlowmeterTypes { get; set; }
    }
}

