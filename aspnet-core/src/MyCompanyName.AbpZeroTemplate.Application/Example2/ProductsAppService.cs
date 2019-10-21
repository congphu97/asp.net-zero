

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
	[AbpAuthorize(AppPermissions.Pages_Products)]
    public class ProductsAppService : AbpZeroTemplateAppServiceBase, IProductsAppService
    {
		 private readonly IRepository<Product> _productRepository;
		 private readonly IProductsExcelExporter _productsExcelExporter;
		 

		  public ProductsAppService(IRepository<Product> productRepository, IProductsExcelExporter productsExcelExporter ) 
		  {
			_productRepository = productRepository;
			_productsExcelExporter = productsExcelExporter;
			
		  }
        public async Task<List<GetProductForViewDto>> GetAllProduct()
        {
            var input = new GetAllProductsInput();

            var result = await GetAll(input);


            return result.Items.ToList();
        }
        public async Task<PagedResultDto<GetProductForViewDto>> GetAll(GetAllProductsInput input)
         {
			
			var filteredProducts = _productRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.CategoryID.Contains(input.Filter) || e.Name.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.CategoryIDFilter),  e => e.CategoryID.ToLower() == input.CategoryIDFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name.ToLower() == input.NameFilter.ToLower().Trim());


			var query = (from o in filteredProducts
                         select new GetProductForViewDto() {
							Product = new ProductDto
							{
                                CategoryID = o.CategoryID,
                                Name = o.Name,
                                Id = o.Id
							}
						});

            var totalCount = await query.CountAsync();

            var products = await query
                .OrderBy(input.Sorting ?? "product.id asc")
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<GetProductForViewDto>(
                totalCount,
                products
            );
         }
		 
		 public async Task<GetProductForViewDto> GetProductForView(int id)
         {
            var product = await _productRepository.GetAsync(id);

            var output = new GetProductForViewDto { Product = ObjectMapper.Map<ProductDto>(product) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Products_Edit)]
		 public async Task<GetProductForEditOutput> GetProductForEdit(EntityDto input)
         {
            var product = await _productRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetProductForEditOutput {Product = ObjectMapper.Map<CreateOrEditProductDto>(product)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditProductDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Products_Create)]
		 private async Task Create(CreateOrEditProductDto input)
         {
            var product = ObjectMapper.Map<Product>(input);

			

            await _productRepository.InsertAsync(product);
         }

		 [AbpAuthorize(AppPermissions.Pages_Products_Edit)]
		 private async Task Update(CreateOrEditProductDto input)
         {
            var product = await _productRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, product);
         }

		 [AbpAuthorize(AppPermissions.Pages_Products_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _productRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetProductsToExcel(GetAllProductsForExcelInput input)
         {
			
			var filteredProducts = _productRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.CategoryID.Contains(input.Filter) || e.Name.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.CategoryIDFilter),  e => e.CategoryID.ToLower() == input.CategoryIDFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name.ToLower() == input.NameFilter.ToLower().Trim());


			var query = (from o in filteredProducts
                         select new GetProductForViewDto() { 
							Product = new ProductDto
							{
                                CategoryID = o.CategoryID,
                                Name = o.Name,
                                Id = o.Id
							}
						 });


            var productListDtos = await query.ToListAsync();

            return _productsExcelExporter.ExportToFile(productListDtos);
         }


    }
}