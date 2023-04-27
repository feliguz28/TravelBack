using Travel.Dtos;
using Travel.Models;
using Travel.Repositories;

namespace Travel.Services
{
    public interface IClientService
    {
        public void CreateClient(ClientCreateDto clientCreateDto);
        public void CreateEmergencyContact(ContactEmergencyDto contactEmergencyDto);
        Task<PagerResponse<Client>> GetAllClients(int reserveId);
    }
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public void CreateClient(ClientCreateDto clientCreateDto) => _clientRepository.CreateClient(clientCreateDto);
        public void CreateEmergencyContact(ContactEmergencyDto contactEmergencyDto) => _clientRepository.CreateEmergencyContact(contactEmergencyDto);

        public async Task<PagerResponse<Client>> GetAllClients(int reserveId)
        {
            var clients = await _clientRepository.GetAllClients(reserveId);
            return clients;
        }
    }
}
