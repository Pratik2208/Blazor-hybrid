using Hybrid.Data.Entities;
using Hybrid.Shared;
using Microsoft.Azure.Cosmos.Core.Collections;
using Microsoft.EntityFrameworkCore;

namespace Hybrid.Data.Repositories
{
    public class NoteRepository : GenericRepository<Note>
    {
        private readonly CosmosDbContext context;

        public NoteRepository(CosmosDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<MethodResult<IEnumerable<Note>>> GetNotesByUserName(string userName)
        {
            try
            {
                var notes = await context.Notes.WithPartitionKey(userName).ToListAsync();
                return MethodResult<IEnumerable<Note>>.Success(notes);
            }
            catch (Exception ex)
            {
                return MethodResult<IEnumerable<Note>>.Fail(ex.Message);
            }
        }
    }
}
