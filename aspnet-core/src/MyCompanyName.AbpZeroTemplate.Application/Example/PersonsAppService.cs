

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
using System.Data.SqlClient;

namespace MyCompanyName.AbpZeroTemplate.Example
{
	[AbpAuthorize(AppPermissions.Pages_Persons)]
    public class PersonsAppService : AbpZeroTemplateAppServiceBase, IPersonsAppService
    {
		 private readonly IRepository<Person> _personRepository;
		 private readonly IPersonsExcelExporter _personsExcelExporter;
		 

		  public PersonsAppService(IRepository<Person> personRepository, IPersonsExcelExporter personsExcelExporter ) 
		  {
			_personRepository = personRepository;
			_personsExcelExporter = personsExcelExporter;
			
		  }
 
        public async Task<PagedResultDto<GetPersonForViewDto>> GetAll(GetAllPersonsInput input)
         {
			
			var filteredPersons = _personRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.name.Contains(input.Filter) || e.age.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.nameFilter),  e => e.name.ToLower() == input.nameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ageFilter),  e => e.age.ToLower() == input.ageFilter.ToLower().Trim());


			var query = (from o in filteredPersons
                         select new GetPersonForViewDto() {
							Person = new PersonDto
							{
                                name = o.name,
                                age = o.age,
                                Id = o.Id
							}
						});

            var totalCount = await query.CountAsync();

            var persons = await query
                .OrderBy(input.Sorting ?? "person.id asc")
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<GetPersonForViewDto>(
                totalCount,
                persons
            );
         }
		 
		 public async Task<GetPersonForViewDto> GetPersonForView(int id)
         {
            var person = await _personRepository.GetAsync(id);

            var output = new GetPersonForViewDto { Person = ObjectMapper.Map<PersonDto>(person) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Persons_Edit)]
		 public async Task<GetPersonForEditOutput> GetPersonForEdit(EntityDto input)
         {
            var person = await _personRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPersonForEditOutput {Person = ObjectMapper.Map<CreateOrEditPersonDto>(person)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPersonDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Persons_Create)]
		 private async Task Create(CreateOrEditPersonDto input)
         {
            var person = ObjectMapper.Map<Person>(input);

			

            await _personRepository.InsertAsync(person);
         }

		 [AbpAuthorize(AppPermissions.Pages_Persons_Edit)]
		 private async Task Update(CreateOrEditPersonDto input)
         {
            var person = await _personRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, person);
         }

		 [AbpAuthorize(AppPermissions.Pages_Persons_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _personRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetPersonsToExcel(GetAllPersonsForExcelInput input)
         {
			
			var filteredPersons = _personRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.name.Contains(input.Filter) || e.age.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.nameFilter),  e => e.name.ToLower() == input.nameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ageFilter),  e => e.age.ToLower() == input.ageFilter.ToLower().Trim());


			var query = (from o in filteredPersons
                         select new GetPersonForViewDto() { 
							Person = new PersonDto
							{
                                name = o.name,
                                age = o.age,
                                Id = o.Id
							}
						 });


            var personListDtos = await query.ToListAsync();

            return _personsExcelExporter.ExportToFile(personListDtos);
         }


    }
}