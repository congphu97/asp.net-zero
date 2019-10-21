using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Example.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.Example
{
    public interface IPersonsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPersonForViewDto>> GetAll(GetAllPersonsInput input);

        Task<GetPersonForViewDto> GetPersonForView(int id);

		Task<GetPersonForEditOutput> GetPersonForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPersonDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetPersonsToExcel(GetAllPersonsForExcelInput input);

		
    }
}