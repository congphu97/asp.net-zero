using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.Example.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.Example.Exporting
{
    public class ListsExcelExporter : EpPlusExcelExporterBase, IListsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ListsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetListForViewDto> lists)
        {
            return CreateExcelPackage(
                "Lists.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Lists"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("CarID"),
                        L("name"),
                        L("detail"),
                        L("price")
                        );

                    AddObjects(
                        sheet, 2, lists,
                        _ => _.List.CarID,
                        _ => _.List.name,
                        _ => _.List.detail,
                        _ => _.List.price
                        );

					

                });
        }
    }
}
