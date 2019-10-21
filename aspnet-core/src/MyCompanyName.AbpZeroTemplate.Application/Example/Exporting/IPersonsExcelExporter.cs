using System.Collections.Generic;
using MyCompanyName.AbpZeroTemplate.Example.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.Example.Exporting
{
    public interface IPersonsExcelExporter
    {
        FileDto ExportToFile(List<GetPersonForViewDto> persons);
    }
}