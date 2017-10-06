using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        public string Venue { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }  
        
        //For drop down lists we need the numeric value of each option
        //This is meant to be the genre Id as stored in the database
        public int Genre { get; set; }  

        //Hold the list of Genres
        //This is simpler than a list as we are not going to add anything to it
        public IEnumerable<Genre> Genres { get; set; }
    }
}