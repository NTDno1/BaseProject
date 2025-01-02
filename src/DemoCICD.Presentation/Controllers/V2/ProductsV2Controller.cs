using Asp.Versioning;
using DemoCICD.Contract.Services.Product;
using DemoCICD.Contract.Share;
using DemoCICD.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoCICD.Presentation.Controllers.V2;
[ApiVersion(2)]
public class ProductsV2Controller : ApiController
{
    public ProductsV2Controller(ISender sender) : base(sender)
    {
    }

    [HttpGet(Name = "GetProductsV2")]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.ProductResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProductsV2(CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new Query.GetProductQuery());
        return Ok(result);
    }
}
