using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class ArtistsController : Controller
    {
        private ApplicationDbContext _context;

        public ArtistsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Artists
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var artists = _context.Followings
                .Where(u => u.FollowerId == userId)
                .Select(a => a.Artist)
                .ToList();

            return View(artists);
        }

    }
}
