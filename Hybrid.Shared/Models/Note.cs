using System.ComponentModel.DataAnnotations;

namespace Hybrid.Shared.Models
{
    public class Note
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string UserName { get; set; }

        [Required, MaxLength(75)]
        public string Title { get; set; }

        [Required, MaxLength(150, ErrorMessage = "Description cant be more than 150 characters")]
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? ModifiedOn { get; set; }

    }
}
