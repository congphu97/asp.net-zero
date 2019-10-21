

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
	[AbpAuthorize(AppPermissions.Pages_Lists)]
    public class ListsAppService : AbpZeroTemplateAppServiceBase, IListsAppService
    {
		 private readonly IRepository<List> _listRepository;
		 private readonly IListsExcelExporter _listsExcelExporter;
		 

		  public ListsAppService(IRepository<List> listRepository, IListsExcelExporter listsExcelExporter ) 
		  {
			_listRepository = listRepository;
			_listsExcelExporter = listsExcelExporter;
			
		  }
        public async Task<List<GetListForViewDto>> GetAllList()
        {
            var input = new GetAllListsInput();

            var result = await GetAll(input);


            return result.Items.ToList();
        }
        public async Task<PagedResultDto<GetListForViewDto>> GetAll(GetAllListsInput input)
         {
			
			var filteredLists = _listRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.name.Contains(input.Filter) || e.detail.Contains(input.Filter) || e.price.Contains(input.Filter))
						.WhereIf(input.MinCarIDFilter != null, e => e.CarID >= input.MinCarIDFilter)
						.WhereIf(input.MaxCarIDFilter != null, e => e.CarID <= input.MaxCarIDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.nameFilter),  e => e.name.ToLower() == input.nameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.detailFilter),  e => e.detail.ToLower() == input.detailFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.priceFilter),  e => e.price.ToLower() == input.priceFilter.ToLower().Trim());


			var query = (from o in filteredLists
                         select new GetListForViewDto() {
							List = new ListDto
							{
                                CarID = o.CarID,
                                name = o.name,
                                detail = o.detail,
                                price = o.price,
                                Id = o.Id
							}
						});

            var totalCount = await query.CountAsync();

            var lists = await query
                .OrderBy(input.Sorting ?? "list.id asc")
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<GetListForViewDto>(
                totalCount,
                lists
            );
         }
		 
		 public async Task<GetListForViewDto> GetListForView(int id)
         {
            var list = await _listRepository.GetAsync(id);

            var output = new GetListForViewDto { List = ObjectMapper.Map<ListDto>(list) };
			
            return output;
         }

		 
		 [AbpAuthorize(AppPermissions.Pages_Lists_Edit)]
		 public async Task<GetListForEditOutput> GetListForEdit(EntityDto input)
         {
            var list = await _listRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetListForEditOutput {List = ObjectMapper.Map<CreateOrEditListDto>(list)};
            var x = output;
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditListDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Lists_Create)]
		 private async Task Create(CreateOrEditListDto input)
         {
            var list = ObjectMapper.Map<List>(input);

			

            await _listRepository.InsertAsync(list);
         }

		 [AbpAuthorize(AppPermissions.Pages_Lists_Edit)]
		 private async Task Update(CreateOrEditListDto input)
         {
            var list = await _listRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, list);
         }

		 [AbpAuthorize(AppPermissions.Pages_Lists_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _listRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetListsToExcel(GetAllListsForExcelInput input)
         {
			
			var filteredLists = _listRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.name.Contains(input.Filter) || e.detail.Contains(input.Filter) || e.price.Contains(input.Filter))
						.WhereIf(input.MinCarIDFilter != null, e => e.CarID >= input.MinCarIDFilter)
						.WhereIf(input.MaxCarIDFilter != null, e => e.CarID <= input.MaxCarIDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.nameFilter),  e => e.name.ToLower() == input.nameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.detailFilter),  e => e.detail.ToLower() == input.detailFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.priceFilter),  e => e.price.ToLower() == input.priceFilter.ToLower().Trim());


			var query = (from o in filteredLists
                         select new GetListForViewDto() { 
							List = new ListDto
							{
                                CarID = o.CarID,
                                name = o.name,
                                detail = o.detail,
                                price = o.price,
                                Id = o.Id
							}
						 });


            var listListDtos = await query.ToListAsync();

            return _listsExcelExporter.ExportToFile(listListDtos);
         }


    }
}