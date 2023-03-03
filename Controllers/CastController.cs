using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_API.Controllers;
using Movie_API.Models;
using Movie_API.Models.Value_Object;
using MovieStore_API.Models;
using MovieStore_API.Models.DTOs.Cast;
using MovieStore_API.Services.Abstract;
using MovieStore_API.Services.Concrete;
using Swashbuckle.AspNetCore.Annotations;

namespace MovieStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : BaseController
    {
        private readonly ICastService _castService;
        private readonly IMapper _mapper;
        public CastController(ICastService castService, IMapper mapper)
        {
            _castService = castService;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet]
        [SwaggerOperation(Summary = "Get First 100 Casts")]
        public IActionResult Get([FromQuery] PagingQuery query)
        {
            var castList = _castService.GetCasts(query);

            return Ok(new { data = castList, paging = castList.Result });
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get Cast By ID")]
        public async Task<IActionResult> GetById(string id)
        {
            var cast = await _castService.GetCastById(id);
            if (cast == null)
            {
                return NotFound();
            }
            return Ok(cast);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add Cast")]
        public async Task<IActionResult> AddCast([FromBody] CastDto cast)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _castService.AddCast(cast);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Cast By ID")]
        public IActionResult UpdateCast([FromBody] CastDto updateCast, string id)
        {
            if (ModelState.IsValid)
            {
                _castService.UpdateCast(id,updateCast);
                return Ok();

            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Cast By ID")]
        public async Task<IActionResult> DeleteCast(string id)
        {
            var cast = await _castService.GetCastById(id);
            if (cast == null)
            {
                return NotFound();
            }
            _castService.DeleteCast(id);
            return Ok();
        }
    }
}
