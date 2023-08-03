using Microsoft.AspNetCore.Mvc;

namespace TeamMetrics.Api.Controllers;

public class TeamMetricsController : ControllerBase {
    protected new IActionResult Ok() {
        return base.Ok(ResponseWrapper.Ok());
    }

    protected IActionResult Ok<T>(T resultado) {
        return base.Ok(ResponseWrapper.Ok(resultado));
    }
}