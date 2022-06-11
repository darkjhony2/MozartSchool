using AutoMapper;
using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ColegioMozart.Application.AcademicLevels;

public class AcademicLevelDTO : IMapFrom<EAcademicLevel>
{

    [Display(Name = "Id")]
    public int Id { get; set; }

    [Display(Name = "Nivel")]
    public string Scale { get; set; }
    public int ScaleId { get; set; }

    [Display(Name = "Grado")]
    public string Level { get; set; }

    [Display(Name = "Grado Anterior")]
    public string PreviousAcademicLevel { get; set; }
    public int PreviousAcademicLevelId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EAcademicLevel, AcademicLevelDTO>()
            .ForMember(d => d.Level, opt => opt.MapFrom(s => s.Level))
            .ForMember(d => d.ScaleId, opt => opt.MapFrom(s => s.AcademicScale.Id))
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Scale, opt => opt.MapFrom(s => s.AcademicScale.Name))
            .ForMember(d => d.PreviousAcademicLevelId, opt => opt.MapFrom(s => s.PreviousAcademicLevel.Id))
            .ForMember(d => d.PreviousAcademicLevel, opt => opt.MapFrom(s => s.PreviousAcademicLevel.Level));
    }

}
