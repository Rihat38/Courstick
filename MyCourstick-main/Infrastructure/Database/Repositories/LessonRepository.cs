using Courstick.Core.Interfaces;
using Courstick.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Courstick.Infrastructure.Database.Repositories;

public class LessonRepository: ILessonRepository

{

    private readonly ApplicationContext _application;

    public LessonRepository(ApplicationContext application)
    {
        _application = application;
    }
    
    public async Task<Page> AddAsync(Page entity)
    {
        var page = await _application.Pages.AddAsync(entity);
        return page.Entity;
    }

    public Course Update(Page entity)
    {
        var page = _application.Pages.Update(entity);
        throw new NotImplementedException();
    }

    public void Delete(Page entity)
    {
        _application.Pages.Remove(entity);
    }

    public async Task SaveChangesAsync() => await _application.SaveChangesAsync();

    public IEnumerable<Page> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Page?>> GetLessonByCourseIdAsync(int id)
    {
        var thatCourse = await _application.Courses
            .Include(c => c.Page)
            .FirstAsync(c => c.CourseId == id);;
        return thatCourse.Page;
    }
}