using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models.Entities
{

    public class MemberUnits

    {
        [Key]
        public int MenberUnitId { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string WebsiteUrl { get; set; }
        
        
    }
}
