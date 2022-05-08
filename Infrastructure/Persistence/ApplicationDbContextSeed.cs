﻿using ColegioMozart.Domain.Common;
using ColegioMozart.Domain.Entities;
using ColegioMozart.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace ColegioMozart.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var administratorRole = new IdentityRole("Administrator");

        if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await roleManager.CreateAsync(administratorRole);
        }

        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await userManager.CreateAsync(administrator, "Administrator1!");
            await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
        }
    }

    public static async Task SeedSampleDataAsync(ApplicationDbContext context)
    {
        if (!context.Subjects.Any())
        {
            context.Subjects.Add(new ESubject() { Name = "Lenguaje" });
            context.Subjects.Add(new ESubject() { Name = "Ciencia Tecnologia y Ambiente" });
            context.Subjects.Add(new ESubject() { Name = "Literatura" });
            context.Subjects.Add(new ESubject() { Name = "Inglés" });
            context.Subjects.Add(new ESubject() { Name = "Lógico matemático" });
            context.Subjects.Add(new ESubject() { Name = "Razonamiento matemático" });
            context.Subjects.Add(new ESubject() { Name = "Aritmética" });
            context.Subjects.Add(new ESubject() { Name = "Álgebra" });
            context.Subjects.Add(new ESubject() { Name = "Geometría" });
            context.Subjects.Add(new ESubject() { Name = "Trigonometría" });
            context.Subjects.Add(new ESubject() { Name = "Física" });
            context.Subjects.Add(new ESubject() { Name = "Educación física" });

            await context.SaveChangesAsync();
        }

        if (!context.AcademicLevels.Any())
        {
            int id = await AddAcademicLevel(context, "Inicial (3 años)", null);
            id = await AddAcademicLevel(context, "Inicial (4 años)", id);
            id = await AddAcademicLevel(context, "Inicial (5 años)", id);
            id = await AddAcademicLevel(context, "1° Primaria", id);
            id = await AddAcademicLevel(context, "2° Primaria", id);
            id = await AddAcademicLevel(context, "3° Primaria", id);
            id = await AddAcademicLevel(context, "4° Primaria", id);
            id = await AddAcademicLevel(context, "5° Primaria", id);
            id = await AddAcademicLevel(context, "6° Primaria", id);
            id = await AddAcademicLevel(context, "1° Secundaria", id);
            id = await AddAcademicLevel(context, "2° Secundaria", id);
            id = await AddAcademicLevel(context, "3° Secundaria", id);
            id = await AddAcademicLevel(context, "4° Secundaria", id);
            id = await AddAcademicLevel(context, "5° Secundaria", id);
        }

        if (!context.Shifts.Any())
        {
            context.Shifts.Add(new EShift() { Name = "Turno Mañana" });
            context.Shifts.Add(new EShift() { Name = "Turno Tarde" });
            await context.SaveChangesAsync();
        }

        if (!context.Sections.Any())
        {
            context.Sections.Add(new ESection() { Name = "A" });
            context.Sections.Add(new ESection() { Name = "B" });
            await context.SaveChangesAsync();
        }

        if (!context.AcademicPeriods.Any())
        {
            context.AcademicPeriods.Add(new EAcademicPeriod() { Name = "I Trimestre", StartDate = new DateOnly(2022, 3, 1), EndDate = new DateOnly(2022, 6, 10) });
            context.AcademicPeriods.Add(new EAcademicPeriod() { Name = "II Trimestre", StartDate = new DateOnly(2022, 6, 20), EndDate = new DateOnly(2022, 9, 9) });
            context.AcademicPeriods.Add(new EAcademicPeriod() { Name = "III Trimestre", StartDate = new DateOnly(2022, 9, 26), EndDate = new DateOnly(2022, 12, 16) });
            await context.SaveChangesAsync();
        }

        if (!context.Teachers.Any())
        {
            context.Teachers.Add(new ETeacher()
            {
                Email = "imteacher@mozart.edu.pe",
                Phone = "946368628",
                Person = new EPerson()
                {
                    DateOfBirth = new DateOnly(1990, 12, 10),
                    DocumentNumber = "74715976",
                    DocumentType = new EDocumentType()
                    {
                        Name = "DNI",
                        RegexValidation = "",
                        Description = "Documento Nacional de Identidad"
                    },
                    Name = "Diego Sebastián",
                    LastName = "Calderón",
                    MothersLastName = "Alvarado",
                    Gender = new EGender()
                    {
                        Name = "Masculino"
                    }
                }
            });

            await context.SaveChangesAsync();
        }


        if (!context.Views.Any())
        {
            var entity = new EEntity()
            {
                EntityFullName = "ColegioMozart.Domain.Entities.EAcademicLevel",
                EntityDtoFullName = "ColegioMozart.Application.AcademicLevels.AcademicLevelDTO",
                EntityFields = new List<EEntityFields>
                {
                    new EEntityFields()
                    {
                        Name = "Id",
                        DisplayName = "Id",
                        AllowUpdate = false,
                        AllowInsert = false
                    },
                    new EEntityFields()
                    {
                        Name = "Level",
                        DisplayName = "Grado",
                        AllowUpdate = true,
                        AllowInsert = true
                    },
                    new EEntityFields()
                    {
                        Name = "PreviousAcademicLevel",
                        DisplayName = "Grado Anterior",
                        AllowUpdate = true,
                        AllowInsert = true
                    }
                }
            };

            context.Views.Add(new EView() { Name = "AcademicLevel", DisplayName = "Grados", AllowDelete = true, AllowCreate = true, AllowUpdate = true, AllowDetails = true, Entity = entity });

            var entity2 = new EEntity()
            {
                EntityFullName = "ColegioMozart.Domain.Entities.EShift",
                EntityDtoFullName = "ColegioMozart.Application.Shifts.ShiftDTO",
                EntityFields = new List<EEntityFields>
                {
                    new EEntityFields()
                    {
                        Name = "Id",
                        DisplayName = "Id",
                        AllowUpdate = false,
                        AllowInsert = false
                    },
                    new EEntityFields()
                    {
                        Name = "Name",
                        DisplayName = "Turno",
                        AllowUpdate = true,
                        AllowInsert = true
                    }
                }
            };

            context.Views.Add(new EView() { Name = "Shift", DisplayName = "Turnos", AllowDelete = true, AllowCreate = true, AllowUpdate = true, AllowDetails = true, Entity = entity2 });


            //--------------------
            var entity3 = new EEntity()
            {
                EntityFullName = "ColegioMozart.Domain.Entities.ESection",
                EntityDtoFullName = "ColegioMozart.Application.Sections.SectionDTO",
                EntityFields = new List<EEntityFields>
                {
                    new EEntityFields()
                    {
                        Name = "Id",
                        DisplayName = "Id",
                        AllowUpdate = false,
                        AllowInsert = false
                    },
                    new EEntityFields()
                    {
                        Name = "Name",
                        DisplayName = "Sección",
                        AllowUpdate = true,
                        AllowInsert = true
                    }
                }
            };

            context.Views.Add(new EView() { Name = "Section", DisplayName = "Secciones", AllowDelete = true, AllowCreate = true, AllowUpdate = true, AllowDetails = true, Entity = entity3 });


            //--------------------
            var entity4 = new EEntity()
            {
                EntityFullName = "ColegioMozart.Domain.Entities.EAcademicPeriod",
                EntityDtoFullName = "ColegioMozart.Application.AcademicPeriods.AcademicPeriodDTO",
                EntityFields = new List<EEntityFields>
                {
                    new EEntityFields()
                    {
                        Name = "Id",
                        DisplayName = "Id",
                        AllowUpdate = false,
                        AllowInsert = false
                    },
                    new EEntityFields()
                    {
                        Name = "Name",
                        DisplayName = "Periodo",
                        AllowUpdate = true,
                        AllowInsert = true
                    },
                    new EEntityFields()
                    {
                        Name = "StartDate",
                        DisplayName = "Fecha de inicio",
                        AllowUpdate = true,
                        AllowInsert = true
                    },
                    new EEntityFields()
                    {
                        Name = "EndDate",
                        DisplayName = "Fecha de fin",
                        AllowUpdate = true,
                        AllowInsert = true
                    }
                }
            };

            context.Views.Add(new EView() { Name = "AcademicPeriod", DisplayName = "Periodos académicos", AllowDelete = true, AllowCreate = true, AllowUpdate = true, AllowDetails = true, Entity = entity4 });


            //--------------------
            var entity5 = new EEntity()
            {
                EntityFullName = "ColegioMozart.Domain.Entities.ETeacher",
                EntityDtoFullName = "ColegioMozart.Application.Teachers.TeacherDTO",
                EntityFields = new List<EEntityFields>
                {
                    //TODO ENTITY FIELDS
                }
            };

            context.Views.Add(new EView() { Name = "Teachers", DisplayName = "Docentes", AllowDelete = false, AllowCreate = true, AllowUpdate = true, AllowDetails = true, Entity = entity5 });


            await context.SaveChangesAsync();
        }

    }

    private static async Task<int> AddAcademicLevel(ApplicationDbContext context, string level, int? previousId)
    {
        var academicLevel = new EAcademicLevel() { Level = level, PreviousAcademicLevelId = previousId };
        context.AcademicLevels.Add(academicLevel);
        await context.SaveChangesAsync();

        return academicLevel.Id;
    }
}


