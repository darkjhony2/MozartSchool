using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.Genders.Dtos
{
    public class GenderDTO : IMapFrom<EGender>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
