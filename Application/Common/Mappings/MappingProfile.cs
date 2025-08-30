using Application.Features.Clients.Commands.CreateClient;
using Application.Features.Clients.Commands.UpdateClient;
using Application.Features.Clients.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Clients Mappings

            CreateMap<Client, ClientDto>();
            CreateMap<CreateClientCommand, Client>();
            CreateMap<UpdateClientCommand, Client>();

            #endregion
        }
    }
}