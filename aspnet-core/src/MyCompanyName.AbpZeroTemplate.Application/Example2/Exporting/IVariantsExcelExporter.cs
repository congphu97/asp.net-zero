using System.Collections.Generic;
using MyCompanyName.AbpZeroTemplate.Example2.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.Example2.Exporting
{
    public interface IVariantsExcelExporter
    {
        FileDto ExportToFile(List<GetVariantForViewDto> variants);
    }
}