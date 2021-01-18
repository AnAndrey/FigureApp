using System;
using System.Collections.Generic;
using System.Text;

namespace Figure.Contracts.Exceptions
{
    public class NotFoundFigureException:Exception
    {
        public NotFoundFigureException(int id) :
            base($"The figure with '{id}' not found.")
        {
        }

    }
}
