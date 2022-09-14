using Courstick.Core.Models;

namespace Courstick.Infrastructure.Interfaces;

public interface IRepository<TEntity> where TEntity: class
{
    Task<TEntity> AddAsync(TEntity entity);
    Course Update(TEntity entity);
    void Delete(TEntity entity);
    Task SaveChangesAsync();
    IEnumerable<TEntity> GetAll();
}