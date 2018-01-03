﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Multilinks.ApiService.Infrastructure;
using Multilinks.ApiService.Models;
using Multilinks.ApiService.Services;
using Multilinks.DataService.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Multilinks.ApiService.Controllers
{
   [Route("api/[controller]")]
   [ApiVersion("1.0")]
   //[Authorize]
   public class EndpointsController : Controller
   {
      static public readonly Guid defaultServiceAreaId = new Guid("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF");

      private readonly IEndpointService _endpointService;
      private readonly PagingOptions _defaultPagingOptions;

      public EndpointsController(IEndpointService endpointService, IOptions<PagingOptions> defaultPagingOptions)
      {
         _endpointService = endpointService;
         _defaultPagingOptions = defaultPagingOptions.Value;
      }

      // GET api/endpoints/
      [HttpGet(Name = nameof(GetEndpointsAsync))]
      public async Task<IActionResult> GetEndpointsAsync(
         [FromQuery] PagingOptions pagingOptions,
         [FromQuery] SortOptions<EndpointViewModel, EndpointEntity> sortOptions,
         [FromQuery] SearchOptions<EndpointViewModel, EndpointEntity> searchOptions,
         CancellationToken ct)
      {
         if(!ModelState.IsValid) return BadRequest(new ApiError(ModelState));

         pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
         pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

         var endpoints = await _endpointService.GetEndpointsAsync(pagingOptions, sortOptions, searchOptions, ct);

         var collection = PagedCollection<EndpointViewModel>.Create<EndpointsResponse>(Link.ToCollection(nameof(GetEndpointsAsync)),
                                                                                       endpoints.Items.ToArray(),
                                                                                       endpoints.TotalSize,
                                                                                       pagingOptions);

         collection.QueryForm = FormMetadata.FromResource<EndpointViewModel>(Link.ToForm(
                                                                             nameof(GetEndpointsAsync),
                                                                             null,
                                                                             Link.GetMethod,
                                                                             Form.QueryRelation));

         return Ok(collection);
      }

      // GET api/endpoints/{endpointId}
      [HttpGet("{endpointId}", Name = nameof(GetEndpointByIdAsync))]
      public async Task<IActionResult> GetEndpointByIdAsync(Guid endpointId, CancellationToken ct)
      {
         var endpointViewModel = await _endpointService.GetEndpointByIdAsync(endpointId, ct);
         if(endpointViewModel == null) return NotFound();

         return Ok(endpointViewModel);
      }

      // POST api/endpoints
      [HttpPost(Name = nameof(CreateEndpointAsync))]
      public async Task<IActionResult> CreateEndpointAsync(
         [FromBody] NewEndpointForm newEndpoint,
         CancellationToken ct)
      {
         if(!ModelState.IsValid) return BadRequest(new ApiError(ModelState));

         /* TODO: Need to ensure creator Id matches authenticated user. */

         /* Device name should be unique for the same user. */
         var endpointExist = await _endpointService.CheckEndpointExistsAsync(newEndpoint.CreatorId, newEndpoint.Name, ct);
         if(endpointExist)
            return BadRequest(new ApiError("A device with the same name already exists"));

         /* We need to determine what type of endpoint we are creating and do some sanity check
          * before we attempt to create it.
          */
         if(newEndpoint.IsGateway)
         {
            /* We want to create a gateway. */
            if(newEndpoint.DirectionCapability != EndpointEntity.CommsDirectionCapabilities.transmitAndReceive)
               return BadRequest(new ApiError("Gateways needs to be able to communicate both ways"));

            if(!newEndpoint.IsCloudConnected)
               return BadRequest(new ApiError("Gateways needs to be cloud-connected"));
         }
         else if(newEndpoint.IsCloudConnected)
         {
            /* We want to create a client. */
            if(newEndpoint.DirectionCapability != EndpointEntity.CommsDirectionCapabilities.transmitAndReceive)
               return BadRequest(new ApiError("Clients needs to be able to communicate both ways"));

            if(!newEndpoint.IsCloudConnected)
               return BadRequest(new ApiError("Clients needs to be cloud-connected"));

            if(newEndpoint.ServiceAreaId != defaultServiceAreaId)
               return BadRequest(new ApiError("Bad service area Id"));
         }
         else
         {
            /* We want to create a managed device. */
            var gatewayExist = await _endpointService.CheckGatewayExistsAsync(newEndpoint.ServiceAreaId, ct);

            if(!gatewayExist)
               return BadRequest(new ApiError("Gateway to manage this device does not exists"));
         }

         var endpointId = await _endpointService.CreateEndpointAsync(newEndpoint.ServiceAreaId,
                                                                        newEndpoint.CreatorId,
                                                                        newEndpoint.IsCloudConnected,
                                                                        newEndpoint.IsGateway,
                                                                        newEndpoint.DirectionCapability,
                                                                        newEndpoint.Name,
                                                                        newEndpoint.Description,
                                                                        ct);

         /* This is a workaround to build the link of the new endpoint. */
         var newEndpointUrl = Url.Link(nameof(EndpointsController.CreateEndpointAsync), null);
         newEndpointUrl = newEndpointUrl + "/" + endpointId;

         return Created(newEndpointUrl, null);
      }

      // DELETE api/endpoints/{endpointId}
      [HttpDelete("{endpointId}", Name = nameof(DeleteEndpointByIdAsync))]
      public async Task<IActionResult> DeleteEndpointByIdAsync(Guid endpointId, CancellationToken ct)
      {
         /* TODO: Need to ensure authenticated user is allowed to delete. */

         await _endpointService.DeleteEndpointByIdAsync(endpointId, ct);

         return NoContent();
      }
   }
}
