using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Example2.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using System.Collections.Generic;

namespace MyCompanyName.AbpZeroTemplate.Example2
{
    public interface ICategoriesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetCategoryForViewDto>> GetAll(GetAllCategoriesInput input);

        Task<GetCategoryForViewDto> GetCategoryForView(int id);

		Task<GetCategoryForEditOutput> GetCategoryForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditCategoryDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetCategoriesToExcel(GetAllCategoriesForExcelInput input);

        Task<List<GetCategoryForViewDto>> GetAllCategory();
    }
}