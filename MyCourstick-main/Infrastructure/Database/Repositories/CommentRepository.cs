using Courstick.Core.Interfaces;
using Courstick.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Courstick.Infrastructure.Database.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationContext _applicationContext;

    public CommentRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    public async Task<Comment> AddAsync(Comment entity)
    {
        var comment =  _applicationContext.Comments.Add(entity);
        return comment.Entity;
    }

    public Course Update(Comment entity)
    {
        var comment = _applicationContext.Comments.Update(entity);
        throw new NotImplementedException();
    }

    public void Delete(Comment entity)
    {
        throw new NotImplementedException();
    }

    public async Task SaveChangesAsync() => await _applicationContext.SaveChangesAsync();

    public IEnumerable<Comment> GetAll()
    {
        return _applicationContext.Comments.ToList();
    }

    public async Task<List<Comment?>> GetCommentsByCourseIdAsync(int id)
    {
        var val = _applicationContext.Comments.Where(c => c.courseid == id).ToList();
        return val;
    }
}