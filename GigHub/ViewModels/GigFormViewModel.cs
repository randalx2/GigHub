using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }  
        
        //For drop down lists we need the numeric value of each option
        //This is meant to be the genre Id as stored in the database
        [Required]
        public byte Genre { get; set; }  

        //Hold the list of Genres
        //This is simpler than a list as we are not going to add anything to it
        public IEnumerable<Genre> Genres { get; set; }

        //Using a custom get method for this property
        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}