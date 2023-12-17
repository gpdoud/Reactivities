
using Application;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ActivitiesController : BaseApiController
{
    [HttpGet] // api/activities
    public async Task<ActionResult<List<Activity>>> GetActivities() {
        return await Mediator.Send(new List.Query());
    }

    [HttpGet("{id}")] // api/activities/5
    public async Task<ActionResult<Activity>> GetActivity(Guid id) {
        return await Mediator.Send(new Details.Query { Id = id });
    }

    [HttpPost]
    public async Task<IActionResult> CreateActivity(Activity activity) 
    {
        await Mediator.Send(new Create.Command { Activity = activity });
        
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditActivity(Guid id, Activity activity) 
    {
        activity.Id = id;
        
        await Mediator.Send(new Edit.Command { Activity = activity });
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveActivity(Guid id) 
    {
        await Mediator.Send(new Delete.Command { Id = id });

        return NoContent();
    }

}
