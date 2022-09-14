using System.Drawing;
using System.Net.Mime;
using System.Security.Claims;
using Courstick.Core.Dto;
using Courstick.Core.Models;
using Courstick.Core.Services;
using Courstick.Dto;
using Courstick.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Courstick.Core.Interfaces;

namespace Courstick.Controllers;

public class CourseController : Controller
{
    private readonly CourseService _courseService;
    private readonly string userId;
    private readonly UserManager<User> _userManager;
    private readonly ApplicationContext _appContext;
    private readonly ICourseRepository _courseRepository;

    public CourseController(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, CourseService courseService,
        ApplicationContext appContext, ICourseRepository courseRepository)
    {
        _courseService = courseService;
        _userManager = userManager;
        _appContext = appContext;
        userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        _courseRepository = courseRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Course(int id)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var boughtCourses = _appContext.Courses.Where(i => i.Users.Any(x => x.Id == user.Id)).AsEnumerable().ToList();
        
        var thatCourse = await _courseRepository.GetCourseByIdAsync(id);
        if (thatCourse is null)
            return null;
        CourseDto courseDto = new CourseDto();

        courseDto.Lessons = thatCourse.Page?.Select(x => new PageDto()
        {
            Movie = x.Movie,
            Image = x.Image,
            Text = x.Text,
            Type = x.Type
        }).ToList()??new List<PageDto>();
        courseDto.Name = thatCourse.Name;
        courseDto.Price = thatCourse.Price;
        courseDto.Description = thatCourse.Description;
        courseDto.CourseId = id;
        courseDto.Image = thatCourse.Image;
        if (boughtCourses.Contains(thatCourse))
        {
            courseDto.IsBought = true;
        }
        
        if (courseDto is null)
        {
            return Redirect("/Search/Search");
        }
        return View(courseDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> BuyCourse(CourseDto model)
    {
        var user = await _userManager.FindByIdAsync(userId);

        var course = await _appContext.Courses.FirstOrDefaultAsync(c => c.CourseId == model.CourseId);
        
        if (course is null)
        {
            return Redirect("/Search/Search");
        }

        if (user.Courses is null)
        {
            user.Courses = new List<Course>();
        }
        
        if (user.Balance < course.Price)
        {
            return RedirectToAction("LowBalance", "Course");
        }
        
        user.Balance -= course.Price;
        user.Courses.Add(course);

        await _userManager.UpdateAsync(user);
        
        return RedirectToAction("Lessons", "Lessons", new {id = course.CourseId});
    }
    
    public IActionResult LowBalance()
    {
        return View();
    }
}