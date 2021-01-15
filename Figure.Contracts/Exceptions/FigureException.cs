using System;
using System.Collections.Generic;
using System.Text;

namespace Figure.Contracts.Exceptions
{
    public class FigureException : Exception
    {
        public FigureException(string message) : base(message) { }
    }
}
