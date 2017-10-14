using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class Attendance
    {
        //Navigation Properties
        public Gig Gig { get; set; }
        public ApplicationUser Attendee { get; set; }   

        //Foreing Keys properties for optimization

        //The GigId and AttendeeId uniquely rep an Attendance
        //This is a composite key therefore use Key Attb
        //Also specify the order of the columns
        //If you want to query this object directly add it to the db set

        //Composite Primary Key ==> made up of 2 foreign keys
        [Key]
        [Column(Order = 1)]
        public int GigId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string AttendeeId { get; set; }  
    }
}