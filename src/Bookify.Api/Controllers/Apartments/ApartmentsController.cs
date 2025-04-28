using Asp.Versioning;
using Bookify.Application.Apartments.SearchApartments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Apartments;

[Authorize]
[ApiVersion(ApiVersions.V1)] 
[Route("api/v{version:apiVersion}/apartments")]
[ApiController]
public class ApartmentsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> SearchApartments(DateOnly startDate, DateOnly endDate, CancellationToken token)
    {
        var query = new SearchApartmentsQuery(startDate, endDate);
        var result = await sender.Send(query, token);
        return Ok(result.Value);
    }
}