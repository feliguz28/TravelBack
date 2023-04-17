using Travel.Dtos;
using Travel.Models;
using Travel.Repositories;

namespace Travel.Services
{
    public interface IRoomService
    {
        Task<Room> GetRoomById(int roomId);
        Task<PagerResponse<Room>> GetAllRoomsByIdHotel(PagerRequest pager, int hotelId);
        Task<PagerResponse<RoomAvailable>> GetAllRoomsAvailable(PagerRequest pager, RoomAvailableDto roomAvailableDto);
        Task<Room> CreateRoom(RoomCreateDto roomCreateDto);
        Task<Room> UpdateRoom(RoomUpdateDto roomCreateDto);
        Task DeleteRoom(int roomId, int userId);
        Task UpdateStatus(RoomUpdateStatusDto roomUpdateStatusDto);

    }
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<Room> GetRoomById(int roomId) => _roomRepository.GetRoomById(roomId);
        public async Task<PagerResponse<Room>> GetAllRoomsByIdHotel(PagerRequest pager, int hotelId)
        {
            var rooms = await _roomRepository.GetAllRoomsByIdHotel(pager, hotelId);
            return rooms;
        }
        public async Task<PagerResponse<RoomAvailable>> GetAllRoomsAvailable(PagerRequest pager, RoomAvailableDto roomAvailableDto)
        {
            var rooms = await _roomRepository.GetAllRoomsAvailable(pager, roomAvailableDto);
            return rooms;
        }
        public async Task<Room> CreateRoom(RoomCreateDto roomCreateDto)=> await _roomRepository.CreateRoom(roomCreateDto);
        public async Task<Room> UpdateRoom(RoomUpdateDto roomUpdateDto) => await _roomRepository.UpdateRoom(roomUpdateDto);

        public async Task DeleteRoom(int roomId, int userId)
        {
            _roomRepository.DeleteRoom(roomId, userId);
        }
        public async Task UpdateStatus(RoomUpdateStatusDto roomUpdateStatusDto)
        {
            _roomRepository.UpdateStatus(roomUpdateStatusDto);
        }
    }
}
