

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MyCompanyName.AbpZeroTemplate.Example2.Exporting;
using MyCompanyName.AbpZeroTemplate.Example2.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MyCompanyName.AbpZeroTemplate.Example2
{
	[AbpAuthorize(AppPermissions.Pages_Categories)]
    public class CategoriesAppService : AbpZeroTemplateAppServiceBase, ICategoriesAppService
    {
		 private readonly IRepository<Category> _categoryRepository;
		 private readonly ICategoriesExcelExporter _categoriesExcelExporter;
		 

		  public CategoriesAppService(IRepository<Category> categoryRepository, ICategoriesExcelExporter categoriesExcelExporter ) 
		  {
			_categoryRepository = categoryRepository;
			_categoriesExcelExporter = categoriesExcelExporter;
			
		  }
        public async Task<List<GetCategoryForViewDto>> GetAllCategory()
        {
            var input = new GetAllCategoriesInput();

            var result = await GetAll(input);


            return result.Items.ToList();
        }
        public async Task<PagedResultDto<GetCategoryForViewDto>> GetAll(GetAllCategoriesInput input)
         {
			
			var filteredCategories = _categoryRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name.ToLower() == input.NameFilter.ToLower().Trim());


			var query = (from o in filteredCategories
                         select new GetCategoryForViewDto() {
							Category = new CategoryDto
							{
                                Name = o.Name,
                                Id = o.Id
							}
						});

            var totalCount = await query.CountAsync();

            var categories = await query
                .OrderBy(input.Sorting ?? "category.id asc")
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<GetCategoryForViewDto>(
                totalCount,
                categories
            );
         }
		 
		 public async Task<GetCategoryForViewDto> GetCategoryForView(int id)
         {
            var category = await _categoryRepository.GetAsync(id);

            var output = new GetCategoryForViewDto { Category = ObjectMapper.Map<CategoryDto>(category) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Categories_Edit)]
		 public async Task<GetCategoryForEditOutput> GetCategoryForEdit(EntityDto input)
         {
            var category = await _categoryRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetCategoryForEditOutput {Category = ObjectMapper.Map<CreateOrEditCategoryDto>(category)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditCategoryDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Categories_Create)]
		 private async Task Create(CreateOrEditCategoryDto input)
         {
            var category = ObjectMapper.Map<Category>(input);

			

            await _categoryRepository.InsertAsync(category);
         }

		 [AbpAuthorize(AppPermissions.Pages_Categories_Edit)]
		 private async Task Update(CreateOrEditCategoryDto input)
         {
            var category = await _categoryRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, category);
         }

		 [AbpAuthorize(AppPermissions.Pages_Categories_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _categoryRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetCategoriesToExcel(GetAllCategoriesForExcelInput input)
         {
			
			var filteredCategories = _categoryRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name.ToLower() == input.NameFilter.ToLower().Trim());


			var query = (from o in filteredCategories
                         select new GetCategoryForViewDto() { 
							Category = new CategoryDto
							{
                                Name = o.Name,
                                Id = o.Id
							}
						 });


            var categoryListDtos = await query.ToListAsync();

            return _categoriesExcelExporter.ExportToFile(categoryListDtos);
         }


    }
}