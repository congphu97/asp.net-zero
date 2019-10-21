using Abp.Dependency;
using GraphQL;
using GraphQL.Types;
using MyCompanyName.AbpZeroTemplate.Queries.Container;

namespace MyCompanyName.AbpZeroTemplate.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IDependencyResolver resolver) :
            base(resolver)
        {
            Query = resolver.Resolve<QueryContainer>();
        }
    }
}