using System;
using System.Collections.Generic;
using System.Text;

namespace Figure.Business
{
    internal class Triangle : IFigure
    {
        public double? SideA { get; set; }
        public double? SideB { get; set; }
        public double? SideC { get; set; }

        internal Triangle()
        { 
        }
        internal Triangle(double sideA, double sideB, double sideC) 
        {
            SideA = sideA;
            SideB = sideB;
            SideC = sideC;
        }
        public double GetSqare()
        {
            //Heron's Formula
            //Area = √p(p - a)(p − b)(p − c)
            return Math.Sqrt(Perimeter * (Perimeter - SideA.Value) * (Perimeter - SideB.Value) * (Perimeter - SideC.Value));
        }

        public bool IsValid()
        {
            if (!SideA.HasValue || !SideB.HasValue || !SideC.HasValue)
                return false;

            if (SideA.Value <= 0 || SideB.Value <= 0 || SideC.Value <= 0)
                return false;

            if (Perimeter <= SideA.Value || Perimeter <= SideB.Value || Perimeter <= SideC.Value)
                return false;

            return true;
        }

        private double Perimeter => (SideA.Value + SideB.Value + SideC.Value) / 2;
    }
}
