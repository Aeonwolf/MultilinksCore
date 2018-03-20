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
      private readonly IEndpointService _endpointService;
      private readonly PagingOptions _defaultPagingOptions;

      public EndpointsController(IEndpointService endpointService, IOptions<PagingOptions> defaultPagingOptions)
      {
         _endpointService = endpointService;
         _defaultPagingOptions = defaultPagingOptions.Value;
      }

      // GET api/endpoints/
      [HttpGet(Name = nameof(GetEndpointsAsync))]
      [ResponseCache(CacheProfileName = "Collection")]
      [Etag]
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

         collection.QueryForm = FormMetadata.FromResource<EndpointViewModel>(Link.ToForm(nameof(GetEndpointsAsync),
                                                                                         null,
                                                                                         Link.GetMethod,
                                                                                         Form.QueryRelation));

         collection.SubmitForm = FormMetadata.FromModel(new NewEndpointForm(),
                                                        Link.ToForm(nameof(EndpointsController.CreateEndpointAsync),
                                                                    null,
                                                                    Link.PostMethod,
                                                                    Form.CreateRelation));

         if(!Request.GetEtagHandler().NoneMatch(collection))
         {
            return StatusCode(304, collection);
         }

         return Ok(collection);
      }

      // GET api/endpoints/created-by/{creatorId}
      [HttpGet("created-by/{creatorId}", Name = nameof(GetEndpointsByCreatorIdAsync))]
      [ResponseCache(CacheProfileName = "Collection")]
      [Etag]
      public async Task<IActionResult> GetEndpointsByCreatorIdAsync(Guid creatorId,
                                                                    [FromQuery] PagingOptions pagingOptions,
                                                                    [FromQuery] SortOptions<EndpointViewModel, EndpointEntity> sortOptions,
                                                                    [FromQuery] SearchOptions<EndpointViewModel, EndpointEntity> searchOptions,
                                                                    CancellationToken ct)
      {
         if(!ModelState.IsValid) return BadRequest(new ApiError(ModelState));

         pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
         pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

         var endpoints = await _endpointService.GetEndpointsByCreatorIdAsync(creatorId, pagingOptions, sortOptions, searchOptions, ct);

         var collection = PagedCollection<EndpointViewModel>.Create<EndpointsResponse>(Link.ToCollection(nameof(GetEndpointsByCreatorIdAsync)),
                                                                                       endpoints.Items.ToArray(),
                                                                                       endpoints.TotalSize,
                                                                                       pagingOptions);

         collection.QueryForm = FormMetadata.FromResource<EndpointViewModel>(Link.ToForm(nameof(GetEndpointsAsync),
                                                                                         null,
                                                                                         Link.GetMethod,
                                                                                         Form.QueryRelation));

         collection.SubmitForm = FormMetadata.FromModel(new NewEndpointForm(),
                                                        Link.ToForm(nameof(EndpointsController.CreateEndpointAsync),
                                                                    null,
                                                                    Link.PostMethod,
                                                                    Form.CreateRelation));

         if(!Request.GetEtagHandler().NoneMatch(collection))
         {
            return StatusCode(304, collection);
         }

         return Ok(collection);
      }

      // GET api/endpoints/{endpointId}
      [HttpGet("{endpointId}", Name = nameof(GetEndpointByIdAsync))]
      [ResponseCache(CacheProfileName = "Resource")]
      [Etag]
      public async Task<IActionResult> GetEndpointByIdAsync(Guid endpointId, CancellationToken ct)
      {
         var endpointViewModel = await _endpointService.GetEndpointByIdAsync(endpointId, ct);
         if(endpointViewModel == null) return NotFound();

         if(!Request.GetEtagHandler().NoneMatch(endpointViewModel))
         {
            return StatusCode(304, endpointViewModel);
         }

         return Ok(endpointViewModel);
      }

      // POST api/endpoints
      [HttpPost(Name = nameof(CreateEndpointAsync))]
      [ResponseCache(CacheProfileName = "Resource")]
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

         var endpointId = await _endpointService.CreateEndpointAsync(newEndpoint.CreatorId,
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
      [ResponseCache(CacheProfileName = "Resource")]
      public async Task<IActionResult> DeleteEndpointByIdAsync(Guid endpointId, CancellationToken ct)
      {
         /* TODO: Need to ensure authenticated user is allowed to delete. */

         var existingEndpoint = await _endpointService.GetEndpointByIdAsync(endpointId, ct);

         if(existingEndpoint == null) return NotFound(new ApiError("Device does not exists"));

         var endpointDeleted = await _endpointService.DeleteEndpointByIdAsync(endpointId, ct);

         if(!endpointDeleted)
            return BadRequest(new ApiError("Device failed to be deleted"));

         return NoContent();
      }

      // PUT api/endpoints/{endpointId}
      [HttpPut("{endpointId}", Name = nameof(UpdateEndpointByIdAsync))]
      [ResponseCache(CacheProfileName = "Resource")]
      public async Task<IActionResult> UpdateEndpointByIdAsync(Guid endpointId,
         [FromBody] NewEndpointForm newEndpoint,
         CancellationToken ct)
      {
         if(!ModelState.IsValid) return BadRequest(new ApiError(ModelState));

         /* TODO: Need to ensure creator Id matches authenticated user. */

         var existingEndpoint = await _endpointService.GetEndpointByIdAsync(endpointId, ct);

         if(existingEndpoint == null) return NotFound(new ApiError("Device does not exists"));

         /* We will only allow name and description to be changed (for now). */
         if(newEndpoint.CreatorId != existingEndpoint.CreatorId)
         {
            return BadRequest(new ApiError("One or more fields cannot be modified"));
         }

         /* Device name should be unique for the same user. */
         var endpointExist = await _endpointService.CheckEndpointExistsAsync(newEndpoint.CreatorId, newEndpoint.Name, ct);
         if(endpointExist)
            return BadRequest(new ApiError("A device with the same name already exists"));

         var replacedEndpoint = await _endpointService.ReplaceEndpointByIdAsync(endpointId,
                                                                                newEndpoint.CreatorId,
                                                                                newEndpoint.Name,
                                                                                newEndpoint.Description,
                                                                                ct);

         if(replacedEndpoint == null)
            return BadRequest(new ApiError("Device failed to be updated"));

         return Ok(replacedEndpoint);
      }
   }
}
