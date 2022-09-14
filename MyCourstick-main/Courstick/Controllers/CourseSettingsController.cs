using System.Reflection.Metadata.Ecma335;
using Courstick.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Courstick.Core.Dto;
using Courstick.Dto;
using Courstick.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using CreateCourseModel = Courstick.Core.Dto.CreateCourseDto;

namespace Courstick.Controllers;

public class CourseSettingsController : Controller
{
    private readonly string userId;
    private readonly UserManager<User> _userManager;
    private readonly ApplicationContext _appContext;

    public CourseSettingsController(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager,
        ApplicationContext appContext)
    {
        _userManager = userManager;
        _appContext = appContext;
        userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }

    // GET
    [Authorize]
    public IActionResult CreateCourse()
    {
        return View();
    }

    public async Task<IActionResult> YourCourses()
    {
        var user = await _userManager.FindByIdAsync(userId);

        var courses = _appContext.Courses
            .Include(c => c.Users)
            .Where(c => c.Author.Contains(user));

        var courseList = courses.Select(course => new CourseDto()
        {
            Author = new AuthorDto(course.Author.First()),
            Description = course.Description,
            Name = course.Name,
            Price = course.Price,
            SmallDescription = course.SmallDescription,
            CourseId = course.CourseId,
            Image = course.Image
        });

        return View(courseList);
    }

    public async Task<IActionResult> EditCourse(int id)
    {
        var course = await _appContext.Courses.Include(c => c.Page).FirstOrDefaultAsync(a => a.CourseId == id);

        if (course is null)
        {
            return NotFound();
        }

        var model = new EditCourseDto
        {
            Id = id,
            Description = course.Description,
            Name = course.Name,
            Price = course.Price,
            SmallDescription = course.SmallDescription,
            Image = course.Image.Select(i => (int)i).ToArray(),
            Pages = course.Page?.Select(e => new PageDto
            {
                Movie = e.Movie,
                Text = e.Text,
                Image = e.Image
            }).ToList()
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditCourse([FromBody] EditCourseDto model)
    {
        if (model is null)
        {
            return BadRequest("error");
        }

        var course = await _appContext.Courses.FirstOrDefaultAsync(c => c.CourseId == model.Id);

        if (course is null)
        {
            return BadRequest("error");
        }

        course.Name = model.Name;
        course.Description = model.Description;
        course.SmallDescription = model.SmallDescription;
        if (model?.Image is not null && model.Image.Length > 0)
            course.Image = model.Image.Select(i => (byte)i).ToArray();
        course.Price = model.Price;
        course.Rating = 0;

        _appContext.Courses.Update(course);
        await _appContext.SaveChangesAsync();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto model)
    {
        if (model == null)
        {
            return BadRequest("error");
        }

        var user = await _userManager.FindByIdAsync(userId);

        var course = new Course
        {
            Name = model.Name,
            Description = model.Description,
            SmallDescription = model.SmallDescription,
            Image = model.Image.Select(i => (byte)i).ToArray(),
            Price = model.Price,
            Author = new List<User> {user},
            Rating = 0
        };

        var updatedCourse = await _appContext.Courses.AddAsync(course);

        await _appContext.SaveChangesAsync();

        return Json(updatedCourse.Entity.CourseId);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLessons([FromBody] CourseDto courseDto)
    {
        var thatCourse = await _appContext.Courses
            .Include(c => c.Page)
            .FirstAsync(c => c.CourseId == courseDto.CourseId);
        thatCourse.Page?.Clear();
        if (courseDto.Lessons.Count != 0)
        {
            foreach (var lesson in courseDto.Lessons)
            {
                thatCourse.Page?.Add(new Page
                {
                    Movie = lesson.Movie,
                    Type = lesson.Type,
                    Text = lesson.Text,
                    Image = lesson.Image
                });
            }
        }

        await _appContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var courseForDelete = await _appContext.Courses.FirstOrDefaultAsync(c => c.CourseId == id);

        if (courseForDelete is null)
        {
            return BadRequest("error");
        }

        var lessonsForDelete = _appContext.Pages.Where(l => l.CourseId == id).ToList();
        foreach (var page in lessonsForDelete)
        {
            _appContext.Pages.Remove(page);
        }

        _appContext.Courses.Remove(courseForDelete);

        await _appContext.SaveChangesAsync();

        return RedirectToAction("YourCourses");
    }
}