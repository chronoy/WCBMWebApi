﻿using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public class LoopFlowContrastRespository : ILoopFlowContrastRespository
    {
        private readonly SQLServerDBContext _context;
        public LoopFlowContrastRespository(SQLServerDBContext context)
        {
            _context = context;
        }

        public List<LoopFlowContrastConfig> GetLoopFlowContrastConfigs(int stationID, List<int> contrastStates, DateTime beginDateTime, DateTime endDateTime)
        {
            var configs = (from config in _context.LoopFlowContrastConfigs.Where(config => config.StationID == stationID
                                                                                         && contrastStates.Contains(config.ContrastStateID)
                                                                                         && DateTime.Compare(config.StartDateTime, beginDateTime) >= 0
                                                                                         && DateTime.Compare(config.StartDateTime, endDateTime) <= 0)
                           join station in _context.Stations
                           on config.StationID equals station.ID
                           join inUseLoop in _context.StationLoops
                           on config.InUseLoopID equals inUseLoop.ID into inUseLoops
                           from inUseLoop in inUseLoops.DefaultIfEmpty()
                           join contrastLoop in _context.StationLoops
                           on config.ContrastLoopID equals contrastLoop.ID into contrastLoops
                           from contrastLoop in contrastLoops.DefaultIfEmpty()
                           select new LoopFlowContrastConfig
                           {
                               ID = config.ID,
                               StationID = config.StationID,
                               InUseLoopID = config.InUseLoopID,
                               ContrastLoopID = config.ContrastLoopID,
                               ContrastStateID = config.ContrastStateID,
                               StartDateTime = config.StartDateTime,
                               EndDateTime = config.EndDateTime,
                               StationName = station.Name,
                               InUseLoopName = inUseLoop.AbbrName,
                               ContrastLoopName = contrastLoop.AbbrName,
                               ContrastState = config.ContrastStateID == 1 ? "完成对比" : "对比中",
                               FlowmeterModel= inUseLoop.FlowmeterModel,
                               FlowmeterManufacturer= inUseLoop.FlowmeterManufacturer
                           }).ToList();
            return configs;
        }

        public string AddLoopFlowContrastConfig(LoopFlowContrastConfig entity)
        {
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                entity.StartDateTime = DateTime.Now;
                entity.ContrastStateID = 0;
                _context.LoopFlowContrastConfigs.Add(entity);
                _context.SaveChanges();
                _context.Entry(entity);
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return "OtherError";
            }
            return "OK";
        }

        public string UpdateLoopFlowContrastConfig(LoopFlowContrastConfig entity)
        {
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                _context.LoopFlowContrastConfigs.Update(entity);
                _context.SaveChanges();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();

                return "OtherError";
            }
            return "OK";
        }

        public bool DeleteLoopFlowContrastConfig(int configID)
        {
            bool result = false;
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                LoopFlowContrastConfig config = _context.LoopFlowContrastConfigs.First(x => x.ID == configID);
                _context.LoopFlowContrastConfigs.Remove(config);
                result = _context.SaveChanges() > 0;
                tran.Commit();
            }
            catch (Exception)
            {
                tran.Rollback();
                return result;
            }
            return result;
        }

        public LoopFlowContrastConfig GetLoopFlowContrastConfig(int configID)
        {
            return _context.LoopFlowContrastConfigs.Where(config => config.ID == configID).First();
        }

        public List<LoopFlowContrastTag> GetLoopFlowContrastTags(int LoopID, string IFIXNodeName)
        {
            return (from tag in _context.LoopFlowContrastTags
                    join loop in _context.StationLoops.Where(loop => loop.ID == LoopID)
                    on tag.CollectDataTypeID equals loop.CollectDataTypeID
                    join station in _context.Stations
                    on loop.StationID equals station.ID
                    select new LoopFlowContrastTag
                    {
                        ID = loop.ID,
                        Name = tag.Name,
                        Address = IFIXNodeName + ":" + station.AbbrName + "_" + loop.AbbrName + "_" + tag.Address,
                        CollectDataTypeID = loop.CollectDataTypeID
                    }).ToList();


        }
        public string FinishLoopFlowContrastConfig(LoopFlowContrastConfig config, LoopFlowContrastRecord record)
        {
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                _context.LoopFlowContrastRecords.Add(record);
                _context.LoopFlowContrastConfigs.Update(config);
                _context.SaveChanges();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return ex.Message;
            }
            return "OK";
        }

        public LoopFlowContrastRecord GetLoopFlowContrastRecord(int configID)
        {
            try
            {
                return _context.LoopFlowContrastRecords.Where(record => record.LoopFlowContrastConfigID == configID).First();
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
