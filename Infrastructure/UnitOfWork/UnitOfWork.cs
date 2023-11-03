using Infrastructure.Repository;
using Core.Interfaces;
using Infrastructure.Data;
using Persistence.Data;

namespace Application.UnitOfWork;
    public class UnitOfWork : IUnitOfWork

    {
        private readonly FormulaRaceContext context;
        private DriverRepository _Drivers;
        private TeamRepository _Teams;

        public UnitOfWork(FormulaRaceContext _context)
        {
            context = _context;
        }

        public IDriver Drivers
        {
            get
            {
                if (_Drivers == null)
                {
                    _Drivers = new DriverRepository(context);
                }
                return _Drivers;
            }
        }

        public ITeam Teams
        {
            get
            {
                if (_Teams == null)
                {
                    _Teams = new TeamRepository(context);
                }
                return _Teams;
            }
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
        public Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }
    }