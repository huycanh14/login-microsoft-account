using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlecEdu_api.API.Common;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController: ControllerBase
{
    private readonly ILogger<BaseApiController> _logger;
    private readonly IMediator _mediator;

    protected BaseApiController(ILogger<BaseApiController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
}
