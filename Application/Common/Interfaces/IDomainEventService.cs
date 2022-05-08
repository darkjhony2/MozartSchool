using ColegioMozart.Domain.Common;

namespace ColegioMozart.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
