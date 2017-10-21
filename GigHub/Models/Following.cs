using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class Following
    {
        //Nav Properties
        public ApplicationUser Follower { get; set; }
        public ApplicationUser Artist { get; set; } 

        //Foreign Keys
        [Key]
        [Column(Order = 1)]
        public string FollowerId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string ArtistId { get; set; }   
    }
}