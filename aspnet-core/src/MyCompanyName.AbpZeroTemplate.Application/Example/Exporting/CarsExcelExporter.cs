using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.Example.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.Example.Exporting
{
    public class CarsExcelExporter : EpPlusExcelExporterBase, ICarsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public CarsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetCarForViewDto> cars)
        {
            return CreateExcelPackage(
                "Cars.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Cars"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("name"),
                        L("detail"),
                        L("price")
                        );

                    AddObjects(
                        sheet, 2, cars,
                        _ => _.Car.name,
                        _ => _.Car.detail,
                        _ => _.Car.price
                        );

					

                });
        }
    }
}
