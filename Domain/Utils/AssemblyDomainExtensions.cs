namespace ColegioMozart.Domain.Utils;

public static class AssemblyDomainExtensions
{
    public static Type GetTypeDomain(string type)
    {
        return Type.GetType(type); 
    }

}
