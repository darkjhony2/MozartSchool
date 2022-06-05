using ColegioMozart.Domain.Entities;
using ColegioMozart.Infrastructure.Persistence;

namespace ColegioMozart.Infrastructure.Seeds;

public class EvualuationTypeSeed : ISeedDbContext
{
    public async Task SeedSampleDataAsync(ApplicationDbContext context)
    {
        if (!context.EvaluationTypes.Any())
        {
            context.EvaluationTypes.Add(new EEvaluationType { Name = "Evaluación de entrada" });
            context.EvaluationTypes.Add(new EEvaluationType { Name = "Evualuación mensual" });
            context.EvaluationTypes.Add(new EEvaluationType { Name = "Práctica calificada" });
            context.EvaluationTypes.Add(new EEvaluationType { Name = "Evualuación final" });

            await context.SaveChangesAsync();
        }
    }
}
