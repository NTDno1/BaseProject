using DemoCICD.Contract.Services.Product;
using DemoCICD.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoCICD.Presentation.Controllers.V1;
public class ProductsController : ApiController
{
    public ProductsController(ISender sender) : base(sender)
    {
    }

    [HttpGet(Name = "GetProducts")]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new Query.GetProductQuery());
        return Ok();
    }
}
