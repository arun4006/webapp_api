using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webapp_api.Models;
using System.Data.Entity;
using Webapp_api.ViewModels;

namespace Webapp_api.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies

        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            // base.Dispose(disposing);
        }
        public ActionResult Index()
        {   
            var movies = _context.Movies.Include(c => c.Genre).ToList();


            return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movies = _context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);
            if (movies == null)
                return HttpNotFound();
            return View(movies);
        }

        public ActionResult Edit(int id)
        {
            var movies = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movies == null)
            {
                return HttpNotFound();
            }
            var viewmodel = new MovieFormViewModel
            {
                Movies = movies,
                Genre = _context.Genre.ToList()
            };
            return View("MovieForm", viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movies movies)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel
                {
                    Movies = movies,
                    Genre = _context.Genre.ToList()
                };

                return View("MovieForm", viewModel);
            }
            if (movies.Id == 0)
            {

                _context.Movies.Add(movies);
                
                
            }
            else
            {
                var moviesInDB = _context.Movies.Single(c => c.Id == movies.Id);
                moviesInDB.Name = movies.Name;
                moviesInDB.ReleaseDate = movies.ReleaseDate;
                moviesInDB.GenreId = movies.GenreId;
                moviesInDB.NumberInStock = movies.NumberInStock;

            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        public ActionResult MovieForm()
        {
            var genres = _context.Genre.ToList();
            var viewmodel = new MovieFormViewModel
            {
                Movies=new Movies(),  
                Genre = genres
            };

            return View("MovieForm", viewmodel);
        }

        //public ActionResult New()
        //{
        //    return View("MovieForm");
        //}
    }
} 