using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Schmoli.ServiceTemplate.Models;
using Schmoli.ServiceTemplate.Requests;
using Schmoli.ServiceTemplate.Resources;
using Schmoli.ServiceTemplate.Services;
using Schmoli.Services.Core.Exceptions;
using Schmoli.Services.Core.Results;

namespace Schmoli.ServiceTemplate.Controllers
{
    [ApiVersion(Startup.ApiVersionMajorString)]
    [ApiController]
    [Route("api/v{apiVersion:apiVersion}/[controller]")]
    public class SecondaryItemsController : ControllerBase
    {
        private readonly ISecondaryItemService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<SecondaryItemsController> _logger;

        public SecondaryItemsController(ISecondaryItemService service, IMapper mapper, ILogger<SecondaryItemsController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
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
        public async Task<ActionResult<SecondaryItemResource>> Create([FromBody] SecondaryItemSaveResource resourceToSave, ApiVersion apiVersion)
        {
            try
            {
                var model = _mapper.Map<SecondaryItemSaveResource, SecondaryItem>(resourceToSave);
                model = await _service.Create(model);
                var resource = _mapper.Map<SecondaryItem, SecondaryItemResource>(model);
                return CreatedAtAction(nameof(Get), new { id = 1, apiVersion = apiVersion.ToString() }, resource);
            }
            catch (ArgumentNotUniqueException exception)
            {
                return Conflict(exception.ArgumentName);
            }
        }

        /// <summary>
        /// Returns all items
        /// </summary>
        /// <remarks>
        /// Addtional API documentation
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<PagedResultSet<SecondaryItemResource>>> Search([FromQuery] SecondaryItemPagedRequest pagedRequest)
        {
            var models = await _service.Search(pagedRequest);
            var resources = _mapper.Map<PagedResultSet<SecondaryItem>, PagedResultSet<SecondaryItemResource>>(models);
            return Ok(resources);
        }

        /// <summary>
        /// Get item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SecondaryItemResource>> Get(long id)
        {
            var item = await _service.GetById(id);

            if (item == null)
            {
                return NotFound();
            }

            var resource = _mapper.Map<SecondaryItem, SecondaryItemResource>(item);
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
        public async Task<ActionResult<SecondaryItemResource>> Update(long id, [FromBody] SecondaryItemSaveResource resourceToSave)
        {
            var modelToUpdate = await _service.GetById(id);

            if (modelToUpdate == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<SecondaryItemSaveResource, SecondaryItem>(resourceToSave);

            try
            {

                await _service.Update(modelToUpdate, model);

                var resource = _mapper.Map<SecondaryItem, SecondaryItemResource>(modelToUpdate);

                return Ok(resource);
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
        /// Delete item
        /// </summary>
        /// <remarks>
        /// Addtional API documentation
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
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

            try
            {
                await _service.Delete(item);
            }
            catch (ArgumentInUseException exception)
            {
                return Conflict(exception.ArgumentName);
            }

            return NoContent();
        }

    }
}
