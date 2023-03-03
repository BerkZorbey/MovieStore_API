using Movie_API.Models.Value_Object;
using Movie_API.Models;
using AutoMapper;
using MongoDB.Driver;
using MovieStore_API.Models;
using MovieStore_API.Services.Abstract;
using MovieStore_API.Models.DTOs.Director;
using MovieStore_API.Repository.Abstract;
using MovieStore_API.Models.DTOs.Cast;
using MovieStore_API.Repository.Concrete;

namespace MovieStore_API.Services.Concrete
{
    public class DirectorService : IDirectorService
    {
        private readonly IDirectorRepository _directorRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        public DirectorService(IDirectorRepository directorRepository, IMapper mapper, IMovieRepository movieRepository)
        {
            _directorRepository = directorRepository;
            _mapper = mapper;
            _movieRepository = movieRepository;
        }

        public async Task<Director> AddDirector(DirectorDto director)
        {
            List<MoviesDirectedByDto> addMovie = new List<MoviesDirectedByDto>();
            var getMovies = await _movieRepository.GetAllMovies();
            foreach(var item in director.MoviesDirectedBy)
            {
                var movie = getMovies.Where(x => x.Name == item.Name).FirstOrDefault();
                if(movie != null)
                {
                    var mapMovie = _mapper.Map<MoviesDirectedByDto>(movie);
                    addMovie.Add(mapMovie);

                }
            }
            var mapDirector = _mapper.Map<Director>(director);
            if (mapDirector.MoviesDirectedBy == null) mapDirector.MoviesDirectedBy = new List<MoviesDirectedByDto>();
            mapDirector.MoviesDirectedBy = addMovie;
            await _directorRepository.AddDirector(mapDirector);
            return mapDirector;

        }

        public void DeleteDirector(string id)
        {
            _directorRepository.DeleteDirector(id);
        }

        public async Task<List<Director>> GetAllDirectors()
        {
            var directors = await _directorRepository.GetAllDirectors();
            return directors;
        }

        public async Task<Director> GetDirectorById(string id)
        {
            var director = await _directorRepository.GetDirectorById(id);
            return director;
        }

        public PagingResultModel<Director> GetDirectors(PagingQuery query)
        {
            var response = _directorRepository.GetDirectors(query);
            return response;
        }

        public async void UpdateDirector(string id,DirectorDto updateDirector)
        {
            List<MoviesDirectedByDto> addMovie = new List<MoviesDirectedByDto>();
            var director = await _directorRepository.GetDirectorById(id);
            var getMovies = await _movieRepository.GetAllMovies();
            if (director == null)
            {
                throw new Exception("Data is not found");
            }
            
            director.Name = updateDirector.Name != default ? updateDirector.Name : director.Name;
            director.Surname = updateDirector.Surname != default ? updateDirector.Surname : director.Surname;
            foreach (var item in updateDirector.MoviesDirectedBy)
            {
                var movie = getMovies.Where(x => x.Name == item.Name).FirstOrDefault();
                var directorMovie = director.MoviesDirectedBy.Where(x => x.Name == item.Name).FirstOrDefault();
                if(movie != null)
                {
                    if (directorMovie == null)
                    {
                        var movieMapper = _mapper.Map<MoviesDirectedByDto>(movie);

                        addMovie.Add(movieMapper);
                    }


                }
            }

            director.MoviesDirectedBy.AddRange(addMovie);
            _directorRepository.UpdateDirector(director);
        }
    }
}
