﻿using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Extensions.Configuration;


namespace Respository
{
    public class PDBRespository:IPDBRespository
    {
        private readonly SQLServerDBContext _context;
        public PDBRespository()
        {
            var Options = new DbContextOptionsBuilder<SQLServerDBContext>();
            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile($"appsettings.json")
                                    .Build();
            //var config = configuration.Build();
            //var connectionString = config.GetConnectionString("ConnectionString");

            //IConfiguration Configuration = new ConfigurationBuilder().Add(new Microsoft.Extensions.Configuration.Json.JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true }).Build();
            Options.UseSqlServer(configuration.GetConnectionString("SQLConnection"));
            _context = new SQLServerDBContext(Options.Options);
        }

        public List<PDBTag> GetPDBTag()
        {
            List<Tag> tags1 = _context.Tags.ToList();   
            List<PDBTag> tags=  (from tag in _context.Tags
                                join
                                loop in _context.StationLoops
                                on tag.StationDeviceCollectID equals loop.CollectDataTypeID
                                join
                                station in _context.Stations
                                on
                                loop.StationID equals station.ID
                                join
                                collector in _context.Collectors.Where(obj=>obj.IsUse == true)
                                on
                                station.CollectorID equals collector.ID
                                select new PDBTag
                                {
                                    Name=station.AbbrName + '_' +loop.AbbrName + '_' +tag.Name,
                                    Address=collector.IFixNodeName + '.' +station.AbbrName + '_' +loop.AbbrName + '_' +tag.Address,
                                    Value="????",
                                    Quality= "Uncertain"
                                }).ToList();
            return tags;                           
        }
    }
}