using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace MyCompanyName.AbpZeroTemplate.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));

            var orders = pages.CreateChildPermission(AppPermissions.Pages_Orders, L("Orders"), multiTenancySides: MultiTenancySides.Host);
            orders.CreateChildPermission(AppPermissions.Pages_Orders_Create, L("CreateNewOrder"), multiTenancySides: MultiTenancySides.Host);
            orders.CreateChildPermission(AppPermissions.Pages_Orders_Edit, L("EditOrder"), multiTenancySides: MultiTenancySides.Host);
            orders.CreateChildPermission(AppPermissions.Pages_Orders_Delete, L("DeleteOrder"), multiTenancySides: MultiTenancySides.Host);



            var variants = pages.CreateChildPermission(AppPermissions.Pages_Variants, L("Variants"), multiTenancySides: MultiTenancySides.Host);
            variants.CreateChildPermission(AppPermissions.Pages_Variants_Create, L("CreateNewVariant"), multiTenancySides: MultiTenancySides.Host);
            variants.CreateChildPermission(AppPermissions.Pages_Variants_Edit, L("EditVariant"), multiTenancySides: MultiTenancySides.Host);
            variants.CreateChildPermission(AppPermissions.Pages_Variants_Delete, L("DeleteVariant"), multiTenancySides: MultiTenancySides.Host);



            var products = pages.CreateChildPermission(AppPermissions.Pages_Products, L("Products"), multiTenancySides: MultiTenancySides.Host);
            products.CreateChildPermission(AppPermissions.Pages_Products_Create, L("CreateNewProduct"), multiTenancySides: MultiTenancySides.Host);
            products.CreateChildPermission(AppPermissions.Pages_Products_Edit, L("EditProduct"), multiTenancySides: MultiTenancySides.Host);
            products.CreateChildPermission(AppPermissions.Pages_Products_Delete, L("DeleteProduct"), multiTenancySides: MultiTenancySides.Host);



            var categories = pages.CreateChildPermission(AppPermissions.Pages_Categories, L("Categories"), multiTenancySides: MultiTenancySides.Host);
            categories.CreateChildPermission(AppPermissions.Pages_Categories_Create, L("CreateNewCategory"), multiTenancySides: MultiTenancySides.Host);
            categories.CreateChildPermission(AppPermissions.Pages_Categories_Edit, L("EditCategory"), multiTenancySides: MultiTenancySides.Host);
            categories.CreateChildPermission(AppPermissions.Pages_Categories_Delete, L("DeleteCategory"), multiTenancySides: MultiTenancySides.Host);



            var persons = pages.CreateChildPermission(AppPermissions.Pages_Persons, L("Persons"), multiTenancySides: MultiTenancySides.Host);
            persons.CreateChildPermission(AppPermissions.Pages_Persons_Create, L("CreateNewPerson"), multiTenancySides: MultiTenancySides.Host);
            persons.CreateChildPermission(AppPermissions.Pages_Persons_Edit, L("EditPerson"), multiTenancySides: MultiTenancySides.Host);
            persons.CreateChildPermission(AppPermissions.Pages_Persons_Delete, L("DeletePerson"), multiTenancySides: MultiTenancySides.Host);



            var lists = pages.CreateChildPermission(AppPermissions.Pages_Lists, L("Lists"), multiTenancySides: MultiTenancySides.Host);
            lists.CreateChildPermission(AppPermissions.Pages_Lists_Create, L("CreateNewList"), multiTenancySides: MultiTenancySides.Host);
            lists.CreateChildPermission(AppPermissions.Pages_Lists_Edit, L("EditList"), multiTenancySides: MultiTenancySides.Host);
            lists.CreateChildPermission(AppPermissions.Pages_Lists_Delete, L("DeleteList"), multiTenancySides: MultiTenancySides.Host);



            var cars = pages.CreateChildPermission(AppPermissions.Pages_Cars, L("Cars"), multiTenancySides: MultiTenancySides.Host);
            cars.CreateChildPermission(AppPermissions.Pages_Cars_Create, L("CreateNewCar"), multiTenancySides: MultiTenancySides.Host);
            cars.CreateChildPermission(AppPermissions.Pages_Cars_Edit, L("EditCar"), multiTenancySides: MultiTenancySides.Host);
            cars.CreateChildPermission(AppPermissions.Pages_Cars_Delete, L("DeleteCar"), multiTenancySides: MultiTenancySides.Host);


            pages.CreateChildPermission(AppPermissions.Pages_DemoUiComponents, L("DemoUiComponents"));

            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Unlock, L("Unlock"));

            var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageRoles, L("ManagingRoles"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_UiCustomization, L("VisualSettings"));

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

            //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_MoveTenantsToAnotherEdition, L("MoveTenantsToAnotherEdition"), multiTenancySides: MultiTenancySides.Host); 

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard, L("HangfireDashboard"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroTemplateConsts.LocalizationSourceName);
        }
    }
}
