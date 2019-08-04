using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilEventoRepository
    {
        Task<Evento[]> GetAllEvento(bool includePalestrantes);

        Task<Evento[]> GetAllEventoByTema(string Tema, bool includePalestrantes);

        Task<Evento> GetEventoById(int EventoId, bool includePalestrantes);
    }
}