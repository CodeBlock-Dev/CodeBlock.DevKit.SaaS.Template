using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Web.Api.Filters;
using CodeBlock.DevKit.Web.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CanBeYours.Api.Controllers;

[Route("test")]
public class TestController : BaseApiController
{
    /// <summary>
    /// This api is protected by authorization
    /// </summary>
    [Authorize(Policies.ADMIN_ROLE)]
    [HttpGet]
    [Route("authorized")]
    public async Task<Result> Authorized()
    {
        return Result.Success();
    }

    /// <summary>
    /// This api has no rate limit
    /// </summary>
    [HttpGet]
    [Route("unlimited")]
    [DisableRateLimiting]
    public async Task<Result> Unlimited()
    {
        return Result.Success();
    }

    /// <summary>
    /// This api is proteted by rate limit
    /// </summary>
    [HttpGet]
    [Route("limited")]
    public async Task<Result> Limited()
    {
        return Result.Success();
    }
}
