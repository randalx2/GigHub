using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers
{
    //Only allow authenticated users to use this
    [Authorize]
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        //api/followings
        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            //Get the Id of the currently logged in user from the user object ==> better security

            //Get userId to avoid many db calls
            var userId = User.Identity.GetUserId();

            //Check for duplicate following for the current user for the given gig
            if (_context.Followings.Any(a => a.FollowerId == userId && a.ArtistId == dto.ArtistId))
                return BadRequest("The following already exists.");

            //Create the Following object
            var following = new Following
            {
                FollowerId = userId,
                ArtistId = dto.ArtistId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }
    }
}
