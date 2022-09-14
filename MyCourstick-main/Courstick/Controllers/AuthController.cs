using Courstick.Core.Models;
using Courstick.Views.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Courstick.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    // GET
    public IActionResult Authorization()
    {
        if (HttpContext.Session.GetString("login") != null)
        {
            return Redirect("/Profile/profile");
        }

        return View();
    }

    public IActionResult Registration()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Registration([FromBody] RegisterModel model)
    {
        if (await userManager.FindByEmailAsync(model.Email) == null &&
            await userManager.FindByEmailAsync(model.Login) == null)
        {
            var user = new User {Email = model.Email, UserName = model.Login};
            var result = await userManager.CreateAsync(user, model.Password);
            await userManager.AddToRoleAsync(user, "defaultUser");

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return BadRequest("Что-то пошло не так");
            }

            await userManager.UpdateAsync(user);

            await signInManager.SignInAsync(user, isPersistent: false);
            return Ok("Пользователь успешно зарегистрирован");
        }
        else
        {
            return BadRequest("Данный пользователь уже существует");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Authorization([FromBody] AuthorizationModel model)
    {
        var result = await signInManager.PasswordSignInAsync(model.Login, model.Password, model.IsRemember, false);

        if (!result.Succeeded)
        {
            return BadRequest(new {Message = "Неправильный логин или пароль"});
        }

        Console.WriteLine(result);


        HttpContext.Session.SetString("login", model.Login);
        return Redirect("/Profile/profile");
    }
}