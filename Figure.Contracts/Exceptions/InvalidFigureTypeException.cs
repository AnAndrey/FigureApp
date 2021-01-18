using System;

namespace Figure.Contracts.Exceptions
{
    public class InvalidFigureTypeException : Exception
    {
        public InvalidFigureTypeException(string type) :
            base($"Invalid figure type '{type}'.")
        { }
    }
}
