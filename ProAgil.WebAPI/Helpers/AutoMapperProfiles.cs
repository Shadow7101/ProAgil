using System.Linq;
using AutoMapper;
using ProAgil.Domain;
using ProAgil.WebAPI.Dtos;

namespace ProAgil.WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Evento, EventoDTO>().ForMember(destnatario => destnatario.Palestrantes, opt =>
            {
                opt.MapFrom(src => src.PalestranteEventos.Select(x => x.Palestrante).ToList());
            }).ReverseMap();
            CreateMap<Palestrante, PalestranteDTO>().ForMember(destinatario => destinatario.Eventos, opt =>
            {
                opt.MapFrom(src => src.PalestranteEventos.Select(x => x.Evento).ToList());
            }).ReverseMap();
            CreateMap<Lote, LoteDTO>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDTO>().ReverseMap();
        }
    }
}