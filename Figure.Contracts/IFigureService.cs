using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Figure.Contracts
{
    public interface IFigureService
    {
        Task<int> SaveFigureAsync(FigureRequest figure);
        Task<double> GetFigureAreaAsync(int id);
    }
}
