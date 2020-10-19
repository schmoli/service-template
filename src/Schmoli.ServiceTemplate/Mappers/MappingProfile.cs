using AutoMapper;
using Schmoli.ServiceTemplate.Models;
using Schmoli.ServiceTemplate.Resources;
using Schmoli.Services.Core.Results;

namespace Schmoli.ServiceTemplate.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entity to Resource
            CreateMap<PrimaryItem, PrimaryItemResource>();
            CreateMap<PagedResultSet<PrimaryItem>, PagedResultSet<PrimaryItemResource>>();
            CreateMap<SecondaryItem, SecondaryItemResource>();
            CreateMap<PagedResultSet<SecondaryItem>, PagedResultSet<SecondaryItemResource>>();
            CreateMap<SecondaryItem, SecondaryItemSaveResource>();

            // Resource to Entity
            CreateMap<PrimaryItemResource, PrimaryItem>();
            CreateMap<PrimaryItemSaveResource, PrimaryItem>();
            CreateMap<SecondaryItemResource, SecondaryItem>();
            CreateMap<SecondaryItemSaveResource, SecondaryItem>();
        }
    }
}
