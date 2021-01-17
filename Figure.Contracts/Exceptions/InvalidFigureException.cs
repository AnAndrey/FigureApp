using System;
using System.Collections.Generic;
using System.Text;

namespace Figure.Contracts.Exceptions
{
    public class InvalidFigureException: Exception
    {
        public InvalidFigureException(string type, string @params) :
            base($"The figure '{type}' with params '{@params}' is invalid.") { }
    }
}
