using Application.Interface;
using Application.Model;
using Application.ViewModel;
using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Webframework.Api;
using Webframework.Filters;
using Common.Exceptions;

namespace Presentation.Controllers
{
    [ApiController]
    [ApiResultFilter]
    [Route("{controller}")]
    public class APIController : ControllerBase
    {
        private readonly IPostService _post;
        private readonly IUserServices _user;
        private readonly IJwtServices _jwt;
        private readonly ILogger<APIController> _logger;

        public APIController(IPostService post, IUserServices user, IJwtServices jwt, ILogger<APIController> logger)
        {
            _post = post;
            _user = user;
            _jwt = jwt;
            _logger = logger;
        }

        [HttpPost("AddPost")]
        
        public async Task<AddPostViewModel> AddPost(AddPostViewModel model, CancellationToken cancellationToken)
        {
           // HttpContext.RequestAborted => WeakReference can use it instead of canellation tioken
          var data =await  _post.AddPost(model, cancellationToken);
          return model;
        }

        [HttpPost("AddUser")]
        public async Task<ApiResult<Userdto>> AddUser(Userdto userdto, CancellationToken cancellationToken)
        {
            if (await _user.Any(userdto.UserName.ToLower()))
            {
                return new ApiResult<Userdto>(false, ApiResultStatusCode.BadRequest, userdto , "نام کاربری موجود می باشد.");
            }
            else
            {
              var res=  await _user.AddUserAsync(userdto.UserName.ToLower(), userdto.FullName, userdto.Password, userdto.Age,userdto.Email,
                    cancellationToken);
              if (res==true)
              {
                  return Ok();
              }
              else
              {
                  return new ApiResult<Userdto>(false, ApiResultStatusCode.ServerError,userdto);
              }
            }
        }
        [HttpGet("Test")] // its important to set name of api method
        public async Task<ApiResult> Test()
        {
            throw new BadRequestException("hi this is a test",ApiResultStatusCode.BadRequest);
            
        }
        [HttpPost("Token")]
        public async Task<ApiResult<Logindto>> Token(Logindto model,CancellationToken cancellationToken)
        {
            var user = await _user.GetByUsername(model.UserName, model.Password, cancellationToken);
            if (user == null)
            {
                //throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است.");
                return BadRequest();
            }

            return null;
            //var jwt = await _jwt.Generate(user);
            //return jwt;
        }

    }
}
