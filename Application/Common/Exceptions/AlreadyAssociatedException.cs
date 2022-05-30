namespace ColegioMozart.Application.Common.Exceptions
{
    public class AlreadyAssociatedException : Exception
    {

        public AlreadyAssociatedException(string message) : base(message)
        {
        }

        public AlreadyAssociatedException(string entityFirst, string entityTwo)
        : base($"Ya se encuentra asociado {entityFirst} con {entityTwo}.")
        {


        }
    }
}
