using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Example2.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using System.Collections.Generic;

namespace MyCompanyName.AbpZeroTemplate.Example2
{
    public interface IOrdersAppService : IApplicationService 
    {
        Task<PagedResultDto<GetOrderForViewDto>> GetAll(GetAllOrdersInput input);

        Task<GetOrderForViewDto> GetOrderForView(int id);

		Task<GetOrderForEditOutput> GetOrderForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditOrderDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetOrdersToExcel(GetAllOrdersForExcelInput input);

        Task<List<GetOrderForViewDto>> GetAllOrder(string UserFilter);

    }
}