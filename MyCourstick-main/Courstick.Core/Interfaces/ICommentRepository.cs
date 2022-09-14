using Courstick.Core.Models;
using Courstick.Infrastructure.Interfaces;

namespace Courstick.Core.Interfaces;

public interface ICommentRepository: IRepository<Comment>
{
    Task<List<Comment?>> GetCommentsByCourseIdAsync(int id);
}