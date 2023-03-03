using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_API.Controllers;
using Movie_API.Models;
using Movie_API.Models.Value_Object;
using MovieStore_API.Models;
using MovieStore_API.Models.DTOs.Director;
using MovieStore_API.Services.Abstract;
using MovieStore_API.Services.Concrete;
using Swashbuckle.AspNetCore.Annotations;

namespace MovieStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : BaseController
    {
        private readonly IDirectorService _directorService;
        private readonly IMapper _mapper;
        public DirectorController(IDirectorService directorService, IMapper mapper)
        {
            _directorService = directorService;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet]
        [SwaggerOperation(Summary = "Get First 100 Directors")]
        public IActionResult Get([FromQuery] PagingQuery query)
        {
            var directorList = _directorService.GetDirectors(query);

            return Ok(new { data = directorList, paging = directorList.Result });
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get Director By ID")]
        public async Task<IActionResult> GetById(string id)
        {
            var director = await _directorService.GetDirectorById(id);
            if (director == null)
            {
                return NotFound();
            }
            return Ok(director);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add Director")]
        public async Task<IActionResult> AddDirector([FromBody] DirectorDto director)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
           
            await _directorService.AddDirector(director);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Director By ID")]
        public IActionResult UpdateDirector([FromBody] DirectorDto updateDirector, string id)
        {
            if (ModelState.IsValid)
            {
                 _directorService.UpdateDirector(id, updateDirector);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Director By ID")]
        public async Task<IActionResult> DeleteDirector(string id)
        {
            var director = await _directorService.GetDirectorById(id);
            if (director == null)
            {
                return NotFound();
            }
            _directorService.DeleteDirector(id);
            return Ok();
        }
    }
}
