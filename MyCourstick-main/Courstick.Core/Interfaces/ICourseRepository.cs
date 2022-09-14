using Courstick.Core.Models;
using Courstick.Infrastructure.Interfaces;

namespace Courstick.Core.Interfaces;

public interface ICourseRepository : IRepository<Course>
{
    Task<Course?> GetCourseByIdAsync(int courseId);

    Task<List<Course?>> GetCourseByNameAsync(string name);
}