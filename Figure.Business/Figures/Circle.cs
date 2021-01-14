using System;

namespace Figure.Business
{
    public class Circle : IFigure
    {
        public int? Radius { get; set; }
        public double GetSqare() 
        {
            return Math.PI * Math.Pow(Radius.Value, 2);
        }

        public bool IsValid()
        {
            return Radius.HasValue;
        }
    }
}
