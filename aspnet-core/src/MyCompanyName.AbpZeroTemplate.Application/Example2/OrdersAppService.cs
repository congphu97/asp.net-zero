

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
	[AbpAuthorize(AppPermissions.Pages_Orders)]
    public class OrdersAppService : AbpZeroTemplateAppServiceBase, IOrdersAppService
    {
		 private readonly IRepository<Order> _orderRepository;
		 private readonly IOrdersExcelExporter _ordersExcelExporter;
		 

		  public OrdersAppService(IRepository<Order> orderRepository, IOrdersExcelExporter ordersExcelExporter ) 
		  {
			_orderRepository = orderRepository;
			_ordersExcelExporter = ordersExcelExporter;
			
		  }
        public async Task<List<GetOrderForViewDto>> GetAllOrder(string UserFilter)
        {
            var input = new GetAllOrdersInput { UserFilter = UserFilter };
                

            var result = await GetAll(input);


            return result.Items.ToList();
        }

        public async Task<PagedResultDto<GetOrderForViewDto>> GetAll(GetAllOrdersInput input)
         {
			
			var filteredOrders = _orderRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ProductID.Contains(input.Filter) || e.OrderDate.Contains(input.Filter) || e.Status.Contains(input.Filter) || e.PaymentDate.Contains(input.Filter) || e.User.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductIDFilter),  e => e.ProductID.ToLower() == input.ProductIDFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.OrderDateFilter),  e => e.OrderDate.ToLower() == input.OrderDateFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.StatusFilter),  e => e.Status.ToLower() == input.StatusFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PaymentDateFilter),  e => e.PaymentDate.ToLower() == input.PaymentDateFilter.ToLower().Trim())
						.WhereIf(input.MinAmountFilter != null, e => e.Amount >= input.MinAmountFilter)
						.WhereIf(input.MaxAmountFilter != null, e => e.Amount <= input.MaxAmountFilter)
						.WhereIf(input.MinTotalVATFilter != null, e => e.TotalVAT >= input.MinTotalVATFilter)
						.WhereIf(input.MaxTotalVATFilter != null, e => e.TotalVAT <= input.MaxTotalVATFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserFilter),  e => e.User.ToLower() == input.UserFilter.ToLower().Trim());


			var query = (from o in filteredOrders
                         select new GetOrderForViewDto() {
							Order = new OrderDto
							{
                                ProductID = o.ProductID,
                                OrderDate = o.OrderDate,
                                Status = o.Status,
                                PaymentDate = o.PaymentDate,
                                Amount = o.Amount,
                                TotalVAT = o.TotalVAT,
                                User = o.User,
                                Id = o.Id
							}
						});

            var totalCount = await query.CountAsync();

            var orders = await query
                .OrderBy(input.Sorting ?? "order.id asc")
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<GetOrderForViewDto>(
                totalCount,
                orders
            );
         }
		 
		 public async Task<GetOrderForViewDto> GetOrderForView(int id)
         {
            var order = await _orderRepository.GetAsync(id);

            var output = new GetOrderForViewDto { Order = ObjectMapper.Map<OrderDto>(order) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Orders_Edit)]
		 public async Task<GetOrderForEditOutput> GetOrderForEdit(EntityDto input)
         {
            var order = await _orderRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetOrderForEditOutput {Order = ObjectMapper.Map<CreateOrEditOrderDto>(order)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditOrderDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Orders_Create)]
		 private async Task Create(CreateOrEditOrderDto input)
         {
            var order = ObjectMapper.Map<Order>(input);

			

            await _orderRepository.InsertAsync(order);
         }

		 [AbpAuthorize(AppPermissions.Pages_Orders_Edit)]
		 private async Task Update(CreateOrEditOrderDto input)
         {
            var order = await _orderRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, order);
         }

		 [AbpAuthorize(AppPermissions.Pages_Orders_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _orderRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetOrdersToExcel(GetAllOrdersForExcelInput input)
         {
			
			var filteredOrders = _orderRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ProductID.Contains(input.Filter) || e.OrderDate.Contains(input.Filter) || e.Status.Contains(input.Filter) || e.PaymentDate.Contains(input.Filter) || e.User.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductIDFilter),  e => e.ProductID.ToLower() == input.ProductIDFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.OrderDateFilter),  e => e.OrderDate.ToLower() == input.OrderDateFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.StatusFilter),  e => e.Status.ToLower() == input.StatusFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PaymentDateFilter),  e => e.PaymentDate.ToLower() == input.PaymentDateFilter.ToLower().Trim())
						.WhereIf(input.MinAmountFilter != null, e => e.Amount >= input.MinAmountFilter)
						.WhereIf(input.MaxAmountFilter != null, e => e.Amount <= input.MaxAmountFilter)
						.WhereIf(input.MinTotalVATFilter != null, e => e.TotalVAT >= input.MinTotalVATFilter)
						.WhereIf(input.MaxTotalVATFilter != null, e => e.TotalVAT <= input.MaxTotalVATFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserFilter),  e => e.User.ToLower() == input.UserFilter.ToLower().Trim());


			var query = (from o in filteredOrders
                         select new GetOrderForViewDto() { 
							Order = new OrderDto
							{
                                ProductID = o.ProductID,
                                OrderDate = o.OrderDate,
                                Status = o.Status,
                                PaymentDate = o.PaymentDate,
                                Amount = o.Amount,
                                TotalVAT = o.TotalVAT,
                                User = o.User,
                                Id = o.Id
							}
						 });


            var orderListDtos = await query.ToListAsync();

            return _ordersExcelExporter.ExportToFile(orderListDtos);
         }


    }
}