using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using Travel.Dtos;
using Travel.Models;

namespace Travel.Repositories
{
    public interface IClientRepository
    {
        public void CreateClient(ClientCreateDto clientCreateDto);
        public void CreateEmergencyContact(ContactEmergencyDto contactEmergencyDto);
        Task<PagerResponse<Client>> GetAllClients(int reserveId);
        Task<EmergencyContact> GetEmergencyContact(int reserveId);
    }
    public class ClientRepository : IClientRepository
    {
        private readonly ILogger<ClientRepository> _logger;
        private readonly SqlConnection _db;
        public ClientRepository(SqlConnection db, ILogger<ClientRepository> logger)
        {
            _db = db;
            _logger = logger;
        }
        public void CreateClient(ClientCreateDto clientCreateDto)
        {
            string sqlConsult = "CreateClient";
            var parameters = new
            {
                clientCreateDto.FirstName,
                clientCreateDto.LastName,
                clientCreateDto.Gender,
                clientCreateDto.DocumentTypeId,
                clientCreateDto.Document,
                clientCreateDto.Email,
                clientCreateDto.ReserveId
            };
            _db.ExecuteScalar<int>(sqlConsult, parameters, commandType: CommandType.StoredProcedure);
        }
        public void CreateEmergencyContact(ContactEmergencyDto contactEmergencyDto)
        {
            string sqlConsult = "CreateEmergencyContact";
            var parameters = new
            {
                contactEmergencyDto.FirstName,
                contactEmergencyDto.LastName,
                contactEmergencyDto.ContactNumber,
                contactEmergencyDto.ReserveId
            };
            _db.ExecuteScalar<int>(sqlConsult, parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<PagerResponse<Client>> GetAllClients( int reserveId)
        {
            var results = new PagerResponse<Client>();
            var parameters = new { reserveId };
            results.Items = await _db.QueryAsync<Client>("GetAllClients", parameters, commandType: CommandType.StoredProcedure);

            return results;
        }

        public async Task<EmergencyContact> GetEmergencyContact(int reserveId)
        {
            string sqlConsult = "GetEmergencyContact";
            var parameters = new { reserveId };
            var result = _db.QueryFirstOrDefault<EmergencyContact>(sqlConsult, parameters, commandType: CommandType.StoredProcedure);

            return result;
        }

    }
}
