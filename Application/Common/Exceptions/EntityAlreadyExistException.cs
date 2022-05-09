using System.Linq.Expressions;

namespace ColegioMozart.Application.Common.Exceptions;

public class EntityAlreadyExistException : Exception
{
    private const string message = "Ya existe un registro con los datos ingresados.";
    public HashSet<string> Fields { get; private set; }

    public EntityAlreadyExistException()
        : base(message)
    {
        Fields = new HashSet<string> { };
    }

    public EntityAlreadyExistException(HashSet<string> fields)
        : base(message)
    {
        Fields = fields;
    }

    public EntityAlreadyExistException(string field)
       : base(message)
    {
        Fields = new HashSet<string> { field };
    }

}
