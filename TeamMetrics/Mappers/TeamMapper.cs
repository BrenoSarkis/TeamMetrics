using TeamMetrics.Domain;

namespace TeamMetrics.Api.Mappers;

public static class TeamMapper {
    public static TeamDto ToTeamDto(Team team) =>
        new() {
            Id = team.Id,
        };    
    
    public static IEnumerable<TeamDto> ToTeamDtos(IEnumerable<Team> teams) => teams.Select(ToTeamDto);
}

public class TeamDto {
    public Guid Id { get; set; }
}