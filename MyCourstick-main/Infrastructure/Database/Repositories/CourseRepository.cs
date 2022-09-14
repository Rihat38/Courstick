using Courstick.Core.Interfaces;
using Courstick.Core.Models;
using Courstick.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Courstick.Infrastructure.Database.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly ApplicationContext _applicationContext;

    public CourseRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task<Course> AddAsync(Course entity)
    {
        var entityEntry = await _applicationContext.Courses.AddAsync(entity);
        return entityEntry.Entity;
    }

    public Course Update(Course entity)
    {
        var entityEntry = _applicationContext.Courses.Update(entity);
        return entityEntry.Entity;
    }

    public void Delete(Course entity) => _applicationContext.Courses.Remove(entity);

    public async Task SaveChangesAsync() => await _applicationContext.SaveChangesAsync();
    public  IEnumerable<Course> GetAll() => _applicationContext.Courses.ToList();
    public async Task<Course?> GetCourseByIdAsync(int courseId) => await _applicationContext.Courses.Include(x=>x.Page).FirstOrDefaultAsync(c => c.CourseId == courseId);

    public async Task<List<Course?>> GetCourseByNameAsync(string name)
    {
        var val =  _applicationContext.Courses.Where(c => c.Name == name).ToList();
        return val;
    }
        
}