using Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Respository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EquipmentMeteringCertificateService : IEquipmentMeteringCertificateService
    {
        public readonly IEquipmentMeteringCertificateRespository _equipmentMeteringCertificateRespository;
        public EquipmentMeteringCertificateService(IEquipmentMeteringCertificateRespository equipmentMeteringCertificateRespository)
        {
            _equipmentMeteringCertificateRespository = equipmentMeteringCertificateRespository;
        }

        public Task<List<EquipmentMeteringCertificate>> GetEquipmentMeteringCertificates(DateTime beginSearchDate, DateTime endSearchDate)
        {
            return Task.Run(() => _equipmentMeteringCertificateRespository.GetEquipmentMeteringCertificates(beginSearchDate, endSearchDate));
        }

        public Task<string> AddEquipmentMeteringCertificate(EquipmentMeteringCertificate entity)
        {
            return Task.Run(() => _equipmentMeteringCertificateRespository.AddEquipmentMeteringCertificate(entity));
        }

        public Task<string> UpdateEquipmentMeteringCertificate(EquipmentMeteringCertificate entity)
        {
            return Task.Run(() => _equipmentMeteringCertificateRespository.UpdateEquipmentMeteringCertificate(entity));
        }

        public Task<bool> DeleteEquipmentMeteringCertificate(List<int> ids)
        {
            return Task.Run(() => _equipmentMeteringCertificateRespository.DeleteEquipmentMeteringCertificate(ids));
        }

        public Task<byte[]> ExportEquipmentMeteringCertificates(List<EquipmentMeteringCertificate> equipmentMeteringCertificates, string templatePath)
        {
            byte[] result;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo existingFile = new(templatePath);
            using ExcelPackage package = new(existingFile);
            ExcelWorksheet certificate = package.Workbook.Worksheets["证书信息"];
            #region 证书信息
            for (int i = 0; i < equipmentMeteringCertificates.Count; i++)
            {
                //序号
                certificate.Cells[i + 3, 1].Value = i + 1;
                certificate.Cells[i + 3, 1].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 1].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 1].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //机构                                                                             
                certificate.Cells[i + 3, 2].Value = equipmentMeteringCertificates[i].Agency;
                certificate.Cells[i + 3, 2].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 2].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 2].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //证书编号                                                                     
                certificate.Cells[i + 3, 3].Value = equipmentMeteringCertificates[i].CertificateNumber;
                certificate.Cells[i + 3, 3].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 3].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 3].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //客户名称                                                                               
                certificate.Cells[i + 3, 4].Value = equipmentMeteringCertificates[i].Customer;
                certificate.Cells[i + 3, 4].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 4].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 4].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //客户联系方式                                                                              
                certificate.Cells[i + 3, 5].Value = equipmentMeteringCertificates[i].CustomerContact;
                certificate.Cells[i + 3, 5].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 5].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 5].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //客户地址                                                                              
                certificate.Cells[i + 3, 6].Value = equipmentMeteringCertificates[i].CustomerAddress;
                certificate.Cells[i + 3, 6].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 6].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 6].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //器具名称                                                                              
                certificate.Cells[i + 3, 7].Value = equipmentMeteringCertificates[i].EquipmentCategory;
                certificate.Cells[i + 3, 7].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 7].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 7].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //型号规格                                                                              
                certificate.Cells[i + 3, 8].Value = equipmentMeteringCertificates[i].EquipmentModel;
                certificate.Cells[i + 3, 8].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 8].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 8].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //出厂编号                                                                              
                certificate.Cells[i + 3, 9].Value = equipmentMeteringCertificates[i].EquipmentSerialNumber;
                certificate.Cells[i + 3, 9].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 9].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 9].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //制造厂商                                                                             
                certificate.Cells[i + 3, 10].Value = equipmentMeteringCertificates[i].EquipmentManufacturer;
                certificate.Cells[i + 3, 10].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 10].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 10].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //准确度等级                                                                              
                certificate.Cells[i + 3, 11].Value = equipmentMeteringCertificates[i].EquipmentAccuracy;
                certificate.Cells[i + 3, 11].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 11].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 11].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //检定结论                                                                            
                certificate.Cells[i + 3, 12].Value = equipmentMeteringCertificates[i].CertificateConclusion;
                certificate.Cells[i + 3, 12].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 12].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 12].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //批准人                                                                         
                certificate.Cells[i + 3, 13].Value = equipmentMeteringCertificates[i].ApprovedBy;
                certificate.Cells[i + 3, 13].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 13].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 13].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //批准人职务                                                                            
                certificate.Cells[i + 3, 14].Value = equipmentMeteringCertificates[i].ApproverPosition;
                certificate.Cells[i + 3, 14].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 14].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 14].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //检定/校准员                                                                          
                certificate.Cells[i + 3, 15].Value = equipmentMeteringCertificates[i].VerifiedBy;
                certificate.Cells[i + 3, 15].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 15].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 15].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //核验员
                certificate.Cells[i + 3, 16].Value = equipmentMeteringCertificates[i].CheckedBy;
                certificate.Cells[i + 3, 16].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 16].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 16].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //接收日期                                                                            
                certificate.Cells[i + 3, 17].Value = equipmentMeteringCertificates[i].ReceiptDate?.ToString("yyyy/MM/dd") ?? "";
                certificate.Cells[i + 3, 17].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 17].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 17].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //检定/校准日期                                                                            
                certificate.Cells[i + 3, 18].Value = equipmentMeteringCertificates[i].VerificationDate.ToString("yyyy/MM/dd") ?? "";
                certificate.Cells[i + 3, 18].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 18].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 18].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 18].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //有效期至                                                                            
                certificate.Cells[i + 3, 19].Value = equipmentMeteringCertificates[i].ValidityDate.ToString("yyyy/MM/dd") ?? "";
                certificate.Cells[i + 3, 19].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 19].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 19].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 19].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //签发/批准日期                                                                            
                certificate.Cells[i + 3, 20].Value = equipmentMeteringCertificates[i].AuthorizationDate?.ToString("yyyy/MM/dd") ?? "";
                certificate.Cells[i + 3, 20].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 20].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 20].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 20].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //依据/参照文件                                                                            
                certificate.Cells[i + 3, 21].Value = equipmentMeteringCertificates[i].ReferenceRegulation;
                certificate.Cells[i + 3, 21].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 21].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 21].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 21].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //检定介质                                                                            
                certificate.Cells[i + 3, 22].Value = equipmentMeteringCertificates[i].ENVMedium;
                certificate.Cells[i + 3, 22].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 22].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 22].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 22].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //检定流量                                                                            
                certificate.Cells[i + 3, 23].Value = equipmentMeteringCertificates[i].ENVFlowrate;
                certificate.Cells[i + 3, 23].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 23].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 23].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 23].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //介质压力                                                                            
                certificate.Cells[i + 3, 24].Value = equipmentMeteringCertificates[i].ENVMediumPerssure;
                certificate.Cells[i + 3, 24].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 24].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 24].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 24].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //介质温度                                                                            
                certificate.Cells[i + 3, 25].Value = equipmentMeteringCertificates[i].ENVMediumTemperature;
                certificate.Cells[i + 3, 25].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 25].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 25].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 25].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //环境压力                                                                            
                certificate.Cells[i + 3, 26].Value = equipmentMeteringCertificates[i].ENVEnvironmentPressure;
                certificate.Cells[i + 3, 26].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 26].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 26].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 26].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //环境温度                                                                            
                certificate.Cells[i + 3, 27].Value = equipmentMeteringCertificates[i].ENVEnvironmentTemperature;
                certificate.Cells[i + 3, 27].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 27].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 27].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 27].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //湿度                                                                            
                certificate.Cells[i + 3, 28].Value = equipmentMeteringCertificates[i].ENVEnvironmentHumidity;
                certificate.Cells[i + 3, 28].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 28].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 28].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 28].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //安装条件                                                                            
                certificate.Cells[i + 3, 29].Value = equipmentMeteringCertificates[i].ENVInstallationCondition;
                certificate.Cells[i + 3, 29].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 29].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 29].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 29].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //安装方向                                                                            
                certificate.Cells[i + 3, 30].Value = equipmentMeteringCertificates[i].ENVInstallationDirection;
                certificate.Cells[i + 3, 30].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 30].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 30].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 30].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //限制使用条件                                                                            
                certificate.Cells[i + 3, 31].Value = equipmentMeteringCertificates[i].ENVRestrictiveCondition;
                certificate.Cells[i + 3, 31].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 31].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 31].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 31].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //限制测量范围                                                                            
                certificate.Cells[i + 3, 32].Value = equipmentMeteringCertificates[i].ENVRestrictiveMeasurementRange;
                certificate.Cells[i + 3, 32].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 32].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 32].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 32].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //地点                                                                            
                certificate.Cells[i + 3, 33].Value = equipmentMeteringCertificates[i].ENVLocation;
                certificate.Cells[i + 3, 33].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 33].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 33].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 33].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //N2                                                                            
                certificate.Cells[i + 3, 34].Value = equipmentMeteringCertificates[i].ENVN2;
                certificate.Cells[i + 3, 34].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 34].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 34].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 34].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //CH4                                                                            
                certificate.Cells[i + 3, 35].Value = equipmentMeteringCertificates[i].ENVCH4;
                certificate.Cells[i + 3, 35].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 35].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 35].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 35].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //CO2                                                                            
                certificate.Cells[i + 3, 36].Value = equipmentMeteringCertificates[i].ENVCO2;
                certificate.Cells[i + 3, 36].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 36].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 36].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 36].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //C2H6                                                                            
                certificate.Cells[i + 3, 37].Value = equipmentMeteringCertificates[i].ENVC2H6;
                certificate.Cells[i + 3, 37].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 37].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 37].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 37].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //C3H8                                                                            
                certificate.Cells[i + 3, 38].Value = equipmentMeteringCertificates[i].ENVC3H8;
                certificate.Cells[i + 3, 38].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 38].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 38].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 38].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //i-C4H10                                                                            
                certificate.Cells[i + 3, 39].Value = equipmentMeteringCertificates[i].ENViC4H10;
                certificate.Cells[i + 3, 39].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 39].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 39].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 39].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //n-C4H10                                                                          
                certificate.Cells[i + 3, 40].Value = equipmentMeteringCertificates[i].ENVnC4H10;
                certificate.Cells[i + 3, 40].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 40].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 40].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 40].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //i-C5H12                                                                        
                certificate.Cells[i + 3, 41].Value = equipmentMeteringCertificates[i].ENViC5H12;
                certificate.Cells[i + 3, 41].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 41].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 41].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 41].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //n-C5H12                                                                           
                certificate.Cells[i + 3, 42].Value = equipmentMeteringCertificates[i].ENVnC5H12;
                certificate.Cells[i + 3, 42].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 42].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 42].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 42].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //C6+
                certificate.Cells[i + 3, 43].Value = equipmentMeteringCertificates[i].ENVC6;
                certificate.Cells[i + 3, 43].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 43].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 43].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 43].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //H2
                certificate.Cells[i + 3, 44].Value = equipmentMeteringCertificates[i].ENVH2;
                certificate.Cells[i + 3, 44].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 44].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 44].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 44].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //流量计口径                                                   
                certificate.Cells[i + 3, 45].Value = equipmentMeteringCertificates[i].EquipmentCaliber;
                certificate.Cells[i + 3, 45].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 45].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 45].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 45].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //流量测量范围
                certificate.Cells[i + 3, 46].Value = equipmentMeteringCertificates[i].EquipmentMeasurementRange;
                certificate.Cells[i + 3, 46].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 46].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 46].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 46].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //最大工作压力                                                                           
                certificate.Cells[i + 3, 47].Value = equipmentMeteringCertificates[i].EquipmnetMaxWorkPressure;
                certificate.Cells[i + 3, 47].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 47].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 47].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 47].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //压力等级                                                                           
                certificate.Cells[i + 3, 48].Value = equipmentMeteringCertificates[i].EquipmentPressureClass;
                certificate.Cells[i + 3, 48].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 48].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 48].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 48].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //K系数
                certificate.Cells[i + 3, 49].Value = equipmentMeteringCertificates[i].EquipmentKFactor;
                certificate.Cells[i + 3, 49].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 49].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 49].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 49].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //单点修正系数
                certificate.Cells[i + 3, 50].Value = equipmentMeteringCertificates[i].EquipmentCorrectionCoefficient;
                certificate.Cells[i + 3, 50].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 50].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 50].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 50].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //qt以上最大示值误差
                certificate.Cells[i + 3, 51].Value = equipmentMeteringCertificates[i].RSQtUpMaxIndicatingValueError;
                certificate.Cells[i + 3, 51].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 51].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 51].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 51].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //qt以下最大示值误差                                                   
                certificate.Cells[i + 3, 52].Value = equipmentMeteringCertificates[i].RSQtDNMaxIndicatingValueError;
                certificate.Cells[i + 3, 52].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 52].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 52].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 52].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //qt以上重复性
                certificate.Cells[i + 3, 53].Value = equipmentMeteringCertificates[i].RSQtUpRepeatability;
                certificate.Cells[i + 3, 53].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 53].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 53].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 53].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //qt以下重复性       
                certificate.Cells[i + 3, 54].Value = equipmentMeteringCertificates[i].RSQtDNRepeatability;
                certificate.Cells[i + 3, 54].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 54].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 54].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 54].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //外观技术要求
                certificate.Cells[i + 3, 55].Value = equipmentMeteringCertificates[i].RSAppearanceTechicalRequirement;
                certificate.Cells[i + 3, 55].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 55].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 55].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 55].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //外观检定结果          
                certificate.Cells[i + 3, 56].Value = equipmentMeteringCertificates[i].RSAppearanceVerifiedResult;
                certificate.Cells[i + 3, 56].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 56].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 56].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 56].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //绝缘电阻技术要求
                certificate.Cells[i + 3, 57].Value = equipmentMeteringCertificates[i].RSInsulationResistanceTechicalRequirement;
                certificate.Cells[i + 3, 57].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 57].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 57].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 57].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //绝缘电阻检定结果
                certificate.Cells[i + 3, 58].Value = equipmentMeteringCertificates[i].RSInsulationResistanceVerifiedResult;
                certificate.Cells[i + 3, 58].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 58].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 58].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 58].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //示值误差技术要求
                certificate.Cells[i + 3, 59].Value = equipmentMeteringCertificates[i].RsIndicatingValueErrorTechicalRequirement;
                certificate.Cells[i + 3, 59].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 59].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 59].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 59].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //示值误差检定结果
                certificate.Cells[i + 3, 60].Value = equipmentMeteringCertificates[i].RsIndicatingValueErrorVerifiedResult;
                certificate.Cells[i + 3, 60].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 60].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 60].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 60].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //回差技术要求
                certificate.Cells[i + 3, 61].Value = equipmentMeteringCertificates[i].RSReturnDifferenceTechicalRequirement;
                certificate.Cells[i + 3, 61].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 61].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 61].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 61].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //回查检定结果
                certificate.Cells[i + 3, 62].Value = equipmentMeteringCertificates[i].RSReturnDifferenceVerifiedResult;
                certificate.Cells[i + 3, 62].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 62].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 62].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 62].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //备注
                certificate.Cells[i + 3, 63].Value = equipmentMeteringCertificates[i].Note;
                certificate.Cells[i + 3, 63].Style.Font.Color.SetColor(Color.Black);//字体颜色
                certificate.Cells[i + 3, 63].Style.Font.Name = "SimSun";//字体
                certificate.Cells[i + 3, 63].Style.Font.Size = 10;//字体大小
                certificate.Cells[i + 3, 63].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }
            #endregion

            ExcelWorksheet resultSheet = package.Workbook.Worksheets["检定校准结果"];
            #region 检定/校准结果
            List<EquipmentMeteringResultData> meteringResultDatas = new();
            equipmentMeteringCertificates.ForEach(x => { if (x.MeteringResultDatas != null) meteringResultDatas.AddRange(x.MeteringResultDatas); });
            for (int i = 0; i < meteringResultDatas.Count; i++)
            {
                //序号
                resultSheet.Cells[i + 2, 1].Value = i + 1;
                resultSheet.Cells[i + 2, 1].Style.Font.Color.SetColor(Color.Black);//字体颜色
                resultSheet.Cells[i + 2, 1].Style.Font.Name = "SimSun";//字体
                resultSheet.Cells[i + 2, 1].Style.Font.Size = 10;//字体大小
                resultSheet.Cells[i + 2, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //证书编号
                resultSheet.Cells[i + 2, 2].Value = meteringResultDatas[i].CertificateNumber;
                resultSheet.Cells[i + 2, 2].Style.Font.Color.SetColor(Color.Black);//字体颜色
                resultSheet.Cells[i + 2, 2].Style.Font.Name = "SimSun";//字体
                resultSheet.Cells[i + 2, 2].Style.Font.Size = 10;//字体大小
                resultSheet.Cells[i + 2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //检定/校准数据1
                resultSheet.Cells[i + 2, 3].Value = meteringResultDatas[i].V1;
                resultSheet.Cells[i + 2, 3].Style.Font.Color.SetColor(Color.Black);//字体颜色
                resultSheet.Cells[i + 2, 3].Style.Font.Name = "SimSun";//字体
                resultSheet.Cells[i + 2, 3].Style.Font.Size = 10;//字体大小
                resultSheet.Cells[i + 2, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //检定/校准数据2
                resultSheet.Cells[i + 2, 4].Value = meteringResultDatas[i].V2;
                resultSheet.Cells[i + 2, 4].Style.Font.Color.SetColor(Color.Black);//字体颜色
                resultSheet.Cells[i + 2, 4].Style.Font.Name = "SimSun";//字体
                resultSheet.Cells[i + 2, 4].Style.Font.Size = 10;//字体大小
                resultSheet.Cells[i + 2, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //检定/校准数据3
                resultSheet.Cells[i + 2, 5].Value = meteringResultDatas[i].V3;
                resultSheet.Cells[i + 2, 5].Style.Font.Color.SetColor(Color.Black);//字体颜色
                resultSheet.Cells[i + 2, 5].Style.Font.Name = "SimSun";//字体
                resultSheet.Cells[i + 2, 5].Style.Font.Size = 10;//字体大小
                resultSheet.Cells[i + 2, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //检定/校准数据4
                resultSheet.Cells[i + 2, 6].Value = meteringResultDatas[i].V4;
                resultSheet.Cells[i + 2, 6].Style.Font.Color.SetColor(Color.Black);//字体颜色
                resultSheet.Cells[i + 2, 6].Style.Font.Name = "SimSun";//字体
                resultSheet.Cells[i + 2, 6].Style.Font.Size = 10;//字体大小
                resultSheet.Cells[i + 2, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //检定/校准数据5
                resultSheet.Cells[i + 2, 7].Value = meteringResultDatas[i].V5;
                resultSheet.Cells[i + 2, 7].Style.Font.Color.SetColor(Color.Black);//字体颜色
                resultSheet.Cells[i + 2, 7].Style.Font.Name = "SimSun";//字体
                resultSheet.Cells[i + 2, 7].Style.Font.Size = 10;//字体大小
                resultSheet.Cells[i + 2, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //检定/校准数据6
                resultSheet.Cells[i + 2, 8].Value = meteringResultDatas[i].V6;
                resultSheet.Cells[i + 2, 8].Style.Font.Color.SetColor(Color.Black);//字体颜色
                resultSheet.Cells[i + 2, 8].Style.Font.Name = "SimSun";//字体
                resultSheet.Cells[i + 2, 8].Style.Font.Size = 10;//字体大小
                resultSheet.Cells[i + 2, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }
            #endregion

            ExcelWorksheet checkedSheet = package.Workbook.Worksheets["核验结果"];
            #region 核验结果
            List<EquipmentMeteringCheckedData> meteringCheckedDatas = new();
            equipmentMeteringCertificates.ForEach(x => { if (x.MeteringCheckedDatas != null) meteringCheckedDatas.AddRange(x.MeteringCheckedDatas); });
            for (int i = 0; i < meteringCheckedDatas.Count; i++)
            {
                //序号
                checkedSheet.Cells[i + 2, 1].Value = i + 1;
                checkedSheet.Cells[i + 2, 1].Style.Font.Color.SetColor(Color.Black);//字体颜色
                checkedSheet.Cells[i + 2, 1].Style.Font.Name = "SimSun";//字体
                checkedSheet.Cells[i + 2, 1].Style.Font.Size = 10;//字体大小
                checkedSheet.Cells[i + 2, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //证书编号
                resultSheet.Cells[i + 2, 2].Value = meteringCheckedDatas[i].CertificateNumber;
                resultSheet.Cells[i + 2, 2].Style.Font.Color.SetColor(Color.Black);//字体颜色
                resultSheet.Cells[i + 2, 2].Style.Font.Name = "SimSun";//字体
                resultSheet.Cells[i + 2, 2].Style.Font.Size = 10;//字体大小
                resultSheet.Cells[i + 2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //核验流量
                resultSheet.Cells[i + 2, 2].Value = meteringCheckedDatas[i].V1;
                resultSheet.Cells[i + 2, 2].Style.Font.Color.SetColor(Color.Black);//字体颜色
                resultSheet.Cells[i + 2, 2].Style.Font.Name = "SimSun";//字体
                resultSheet.Cells[i + 2, 2].Style.Font.Size = 10;//字体大小
                resultSheet.Cells[i + 2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //示值误差
                resultSheet.Cells[i + 2, 2].Value = meteringCheckedDatas[i].V2;
                resultSheet.Cells[i + 2, 2].Style.Font.Color.SetColor(Color.Black);//字体颜色
                resultSheet.Cells[i + 2, 2].Style.Font.Name = "SimSun";//字体
                resultSheet.Cells[i + 2, 2].Style.Font.Size = 10;//字体大小
                resultSheet.Cells[i + 2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //重复性
                resultSheet.Cells[i + 2, 2].Value = meteringCheckedDatas[i].V3;
                resultSheet.Cells[i + 2, 2].Style.Font.Color.SetColor(Color.Black);//字体颜色
                resultSheet.Cells[i + 2, 2].Style.Font.Name = "SimSun";//字体
                resultSheet.Cells[i + 2, 2].Style.Font.Size = 10;//字体大小
                resultSheet.Cells[i + 2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }
            #endregion

            result = package.GetAsByteArray();

            return Task.Run(() => result);
        }
    }
}
