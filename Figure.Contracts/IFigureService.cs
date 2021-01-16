using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Figure.Contracts
{
    public interface IFigureService
    {
        Task<CreatedFigureResponce> SaveFigureAsync(FigureRequest figure);
        Task<FigureAreaResponce> GetFigureAreaAsync(int id);
    }
}
