using System.ComponentModel.DataAnnotations;

namespace Hybrid.Business.Dtos.RequestDto
{
    public class NoteUpdateDto
    {
        public Guid Id { get; set; } = Guid.Empty;

        [Required]
        public string UserName { get; set; }

        [Required, MaxLength(75)]
        public string Title { get; set; }

        [Required, MaxLength(250)]
        public string Description { get; set; }
    }
}
