using Hybrid.Business.Dtos.RequestDto;
using Hybrid.Business.Dtos.ResponseDto;
using Hybrid.Business.Extensions;
using Hybrid.Data.Repositories;

namespace Hybrid.Business.Services
{
    public interface INotesService
    {
        Task<NoteResponseDto> AddNote(NoteAddDto addDto);
        Task<bool> DeleteNote(Guid id);
        Task<NoteResponseDto> GetNote(Guid id);
        Task<IEnumerable<NoteResponseDto>> GetNotes();
        Task<IEnumerable<NoteResponseDto>> GetNotesByUserName(string userName);
        Task<NoteResponseDto> UpdateNote(NoteUpdateDto updateDto);
    }

    public class NotesService : INotesService
    {
        private readonly NoteRepository repository;
        public NotesService(NoteRepository repository)
        {
            this.repository = repository;
        }

        public async Task<NoteResponseDto> AddNote(NoteAddDto addDto)
        {
            var note = addDto.ToNote();
            var repoResult = await repository.AddEntity(note);
            return repoResult.IsSuccess ? note.ToNoteResponse() : null!;
        }

        public async Task<NoteResponseDto> GetNote(Guid id)
        {
            var repoResult = await repository.Get(id);
            return (repoResult.IsSuccess && repoResult.Data != null) ? repoResult.Data.ToNoteResponse() : null!;
        }

        public async Task<IEnumerable<NoteResponseDto>> GetNotes()
        {
            var repoResult = await repository.GetAll();
            return repoResult.IsSuccess ? repoResult.Data!.ToNoteResponse() : null!;
        }

        public async Task<NoteResponseDto> UpdateNote(NoteUpdateDto updateDto)
        {
            var note = updateDto.ToNote();
            var repoResult = await repository.Update(note);
            return repoResult.IsSuccess ? note.ToNoteResponse() : null!;
        }

        public async Task<bool> DeleteNote(Guid id)
        {
            var repoResult = await repository.Delete(id, "Note");
            return repoResult.IsSuccess;
        }

        public async Task<IEnumerable<NoteResponseDto>> GetNotesByUserName(string userName)
        {
            var repoResult = await repository.GetNotesByUserName(userName);
            return (repoResult.IsSuccess && !(repoResult.Data!.Any())) ? [] : repoResult.Data!.ToNoteResponse();
        }
    }
}
