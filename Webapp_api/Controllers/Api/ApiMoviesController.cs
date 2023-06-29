using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Webapp_api.Dtos;
using Webapp_api.Models;

namespace Webapp_api.Controllers.Api
{
    public class ApiMoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public ApiMoviesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        [Route("api/getmovies")]
        public IEnumerable<MovieDtos> ListMovies()
        {
            return _context.Movies.ToList().Select(Mapper.Map<Movies, MovieDtos>);
        }

        [HttpGet]
        [Route("api/getmoviesbyid/{id}")]
        public IHttpActionResult ListMoviesById(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movies, MovieDtos>(movie));
        }

        [HttpPost]
        [Route("api/newmovies")]
        public IHttpActionResult CreateMovies(MovieDtos movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDtos, Movies>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        [HttpPut]
        [Route("api/updatemovie/{id}")]
        public IHttpActionResult ModifyMovie(int id, MovieDtos movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                return NotFound();

            Mapper.Map(movieDto, movieInDb);

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("api/delete_movie/{id}")]
        public IHttpActionResult DeletedMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                return NotFound();

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();

            return Ok();
        }

    }
}
