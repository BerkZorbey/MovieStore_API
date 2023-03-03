using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Movie_API.Models;
using Movie_API.Models.Value_Object;
using MovieStore_API.Models.DTOs;
using MovieStore_API.Models.DTOs.Movie;
using MovieStore_API.Services.Abstract;
using MovieStore_API.Services.Concrete;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections;
using System.Text.Json;

namespace Movie_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : BaseController
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public MovieController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet]
        [SwaggerOperation(Summary = "Get First 100 Movies")]
        public async Task<IActionResult> Get([FromQuery] PagingQuery query)
        {
            var movieList = await _movieService.GetMovies(query);
            
            return Ok(new {data=movieList,paging=movieList.Result});
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get Movie By ID")]
        public async Task<IActionResult> GetById(string id)
        {
            var movie = await _movieService.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }
        
        [HttpPost]
        [SwaggerOperation(Summary = "Add Movie")]
        public async Task<IActionResult> AddMovie([FromBody] MovieDto movie)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var newMovie = _mapper.Map<Movie>(movie);
            await _movieService.AddMovie(newMovie);
            return StatusCode(201);

        }
        
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Movie By ID")]
        public IActionResult UpdateMovie([FromBody] MovieUpdateDto updateMovie, string id)
        {
            _movieService.UpdateMovie(id, updateMovie);
            return Ok();
        }
        
        //[HttpPatch("{id}")]
        //[SwaggerOperation(Summary = "Update Movie Duration By ID")]
        //public IActionResult UpdateMovieDuration([FromBody] MovieDurationDTO movieDuration, string id)
        //{
            
        //    var movie = _movieService.GetMovieById(id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }
        //    movie.Duration = movieDuration.Duration != default ? movieDuration.Duration : movie.Duration;
        //    _movieService.UpdateMovie(movie);
        //    return Ok();
        //}



        
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Movie By ID")]
        public async Task<IActionResult> DeleteMovie(string id)
        {
            var movie = await _movieService.GetMovieById(id);
            if(movie == null)
            {
                return NotFound();
            }
            _movieService.DeleteMovie(movie);
            return Ok();
        }
        
    }
}
