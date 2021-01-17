using System;
using System.Collections.Generic;
using System.Text;

namespace Figure.Contracts.Exceptions
{
    public class InvalidFigureRequestException:Exception
    {
        public InvalidFigureRequestException(string name) : 
            base($"The property '{name}' should not be empty.") { }

    }
}
