using System;
using System.Collections.Generic;
using System.Text;

namespace Figure.Contracts.Exceptions
{
    public class CorruptedFigureException:Exception
    {
        public CorruptedFigureException(int id) :
            base($"The figure with '{id}' id is invalid.")
        { 
        }
    }
}
