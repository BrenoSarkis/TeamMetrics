using Microsoft.AspNetCore.Mvc;
using TeamMetrics.Common;
using TeamMetrics.Domain.Application;
using TeamMetrics.Domain.Boundaries;
using TeamMetrics.Domain.Boundaries.Commands;

namespace TeamMetrics.Api.Controllers;

[ApiController]
[Route("api/teams")]
public class SynchronizeTeamsController : TeamMetricsController {
    private readonly Messenger messenger;

    public SynchronizeTeamsController(Messenger messenger) {
        messenger.ThrowIfNull();

        this.messenger = messenger;
    }

    [HttpPost("", Name = nameof(SynchronizeTeams))]
    public async Task<IActionResult> Execute(SynchronizeTeams message) {
        message ??= new SynchronizeTeams();

        await messenger.Send(message);
        return Ok();
    }
}