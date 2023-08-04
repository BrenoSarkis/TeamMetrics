using Microsoft.AspNetCore.Mvc;
using TeamMetrics.Common;
using TeamMetrics.Domain.Boundaries;
using TeamMetrics.Domain.Boundaries.Queries;
using static TeamMetrics.Api.Mappers.Mapper;
using static TeamMetrics.Api.Mappers.TeamMapper;

namespace TeamMetrics.Api.Controllers;

[ApiController]
[Route("api/teams")]
public class GetTeamsController : TeamMetricsController {
    private readonly Messenger messenger;

    public GetTeamsController(Messenger messenger) {
        messenger.ThrowIfNull();

        this.messenger = messenger;
    }

    [HttpGet("", Name = nameof(GetTeams))]
    public async Task<IActionResult> Execute([FromQuery] GetTeams message) {
        message ??= new GetTeams();

        var teams = await messenger.Send(message);
        var mappedData = Map(teams, ToTeamDtos);
        return Ok(mappedData);
    }
}