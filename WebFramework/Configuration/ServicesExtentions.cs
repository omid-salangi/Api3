using Application.Interface;
using Application.Services;
using Data.Repositories;
using Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Webframework.Configuration
{
    public static class ServicesExtention
    {
        public static void AddDependencyInjections(this IServiceCollection services)
        {
            // service layer
            services.AddScoped<IPostService, PostServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IJwtServices, JwtServices>();

            //data layer
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUserRepository, UserRepository>();


        }

        //public static void AddJwtAuthentication(this IServiceCollection services)
        //{
        //    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        //    {
        //        var secretkey = Encoding.UTF8.GetBytes(">=%2TPd\\3!?~9eed");
        //        var encryptionkey = Encoding.UTF8.GetBytes("");

        //        var validationParameters = new TokenValidationParameters
        //        {
        //            ClockSkew = TimeSpan.Zero, // default: 5 min
        //            RequireSignedTokens = true,

        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(secretkey),

        //            RequireExpirationTime = true,
        //            ValidateLifetime = true,

        //            ValidateAudience = true, //default : false
        //            ValidAudience = "Blog",

        //            ValidateIssuer = true, //default : false
        //            ValidIssuer = "Blog",

        //            TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey)
        //        };

        //        options.RequireHttpsMetadata = false;
        //        options.SaveToken = true;
        //        options.TokenValidationParameters = validationParameters;
        //        options.Events = new JwtBearerEvents
        //        {
        //            OnAuthenticationFailed = context =>
        //            {
        //                //var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
        //                //logger.LogError("Authentication failed.", context.Exception);

        //                if (context.Exception != null)
        //                    throw new AppException(ApiResultStatusCode.UnAuthorized, "Authentication failed.", HttpStatusCode.Unauthorized, context.Exception, null);

        //                return Task.CompletedTask;
        //            },
        //            OnTokenValidated = async context =>
        //            {
        //                var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<User>>();
        //                var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();

        //                var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
        //                if (claimsIdentity.Claims?.Any() != true)
        //                    context.Fail("This token has no claims.");

        //                var securityStamp = claimsIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);
        //                if (!securityStamp.HasValue())
        //                    context.Fail("This token has no secuirty stamp");

        //                //Find user and token from database and perform your custom validation
        //                var userId = claimsIdentity.GetUserId<int>();
        //                var user = await userRepository.GetByIdAsync(context.HttpContext.RequestAborted, userId);

        //                //if (user.SecurityStamp != Guid.Parse(securityStamp))
        //                //    context.Fail("Token secuirty stamp is not valid.");

        //                var validatedUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
        //                if (validatedUser == null)
        //                    context.Fail("Token secuirty stamp is not valid.");

        //                if (!user.IsActive)
        //                    context.Fail("User is not active.");

        //                await userRepository.UpdateLastLoginDateAsync(user, context.HttpContext.RequestAborted);
        //            },
        //            OnChallenge = context =>
        //            {
        //                //var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
        //                //logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);

        //                if (context.AuthenticateFailure != null)
        //                    throw new AppException(ApiResultStatusCode.UnAuthorized, "Authenticate failure.", HttpStatusCode.Unauthorized, context.AuthenticateFailure, null);
        //                throw new AppException(ApiResultStatusCode.UnAuthorized, "You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);

        //                //return Task.CompletedTask;
        //            }
        //        };
        //    });
        //}
    }
}
