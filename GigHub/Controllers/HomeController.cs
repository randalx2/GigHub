using GigHub.Models;
using System;

//To use lamda expressions for eager loading
using System.Data.Entity;

using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        //Initialize in constructor
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            //Also include the navigation property ==> ie artists associated with each gig
            //Also filter only for upcoming Gigs in the future
            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Where(g => g.DateTime > DateTime.Now);

            return View(upcomingGigs);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}