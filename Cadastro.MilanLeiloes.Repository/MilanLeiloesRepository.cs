using System.Threading.Tasks;

namespace Cadastro.MilanLeiloes.Repository
{
    public class MilanLeiloesRepository : IMilanLeiloesRepository
    {
        public readonly ApplicationDbContext _context;

        public MilanLeiloesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
