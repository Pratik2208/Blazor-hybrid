using System.ComponentModel.DataAnnotations;

namespace Hybrid.Business.Dtos.ResponseDto
{
    public class NoteResponseDto
    {
        public Guid Id { get; set; } = Guid.Empty;

        [Required, MaxLength(75)]
        public required string Title { get; set; }

        [Required, MaxLength(250)]
        public required string Description { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime ModifiedOn { get; set; }
    }
}
