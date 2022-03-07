using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography.X509Certificates;
using Application.Interface;
using Application.Model;
using AutoMapper;
using Common.Api;
using Common.Exceptions;
using Common.Utilities;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Webframework.Api;
using Webframework.Filters;
using Microsoft.AspNetCore.Hosting;


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
    private readonly IMapper _mapper;
    private readonly IImageServices _image;
    private readonly IWebHostEnvironment _env;

    public APIController(IJwtServices jwt, ILogger<APIController> logger, IPostService post, IUserServices user, UserManager<User> userManager, RoleManager<Roles> roleManager, SignInManager<User> signInManager, IMapper mapper, IImageServices image, IWebHostEnvironment env)
    {
        _jwt = jwt;
        _logger = logger;
        _post = post;
        _user = user;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _image = image;
        _env = env;
    }

    [HttpPost("AddPost")]
    public async Task AddPost(Postdto model, CancellationToken cancellationToken)
    {
        // HttpContext.RequestAborted => WeakReference can use it instead of cancellation token
        var temp = _mapper.Map<Post>(model); 
        await _post.AddPost(temp, cancellationToken);
        
    }

    [HttpPost("AddUser")]
    [AllowAnonymous]
    public async Task<ApiResult<Userdto>> AddUser(Userdto userdto, CancellationToken cancellationToken)
    {
        if (await _user.Any(userdto.UserName , cancellationToken))
            return new ApiResult<Userdto>(false, ApiResultStatusCode.BadRequest, userdto, "نام کاربری موجود می باشد.");
        var user = _mapper.Map<User>(userdto);
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
    [AllowAnonymous]
    public async Task<ApiResult> Test()
    {
        throw new BadRequestException("hi this is a test", ApiResultStatusCode.BadRequest);
    }

    
    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<ApiResult> Login(Logindto model, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user == null)
        {
            throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است.");
        }
        else if (await _userManager.CheckPasswordAsync(user, model.Password)) 
        {
            var jwt = await _jwt.Generate(user);
            return new ApiResult(true,0, jwt);
        }
        else
        {
            throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است.");
        }

        
    }
    [HttpPost("UploadPhoto")]
    [AllowAnonymous]
    public async Task<ApiResult> UploadPhoto(IFormFile file , CancellationToken cancellationToken)
    {
        if (file != null)
        {
            byte[] f = await file.GetBytes();
           await _image.SaveImage(f, _env.WebRootPath, file.FileName, cancellationToken);
            return Ok();
        }
        else
        {
            throw new BadRequestException("فایل ارسالی نمی تواند خالی باشد.");
        }
    }
}

// we can use httpcontext.user for token informations