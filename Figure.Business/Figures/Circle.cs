using System;

namespace Figure.Business
{
    internal class Circle : IFigure
    {
        internal Circle()
        {
        }
        internal Circle(int radius) 
        {
            Radius = radius;
        }
        public double? Radius { get; set; }
        public double GetSqare() 
        {
            return Math.PI * Math.Pow(Radius.Value, 2);
        }

        public bool IsValid()
        {
            return Radius.HasValue && Radius.Value > 0;
        }
    }
}
