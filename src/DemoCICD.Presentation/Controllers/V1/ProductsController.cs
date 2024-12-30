using DemoCICD.Contract.Services.Product;
using DemoCICD.Contract.Share;
using DemoCICD.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoCICD.Presentation.Controllers.V1;
public class ProductsController : ApiController
{
    public ProductsController(ISender sender) : base(sender)
    {
    }

    [HttpGet(Name = "GetProducts")]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.ProductResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new Query.GetProductQuery());
        return Ok(result);
    }
}
