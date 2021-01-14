using Figure.Contracts.Db;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Figure.SqliteDb
{
    public class FigureRepository : IFigureRepository
    {
        private readonly IDatabaseContext _dbContext;

        public FigureRepository(IDatabaseContext context)
        {
            _dbContext = context;
        }
        public async Task<FigureRecord> SaveAsync(FigureRecord figure)
        {
            var entity = _dbContext.Figures.Add(figure);
            await _dbContext.SaveChangesAsync();
            return entity.Entity;
        }
        public async Task<FigureRecord> GetAsync(int id)
        {
            return await _dbContext.Figures
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
