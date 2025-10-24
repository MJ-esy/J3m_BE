using System.Linq.Expressions;
using J3m_BE.Data;
using J3m_BE.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace J3m_BE.Repositories.Implementations;

// Generic repository implementation
// T represents the entity type
// Provides common data access methods
// Uses Entity Framework Core for database operations
// Implements IGenericRepository<T> interface

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    // Get all entities
    public async Task<IEnumerable<T>> GetAllAsync()
        => await _dbSet.AsNoTracking().ToListAsync();

    // Get entity by ID
    public async Task<T?> GetByIdAsync(int id)
        => await _dbSet.FindAsync(id);

    // Find entities based on a predicate
    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        => await _dbSet.Where(predicate).ToListAsync();

    // Add a new entity
    public async Task AddAsync(T entity)
        => await _dbSet.AddAsync(entity);

    // Update an existing entity
    public void Update(T entity)
        => _dbSet.Update(entity);

    // Delete an entity
    public void Remove(T entity)
        => _dbSet.Remove(entity);
    
    // Check if an entity exists by ID
    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        => await _dbSet.AsNoTracking().AnyAsync(predicate);
    
    // Save changes to the database
    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();

}