using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StationLoopDiagnosticDataDetail
    {
       
    }
    [Table("tRealtimeDiagnosticDataDanielTT")]
    public class DanielTTDiagnosticDataDetail:IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public byte P2 { get; set; }
        public double v0 { get; set; }
        public double v1 { get; set; }
        public double v2 { get; set; }

        public void Dispose()
        {

        }
    }
    [Table("tRealtimeDiagnosticDataDanielPT")]
    public class DanielPTDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public byte P2 { get; set; }
        public double v0 { get; set; }
        public double v1 { get; set; }
        public double v2 { get; set; }

        public void Dispose()
        {

        }
    }

    [Table("tRealtimeDiagnosticDataDanielFM")]
    public class DanielFMDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public byte P2 { get; set; }
        public byte P3 { get; set; }
        public byte P4 { get; set; }
        public byte P5 { get; set; }
        public byte P6 { get; set; }
        public byte P7 { get; set; }
        public byte P8 { get; set; }
        public byte P9 { get; set; }
        public byte P10 { get; set; }
        public byte P11 { get; set; }
        public byte P12 { get; set; }
        public byte P13 { get; set; }
        public byte P14 { get; set; }
        public byte P15 { get; set; }
        public byte P16 { get; set; }
        public byte P17 { get; set; }
        public byte P18 { get; set; }
        public byte P19 { get; set; }
        public byte P20 { get; set; }
        public byte P21 { get; set; }
        public byte P22 { get; set; }
        public byte P23 { get; set; }
        public byte P24 { get; set; }
        public byte P25 { get; set; }
        public byte P26 { get; set; }
        public byte P27 { get; set; }
        public byte P28 { get; set; }
        public byte P29 { get; set; }
        public byte P30 { get; set; }
        public byte P31 { get; set; }
        public byte P32 { get; set; }
        public byte P33 { get; set; }
        public byte P34 { get; set; }
        public byte P35 { get; set; }
        public byte P36 { get; set; }
        public byte P37 { get; set; }
        public byte P38 { get; set; }
        public byte P39 { get; set; }
        public byte P40 { get; set; }
        public double v0 { get; set; }
        public double v1 { get; set; }
        public double v2 { get; set; }
        public double v3 { get; set; }
        public double v4 { get; set; }
        public double v5 { get; set; }
        public double v6 { get; set; }
        public double v7 { get; set; }
        public double v8 { get; set; }
        public double v9 { get; set; }
        public double v10 { get; set; }
        public double v11 { get; set; }
        public double v12 { get; set; }
        public double v13 { get; set; }
        public double v14 { get; set; }
        public double v15 { get; set; }
        public double v16 { get; set; }
        public double v17 { get; set; }
        public double v18 { get; set; }
        public double v19 { get; set; }
        public double v20 { get; set; }
        public double v21 { get; set; }
        public double v22 { get; set; }
        public double v23 { get; set; }
        public double v24 { get; set; }
        public double v25 { get; set; }
        public double v26 { get; set; }
        public double v27 { get; set; }
        public double v28 { get; set; }
        public double v29 { get; set; }
        public double v30 { get; set; }
        public double v31 { get; set; }
        public double v32 { get; set; }
        public double v33 { get; set; }
        public double v34 { get; set; }
        public double v35 { get; set; }
        public double v36 { get; set; }
        public double v37 { get; set; }
        public double v38 { get; set; }
        public double v39 { get; set; }
        public double v40 { get; set; }
        public double c0 { get; set; }
        public double c1 { get; set; }
        public double c2 { get; set; }
        public double c3 { get; set; }

        public void Dispose()
        {
            
        }
    }

    [Table("tRealtimeDiagnosticDataDanielFC")]
    public class DanielFCDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public byte P2 { get; set; }
        public double v0 { get; set; }
        public double v1 { get; set; }
        public double v2 { get; set; }

        public void Dispose()
        {

        }
    }

    [Table("tRealtimeDiagnosticDataDanielVOS")]
    public class DanielVOSDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public double v0 { get; set; }
        public double v1 { get; set; }

        public void Dispose()
        {
 
        }
    }

    [Table("tRealtimeDiagnosticDataElsterTT")]
    public class ElsterTTDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public double v0 { get; set; }
        public double v1 { get; set; }
        public void Dispose()
        {

        }
    }
    [Table("tRealtimeDiagnosticDataElsterPT")]
    public class ElsterPTDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public double v0 { get; set; }
        public double v1 { get; set; }

        public void Dispose()
        {

        }
    }

    [Table("tRealtimeDiagnosticDataElsterFM")]
    public class ElsterFMDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public byte P2 { get; set; }
        public byte P3 { get; set; }
        public byte P4 { get; set; }
        public byte P5 { get; set; }
        public byte P6 { get; set; }
        public byte P7 { get; set; }
        public byte P8 { get; set; }
        public byte P9 { get; set; }
        public byte P10 { get; set; }
        public byte P11 { get; set; }
        public byte P12 { get; set; }
        public byte P13 { get; set; }
        public byte P14 { get; set; }
        public byte P15 { get; set; }
        public byte P16 { get; set; }
        public byte P17 { get; set; }
        public byte P18 { get; set; }
        public byte P19 { get; set; }
        public byte P20 { get; set; }
        public byte P21 { get; set; }
        public byte P22 { get; set; }
        public byte P23 { get; set; }
        public byte P24 { get; set; }
        public byte P25 { get; set; }
        public byte P26 { get; set; }
        public byte P27 { get; set; }
        public byte P28 { get; set; }
        public byte P29 { get; set; }
        public byte P30 { get; set; }
        public byte P31 { get; set; }
        public byte P32 { get; set; }
        public byte P33 { get; set; }
        public byte P34 { get; set; }
        public byte P35 { get; set; }
        public byte P36 { get; set; }
        public byte P37 { get; set; }
        public byte P38 { get; set; }
        public byte P39 { get; set; }
        public byte P40 { get; set; }
        public byte P41 { get; set; }
        public byte P42 { get; set; }
        public byte P43 { get; set; }
        public byte P44 { get; set; }
        public byte P45 { get; set; }
        public byte P46 { get; set; }
        public byte P47 { get; set; }
        public byte P48 { get; set; }
        public byte P49 { get; set; }
        public double v0 { get; set; }
        public double v1 { get; set; }
        public double v2 { get; set; }
        public double v3 { get; set; }
        public double v4 { get; set; }
        public double v5 { get; set; }
        public double v6 { get; set; }
        public double v7 { get; set; }
        public double v8 { get; set; }
        public double v9 { get; set; }
        public double v10 { get; set; }
        public double v11 { get; set; }
        public double v12 { get; set; }
        public double v13 { get; set; }
        public double v14 { get; set; }
        public double v15 { get; set; }
        public double v16 { get; set; }
        public double v17 { get; set; }
        public double v18 { get; set; }
        public double v19 { get; set; }
        public double v20 { get; set; }
        public double v21 { get; set; }
        public double v22 { get; set; }
        public double v23 { get; set; }
        public double v24 { get; set; }
        public double v25 { get; set; }
        public double v26 { get; set; }
        public double v27 { get; set; }
        public double v28 { get; set; }
        public double v29 { get; set; }
        public double v30 { get; set; }
        public double v31 { get; set; }
        public double v32 { get; set; }
        public double v33 { get; set; }
        public double v34 { get; set; }
        public double v35 { get; set; }
        public double v36 { get; set; }
        public double v37 { get; set; }
        public double v38 { get; set; }
        public double v39 { get; set; }
        public double v40 { get; set; }
        public double v41 { get; set; }
        public double v42 { get; set; }
        public double v43 { get; set; }
        public double v44 { get; set; }
        public double v45 { get; set; }
        public double v46 { get; set; }
        public double v47 { get; set; }
        public double v48 { get; set; }
        public double v49 { get; set; }
        public double v50 { get; set; }
        public void Dispose()
        {

        }
    }

    [Table("tRealtimeDiagnosticDataElsterFC")]
    public class ElsterFCDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public double v0 { get; set; }
        public double v1 { get; set; }

        public void Dispose()
        {

        }
    }

    [Table("tRealtimeDiagnosticDataElsterVOS")]
    public class ElsterVOSDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public double v0 { get; set; }
        public double v1 { get; set; }

        public void Dispose()
        {

        }
    }
    [Table("tRealtimeDiagnosticDataSickTT")]
    public class SickTTDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public double v0 { get; set; }
        public double v1 { get; set; }
        public void Dispose()
        {

        }
    }
    [Table("tRealtimeDiagnosticDataSickPT")]
    public class SickPTDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public double v0 { get; set; }
        public double v1 { get; set; }

        public void Dispose()
        {

        }
    }

    [Table("tRealtimeDiagnosticDataSickFM")]
    public class SickFMDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public byte P2 { get; set; }
        public byte P3 { get; set; }
        public byte P4 { get; set; }
        public byte P5 { get; set; }
        public byte P6 { get; set; }
        public byte P7 { get; set; }
        public byte P8 { get; set; }
        public byte P9 { get; set; }
        public byte P10 { get; set; }
        public byte P11 { get; set; }
        public byte P12 { get; set; }
        public byte P13 { get; set; }
        public byte P14 { get; set; }
        public byte P15 { get; set; }
        public byte P16 { get; set; }
        public byte P17 { get; set; }
        public byte P18 { get; set; }
        public byte P19 { get; set; }
        public byte P20 { get; set; }
        public byte P21 { get; set; }
        public byte P22 { get; set; }
        public byte P23 { get; set; }
        public byte P24 { get; set; }
        public byte P25 { get; set; }
        public byte P26 { get; set; }
        public byte P27 { get; set; }
        public byte P28 { get; set; }
        public byte P29 { get; set; }
        public byte P30 { get; set; }
        public byte P31 { get; set; }
        public byte P32 { get; set; }
        public byte P33 { get; set; }
        public byte P34 { get; set; }      
        public double v0 { get; set; }
        public double v1 { get; set; }
        public double v2 { get; set; }
        public double v3 { get; set; }
        public double v4 { get; set; }
        public double v5 { get; set; }
        public double v6 { get; set; }
        public double v7 { get; set; }
        public double v8 { get; set; }
        public double v9 { get; set; }
        public double v10 { get; set; }
        public double v11 { get; set; }
        public double v12 { get; set; }
        public double v13 { get; set; }
        public double v14 { get; set; }
        public double v15 { get; set; }
        public double v16 { get; set; }
        public double v17 { get; set; }
        public double v18 { get; set; }
        public double v19 { get; set; }
        public double v20 { get; set; }
        public double v21 { get; set; }
        public double v22 { get; set; }
        public double v23 { get; set; }
        public double v24 { get; set; }
        public double v25 { get; set; }
        public double v26 { get; set; }
        public double v27 { get; set; }
        public double v28 { get; set; }
        public double v29 { get; set; }
        public double v30 { get; set; }
        public double v31 { get; set; }
        public double v32 { get; set; }
        public double v33 { get; set; }
        public double v34 { get; set; }
        public void Dispose()
        {

        }
    }

    [Table("tRealtimeDiagnosticDataSickFC")]
    public class SickFCDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public double v0 { get; set; }
        public void Dispose()
        {

        }
    }

    [Table("tRealtimeDiagnosticDataSickVOS")]
    public class SickVOSDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public double v0 { get; set; }
        public double v1 { get; set; }

        public void Dispose()
        {

        }
    }
    [Table("tRealtimeDiagnosticDataWeiseTT")]
    public class WeiseTTDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public double v0 { get; set; }
        public double v1 { get; set; }
        public void Dispose()
        {

        }
    }
    [Table("tRealtimeDiagnosticDataWeisePT")]
    public class WeisePTDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public byte P2 { get; set; }
        public double v0 { get; set; }
        public double v1 { get; set; }
        public double v2 { get; set; }
        public void Dispose()
        {

        }
    }

    [Table("tRealtimeDiagnosticDataWeiseFM")]
    public class WeiseFMDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public byte P2 { get; set; }
        public byte P3 { get; set; }
        public byte P4 { get; set; }
        public byte P5 { get; set; }
        public byte P6 { get; set; }
        public byte P7 { get; set; }
        public byte P8 { get; set; }
        public byte P9 { get; set; }
        public byte P10 { get; set; }
        public byte P11 { get; set; }
        public byte P12 { get; set; }
        public byte P13 { get; set; }
        public byte P14 { get; set; }
        public byte P15 { get; set; }
        public byte P16 { get; set; }
        public byte P17 { get; set; }
        public byte P18 { get; set; }
        public byte P19 { get; set; }
        public byte P20 { get; set; }
        public byte P21 { get; set; }
        public byte P22 { get; set; }
        public byte P23 { get; set; }
        public byte P24 { get; set; }
        public byte P25 { get; set; }
        public byte P26 { get; set; }
        public byte P27 { get; set; }
        public byte P28 { get; set; }
        public byte P29 { get; set; }
        public byte P30 { get; set; }
        public byte P31 { get; set; }
        public byte P32 { get; set; }
        public byte P33 { get; set; }
        public byte P34 { get; set; }
        public byte P35 { get; set; }
        public byte P36 { get; set; }
        public byte P37 { get; set; }
        public byte P38 { get; set; }
        public byte P39 { get; set; }
        public byte P40 { get; set; }
        public byte P41 { get; set; }
        public byte P42 { get; set; }
        public byte P43 { get; set; }
        public byte P44 { get; set; }
        public byte P45 { get; set; }
        public byte P46 { get; set; }
        public byte P47 { get; set; }
        public byte P48 { get; set; }
        public byte P49 { get; set; }
        public byte P50 { get; set; }
        public byte P51 { get; set; }
        public byte P52 { get; set; }
        public byte P53 { get; set; }
        public byte P54 { get; set; }
        public byte P55 { get; set; }
        public byte P56 { get; set; }
        public byte P57 { get; set; }
        public byte P58 { get; set; }
        public byte P59 { get; set; }
        public byte P60 { get; set; }
        public byte P61 { get; set; }
        public byte P62 { get; set; }
        public byte P63 { get; set; }
        public byte P64 { get; set; }
        public byte P65 { get; set; }
        public byte P66 { get; set; }
        public byte P67 { get; set; }
        public byte P68 { get; set; }
        public int v0 { get; set; }
        public int v1 { get; set; }
        public int v2 { get; set; }
        public int v3 { get; set; }
        public int v4 { get; set; }
        public int v5 { get; set; }
        public int v6 { get; set; }
        public int v7 { get; set; }
        public int v8 { get; set; }
        public int v9 { get; set; }
        public int v10 { get; set; }
        public int v11 { get; set; }
        public int v12 { get; set; }
        public int v13 { get; set; }
        public int v14 { get; set; }
        public int v15 { get; set; }
        public int v16 { get; set; }
        public int v17 { get; set; }
        public int v18 { get; set; }
        public int v19 { get; set; }
        public int v20 { get; set; }
        public int v21 { get; set; }
        public int v22 { get; set; }
        public int v23 { get; set; }
        public int v24 { get; set; }
        public int v25 { get; set; }
        public int v26 { get; set; }
        public int v27 { get; set; }
        public int v28 { get; set; }
        public int v29 { get; set; }
        public int v30 { get; set; }
        public int v31 { get; set; }
        public int v32 { get; set; }
        public int v33 { get; set; }
        public int v34 { get; set; }
        public int v35 { get; set; }
        public int v36 { get; set; }
        public int v37 { get; set; }
        public int v38 { get; set; }
        public int v39 { get; set; }
        public int v40 { get; set; }
        public int v41 { get; set; }
        public int v42 { get; set; }
        public int v43 { get; set; }
        public int v44 { get; set; }
        public int v45 { get; set; }
        public int v46 { get; set; }
        public int v47 { get; set; }
        public int v48 { get; set; }
        public int v49 { get; set; }
        public int v50 { get; set; }
        public int v51 { get; set; }
        public int v52 { get; set; }
        public int v53 { get; set; }
        public int v54 { get; set; }
        public int v55 { get; set; }
        public int v56 { get; set; }
        public int v57 { get; set; }
        public int v58 { get; set; }
        public int v59 { get; set; }
        public int v60 { get; set; }
        public int v61 { get; set; }
        public int v62 { get; set; }
        public int v63 { get; set; }
        public int v64 { get; set; }
        public int v65 { get; set; }
        public int v66 { get; set; }
        public int v67 { get; set; }
        public int v68 { get; set; }
        public void Dispose()
        {

        }
    }

    [Table("tRealtimeDiagnosticDataWeiseFC")]
    public class WeiseFCDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public double v0 { get; set; }
        public void Dispose()
        {

        }
    }

    [Table("tRealtimeDiagnosticDataWeiseVOS")]
    public class WeiseVOSDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public double v0 { get; set; }
        public double v1 { get; set; }

        public void Dispose()
        {

        }
    }

    [Table("tRealtimeDiagnosticDataRMGTT")]
    public class RMGTTDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public double v0 { get; set; }
        public double v1 { get; set; }
        public void Dispose()
        {

        }
    }
    [Table("tRealtimeDiagnosticDataRMGPT")]
    public class RMGPTDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public double v0 { get; set; }
        public double v1 { get; set; }
        public void Dispose()
        {

        }
    }

    [Table("tRealtimeDiagnosticDataRMGFM")]
    public class RMGFMDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public byte P2 { get; set; }
        public byte P3 { get; set; }
        public byte P4 { get; set; }
        public byte P5 { get; set; }
        public byte P6 { get; set; }
        public byte P7 { get; set; }
        public byte P8 { get; set; }
        public byte P9 { get; set; }
        public byte P10 { get; set; }
        public byte P11 { get; set; }
        public byte P12 { get; set; }
        public byte P13 { get; set; }
        public byte P14 { get; set; }
        public byte P15 { get; set; }
        public byte P16 { get; set; }
        public byte P17 { get; set; }
        public byte P18 { get; set; }
        public byte P19 { get; set; }
        public byte P20 { get; set; }
        public byte P21 { get; set; }
        public byte P22 { get; set; }
        public byte P23 { get; set; }
        public byte P24 { get; set; }
        public byte P25 { get; set; }
        public byte P26 { get; set; }
        public byte P27 { get; set; }
        public byte P28 { get; set; }
        public byte P29 { get; set; }
        public byte P30 { get; set; }
        public byte P31 { get; set; }
        public byte P32 { get; set; }
        public byte P33 { get; set; }
        public byte P34 { get; set; }
        public byte P35 { get; set; }
        public byte P36 { get; set; }
        public byte P37 { get; set; }
        public byte P38 { get; set; }
        public byte P39 { get; set; }
        public byte P40 { get; set; }
        public byte P41 { get; set; }
        public byte P42 { get; set; }
        public byte P43 { get; set; }
        public byte P44 { get; set; }
        public byte P45 { get; set; }
        public byte P46 { get; set; }
        public byte P47 { get; set; }
        public byte P48 { get; set; }
        public byte P49 { get; set; }
        public int v0 { get; set; }
        public int v1 { get; set; }
        public int v2 { get; set; }
        public int v3 { get; set; }
        public int v4 { get; set; }
        public int v5 { get; set; }
        public int v6 { get; set; }
        public int v7 { get; set; }
        public int v8 { get; set; }
        public int v9 { get; set; }
        public int v10 { get; set; }
        public int v11 { get; set; }
        public int v12 { get; set; }
        public int v13 { get; set; }
        public int v14 { get; set; }
        public int v15 { get; set; }
        public int v16 { get; set; }
        public int v17 { get; set; }
        public int v18 { get; set; }
        public int v19 { get; set; }
        public int v20 { get; set; }
        public int v21 { get; set; }
        public int v22 { get; set; }
        public int v23 { get; set; }
        public int v24 { get; set; }
        public int v25 { get; set; }
        public int v26 { get; set; }
        public int v27 { get; set; }
        public int v28 { get; set; }
        public int v29 { get; set; }
        public int v30 { get; set; }
        public int v31 { get; set; }
        public int v32 { get; set; }
        public int v33 { get; set; }
        public int v34 { get; set; }
        public int v35 { get; set; }
        public int v36 { get; set; }
        public int v37 { get; set; }
        public int v38 { get; set; }
        public int v39 { get; set; }
        public int v40 { get; set; }
        public int v41 { get; set; }
        public int v42 { get; set; }
        public int v43 { get; set; }
        public int v44 { get; set; }
        public int v45 { get; set; }
        public int v46 { get; set; }
        public int v47 { get; set; }
        public int v48 { get; set; }
        public int v49 { get; set; }
        public void Dispose()
        {

        }
    }

    [Table("tRealtimeDiagnosticDataRMGFC")]
    public class RMGFCDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public double v0 { get; set; }
        public void Dispose()
        {

        }
    }

    [Table("tRealtimeDiagnosticDataRMGVOS")]
    public class RMGVOSDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public double v0 { get; set; }
        public double v1 { get; set; }

        public void Dispose()
        {

        }
    }
}
