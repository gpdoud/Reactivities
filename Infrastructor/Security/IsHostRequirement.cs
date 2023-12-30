using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructor;


public class IsHostRequirement : IAuthorizationRequirement
{

}

public class IsHostRequirementHandler : AuthorizationHandler<IsHostRequirement>
{
    private readonly DataContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IsHostRequirementHandler(DataContext dbContext, 
        IHttpContextAccessor httpContextAccessor) 
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsHostRequirement requirement)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if(userId == null) return Task.CompletedTask;

        var activiteyId = Guid.Parse(_httpContextAccessor.HttpContext?.Request.RouteValues
            .SingleOrDefault(x => x.Key == "id").Value?.ToString());

        var attendee = _dbContext.ActiviteyAttendees
            //.AsNoTracking() // not needed with current softeare
            .SingleOrDefaultAsync(x => x.AppUserId == userId && x.ActivityId == activiteyId).Result;

        if(attendee == null) return Task.CompletedTask;

        if(attendee.IsHost) context.Succeed(requirement);

        return Task.CompletedTask; 
    }
}
