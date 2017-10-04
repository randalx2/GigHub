using System;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; } 
        public ApplicationUser Artist { get; set; }
        public DateTime DateTime { get; set; }
        public string Venue { get; set; }   

        //Navigation Property
        public Genre Genre { get; set; }    
    }
}