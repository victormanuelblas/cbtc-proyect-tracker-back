namespace Tracker.Domain.Ports.Out
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository CustomersRepository { get; }
        IUserRepository UsersRepository { get; }
        IShipmentRepository ShipmentsRepository { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        
    }
}