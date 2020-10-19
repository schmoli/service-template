using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schmoli.ServiceTemplate.Models;
using Schmoli.ServiceTemplate.Requests;
using Schmoli.ServiceTemplate.Resources;
using Schmoli.ServiceTemplate.Services;
using Schmoli.Services.Core.Exceptions;
using Schmoli.Services.Core.Results;

namespace Schmoli.ServiceTemplate.Controllers
{
    /// <summary>
    /// Blah blah blah
    /// </summary>
    [ApiVersion(Startup.ApiVersionMajorString)]
    [ApiController]
    [Route("api/v{apiVersion:apiVersion}/[controller]")]
    public class PrimaryItemsController : ControllerBase
    {
        private readonly IPrimaryItemService _service;
        private readonly IMapper _mapper;

        public PrimaryItemsController(IPrimaryItemService entityService, IMapper mapper)
        {
            _service = entityService;
            _mapper = mapper;
        }

        /// <summary>
        /// Create item
        /// </summary>
        /// <param name="resourceToSave"></param>
        /// <param name="apiVersion"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<PrimaryItemResource>> Create([FromBody] PrimaryItemSaveResource resourceToSave, ApiVersion apiVersion)
        {
            try
            {
                var model = _mapper.Map<PrimaryItemSaveResource, PrimaryItem>(resourceToSave);
                model = await _service.Create(model);
                var resource = _mapper.Map<PrimaryItem, PrimaryItemResource>(model);
                return CreatedAtAction(nameof(Get), new { id = 1, apiVersion = apiVersion.ToString() }, resource);

            }
            catch (ArgumentNotUniqueException exception)
            {
                return Conflict(exception.ArgumentName);
            }
            catch (ArgumentNotFoundException exception)
            {
                return BadRequest(exception.ArgumentName);
            }
        }

        /// <summary>
        /// Return item by Id
        /// </summary>
        /// <remarks>
        /// Additional documentation here under swagger page
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<PrimaryItemResource>> Get(long id)
        {
            var item = await _service.GetById(id);

            if (item == null)
            {
                return NotFound();
            }

            var resource = _mapper.Map<PrimaryItem, PrimaryItemResource>(item);
            return Ok(resource);
        }

        /// <summary>
        /// Update item
        /// </summary>
        /// <remarks>
        /// Addtional API documentation
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public async Task<ActionResult<PrimaryItemResource>> Update(long id, [FromBody] PrimaryItemSaveResource resourceToSave)
        {
            var modelToUpdate = await _service.GetById(id);

            if (modelToUpdate == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<PrimaryItemSaveResource, PrimaryItem>(resourceToSave);

            try
            {
                await _service.Update(modelToUpdate, model);
                var resource = _mapper.Map<PrimaryItem, PrimaryItemResource>(modelToUpdate);

                return Ok(resource);
            }
            catch (ArgumentNotFoundException exception)
            {
                return BadRequest(exception.ArgumentName);
            }
            catch (ArgumentNotUniqueException exception)
            {
                return Conflict(exception.ArgumentName);
            }
        }

        /// <summary>
        /// Delete item
        /// </summary>
        /// <remarks>
        /// Addtional API documentation
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var item = await _service.GetById(id);

            if (item == null)
            {
                return NotFound();
            }

            await _service.Delete(item);

            return NoContent();
        }

        /// <summary>
        /// Get all items matching the request parameters
        /// </summary>
        /// <remarks>
        /// Additional documentation here under swagger page
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<PagedResultSet<PrimaryItemResource>>> Search([FromQuery] PrimaryItemPagedRequest pagedRequest)
        {
            var models = await _service.GetPagedResultsWithSecondaryItem(pagedRequest);
            var resources = _mapper.Map<PagedResultSet<PrimaryItem>, PagedResultSet<PrimaryItemResource>>(models);

            return Ok(resources);
        }
    }
}
