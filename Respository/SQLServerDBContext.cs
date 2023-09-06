﻿using System;
using System.Collections.Generic;
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
        {        }
       // public DbSet<Tag> Tags { get; set; }
       // public DbSet<Company> companies { get; set; }
       // public DbSet<Area> Areas { get; set; }
       // public DbSet<Station> Stations { get; set; }
       // public DbSet<StationEquipment> StationEquipments { get; set; }
       // public DbSet<StationEquipmentDiagnosticData> StationEquipmentDiagnosticDatas { get; set; }

       // public DbSet<StationLoop> StationLoops { get; set; }
       // public DbSet<StationLoopDiagnosticData> StationLoopDiagnosticDatas { get; set; }


       // public DbSet<Trend> Trends { get; set; }
       // public DbSet<Alarm> Alarms { get; set; }
       // public DbSet<DiagnosticDataDetail> DiagnosticDataDetails { get; set; }
       // public DbSet<DiagnosticAlarm> DiagnosticAlarms { get; set; }
       // public DbSet<AlarmKPI> AlarmKPIs { get; set; }
       ////Loop Check Data
       // public DbSet<StationLoopCheckData> LoopCheckDatas { get; set; }
       // //Equipment Check Data
       // public DbSet<ABBGCCheckData> ABBGCCheckDatas { get; set; }
       // public DbSet<DanielGCCheckData> DanielGCCheckDatas { get; set; }
       // public DbSet<ElsterGCCheckData> ElsterGCCheckDatas { get; set; }
       // //public DbSet<VOSKeyCheckData> VOSKeyCheckDatas { get; set; }
       // // public DbSet<LoopUncertain> LoopUncertains { get; set; }
       // // public DbSet<DataItem> FlowrateData { get; set; }

        public DbSet<User> Users { get; set; }
       // public DbSet<TrendGroup> TrendGroups { get; set; }
       // //public DbSet<DataItem> DataItems { get; set; }
       // //public DbSet<ProductionReport> ProductionReports { get; set; }
       // //public DbSet<ExpertKnowledge> ExpertKnowledges { get; set; }

       // //public DbSet<UserLogRecord> UserLogRecords { get; set; }

       // public DbSet<Equipment> Equipments { get; set; }
    }
}

