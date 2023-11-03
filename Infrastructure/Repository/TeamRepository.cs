using System.Linq.Expressions;
using Core.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class TeamRepository : GenericRepository<Team>, ITeam
    {
        private readonly FormulaRaceContext _context;
        public TeamRepository(FormulaRaceContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Team>> GetAllAsync()
        {
            return await _context.Teams.ToListAsync();
        }
    }
}