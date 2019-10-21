using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.Example.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.Example.Exporting
{
    public class PersonsExcelExporter : EpPlusExcelExporterBase, IPersonsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PersonsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPersonForViewDto> persons)
        {
            return CreateExcelPackage(
                "Persons.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Persons"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("name"),
                        L("age")
                        );

                    AddObjects(
                        sheet, 2, persons,
                        _ => _.Person.name,
                        _ => _.Person.age
                        );

					

                });
        }
    }
}
