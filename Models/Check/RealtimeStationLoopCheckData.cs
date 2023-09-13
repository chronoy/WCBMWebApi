using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class RealtimeStationLoopCheckData
    {
        [Key]
        public int ID { get; set; }
        public DateTime Datetime { get; set; }

        public string CollectorName { get; set; }

        [Column("CheckDataStatus")]
        public int CheckDataStatusID { get; set; }
    }
    //Daniel
    [Table("tRealtimeCheckDataDanielVOS")]
    public class RealtimeDanielVOSCheckData : RealtimeStationLoopCheckData
    {
        public string CompanyName { get; set; }
        public double? CollectTimes { get; set; }
        public string FlowmeterProcessLocation { get; set; }
        public double? InsideDiameter { get; set; }
        public double? Pulse1FullFlowrate { get; set; }
        public double? Pulse1MaxFrequency { get; set; }
        public double? Pulse1KCoefficient { get; set; }
        public double? Pulse1InverseKCoefficient { get; set; }
        public double? Pulse2FullFlowrate { get; set; }
        public double? Pulse2MaxFrequency { get; set; }
        public double? Pulse2KCoefficient { get; set; }
        public double? Pulse2InverseKCoefficient { get; set; }
        public double? ForwardCurveCorrectedCoefficientC0 { get; set; }
        public double? ForwardCurveCorrectedCoefficientC1 { get; set; }
        public double? ForwardCurveCorrectedCoefficientC2 { get; set; }
        public double? ForwardCurveCorrectedCoefficientC3 { get; set; }
        public double? ForwardCorrectedFlowrateValue1 { get; set; }
        public double? ForwardCorrectedFlowrateValue2 { get; set; }
        public double? ForwardCorrectedFlowrateValue3 { get; set; }
        public double? ForwardCorrectedFlowrateValue4 { get; set; }
        public double? ForwardCorrectedFlowrateValue5 { get; set; }
        public double? ForwardCorrectedFlowrateValue6 { get; set; }
        public double? ForwardCorrectedFlowrateValue7 { get; set; }
        public double? ForwardCorrectedFlowrateValue8 { get; set; }
        public double? ForwardCorrectedFlowrateValue9 { get; set; }
        public double? ForwardCorrectedFlowrateValue10 { get; set; }
        public double? ForwardCorrectedFlowrateValue11 { get; set; }
        public double? ForwardCorrectedFlowrateValue12 { get; set; }
        public double? ForwardCorrectedCoefficient1 { get; set; }
        public double? ForwardCorrectedCoefficient2 { get; set; }
        public double? ForwardCorrectedCoefficient3 { get; set; }
        public double? ForwardCorrectedCoefficient4 { get; set; }
        public double? ForwardCorrectedCoefficient5 { get; set; }
        public double? ForwardCorrectedCoefficient6 { get; set; }
        public double? ForwardCorrectedCoefficient7 { get; set; }
        public double? ForwardCorrectedCoefficient8 { get; set; }
        public double? ForwardCorrectedCoefficient9 { get; set; }
        public double? ForwardCorrectedCoefficient10 { get; set; }
        public double? ForwardCorrectedCoefficient11 { get; set; }
        public double? ForwardCorrectedCoefficient12 { get; set; }
        public double? LowFlowRateCutOff { get; set; }
        public double? CorrectionMethod { get; set; }
        public string FlowDirection { get; set; }
        public double? PressureAvg { get; set; }
        public double? PressureMax { get; set; }
        public double? PressureMin { get; set; }
        public double? TemperatureAvg { get; set; }
        public double? TemperatureMax { get; set; }
        public double? TemperatureMin { get; set; }
        public double? Path1APerformanceAvg { get; set; }
        public double? Path1BPerformanceAvg { get; set; }
        public double? Path2APerformanceAvg { get; set; }
        public double? Path2BPerformanceAvg { get; set; }
        public double? Path3APerformanceAvg { get; set; }
        public double? Path3BPerformanceAvg { get; set; }
        public double? Path4APerformanceAvg { get; set; }
        public double? Path4BPerformanceAvg { get; set; }
        public double? PerformanceUpAverageAvg { get; set; }
        public double? PerformanceDownAverageAvg { get; set; }
        public double? PerformanceAverageAvg { get; set; }
        public double? PerformanceAverageAvgCheckResult { get; set; }
        public double? Path1AGainAvg { get; set; }
        public int? Path1AGainAvgCheckResult { get; set; }
        public double? Path1BGainAvg { get; set; }
        public int? Path1BGainAvgCheckResult { get; set; }
        public double? Path2AGainAvg { get; set; }
        public int? Path2AGainAvgCheckResult { get; set; }
        public double? Path2BGainAvg { get; set; }
        public int? Path2BGainAvgCheckResult { get; set; }
        public double? Path3AGainAvg { get; set; }
        public int? Path3AGainAvgCheckResult { get; set; }
        public double? Path3BGainAvg { get; set; }
        public int? Path3BGainAvgCheckResult { get; set; }
        public double? Path4AGainAvg { get; set; }
        public int? Path4AGainAvgCheckResult { get; set; }
        public double? Path4BGainAvg { get; set; }
        public int? Path4BGainAvgCheckResult { get; set; }
        public double? GainUpAverageAvg { get; set; }
        public int? GainUpAverageAvgCheckResult { get; set; }
        public double? GainDownAverageAvg { get; set; }
        public int? GainDownAverageAvgCheckResult { get; set; }
        public double? Path1ASNRAvg { get; set; }
        public int? Path1ASNRAvgCheckResult { get; set; }
        public double? Path1BSNRAvg { get; set; }
        public int? Path1BSNRAvgCheckResult { get; set; }
        public double? Path2ASNRAvg { get; set; }
        public int? Path2ASNRAvgCheckResult { get; set; }
        public double? Path2BSNRAvg { get; set; }
        public int? Path2BSNRAvgCheckResult { get; set; }
        public double? Path3ASNRAvg { get; set; }
        public int? Path3ASNRAvgCheckResult { get; set; }
        public double? Path3BSNRAvg { get; set; }
        public int? Path3BSNRAvgCheckResult { get; set; }
        public double? Path4ASNRAvg { get; set; }
        public double? Path4ASNRAvgCheckResult { get; set; }
        public double? Path4BSNRAvg { get; set; }
        public int? Path4BSNRAvgCheckResult { get; set; }
        public double? SNRUpAverageAvg { get; set; }
        public int? SNRUpAverageAvgCheckResult { get; set; }
        public double? SNRDownAverageAvg { get; set; }
        public int? SNRDownAverageAvgCheckResult { get; set; }
        public double? Path1VOSMax { get; set; }
        public double? Path1VOSMin { get; set; }
        public double? Path1VOSAvg { get; set; }
        public double? Path2VOSMax { get; set; }
        public double? Path2VOSMin { get; set; }
        public double? Path2VOSAvg { get; set; }
        public double? Path3VOSMax { get; set; }
        public double? Path3VOSMin { get; set; }
        public double? Path3VOSAvg { get; set; }
        public double? Path4VOSMax { get; set; }
        public double? Path4VOSMin { get; set; }
        public double? Path4VOSAvg { get; set; }
        public double? PathsVOSAverageMax { get; set; }
        public double? PathsVOSAverageMin { get; set; }
        public double? PathsVOSAverageAvg { get; set; }
        public double? FCCalculatedVOSDeviationRate { get; set; }
        public int? FCCalculatedVOSDeviationRateCheckResult { get; set; }
        public double? CalculatedVOSAvg { get; set; }
        public double? FCCalculatedVOSAvg { get; set; }
        public double? VOSCheckDeviationRate { get; set; }
        public int? VOSCheckDeviationRateCheckResult { get; set; }
        public double? Path1VOGMax { get; set; }
        public double? Path1VOGMin { get; set; }
        public double? Path1VOGAvg { get; set; }
        public double? Path2VOGMax { get; set; }
        public double? Path2VOGMin { get; set; }
        public double? Path2VOGAvg { get; set; }
        public double? Path3VOGMax { get; set; }
        public double? Path3VOGMin { get; set; }
        public double? Path3VOGAvg { get; set; }
        public double? Path4VOGMax { get; set; }
        public double? Path4VOGMin { get; set; }
        public double? Path4VOGAvg { get; set; }
        public double? PathsVOGAverageMax { get; set; }
        public double? PathsVOGAverageMin { get; set; }
        public double? PathsVOGAverageAvg { get; set; }
        public double? Path1VOGDeviationRate { get; set; }
        public double? Path2VOGDeviationRate { get; set; }
        public double? Path3VOGDeviationRate { get; set; }
        public double? Path4VOGDeviationRate { get; set; }
        public string Path1Status { get; set; }
        public string Path2Status { get; set; }
        public string Path3Status { get; set; }
        public string Path4Status { get; set; }
        public double? C1Avg { get; set; }
        public double? C2Avg { get; set; }
        public double? C3Avg { get; set; }
        public double? nC4Avg { get; set; }
        public double? iC4Avg { get; set; }
        public double? nC5Avg { get; set; }
        public double? iC5Avg { get; set; }
        public double? NeoC5Avg { get; set; }
        public double? C6Avg { get; set; }
        public double? N2Avg { get; set; }
        public double? CO2Avg { get; set; }
        public double? ComponentSum { get; set; }
        public double? ProfileFactorAvg { get; set; }
        public int? ProfileFactorAvgCheckResult { get; set; }
        public double? SwirlAngleAvg { get; set; }
        public int? SwirlAngleAvgCheckResult { get; set; }
        public double? GrossFlowrateAvg { get; set; }
        public double? StandardFlowrateAvg { get; set; }
        public double? MassFlowrateAvg { get; set; }
        public double? EnergyFlowrateAvg { get; set; }
        public double? VOSAvgMaxDeviation { get; set; }
        public double? VOSMaxDeviation { get; set; }
        public double? DensityAvg { get; set; }
        public double? LoopStatus { get; set; }
        public int? CheckResult { get; set; }
        public string Remark1 { get; set; }

    }
   
    [Keyless]
    [Table("tRealtimeCheckDataDanielVOSChartData")]
    public class RealtimeCheckDataDanielVOSChartData 
    {
   
        public int ID { get; set; }
        public DateTime DateTime { get; set; }
        public double? ProfileFactor { get; set; }
        public double? Path1VOS { get; set; }
        public double? Path2VOS { get; set; }
        public double? Path3VOS { get; set; }
        public double? Path4VOS { get; set; }
        public double? PathsVOSAverage { get; set; }
        public double? Path1VOG { get; set; }
        public double? Path2VOG { get; set; }
        public double? Path3VOG { get; set; }
        public double? Path4VOG { get; set; }
    }
    [Table("tRealtimeCheckDataDanielFR")]
    public class RealtimeDanielFRCheckData : RealtimeStationLoopCheckData
    {
        public double? Pressure { get; set; }
        public double? Temperature { get; set; }
        public double? PathsVOGAverage { get; set; }
        public double? InsideDiameter { get; set; }
        public double? OutsideDiameter { get; set; }
        public double? SectionArea { get; set; }
        public double? TemperatureParameter { get; set; }
        public double? PressureParameter { get; set; }
        public double? GrossFlowrate { get; set; }
        public double? StandardFlowrate { get; set; }
        public double? MassFlowrate { get; set; }
        public double? EnergyFlowrate { get; set; }
        public double? C1 { get; set; }
        public double? C2 { get; set; }
        public double? C3 { get; set; }
        public double? nC4 { get; set; }
        public double? iC4 { get; set; }
        public double? nC5 { get; set; }
        public double? iC5 { get; set; }
        public double? NeoC5 { get; set; }
        public double? C6 { get; set; }
        public double? N2 { get; set; }
        public double? CO2 { get; set; }
        public double? GrossDensity { get; set; }
        public double? StandardDensity { get; set; }
        public double? HighCalorificValue { get; set; }
        public double? CalculatedGrossFlowrate { get; set; }
        public double? CalculatedStandardFlowrate { get; set; }
        public double? CalculatedMassFlowrate { get; set; }
        public double? CalculatedEnergyFlowrate { get; set; }
        public double? GrossFlowrateDeviationRate { get; set; }
        public int? GrossFlowrateDeviationRateCheckResult { get; set; }
        public double? StandardFlowrateDeviationRate { get; set; }
        public int? StandardFlowrateDeviationRateCheckResult { get; set; }
        public double? MassFlowrateDeviationRate { get; set; }
        public int? MassFlowrateDeviationRateCheckResult { get; set; }
        public double? EnergyFlowrateDeviationRate { get; set; }
        public int? EnergyFlowrateDeviationRateCheckResult { get; set; }
    }

    [Table("tRealtimeCheckDataDanielLoop")]
    public class RealtimeDanielLoopCheckData : RealtimeStationLoopCheckData
    {
    }

    //Elster
    [Table("tRealtimeCheckDataElsterVOS")]
    public class RealtimeElsterVOSCheckData : RealtimeStationLoopCheckData
    {
    }
    [Keyless]
    [Table("tRealtimeCheckDataElsterVOSChartData")]
    public class RealtimeCheckDataElsterVOSChartData
    {
       
    }
    [Table("tRealtimeCheckDataElsterFR")]
    public class RealtimeElsterFRCheckData : RealtimeStationLoopCheckData
    {
    }
    [Table("tRealtimeCheckDataElsterLoop")]
    public class RealtimeElsterLoopCheckData : RealtimeStationLoopCheckData
    {
    }
    //Sick
    [Table("tRealtimeCheckDataSickVOS")]
    public class RealtimeSickVOSCheckData : RealtimeStationLoopCheckData
    {
    }
    [Keyless]
    [Table("tRealtimeCheckDataSickVOSChartData")]
    public class RealtimeCheckDataSickVOSChartData
    {

    }
    [Table("tRealtimeCheckDataSickFR")]
    public class RealtimeSickFRCheckData : RealtimeStationLoopCheckData
    {
    }
    [Table("tRealtimeCheckDataSickLoop")]
    public class RealtimeSickLoopCheckData : RealtimeStationLoopCheckData
    {
    }
    //Weise
    [Table("tRealtimeCheckDataWeiseVOS")]
    public class RealtimeWeiseVOSCheckData : RealtimeStationLoopCheckData
    {
    }
    [Keyless]
    [Table("tRealtimeCheckDataWeiseVOSChartData")]
    public class RealtimeCheckDataWeiseVOSChartData
    {

    }
    [Table("tRealtimeCheckDataWeiseFR")]
    public class RealtimeWeiseFRCheckData : RealtimeStationLoopCheckData
    {
    }
    [Table("tRealtimeCheckDataWeiseLoop")]
    public class RealtimeWeiseLoopCheckData : RealtimeStationLoopCheckData
    {
    }
    //RMG
    [Table("tRealtimeCheckDataRMGVOS")]
    public class RealtimeRMGVOSCheckData : RealtimeStationLoopCheckData
    {
    }
    [Keyless]
    [Table("tRealtimeCheckDataRMGVOSChartData")]
    public class RealtimeCheckDataRMGVOSChartData
    {

    }
    [Table("tRealtimeCheckDataRMGFR")]
    public class RealtimeRMGFRCheckData : RealtimeStationLoopCheckData
    {
    }
    [Table("tRealtimeCheckDataRMGLoop")]
    public class RealtimeRMGLoopCheckData : RealtimeStationLoopCheckData
    {
    }
}
