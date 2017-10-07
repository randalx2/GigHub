using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; } 

        public ApplicationUser Artist { get; set; }

        //Add a Foreing Key to reduce db calls
        //This is a string since in the ApplicationUser class the user id is a string and not int
        [Required]
        public string ArtistId { get; set; }    

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }   

        //Navigation Property
        public Genre Genre { get; set; }

        //Foreign Key for Genre
        [Required]
        public byte GenreId { get; set; }   
    }
}