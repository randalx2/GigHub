using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        //Initialze the db context in the constructor
        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();

            //Eager load with the navigation property
            var gigs = (from g in _context.Gigs
                        where g.ArtistId == userId
                        && g.DateTime > DateTime.Now
                        select g)
                        .Include(g => g.Genre)
                        .ToList();

            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending"
            };

            return View("Gigs", viewModel);
        }

    
        //Only registered users and artist should be able to add a gig to view this page
        //Use the Authorize Data annotation
        //This is the method to GET to the page
        [Authorize]
        public ActionResult Create()
        {
            //Get the list of Genres from the db
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Heading = "Add a Gig"
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();

            var gig = (from g in _context.Gigs
                       where g.Id == id && g.ArtistId == userId
                       select g).Single();

            //Get the list of Genres from the db
            var viewModel = new GigFormViewModel
            {
                Heading = "Edit a Gig",
                Id = gig.Id,
                Genres = _context.Genres.ToList(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            //If model state is not valid stay on this page
            if (!ModelState.IsValid)
            {
                //Set the Genres Property of the view model before returning it
                //This is because we are using HttpPost and not HttpGet Create
                viewModel.Genres = _context.Genres.ToList();

                return View("GigForm", viewModel);
            }
                
            var gig = new Gig
            {
                //Set the foreign key and its nav will be set
                ArtistId = User.Identity.GetUserId(),

                //The controller should be responsible for formatting the data
                //The controller is mainly the manager for the process
                //GetDateTime = GetDateTime.Parse(string.Format("{0} {1}", viewModel.Date, viewModel.Time)),

                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            //Add this Gig Object to our context
            _context.Gigs.Add(gig);

            //Save changes to the db
            _context.SaveChanges();

            //After adding a successful gig redirect to user to the home page
            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            //If model state is not valid stay on this page
            if (!ModelState.IsValid)
            {
                //Set the Genres Property of the view model before returning it
                //This is because we are using HttpPost and not HttpGet Create
                viewModel.Genres = _context.Genres.ToList();

                return View("GigForm", viewModel);
            }

            var userId = User.Identity.GetUserId();

            var gig = (from g in _context.Gigs
                       where g.Id == viewModel.Id && g.ArtistId == userId
                       select g).Single();

            //Update the Gig properties
            gig.Venue = viewModel.Venue;
            gig.DateTime = viewModel.GetDateTime();
            gig.GenreId = viewModel.Genre;

            //Save changes to the db
            _context.SaveChanges();

            //After adding a successful gig redirect to user to the home page
            return RedirectToAction("Mine", "Gigs");
        }
    }
}