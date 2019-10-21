using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.Example2.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.Example2.Exporting
{
    public class OrdersExcelExporter : EpPlusExcelExporterBase, IOrdersExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public OrdersExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetOrderForViewDto> orders)
        {
            return CreateExcelPackage(
                "Orders.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Orders"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ProductID"),
                        L("OrderDate"),
                        L("Status"),
                        L("PaymentDate"),
                        L("Amount"),
                        L("TotalVAT"),
                        L("User")
                        );

                    AddObjects(
                        sheet, 2, orders,
                        _ => _.Order.ProductID,
                        _ => _.Order.OrderDate,
                        _ => _.Order.Status,
                        _ => _.Order.PaymentDate,
                        _ => _.Order.Amount,
                        _ => _.Order.TotalVAT,
                        _ => _.Order.User
                        );

					

                });
        }
    }
}
