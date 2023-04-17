using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using Travel.Dtos;
using Travel.Models;

namespace Travel.Repositories
{
    public interface IRoomRepository
    {
        public Room? GetRoomById(int roomId);
        Task<PagerResponse<Room>> GetAllRoomsByIdHotel(PagerRequest pager, int hotelId);
        Task<PagerResponse<RoomAvailable>> GetAllRoomsAvailable(PagerRequest pager, RoomAvailableDto roomAvailableDto);
        Task<Room> CreateRoom(RoomCreateDto roomCreateDto);
        Task<Room> UpdateRoom(RoomUpdateDto roomUpdateDto);
        public void DeleteRoom(int roomId, int userId);
        public void UpdateStatus(RoomUpdateStatusDto roomUpdateStatusDto);
    }
    public class RoomRepository : IRoomRepository
    {
        private readonly ILogger<RoomRepository> _logger;
        private readonly SqlConnection _db;
        public RoomRepository(SqlConnection db, ILogger<RoomRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public Room? GetRoomById(int roomId)
        {
            string sqlConsult = "GetRoomById";
            var parameters = new { RoomId = roomId };
            var result = _db.QueryFirstOrDefault<Room>(sqlConsult, parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
        public async Task<PagerResponse<Room>> GetAllRoomsByIdHotel(PagerRequest pager, int hotelId)
        {
            var results = new PagerResponse<Room>();
            var parametersCount = new { pager.filter, hotelId };

            results.TotalCount = await _db.QuerySingleAsync<int>("GetCountAllRoomsByIdHotel", parametersCount, commandType: CommandType.StoredProcedure);
            pager.pageNumber = pager.registerPage * pager.pageNumber - pager.registerPage;
            var parameters = new { hotelId, pager.pageNumber, pager.registerPage, pager.filter };           
            results.Items = await _db.QueryAsync<Room>("GetAllRoomsByIdHotel", parameters, commandType: CommandType.StoredProcedure);

            return results;
        }

        public async Task<PagerResponse<RoomAvailable>> GetAllRoomsAvailable(PagerRequest pager, RoomAvailableDto roomAvailableDto)
        {
            var results = new PagerResponse<RoomAvailable>();
            var parametersCount = new {  roomAvailableDto.DateCheckIn
                                        ,roomAvailableDto.DateCheckOut
                                        ,roomAvailableDto.Location
                                        ,roomAvailableDto.NumberPerson
                                        ,pager.filter };

            results.TotalCount = await _db.QuerySingleAsync<int>("GetCountAllAvailableRooms", parametersCount, commandType: CommandType.StoredProcedure);
            pager.pageNumber = pager.registerPage * pager.pageNumber - pager.registerPage;
            var DateCheckIn = roomAvailableDto.DateCheckIn.Value.Date.ToString("yyyy/MM/dd");
            var DateCheckOut = roomAvailableDto.DateCheckOut.Value.Date.ToString("yyyy/MM/dd");
            var parameters = new {   DateCheckIn
                                    ,DateCheckOut
                                    ,roomAvailableDto.Location
                                    ,roomAvailableDto.NumberPerson
                                    ,pager.pageNumber
                                    ,pager.registerPage
                                    ,pager.filter };
            results.Items = await _db.QueryAsync<RoomAvailable>("GetAllAvailableRooms", parameters, commandType: CommandType.StoredProcedure);

            return results;
        }
        public async Task<Room> CreateRoom(RoomCreateDto roomCreateDto)
        {
            string sqlConsult = "CreateRoom";
            var parameters = new
            {
                roomCreateDto.RoomName,
                roomCreateDto.RoomDescription,
                roomCreateDto.Status,
                roomCreateDto.Value,
                roomCreateDto.HotelId,
                roomCreateDto.RoomTypeId,
                roomCreateDto.CreatedUser,
                roomCreateDto.Location,
                roomCreateDto.UrlImage
            };
            var newRoom = _db.ExecuteScalar<int>(sqlConsult, parameters, commandType: CommandType.StoredProcedure);
            return GetRoomById(newRoom);
        }
        public async Task<Room> UpdateRoom(RoomUpdateDto roomUpdateDto)
        {
            string sqlConsult = "UpdateRoom";
            var parameters = new
            {
                roomUpdateDto.RoomId,
                roomUpdateDto.RoomName,
                roomUpdateDto.RoomDescription,
                roomUpdateDto.Status,
                roomUpdateDto.Value,
                roomUpdateDto.HotelId,
                roomUpdateDto.RoomTypeId,
                roomUpdateDto.UpdateUser,
                roomUpdateDto.Location,
                roomUpdateDto.UrlImage
            };
            var updateRoom = _db.ExecuteScalar<int>(sqlConsult, parameters, commandType: CommandType.StoredProcedure);
            return GetRoomById(updateRoom);
        }
        public void  DeleteRoom(int roomId, int userId)
        {
            string sqlConsult = "DeleteRoom";
            var parameters = new
            {
                roomId,
                UpdateUser = userId
            };
            _db.ExecuteScalar<int>(sqlConsult, parameters, commandType: CommandType.StoredProcedure);
        }
        public void UpdateStatus(RoomUpdateStatusDto roomUpdateStatusDto)
        {
            string sqlConsult = "UpdateRoomStatus";
            var parameters = new
            {
                roomUpdateStatusDto.RoomId,
                roomUpdateStatusDto.UpdateUser,
                roomUpdateStatusDto.Status
            };
            _db.ExecuteScalar<int>(sqlConsult, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
