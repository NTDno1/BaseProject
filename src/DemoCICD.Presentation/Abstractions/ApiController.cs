using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoCICD.Presentation.Abstractions;
public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender)
    {
        Sender = sender;
    }
}
