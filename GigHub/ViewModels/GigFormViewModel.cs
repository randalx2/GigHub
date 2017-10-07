using System;
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
        public byte Genre { get; set; }  

        //Hold the list of Genres
        //This is simpler than a list as we are not going to add anything to it
        public IEnumerable<Genre> Genres { get; set; }

        //Using a custom get method for this property
        public DateTime DateTime
        {
            get { return DateTime.Parse(string.Format("{0} {1}", Date, Time)); }
        }
    }
}