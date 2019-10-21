using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Example2.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using System.Collections.Generic;

namespace MyCompanyName.AbpZeroTemplate.Example2
{
    public interface IProductsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetProductForViewDto>> GetAll(GetAllProductsInput input);

        Task<GetProductForViewDto> GetProductForView(int id);

		Task<GetProductForEditOutput> GetProductForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditProductDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetProductsToExcel(GetAllProductsForExcelInput input);

        Task<List<GetProductForViewDto>> GetAllProduct();
    }
}