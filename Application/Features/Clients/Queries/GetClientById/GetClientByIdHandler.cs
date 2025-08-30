using Application.Common.Interfaces;
using Application.Features.Clients.Dtos;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Clients.Queries.GetClientById
{
    public class GetClientByIdHandler : IRequestHandler<GetClientByIdQuery, ClientDto?>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetClientByIdHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<ClientDto?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetById(request.Id);

            if (client == null)
            {
                return null;
            }

            var clientDto = _mapper.Map<ClientDto>(client);

            return clientDto;
        }
    }
}