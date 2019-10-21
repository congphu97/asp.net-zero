using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Example.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using System.Collections.Generic;

namespace MyCompanyName.AbpZeroTemplate.Example
{
    public interface ICarsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetCarForViewDto>> GetAll(GetAllCarsInput input);

        Task<GetCarForViewDto> GetCarForView(int id);

		Task<GetCarForEditOutput> GetCarForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditCarDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetCarsToExcel(GetAllCarsForExcelInput input);

        Task<List<GetCarForViewDto>> GetAllCar();
    }
}