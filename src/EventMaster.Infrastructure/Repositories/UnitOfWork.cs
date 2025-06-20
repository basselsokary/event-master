using System.Collections.Concurrent;
using EventMaster.Application.Common.Interfaces.Repositories;
using EventMaster.Application.Common.Interfaces.UnitOfWork;
using EventMaster.Domain.Common;
using EventMaster.Infrastructure.Context;
using EventMaster.Infrastructure.Repositories.Implementations;
using EventMaster.Infrastructure.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace EventMaster.Infrastructure.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly ConcurrentDictionary<Type, object> _repositories;
    private IDbContextTransaction? _transaction;
    private bool _disposed = false;
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> _userManager;

    private IUserRepository? _userRepository;
    private IRefreshTokenRepository? _refreshTokenRepository;
    private IEventRepository? _eventRepository;
    private ITicketRepository? _ticketRepository;
    private IBasketRepository? _basketRepository;

    public UnitOfWork(AppDbContext context, IConfiguration configuration, UserManager<AppUser> userManager)
    {
        _context = context;
        _configuration = configuration;
        _userManager = userManager;

        _repositories = new ConcurrentDictionary<Type, object>();
    }

    public IUserRepository Users
        => _userRepository ??= new UserRepository(_context);

    public IRefreshTokenRepository RefreshTokens
        => _refreshTokenRepository ??= new RefreshTokenRepository(_context, _configuration, _userManager);

    public IEventRepository Events
        => _eventRepository ??= new EventRepository(_context);

    public ITicketRepository Tickets
        => _ticketRepository ??= new TicketRepository(_context);

    public IBasketRepository Baskets
        => _basketRepository ??= new BasketRepository(_context);

    /// Generic Repository
    public IEntityBaseRepository<T> Repository<T>() where T : BaseEntity
    {
        return (IEntityBaseRepository<T>)_repositories.GetOrAdd(typeof(T),
            _ => new EntityBaseRepository<T>(_context));
    }

    /// Transaction management
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            throw new InvalidOperationException("Transaction already started.");
        }

        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        return _transaction;
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction to commit.");
        }

        try
        {
            await _transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            _transaction.Dispose();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction to rollback.");
        }

        try
        {
            await _transaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            _transaction.Dispose();
            _transaction = null;
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            // Handle concurrency conflicts
            throw new InvalidOperationException("Concurrency conflict occurred. Please refresh and try again.", ex);
        }
        catch (DbUpdateException ex)
        {
            // Handle database update errors
            throw new InvalidOperationException("Database update failed. Please check your data and try again.", ex);
        }
    }

    /// Dispose pattern
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _transaction?.Dispose();
            _context.Dispose();
            _disposed = true;
        }
    }
}
