using Hybrid.Business.Dtos.RequestDto;
using Hybrid.Business.Dtos.ResponseDto;
using Hybrid.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hybrid.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesService service;
        public NotesController(INotesService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ActionResult> CreateNote([FromBody] NoteAddDto addDto)
        {
            var result = await service.AddNote(addDto);
            return result != null ? Ok(result) : StatusCode(StatusCodes.Status500InternalServerError, "Error While Creating Note");
        }

        [HttpGet("{noteId}")]
        public async Task<ActionResult> GetNote(Guid noteId)
        {
            var result = await service.GetNote(noteId);
            return result != null ? Ok(result) : NotFound($"Note with Id {noteId} Not Found");
        }

        [HttpGet]
        public async Task<ActionResult> GetNotes()
        {
            var result = await service.GetNotes();
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateNote(NoteUpdateDto updateDto)
        {
            var result = await service.UpdateNote(updateDto);
            return result != null ? Ok(result) : StatusCode(StatusCodes.Status500InternalServerError, "Error While Updating Note");
        }

        [HttpDelete("{noteId}")]
        public async Task<ActionResult> DeleteUser(Guid noteId)
        {
            var result = await service.DeleteNote(noteId);
            return result ? Ok("Trip Deleted Successfully") : StatusCode(StatusCodes.Status500InternalServerError, "Error While Deleting Note");
        }

        [HttpGet("/notes/{userName}")]
        public async Task<ActionResult> GetTripsByUserId(string userName)
        {
            var result = await service.GetNotesByUserName(userName);
            return result.Any() ? Ok(result) : Ok(Enumerable.Empty<NoteResponseDto>());
        }
    }
}
