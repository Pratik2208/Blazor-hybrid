using Hybrid.Business.Dtos.RequestDto;
using Hybrid.Data.Entities;

namespace Hybrid.Business.Extensions
{
    public static class RequestDtoToEntityExtension
    {
        public static Note ToNote(this NoteAddDto addDto)
        {
            return new Note
            {
                UserName = addDto.UserName,
                Title = addDto.Title,
                Description = addDto.Description,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };
        }

        public static Note ToNote(this NoteUpdateDto updateDto)
        {
            return new Note
            {
                UserName = updateDto.UserName,
                Id = updateDto.Id,
                Title = updateDto.Title,
                Description = updateDto.Description,
                ModifiedOn = DateTime.Now
            };
        }
    }
}
