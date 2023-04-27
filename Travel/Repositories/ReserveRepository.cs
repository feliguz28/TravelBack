using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using Travel.Dtos;
using Travel.Models;

namespace Travel.Repositories
{
    public interface IReserveRepository
    {
        Task<int> CreateReserve(ReserveCreateDto reserveCreateDto);
        Task<PagerResponse<Reserve>> GetAllReserve(PagerRequest pager);
        Task<Reserve> GetReserveById(int ReserveId);
    }
    public class ReserveRepository : IReserveRepository
    {
        private readonly ILogger<ReserveRepository> _logger;
        private readonly SqlConnection _db;
        public ReserveRepository(SqlConnection db, ILogger<ReserveRepository> logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task<PagerResponse<Reserve>> GetAllReserve(PagerRequest pager)
        {
            var results = new PagerResponse<Reserve>();
            var parametersCount = new { pager.filter };

            results.TotalCount = await _db.QuerySingleAsync<int>("GetCountAllReserve", parametersCount, commandType: CommandType.StoredProcedure);
            pager.pageNumber = pager.registerPage * pager.pageNumber - pager.registerPage;
            var parameters = new { pager.pageNumber, pager.registerPage, pager.filter };
            results.Items = await _db.QueryAsync<Reserve>("GetAllReserve", parameters, commandType: CommandType.StoredProcedure);

            return results;
        }

        public async Task<Reserve> GetReserveById(int reserveId)
        {
            string sqlConsult = "GetAllReserveById";
            var parameters = new { reserveId };
            var result = _db.QueryFirstOrDefault<Reserve>(sqlConsult, parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
        public async Task<int> CreateReserve(ReserveCreateDto reserveCreateDto)
        {
            string sqlConsult = "CreateReserve";
            var DateCheckIn = reserveCreateDto.DateCheckIn.Value.Date.ToString("yyyy/MM/dd");
            var DateCheckOut = reserveCreateDto.DateCheckOut.Value.Date.ToString("yyyy/MM/dd");
            var parameters = new
            {
                DateCheckIn,
                DateCheckOut,
                reserveCreateDto.NumberPerson,
                reserveCreateDto.Location,
                reserveCreateDto.RoomId,
                reserveCreateDto.Status,
                reserveCreateDto.Value,
                reserveCreateDto.CreatedUser
            };
            var ReserveId = _db.ExecuteScalar<int>(sqlConsult, parameters, commandType: CommandType.StoredProcedure);
            return ReserveId;
        }
        
    }
}
