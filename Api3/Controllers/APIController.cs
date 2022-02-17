using System.Diagnostics.Eventing.Reader;
using Application.Interface;
using Application.Model;
using Common.Api;
using Common.Exceptions;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Webframework.Api;
using Webframework.Filters;

namespace Presentation.Controllers;

[ApiController]
[ApiResultFilter]
[Route("[controller]")]
public class APIController : ControllerBase
{
    private readonly IJwtServices _jwt;
    private readonly ILogger<APIController> _logger;
    private readonly IPostService _post;
    private readonly IUserServices _user;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Roles> _roleManager;
    private readonly SignInManager<User> _signInManager;

    public APIController(IJwtServices jwt, ILogger<APIController> logger, IPostService post, IUserServices user, UserManager<User> userManager, RoleManager<Roles> roleManager, SignInManager<User> signInManager)
    {
        _jwt = jwt;
        _logger = logger;
        _post = post;
        _user = user;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    [HttpPost("AddPost")]
    public async Task<Postdto> AddPost(Postdto model, CancellationToken cancellationToken)
    {
        // HttpContext.RequestAborted => WeakReference can use it instead of cancellation token
        var data = await _post.AddPost(model, cancellationToken);
        return model;
    }

    [HttpPost("AddUser")]
    [AllowAnonymous]
    public async Task<ApiResult<Userdto>> AddUser(Userdto userdto, CancellationToken cancellationToken)
    {
        if (await _user.Any(userdto.UserName))
            return new ApiResult<Userdto>(false, ApiResultStatusCode.BadRequest, userdto, "نام کاربری موجود می باشد.");
        var user = new User()
        {
            UserName = userdto.UserName,
            age = userdto.Age,
            Email = userdto.Email,
            FullName = userdto.FullName
        };
        var res = await _userManager.CreateAsync(user, userdto.Password);
        if (res.Succeeded)
            return Ok();
        string message = "";
        foreach (var e in res.Errors)
        {
            message += "|" + e.Description;
        }
        return new ApiResult<Userdto>(false, ApiResultStatusCode.BadRequest, userdto, message);
    }

    [HttpGet("Test")] // its important to set name of api method
    [Authorize]
    public async Task<ApiResult> Test()
    {
        throw new BadRequestException("hi this is a test", ApiResultStatusCode.BadRequest);
    }

    [HttpPost("Token")]
    [AllowAnonymous]
    public async Task<string> Token(Logindto model, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user == null)
        {
            throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است.");
        }
        else if (await _userManager.CheckPasswordAsync(user, model.Password)) 
        {
            var jwt = await _jwt.Generate(user);
            return jwt;
        }
        else
        {
            throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است.");
        }

    }
}

// we can use httpcontext.user for token informations