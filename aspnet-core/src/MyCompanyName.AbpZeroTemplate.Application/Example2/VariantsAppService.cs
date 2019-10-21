

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
	[AbpAuthorize(AppPermissions.Pages_Variants)]
    public class VariantsAppService : AbpZeroTemplateAppServiceBase, IVariantsAppService
    {
		 private readonly IRepository<Variant> _variantRepository;
		 private readonly IVariantsExcelExporter _variantsExcelExporter;
		 

		  public VariantsAppService(IRepository<Variant> variantRepository, IVariantsExcelExporter variantsExcelExporter ) 
		  {
			_variantRepository = variantRepository;
			_variantsExcelExporter = variantsExcelExporter;
			
		  }
        public async Task<List<GetVariantForViewDto>> GetAllVariant()
        {
            var input = new GetAllVariantsInput();

            var result = await GetAll(input);


            return result.Items.ToList();
        }
        public async Task<PagedResultDto<GetVariantForViewDto>> GetAll(GetAllVariantsInput input)
         {
			
			var filteredVariants = _variantRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter) || e.ProductID.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductIDFilter),  e => e.ProductID.ToLower() == input.ProductIDFilter.ToLower().Trim())
						.WhereIf(input.MinPriceFilter != null, e => e.Price >= input.MinPriceFilter)
						.WhereIf(input.MaxPriceFilter != null, e => e.Price <= input.MaxPriceFilter);


			var query = (from o in filteredVariants
                         select new GetVariantForViewDto() {
							Variant = new VariantDto
							{
                                Name = o.Name,
                                ProductID = o.ProductID,
                                Price = o.Price,
                                Id = o.Id
							}
						});

            var totalCount = await query.CountAsync();

            var variants = await query
                .OrderBy(input.Sorting ?? "variant.id asc")
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<GetVariantForViewDto>(
                totalCount,
                variants
            );
         }
		 
		 public async Task<GetVariantForViewDto> GetVariantForView(int id)
         {
            var variant = await _variantRepository.GetAsync(id);

            var output = new GetVariantForViewDto { Variant = ObjectMapper.Map<VariantDto>(variant) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Variants_Edit)]
		 public async Task<GetVariantForEditOutput> GetVariantForEdit(EntityDto input)
         {
            var variant = await _variantRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetVariantForEditOutput {Variant = ObjectMapper.Map<CreateOrEditVariantDto>(variant)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditVariantDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Variants_Create)]
		 private async Task Create(CreateOrEditVariantDto input)
         {
            var variant = ObjectMapper.Map<Variant>(input);

			

            await _variantRepository.InsertAsync(variant);
         }

		 [AbpAuthorize(AppPermissions.Pages_Variants_Edit)]
		 private async Task Update(CreateOrEditVariantDto input)
         {
            var variant = await _variantRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, variant);
         }

		 [AbpAuthorize(AppPermissions.Pages_Variants_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _variantRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetVariantsToExcel(GetAllVariantsForExcelInput input)
         {
			
			var filteredVariants = _variantRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter) || e.ProductID.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name.ToLower() == input.NameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductIDFilter),  e => e.ProductID.ToLower() == input.ProductIDFilter.ToLower().Trim())
						.WhereIf(input.MinPriceFilter != null, e => e.Price >= input.MinPriceFilter)
						.WhereIf(input.MaxPriceFilter != null, e => e.Price <= input.MaxPriceFilter);


			var query = (from o in filteredVariants
                         select new GetVariantForViewDto() { 
							Variant = new VariantDto
							{
                                Name = o.Name,
                                ProductID = o.ProductID,
                                Price = o.Price,
                                Id = o.Id
							}
						 });


            var variantListDtos = await query.ToListAsync();

            return _variantsExcelExporter.ExportToFile(variantListDtos);
         }


    }
}