using Hybrid.Business.Dtos.ResponseDto;
using Hybrid.Data.Entities;

namespace Hybrid.Business.Extensions
{
    public static class EntityToResponseDtoExtension
    {
        public static NoteResponseDto ToNoteResponse(this Note note)
        {
            return new NoteResponseDto
            {
                Id = note.Id,
                Title = note.Title,
                Description = note.Description,
                CreatedOn = note.CreatedOn,
                ModifiedOn = note.ModifiedOn
            };
        }

        public static IEnumerable<NoteResponseDto> ToNoteResponse(this IEnumerable<Note> notes)
        {
            return notes.Select(n => new NoteResponseDto
            {
                Id = n.Id,
                Title = n.Title,
                Description = n.Description,
                CreatedOn = n.CreatedOn,
                ModifiedOn = n.ModifiedOn
            }).AsEnumerable();
        }
    }
}
