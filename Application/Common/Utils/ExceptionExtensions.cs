namespace ColegioMozart.Application.Common.Utils;

public static class ExceptionExtensions
{
    public static TException GetInnerException<TException>(Exception exception)
    where TException : Exception
    {
        Exception innerException = exception;
        while (innerException != null)
        {
            if (innerException is TException result)
            {
                return result;
            }
            innerException = innerException.InnerException;
        }
        return null;
    }
}
