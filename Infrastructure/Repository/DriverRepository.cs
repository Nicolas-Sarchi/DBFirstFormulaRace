using System.Linq.Expressions;
using Core.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class DriverRepository : GenericRepository<Driver>, IDriver
    {
        private readonly FormulaRaceContext _context;
        public DriverRepository(FormulaRaceContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Driver>> GetAllAsync()
        {
            return await _context.Drivers.ToListAsync();
        }
    }
}