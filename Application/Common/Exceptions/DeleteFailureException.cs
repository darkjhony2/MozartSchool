using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioMozart.Application.Common.Exceptions
{
    public class DeleteFailureException : Exception
    {
        public DeleteFailureException(string? message) : base(message)
        {
            
        }
    }
}
