using ColegioMozart.Domain.Common;
using ColegioMozart.Domain.Entities;
using ColegioMozart.Infrastructure.Identity;
using ColegioMozart.Infrastructure.Seeds;
using Microsoft.AspNetCore.Identity;

namespace ColegioMozart.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        var administratorRole = new ApplicationRole()
        {
            Name = "Administrator",
            Enabled = true
        };

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

    public static async Task SeedRoles(string role, RoleManager<ApplicationRole> roleManager)
    {
        var customRole = new ApplicationRole()
        {
            Name = role,
            Enabled = true
        };

        if (roleManager.Roles.All(r => r.Name != customRole.Name))
        {
            await roleManager.CreateAsync(customRole);
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

        var inicial = new EAcademicScale() { Name = "Inicial" };
        var primaria = new EAcademicScale() { Name = "Primaria" };
        var secundaria = new EAcademicScale() { Name = "Secundaria" };

        if (!context.AcademicScales.Any())
        {


            context.AcademicScales.Add(inicial);
            context.AcademicScales.Add(primaria);
            context.AcademicScales.Add(secundaria);

            await context.SaveChangesAsync();
        }

        if (!context.AcademicLevels.Any())
        {
            int id = await AddAcademicLevel(context, "Inicial (3 años)", null, inicial);
            id = await AddAcademicLevel(context, "Inicial (4 años)", id, inicial);
            id = await AddAcademicLevel(context, "Inicial (5 años)", id, inicial);
            id = await AddAcademicLevel(context, "1° Primaria", id, primaria);
            id = await AddAcademicLevel(context, "2° Primaria", id, primaria);
            id = await AddAcademicLevel(context, "3° Primaria", id, primaria);
            id = await AddAcademicLevel(context, "4° Primaria", id, primaria);
            id = await AddAcademicLevel(context, "5° Primaria", id, primaria);
            id = await AddAcademicLevel(context, "6° Primaria", id, primaria);
            id = await AddAcademicLevel(context, "1° Secundaria", id, secundaria);
            id = await AddAcademicLevel(context, "2° Secundaria", id, secundaria);
            id = await AddAcademicLevel(context, "3° Secundaria", id, secundaria);
            id = await AddAcademicLevel(context, "4° Secundaria", id, secundaria);
            id = await AddAcademicLevel(context, "5° Secundaria", id, secundaria);
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
            context.AcademicPeriods.Add(new EAcademicPeriod() { Name = "I Trimestre", StartDate = new DateOnly(2022, 3, 1), EndDate = new DateOnly(2022, 6, 10), Year = 2022 });
            context.AcademicPeriods.Add(new EAcademicPeriod() { Name = "II Trimestre", StartDate = new DateOnly(2022, 6, 20), EndDate = new DateOnly(2022, 9, 9), Year = 2022 });
            context.AcademicPeriods.Add(new EAcademicPeriod() { Name = "III Trimestre", StartDate = new DateOnly(2022, 9, 26), EndDate = new DateOnly(2022, 12, 16), Year = 2022 });
            await context.SaveChangesAsync();
        }

        if (!context.Teachers.Any())
        {
            context.Teachers.Add(new ETeacher()
            {
                Id = new Guid("AD566FD6-4D18-4C46-0C5F-08DA355325DF"),
                Email = "imteacher@mozart.edu.pe",
                Phone = "946368628",
                Person = new EPerson()
                {
                    DateOfBirth = new DateTime(1990, 12, 10),
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
                CreateEntityFullName = "ColegioMozart.Application.Shifts.CreateShiftDTO",
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
                CreateEntityFullName = "ColegioMozart.Application.Sections.CreateSectionDTO",
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
                CreateEntityFullName = "ColegioMozart.Application.AcademicPeriods.CreateAcademicPeriodDTO",
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

        if (!context.ClassRooms.Any())
        {
            context.ClassRooms.Add(new EClassRoom()
            {
                Year = 2022,
                LevelId = context.AcademicLevels.Where(x => x.Level == "Inicial (3 años)").First().Id,
                ShiftId = context.Shifts.Where(x => x.Name == "Turno Mañana").First().Id,
                TutorId = context.Teachers.First().Id,
                SectionId = context.Sections.Where(x => x.Name == "A").First().Id
            });
            await context.SaveChangesAsync();

        }

        if (!context.Students.Any())
        {
            var classroomId = context.ClassRooms.Where(
                    x => x.Year == 2022
                    && x.Level.Level == "Inicial (3 años)"
                    && x.Shift.Name == "Turno Mañana"
                    && x.Section.Name == "A"
                 )
                .First().Id;

            context.Students.Add(new EStudent()
            {
                CurrentAcademicLevelId = context.AcademicLevels.Where(x => x.Level == "Inicial (3 años)").First().Id,
                Person = new EPerson()
                {
                    DateOfBirth = new DateTime(1999, 12, 10),
                    DocumentNumber = "12345678",
                    DocumentTypeId = 1,
                    Name = "Hans",
                    LastName = "Haro",
                    MothersLastName = "Antezana",
                    GenderId = 1
                },
                ClassRoomId = classroomId
            });

            context.Students.Add(new EStudent()
            {
                CurrentAcademicLevelId = context.AcademicLevels.Where(x => x.Level == "Inicial (3 años)").First().Id,
                Person = new EPerson()
                {
                    DateOfBirth = new DateTime(1999, 12, 10),
                    DocumentNumber = "12345679",
                    DocumentTypeId = 1,
                    Name = "Johnny",
                    LastName = "Quezada",
                    MothersLastName = "Perez",
                    GenderId = 1
                },
                ClassRoomId = classroomId
            });

            context.Students.Add(new EStudent()
            {
                CurrentAcademicLevelId = context.AcademicLevels.Where(x => x.Level == "Inicial (3 años)").First().Id,
                Person = new EPerson()
                {
                    DateOfBirth = new DateTime(1999, 12, 10),
                    DocumentNumber = "12345680",
                    DocumentTypeId = 1,
                    Name = "Bryan",
                    LastName = "Chilque",
                    MothersLastName = "Chilque",
                    GenderId = 1
                },
                ClassRoomId = classroomId
            });

            context.Students.Add(new EStudent()
            {
                CurrentAcademicLevelId = context.AcademicLevels.Where(x => x.Level == "Inicial (3 años)").First().Id,
                Person = new EPerson()
                {
                    DateOfBirth = new DateTime(1999, 12, 10),
                    DocumentNumber = "12345681",
                    DocumentTypeId = 1,
                    Name = "Luis",
                    LastName = "Calderon",
                    MothersLastName = "Alvarado",
                    GenderId = 1
                },
                ClassRoomId = classroomId
            });

            context.Students.Add(new EStudent()
            {
                CurrentAcademicLevelId = context.AcademicLevels.Where(x => x.Level == "Inicial (3 años)").First().Id,
                Person = new EPerson()
                {
                    DateOfBirth = new DateTime(1999, 12, 10),
                    DocumentNumber = "12345682",
                    DocumentTypeId = 1,
                    Name = "Daniela",
                    LastName = "Chia",
                    MothersLastName = "Chia",
                    Gender = new EGender()
                    {
                        Name = "Femenino"
                    }
                },
                ClassRoomId = classroomId
            });


            await context.SaveChangesAsync();
        }

        if (!context.ClassSchedules.Any())
        {
            var classroomId = context.ClassRooms.Where(
                    x => x.Year == 2022
                    && x.Level.Level == "Inicial (3 años)"
                    && x.Shift.Name == "Turno Mañana"
                    && x.Section.Name == "A"
                 )
                .First().Id;

            var teacherId = context.Teachers.Where(x => x.Person.DocumentNumber == "74715976").First().Id;

            var taller = new ESubject()
            {
                Name = "Taller"
            };

            var desarrolloEDA = new ESubject()
            {
                Name = "Desarrollo de la EdA"
            };

            context.Subjects.Add(taller);
            context.Subjects.Add(desarrolloEDA);

            context.ClassSchedules.Add(new EClassSchedule()
            {
                ClassRoomId = classroomId,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeOnly(8, 30, 0),
                EndTime = new TimeOnly(11, 0, 0),
                Subject = taller,
                TeacherId = teacherId,
            });

            context.ClassSchedules.Add(new EClassSchedule()
            {
                ClassRoomId = classroomId,
                DayOfWeek = DayOfWeek.Tuesday,
                StartTime = new TimeOnly(8, 30, 0),
                EndTime = new TimeOnly(11, 0, 0),
                Subject = desarrolloEDA,
                TeacherId = teacherId,
            });

            context.ClassSchedules.Add(new EClassSchedule()
            {
                ClassRoomId = classroomId,
                DayOfWeek = DayOfWeek.Wednesday,
                StartTime = new TimeOnly(8, 30, 0),
                EndTime = new TimeOnly(11, 0, 0),
                Subject = taller,
                TeacherId = teacherId,
            });

            context.ClassSchedules.Add(new EClassSchedule()
            {
                ClassRoomId = classroomId,
                DayOfWeek = DayOfWeek.Thursday,
                StartTime = new TimeOnly(8, 30, 0),
                EndTime = new TimeOnly(11, 0, 0),
                Subject = desarrolloEDA,
                TeacherId = teacherId,
            });

            context.ClassSchedules.Add(new EClassSchedule()
            {
                ClassRoomId = classroomId,
                DayOfWeek = DayOfWeek.Friday,
                StartTime = new TimeOnly(8, 30, 0),
                EndTime = new TimeOnly(11, 0, 0),
                Subject = taller,
                TeacherId = teacherId
            });

            await context.SaveChangesAsync();
        }

        if (!context.AttendanceStatus.Any())
        {
            context.AttendanceStatus.Add(new EAttendanceStatus { Name = "Asistió", Abbreviation = "A" });
            context.AttendanceStatus.Add(new EAttendanceStatus { Name = "Falta", Abbreviation = "F" });
            context.AttendanceStatus.Add(new EAttendanceStatus { Name = "Tardanza", Abbreviation = "T" });
            context.AttendanceStatus.Add(new EAttendanceStatus { Name = "Falta justificada", Abbreviation = "F.J." });
            context.AttendanceStatus.Add(new EAttendanceStatus { Name = "Tardanza justificada", Abbreviation = "T.J." });

            await context.SaveChangesAsync();
        }

        if (!context.AttendanceRecords.Any())
        {
            var studentIds = context.Students.ToList().Select(x => x.Id);
            var attendanceStatus = context.AttendanceStatus.ToList().Select(x => x.Id).ToList();
            var rnd = new Random(DateTime.Now.Millisecond);

            var mayMonth = new DateTime(2022, 5, 1);

            var academicPeriod = context.AcademicPeriods
                .Where(x => x.StartDate <= DateOnly.FromDateTime(mayMonth) && x.EndDate >= DateOnly.FromDateTime(mayMonth))
                .First();

            foreach (var studentId in studentIds)
            {
                for (var date = mayMonth; date.Month == 5; date = date.AddDays(1))
                {
                    if (!(date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday))
                    {
                        int attendanceStatusId = rnd.Next(0, attendanceStatus.Count);

                        context.AttendanceRecords.Add(new EAttendanceRecord
                        {
                            StudentId = studentId,
                            Date = DateOnly.FromDateTime(date),
                            AttendanceStatusId = attendanceStatus.ElementAt(attendanceStatusId),
                            AcademicPeriod = academicPeriod
                        });
                    }
                }

            }

            await context.SaveChangesAsync();


        }

        var type = typeof(ISeedDbContext);

        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && p.IsClass);

        foreach (var seedType in types)
        {
            var bClass = (ISeedDbContext)Activator.CreateInstance(seedType);
            await bClass.SeedSampleDataAsync(context);
        }

    }

    private static async Task<int> AddAcademicLevel(ApplicationDbContext context, string level, int? previousId, EAcademicScale academicScale)
    {
        var academicLevel = new EAcademicLevel() { Level = level, PreviousAcademicLevelId = previousId, AcademicScale = academicScale };
        context.AcademicLevels.Add(academicLevel);
        await context.SaveChangesAsync();

        return academicLevel.Id;
    }
}


