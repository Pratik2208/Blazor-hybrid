using System.ComponentModel.DataAnnotations;

namespace Hybrid.Data.Entities
{
    public class Note : Document
    {
        [Required(ErrorMessage = "UserName required")]
        public string UserName { get; set; }
        [Required, MaxLength(75)]
        public required string Title { get; set; }

        [Required, MaxLength(250)]
        public required string Description { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime ModifiedOn { get; set; }
    }
}
