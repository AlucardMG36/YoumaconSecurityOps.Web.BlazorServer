﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.Mediatr.Commands;
using YoumaconSecurityOps.Core.Mediatr.Queries;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YoumaconSecurityOps.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly ILogger<StaffController> _logger;

        public StaffController(IMediator mediator, ILogger<StaffController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // GET: api/<StaffController>
        [HttpGet(nameof(GetStaffList))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IAsyncEnumerable<StaffReader>))]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IAsyncEnumerable<StaffReader>>> GetStaffList([FromQuery] GetStaffQuery staffQuery)
        {
            _logger.LogInformation("{GetStaffList}([FromQuery] GetStaffQuery staffQuery): {@staffQuery}", nameof(GetStaffList), staffQuery);
            
            return Ok(await _mediator.Send(staffQuery));
        }

        // GET api/<StaffController>/5
        [HttpGet(nameof(GetStaffListWithParameters))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IAsyncEnumerable<StaffReader>))]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IAsyncEnumerable<StaffReader>>> GetStaffListWithParameters([FromQuery] GetStaffWithParametersQuery staffWithParametersQuery)
        {
            return Ok(await _mediator.Send(staffWithParametersQuery));
        }

        // POST api/<StaffController>
        [HttpPost(nameof(AddStaffMember))]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> AddStaffMember([FromBody] AddFullStaffEntryCommand addFullStaffEntryCommand)
        {
            return Created(Request.Path.Value, await _mediator.Send(addFullStaffEntryCommand));
        }
    }
}
