using System.Collections;
using System.Collections.Immutable;
using System.Data.Entity;
using System.Text.Json;
using Courstick.Core.Services;
using Courstick.Dto;
using Courstick.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Courstick.Controllers;

public class SearchController : Controller
{
    private readonly CourseService _courseService;

    public SearchController(CourseService courseService)
    {
        _courseService = courseService;
    }

    public IActionResult Search()
    {
        var courses = _courseService.GetCourseList();
        return View(courses);
    }

    [HttpGet]
    public async Task<IActionResult> GetCourseByName(string name)
    {
        var courses = await _courseService.GetCourseListByName(name);
        return Json(courses);
    }


    [HttpGet]
    public IActionResult GetCourses()
    {
        var courses = _courseService.GetCourseList();
        return Json(courses);
    }

    [HttpGet]
    public async Task<IActionResult> GetCoursesByName()
    {
        return Json("");
    }
}