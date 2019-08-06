using System.Threading.Tasks;

namespace Cadastro.MilanLeiloes.Repository
{
    public interface IMilanLeiloesRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Task<bool> SaveChangesAsync();


    }
}
