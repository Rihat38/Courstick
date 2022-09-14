
using Courstick.Core.Dto;
using Courstick.Core.Models;
using Courstick.Core.Services;
using Courstick.Infrastructure;
using Courstick.Infrastructure.Database.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Courstick.Controllers;

public class LessonsController : Controller
{
    private readonly ApplicationContext _appContext;
    private readonly CommentService _commentService;
    private readonly LessonService _lessonService;
    private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;
    

    public LessonsController(ApplicationContext appContext, Microsoft.AspNetCore.Identity.UserManager<User> userManager, CommentService commentService, LessonService lessonService)
    {
        _appContext = appContext;
        _commentService = commentService;
        _lessonService = lessonService;
        _userManager = userManager;
    }
    
    [Authorize]
    public async Task<IActionResult> Lessons(int id)
    {
        var thatCourse = await _lessonService.GetLessons(id);
        ViewBag.lessons = thatCourse;
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetLesson(int id)
    {
        var lesson = await _appContext.Pages.FirstOrDefaultAsync(l => l.PageId == id);
        if (lesson == null)
        {
            return BadRequest();
        }
        return Json(lesson);
    }
    

    [HttpGet]

    public async Task<IActionResult> GetComments(int id)
    {
        var comments = await _commentService.GetComments(id);
        return Json(comments);
    }

    [HttpPost]
    public async Task<IActionResult> SetComment([FromBody] CommentDto comment)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(IdentityExtensions.GetUserId(User.Identity));
            await _commentService.AddComment(comment, user);
            return Ok();
        }
        catch (Exception e)
        {
           return BadRequest("Что-то пошло не так");
        }
    }

    [HttpGet]
    public IActionResult GetLessonsByCourseId(int courseId)
    {
        var lessons = _appContext.Pages.Where(l => l.CourseId == courseId).ToList();
        return Json(lessons);
    }
}