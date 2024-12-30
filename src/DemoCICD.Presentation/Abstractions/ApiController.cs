﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoCICD.Presentation.Abstractions;
[ApiController]
[Route("[controller]")]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender)
    {
        Sender = sender;
    }
}
