using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers
{
    //Only allow authenticated users to use this
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend([FromBody] int gigId)
        {
            //Get the Id of the currently logged in user from the user object ==> better security

            //Get userId to avoid many db calls
            var userId = User.Identity.GetUserId();

            //Check for duplicate attendances for the current user for the given gig
            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == gigId))
                return BadRequest("The attendance already exists.");

            //Create the attendance object to pass
            var attendance = new Attendance
            {
                GigId = gigId,
                AttendeeId = userId
            };

            //Add to our model context
            _context.Attendances.Add(attendance);

            //Save in the db
            _context.SaveChanges();

            return Ok();
        }
    }
}
