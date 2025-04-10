﻿using Bookify.Application.Apartments.SearchApartments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Bookings;

[Route("api/apartments")]
[ApiController]
public class ApartmentsController : ControllerBase
{
    private readonly ISender _sender;

    public ApartmentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> SearchApartments(DateOnly startDate, DateOnly endDate, CancellationToken token)
    {
        var query = new SearchApartmentsQuery(startDate, endDate);
        var result = await _sender.Send(query, token);
        return Ok(result.Value);
    }
}