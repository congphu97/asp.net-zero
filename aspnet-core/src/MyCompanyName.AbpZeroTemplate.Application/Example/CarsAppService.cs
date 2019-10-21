

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MyCompanyName.AbpZeroTemplate.Example.Exporting;
using MyCompanyName.AbpZeroTemplate.Example.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MyCompanyName.AbpZeroTemplate.Example
{
	[AbpAuthorize(AppPermissions.Pages_Cars)]
    public class CarsAppService : AbpZeroTemplateAppServiceBase, ICarsAppService
    {
		 private readonly IRepository<Car> _carRepository;
		 private readonly ICarsExcelExporter _carsExcelExporter;
		 

		  public CarsAppService(IRepository<Car> carRepository, ICarsExcelExporter carsExcelExporter ) 
		  {
			_carRepository = carRepository;
			_carsExcelExporter = carsExcelExporter;
			
		  }
        public async Task<List<GetCarForViewDto>> GetAllCar()
        {
            var input = new GetAllCarsInput();

            var result = await GetAll(input);


            return result.Items.ToList();
        }
        public async Task<PagedResultDto<GetCarForViewDto>> GetAll(GetAllCarsInput input)
         {
			
			var filteredCars = _carRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.name.Contains(input.Filter) || e.detail.Contains(input.Filter) || e.price.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.nameFilter),  e => e.name.ToLower() == input.nameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.detailFilter),  e => e.detail.ToLower() == input.detailFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.priceFilter),  e => e.price.ToLower() == input.priceFilter.ToLower().Trim());


			var query = (from o in filteredCars
                         select new GetCarForViewDto() {
							Car = new CarDto
							{
                                name = o.name,
                                detail = o.detail,
                                price = o.price,
                                Id = o.Id
							}
						});

            var totalCount = await query.CountAsync();

            var cars = await query
                .OrderBy(input.Sorting ?? "car.id asc")
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<GetCarForViewDto>(
                totalCount,
                cars
            );
         }
		 
		 public async Task<GetCarForViewDto> GetCarForView(int id)
         {
            var car = await _carRepository.GetAsync(id);

            var output = new GetCarForViewDto { Car = ObjectMapper.Map<CarDto>(car) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Cars_Edit)]
		 public async Task<GetCarForEditOutput> GetCarForEdit(EntityDto input)
         {
            var car = await _carRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetCarForEditOutput {Car = ObjectMapper.Map<CreateOrEditCarDto>(car)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditCarDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Cars_Create)]
		 private async Task Create(CreateOrEditCarDto input)
         {
            var car = ObjectMapper.Map<Car>(input);

			

            await _carRepository.InsertAsync(car);
         }

		 [AbpAuthorize(AppPermissions.Pages_Cars_Edit)]
		 private async Task Update(CreateOrEditCarDto input)
         {
            var car = await _carRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, car);
         }

		 [AbpAuthorize(AppPermissions.Pages_Cars_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _carRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetCarsToExcel(GetAllCarsForExcelInput input)
         {
			
			var filteredCars = _carRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.name.Contains(input.Filter) || e.detail.Contains(input.Filter) || e.price.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.nameFilter),  e => e.name.ToLower() == input.nameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.detailFilter),  e => e.detail.ToLower() == input.detailFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.priceFilter),  e => e.price.ToLower() == input.priceFilter.ToLower().Trim());


			var query = (from o in filteredCars
                         select new GetCarForViewDto() { 
							Car = new CarDto
							{
                                name = o.name,
                                detail = o.detail,
                                price = o.price,
                                Id = o.Id
							}
						 });


            var carListDtos = await query.ToListAsync();

            return _carsExcelExporter.ExportToFile(carListDtos);
         }


    }
}