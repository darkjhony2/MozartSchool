﻿using System.Linq.Expressions;
using System.Reflection;

namespace ColegioMozart.Application.Common.Utils;

public static class AttributesValues
{
    public static TValue[] GetAttributeValue<TClass, TAttribute, TValue>(
        this Type type,
        Func<TAttribute, TValue> valueSelector,
        Expression<Func<TClass, object>> properties)
        where TAttribute : Attribute
    {
        var rs = new List<TValue>();
        PropertyInfo[] props = type.GetProperties();

        //find the name of properties in the expression
        MemberExpression body = properties.Body as MemberExpression;
        var fields = new List<string>();
        if (body == null)
        {
            NewExpression ubody = properties.Body as NewExpression;
            if (ubody != null)
                foreach (var arg in ubody.Arguments)
                {
                    fields.Add((arg as MemberExpression).Member.Name);
                }
        }

        //get attributes of the properties allowed
        foreach (PropertyInfo prop in props)
        {
            if (!fields.Contains(prop.Name))
                continue;

            var att = prop.GetCustomAttributes(
           typeof(TAttribute), true).FirstOrDefault() as TAttribute;
            if (att != null)
            {
                rs.Add(valueSelector(att));
            }
        }
        return rs.ToArray();
    }
}
