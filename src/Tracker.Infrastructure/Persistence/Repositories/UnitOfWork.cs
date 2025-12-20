using Microsoft.EntityFrameworkCore.Storage;
using Tracker.Domain.Ports.Out;
using Tracker.Infrastructure.Persistence.Context;

namespace Tracker.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        public ICustomerRepository CustomersRepository { get; }
        public IUserRepository UsersRepository { get; }
        public IShipmentRepository ShipmentsRepository { get; }

        public UnitOfWork(ApplicationDbContext context,
         ICustomerRepository customersRepository, 
         IUserRepository usersRepository, 
         IShipmentRepository shipmentsRepository)
        {
            _context = context;
            CustomersRepository = customersRepository;
            UsersRepository = usersRepository;
            ShipmentsRepository = shipmentsRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
            {
                _transaction = await _context.Database.BeginTransactionAsync();
            }
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}