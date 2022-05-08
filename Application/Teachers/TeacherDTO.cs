using AutoMapper;
using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ColegioMozart.Application.Teachers;

public class TeacherDTO : IMapFrom<ETeacher>
{
    [Display(Name = "Id")]
    public Guid Id { get; set; }

    [Display(Name = "Documento")]
    public string DocumentTypeAndNumber { get; set; }

    [Display(Name = "Nombres completos")]
    public string FullName { get; set; }

    [Display(Name = "Correo")]
    public string Email { get; set; }

    [Display(Name = "Celular")]
    public string Phone { get; set; }


    [Display(Name = "Edad")]
    public int Age { get; set; }

    [Display(Name = "Genero")]
    public string GenderName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ETeacher, TeacherDTO>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.DocumentTypeAndNumber, opt => opt.MapFrom(s => s.Person.DocumentType.Name + " - " + s.Person.DocumentNumber))
            .ForMember(d => d.FullName, opt => opt.MapFrom(s => $"{s.Person.Name} {s.Person.LastName} {s.Person.MothersLastName}"))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.Phone, opt => opt.MapFrom(s => s.Phone))
            .ForMember(d => d.Age, opt => opt.MapFrom(s => s.Person.DateOfBirth.GetAge()))
            .ForMember(d => d.GenderName, opt => opt.MapFrom(s => s.Person.Gender.Name));
    }
}
