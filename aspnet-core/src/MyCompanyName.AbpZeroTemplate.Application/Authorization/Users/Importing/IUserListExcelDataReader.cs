using System.Collections.Generic;
using MyCompanyName.AbpZeroTemplate.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace MyCompanyName.AbpZeroTemplate.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
