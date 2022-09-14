using Courstick.Core.Interfaces;
using Courstick.Core.Models;

namespace Courstick.Core.Services;

public class LessonService
{
    private readonly ILessonRepository _lessonRepository;

    public LessonService(ILessonRepository lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }

    public async Task<List<Page>> GetLessons(int id)
    {
        var result = await  _lessonRepository.GetLessonByCourseIdAsync(id);
        return result;
    }
}