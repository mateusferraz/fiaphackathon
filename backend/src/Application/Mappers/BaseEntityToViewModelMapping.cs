using Application.ViewMoldels;
using AutoMapper;
using Domain.Entidades;

namespace Application.Mappers
{
    public class BaseEntityToViewModelMapping : Profile
    {
        public BaseEntityToViewModelMapping()
        {
            CreateMap<Medico, MedicoViewModel>()
                .ForMember(to => to.Id, map => map.MapFrom(from => from.Id))
                .ForMember(to => to.Email, map => map.MapFrom(from => from.Nome))
                .ForMember(to => to.Documento, map => map.MapFrom(from => from.Documento))
                .ForMember(to => to.Crm, map => map.MapFrom(from => from.Crm))
                .ForMember(to => to.Nome, map => map.MapFrom(from => from.Nome))
                .ForMember(to => to.Agendas, map => map.MapFrom(from => from.Agendas))
                .ReverseMap();

            CreateMap<Agenda, AgendaViewModel>()
              .ForMember(to => to.Id, map => map.MapFrom(from => from.Id))
              .ForMember(to => to.Status, map => map.MapFrom(from => from.Status))
              .ForMember(to => to.DataAgendamento, map => map.MapFrom(from => from.DataAgendamento))
                .ReverseMap();
        }
    }
}
