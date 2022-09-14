using Courstick.Core.Models;
using Courstick.Infrastructure.Interfaces;

namespace Courstick.Core.Interfaces;

public interface ILessonRepository : IRepository<Page>
{
    Task<List<Page?>> GetLessonByCourseIdAsync(int id);
    
    
}