using Application.Common.Interfaces;
using Application.Features.Clients.Dtos;
using AutoMapper;
using MediatR;

namespace Application.Features.Clients.Queries.GetAllClients
{
    public class GetAllClientsHandler : IRequestHandler<GetAllClientsQuery, List<ClientDto>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetAllClientsHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<List<ClientDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _clientRepository.GetAll();

            var clientsDto = _mapper.Map<List<ClientDto>>(clients);

            return clientsDto;
        }
    }
}