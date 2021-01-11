using Figure.Contracts.Db;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Figure.Contracts.Db
{
    public interface IDatabaseContext
    {
        DbSet<FigureRecord> Figures { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
