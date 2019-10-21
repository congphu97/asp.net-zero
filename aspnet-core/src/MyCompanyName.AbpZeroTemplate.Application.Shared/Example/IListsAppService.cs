using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Example.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using System.Collections.Generic;

namespace MyCompanyName.AbpZeroTemplate.Example
{
    public interface IListsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetListForViewDto>> GetAll(GetAllListsInput input);

        Task<GetListForViewDto> GetListForView(int id);

		Task<GetListForEditOutput> GetListForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditListDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetListsToExcel(GetAllListsForExcelInput input);

        Task<List<GetListForViewDto>> GetAllList();


    }
}