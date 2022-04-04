using ExempleEFCore.Data;

namespace GenericRepositoryTest.Repositories
{
    internal class BaseRepository
    {
        protected readonly ApplicationDbContext _db;
        public BaseRepository(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }
    }
}
