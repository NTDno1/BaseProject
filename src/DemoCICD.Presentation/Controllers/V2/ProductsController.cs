using Asp.Versioning;
using DemoCICD.Contract.Extensions;
using DemoCICD.Contract.Services.V2.Product;
using DemoCICD.Contract.Share;
using DemoCICD.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoCICD.Presentation.Controllers.V2;
[ApiVersion(2)]
public class ProductsController : ApiController
{
    public ProductsController(ISender sender) : base(sender)
    {
    }

    [HttpGet(Name = "GetProducts")]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.ProductResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Products(
        string? serchTerm = null,
        string? sortColumn = null,
        string? sortOrder = null,
        int pageIndex = 1,
        int pageSize = 10)
    {
        var result = await Sender.Send(new Query.GetProductsQuery(
            serchTerm,
            sortColumn,
            SortOrderExtension.ConvertStringToSortOrder(sortOrder),
            pageIndex,
            pageSize));
        return Ok(result);
    }

    [HttpPost(Name = "CreateProducts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Products([FromBody] Command.CreateProductCommand CreateProduct)
    {
        var result = await Sender.Send(CreateProduct);

        if (result.IsFailure)
        {
            return HandlerFailure(result);
        }

        return Ok(result);
    }
}
