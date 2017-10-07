using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
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

            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            var gig = new Gig
            {
                //Set the foreign key and its nav will be set
                ArtistId = User.Identity.GetUserId(),
                DateTime = DateTime.Parse(string.Format("{0} {1}", viewModel.Date, viewModel.Time)),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            //Add this Gig Object to our context
            _context.Gigs.Add(gig);

            //Save changes to the db
            _context.SaveChanges();

            //After adding a successful gig redirect to user to the home page
            return RedirectToAction("Index", "Home");
        }
    }
}