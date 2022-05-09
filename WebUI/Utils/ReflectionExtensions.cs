using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace WebUI.Utils;

public static class ReflectionExtensions
{

    public static T GetAttribute<T>(this MemberInfo member, bool isRequired)
    where T : Attribute
    {
        var attribute = member.GetCustomAttributes(typeof(T), false).SingleOrDefault();

        if (attribute == null && isRequired)
        {
            throw new ArgumentException(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "The {0} attribute must be defined on member {1}",
                    typeof(T).Name,
                    member.Name));
        }

        return (T)attribute;
    }

    public static string GetPropertyDisplayName(PropertyInfo prop)
    {
        object[] attrs = prop.GetCustomAttributes(true);

        foreach (object attr in attrs)
        {
            DisplayAttribute displayAttr = attr as DisplayAttribute;
            if (displayAttr != null)
            {
                return displayAttr.Name;
            }
        }
        return prop.Name;
    }

    public static Type GetTypeFromAllAssemblies(string typeName)
    {
        foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith("ColegioMozart")))
        {
            if (a.FullName.StartsWith("System"))
                continue;

            foreach (Type t in a.GetTypes())
            {
                if (t.FullName.Equals(typeName))
                    return t;
            }
        }

        return null;
    }

}
