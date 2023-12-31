﻿using Application.CQRS.ReferenceTable.Queries;
using Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferenceTableController : Controller
    {
        private readonly IMediator _mediator;

        public ReferenceTableController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Category")]
        public async Task<ResponseDto> Categorys([FromRoute] GetCategoryQuery query)
                => await _mediator.Send(query);

    }
}
