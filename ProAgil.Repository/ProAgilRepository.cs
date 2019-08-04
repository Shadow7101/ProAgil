using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContexto _context;
        public ProAgilRepository(ProAgilContexto context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        #region | IProAgilRepository
        //---> IProAgilRepository
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        #endregion

        #region | IProAgilEventoRepository
        //--> IProAgilEventoRepository
        public async Task<Evento[]> GetAllEvento(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(x => x.Lotes)
                .Include(x => x.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                .Include(x => x.PalestranteEventos)
                .ThenInclude(x => x.Palestrante);
            }

            query = query.OrderByDescending(c => c.DataEvento);

            return await query.AsNoTracking().ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventoByTema(string Tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(x => x.Lotes)
                .Include(x => x.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                .Include(x => x.PalestranteEventos)
                .ThenInclude(x => x.Palestrante);
            }

            query = query
                .OrderByDescending(c => c.DataEvento)
                .Where(x => x.Tema.ToLower().Contains(Tema.ToLower()));

            return await query.AsNoTracking().ToArrayAsync();
        }

        public async Task<Evento> GetEventoById(int EventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(x => x.Lotes)
                .Include(x => x.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                .Include(x => x.PalestranteEventos)
                .ThenInclude(x => x.Palestrante);
            }

            query = query
                .OrderByDescending(c => c.DataEvento)
                .Where(x => x.Id == EventoId);

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }
        #endregion

        #region | IProAgilPalestranteRepository
        public async Task<List<Palestrante>> GetAllPalestrante(string name, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(x => x.RedesSociais);

            if (includeEventos)
            {
                query = query
                .Include(x => x.PalestranteEventos)
                .ThenInclude(x => x.Evento);
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Nome.ToLower().Contains(name.ToLower()));
            }

            query = query.OrderBy(c => c.Nome);



            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<Palestrante> GetPalestrante(int PalestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(x => x.RedesSociais);

            if (includeEventos)
            {
                query = query
                .Include(x => x.PalestranteEventos)
                .ThenInclude(x => x.Evento);
            }

            query = query
                .OrderBy(c => c.Nome)
                .Where(x => x.Id == PalestranteId);

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }
        #endregion
    }
}