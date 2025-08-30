using Application.Features.Clients.Dtos;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Clients.Queries.GetAllClients
{
    public class GetAllClientsQuery : IRequest<List<ClientDto>>
    {

    }
}