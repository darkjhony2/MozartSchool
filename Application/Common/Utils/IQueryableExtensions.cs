using System.Reflection;

namespace ColegioMozart.Application.Common.Utils;

public static class IQueryableExtensions
{
    public static IQueryable GetQuery(this PropertyInfo propId, IQueryable dbSet, string id)
    {
        object result = null;
        if (propId.PropertyType == typeof(Guid))
        {
            result = dbSet
            .OfType<KeyedEntity<Guid>>()
            .Where(x => x.Id == new Guid(id));
        }
        else if (propId.PropertyType == typeof(int))
        {
            result = dbSet
            .OfType<KeyedEntity<int>>()
            .Where(x => x.Id == Convert.ToInt32(id));
        }

        return (IQueryable)result;
    }

}
