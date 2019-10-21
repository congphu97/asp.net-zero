using System.Collections.Generic;
using MyCompanyName.AbpZeroTemplate.Example.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.Example.Exporting
{
    public interface IListsExcelExporter
    {
        FileDto ExportToFile(List<GetListForViewDto> lists);
    }
}