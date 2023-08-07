using FluentAssertions;
using TeamMetrics.Api.Controllers;
using TeamMetrics.Domain.Application;
using TeamMetrics.Domain.Boundaries.Queries;
using Xunit;

namespace TeamMetrics.Tests.Controllers;

public class WhenGettingTeams {
    [Fact]
    public async Task Sends_Query_To_Messenger() {
        var messenger = new FakeMessenger();
        var controller = new GetTeamsController(messenger);

        await controller.Execute(new GetTeams());

        messenger.SentQuery.Should().BeOfType<GetTeams>();
    }
}