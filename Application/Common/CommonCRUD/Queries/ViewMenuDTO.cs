using ColegioMozart.Application.Common.Mappings;

namespace ColegioMozart.Application.Common.CommonCRUD.Queries;

public class ViewMenuDTO : IMapFrom<EView>
{
    public string Name { get; set; }
    public string DisplayName { get; set; }

}
