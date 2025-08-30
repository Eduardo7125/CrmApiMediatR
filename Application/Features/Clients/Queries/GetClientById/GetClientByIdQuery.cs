using Application.Features.Clients.Dtos;
using MediatR;
using System;

namespace Application.Features.Clients.Queries.GetClientById
{
    public class GetClientByIdQuery : IRequest<ClientDto?>
    {
        public Guid Id { get; set; }
    }
}