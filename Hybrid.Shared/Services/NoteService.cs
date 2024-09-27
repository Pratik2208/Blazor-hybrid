using Hybrid.Shared.Interfaces;
using Hybrid.Shared.Models;
using System.Net.Http.Json;

namespace Hybrid.Shared.Services
{
    public interface INoteService
    {
        Task<IEnumerable<Note>> GetNotesAsync(int pageNo = 1, int count = 10);
        Task<Note> GetNoteAsync(Guid noteId);
        Task<MethodResult> SaveNote(Note note, bool fromUpdate = false);
        Task<bool> DeleteNoteAsync(Guid noteId);
    }

    public class NoteService : INoteService
    {
        private readonly IHttpClientFactory factory;
        private readonly IStorageService storageService;

        public NoteService(IHttpClientFactory factory, IStorageService storageService)
        {
            this.factory = factory;
            this.storageService = storageService;
        }

        public async Task<IEnumerable<Note>> GetNotesAsync(int pageNo = 1, int count = 10)
        {
            string userName = await storageService.GetAsync("UserName");
            var client = factory.CreateClient("client");
            var notes = await client.GetFromJsonAsync<IEnumerable<Note>>($"/notes/{userName}");
            return notes!.Skip((pageNo - 1) * count)
                     .Take(count)
                     .AsEnumerable();
        }

        public async Task<MethodResult> SaveNote(Note note, bool fromUpdate = false)
        {
            note.UserName = await storageService.GetAsync("UserName");
            var client = factory.CreateClient("client");
            var result = !fromUpdate ? await client.PostAsJsonAsync("api/Notes", note) : await client.PutAsJsonAsync("api/Notes", note);
            return result.IsSuccessStatusCode ? MethodResult.Success() : MethodResult.Fail(await result.Content.ReadAsStringAsync());
        }

        public async Task<Note> GetNoteAsync(Guid noteId)
        {
            var client = factory.CreateClient("client");
            var note = await client.GetFromJsonAsync<Note>($"/api/Notes/{noteId}");
            return note!;
        }

        public async Task<bool> DeleteNoteAsync(Guid noteId)
        {
            var client = factory.CreateClient("client");
            var result = await client.DeleteAsync($"/api/Notes/{noteId}");
            return result.IsSuccessStatusCode;
        }
    }
}
