using ColegioMozart.Application.AcademicPeriods;
using ColegioMozart.Application.ClassRoom.dtos;
using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Application.EvaluationTypes.Dtos;
using ColegioMozart.Application.Subjects.Queries;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.Evaluations.Dtos;

public class EvaluationResource : IMapFrom<EEvaluation>
{

    public EvaluationTypeDTO EvaluationType { get; set; }

    public string EvaluationName { get; set; }
    public ESubjectDTO Subject { get; set; }

    public AcademicPeriodDTO AcademicPeriod { get; set; }
    public ClassRoomDto ClassRoom { get; set; }

    public decimal Weight { get; set; }

    public DateTime EvaluationDate { get; set; }

    public DateTime MaxEditDate { get; set; }

    public decimal MaximumScore { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EEvaluation, EvaluationResource>()
            .ForMember(d => d.EvaluationType, opt => opt.MapFrom(s => s.EvaluationType))
            .ForMember(d => d.EvaluationName, opt => opt.MapFrom(s => s.EvaluationName))
            .ForMember(d => d.Subject, opt => opt.MapFrom(s => s.Subject))
            .ForMember(d => d.AcademicPeriod, opt => opt.MapFrom(s => s.AcademicPeriod))
            .ForMember(d => d.ClassRoom, opt => opt.MapFrom(s => s.ClassRoom))
            .ForMember(d => d.Weight, opt => opt.MapFrom(s => s.Weight))
            .ForMember(d => d.EvaluationDate, opt => opt.MapFrom(s => s.EvaluationDate))
            .ForMember(d => d.MaxEditDate, opt => opt.MapFrom(s => s.MaxEditDate))
            .ForMember(d => d.MaximumScore, opt => opt.MapFrom(s => s.MaximumScore));
    }

}
