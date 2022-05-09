using AutoMapper;
using ColegioMozart.Application.Common.Mappings;

namespace ColegioMozart.Application.Common.CommonCRUD.Queries;

public class ViewMenuDTO : IMapFrom<EView>
{
    public string Name { get; set; }
    public string DisplayName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EView, ViewMenuDTO>()
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.DisplayName, opt => opt.MapFrom(s => s.DisplayName));
    }

}
