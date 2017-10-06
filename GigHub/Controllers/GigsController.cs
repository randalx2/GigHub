using GigHub.Models;
using GigHub.ViewModels;
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

 
        public ActionResult Create()
        {
            //Get the list of Genres from the db
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),

            };

            return View(viewModel);
        }
    }
}