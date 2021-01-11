using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Figure.Contracts.Db
{
    public interface IFigureRepository
    {
        Task<FigureRecord> SaveAsync(FigureRecord figure);
        Task<FigureRecord> GetAsync(int id);
    }
}
