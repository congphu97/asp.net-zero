using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Example2.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using System.Collections.Generic;

namespace MyCompanyName.AbpZeroTemplate.Example2
{
    public interface IVariantsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetVariantForViewDto>> GetAll(GetAllVariantsInput input);

        Task<GetVariantForViewDto> GetVariantForView(int id);

		Task<GetVariantForEditOutput> GetVariantForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditVariantDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetVariantsToExcel(GetAllVariantsForExcelInput input);

        Task<List<GetVariantForViewDto>> GetAllVariant();
    }
}