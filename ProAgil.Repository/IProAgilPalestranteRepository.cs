using System.Collections.Generic;
using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilPalestranteRepository
    {
         Task<List<Palestrante>> GetAllPalestrante(string name, bool includeEventos);

         Task<Palestrante> GetPalestrante(int PalestranteId, bool includeEventos);
    }
}