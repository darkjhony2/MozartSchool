using ColegioMozart.Application.Common.Interfaces;

namespace ColegioMozart.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
