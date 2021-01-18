using System;
using System.Collections.Generic;
using System.Text;

namespace Figure.Contracts
{
    public class FigureRequest
    {
        public string Type { get; set; }
        public Dictionary<string, double> Params { get; set; }
    }
}
