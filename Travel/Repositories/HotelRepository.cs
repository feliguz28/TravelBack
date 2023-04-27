using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using Travel.Dtos;
using Travel.Models;

namespace Travel.Repositories
{
    public interface IHotelRepository
    {
        public Hotel? GetHotelById(int hotelId);
        Task<PagerResponse<Hotel>> GetAllHotels(PagerRequest pager);
        Task<Hotel> CreateHotel(HotelCreateDto hotelCreateDto);
        Task<Hotel> UpdateHotel(HotelUpdateDto hotelUpdateDto);
        public void DeleteHotel(int hotelId, int userId);
        public void UpdateStatus(HotelUpdateStatusDto hotelUpdateStatusDto);
    }
    public class HotelRepository : IHotelRepository
    {
        private readonly ILogger<HotelRepository> _logger;
        private readonly SqlConnection _db;
        public HotelRepository(SqlConnection db, ILogger<HotelRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public Hotel? GetHotelById(int hotelId)
        {
            string sqlConsult = "GetHotelById";
            var parameters = new { HotelId = hotelId };
            var result = _db.QueryFirstOrDefault<Hotel>(sqlConsult, parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
        public async Task<PagerResponse<Hotel>> GetAllHotels(PagerRequest pager)
        {
            var results = new PagerResponse<Hotel>();
            var parametersCount = new { pager.filter };

            results.TotalCount = await _db.QuerySingleAsync<int>("GetCountAllHotels", parametersCount, commandType: CommandType.StoredProcedure);
            pager.pageNumber = pager.registerPage * pager.pageNumber - pager.registerPage;
            var parameters = new { pager.pageNumber, pager.registerPage, pager.filter };           
            results.Items = await _db.QueryAsync<Hotel>("GetAllHotels", parameters, commandType: CommandType.StoredProcedure);

            return results;
        }
        public async Task<Hotel> CreateHotel(HotelCreateDto hotelCreateDto)
        {
            string sqlConsult = "CreateHotel";
            var parameters = new
            {
                hotelCreateDto.HotelName,
                hotelCreateDto.HotelLocation,
                hotelCreateDto.Value,
                hotelCreateDto.UrlImage,
                hotelCreateDto.Status,
                hotelCreateDto.CreatedUser
            };
            var newHotel = _db.ExecuteScalar<int>(sqlConsult, parameters, commandType: CommandType.StoredProcedure);
            return GetHotelById(newHotel);
        }
        public async Task<Hotel> UpdateHotel(HotelUpdateDto hotelUpdateDto)
        {
            string sqlConsult = "UpdateHotel";
            var parameters = new
            {
                hotelUpdateDto.HotelId,
                hotelUpdateDto.HotelName,
                hotelUpdateDto.HotelLocation,
                hotelUpdateDto.Value,
                hotelUpdateDto.UrlImage,
                hotelUpdateDto.Status,
                hotelUpdateDto.UpdateUser
            };
            var updateHotel = _db.ExecuteScalar<int>(sqlConsult, parameters, commandType: CommandType.StoredProcedure);
            return GetHotelById(updateHotel);
        }
        public void  DeleteHotel(int hotelId, int userId)
        {
            string sqlConsult = "DeleteHotel";
            var parameters = new
            {
                hotelId,
                UpdateUser = userId
            };
            _db.ExecuteScalar<int>(sqlConsult, parameters, commandType: CommandType.StoredProcedure);
        }
        public void UpdateStatus(HotelUpdateStatusDto hotelUpdateStatusDto)
        {
            string sqlConsult = "UpdateHotelStatus";
            var parameters = new
            {
                hotelUpdateStatusDto.HotelId,
                hotelUpdateStatusDto.UpdateUser,
                hotelUpdateStatusDto.Status
            };
            _db.ExecuteScalar<int>(sqlConsult, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
