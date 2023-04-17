using Travel.Dtos;
using Travel.Models;
using Travel.Repositories;

namespace Travel.Services
{
    public interface IHotelService
    {
        Task<Hotel> GetHotelById(int hotelId);
        Task<PagerResponse<Hotel>> GetAllHotels(PagerRequest pager);
        Task<Hotel> CreateHotel(HotelCreateDto hotelCreateDto);
        Task<Hotel> UpdateHotel(HotelUpdateDto hotelCreateDto);
        Task DeleteHotel(int hotelId, int userId);
        Task UpdateStatus(HotelUpdateStatusDto hotelUpdateStatusDto);

    }
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<Hotel> GetHotelById(int hotelId) => _hotelRepository.GetHotelById(hotelId);
        public async Task<PagerResponse<Hotel>> GetAllHotels(PagerRequest pager)
        {
            var hotels = await _hotelRepository.GetAllHotels(pager);
            return hotels;
        }
        public async Task<Hotel> CreateHotel(HotelCreateDto hotelCreateDto)=> await _hotelRepository.CreateHotel(hotelCreateDto);
        public async Task<Hotel> UpdateHotel(HotelUpdateDto hotelUpdateDto) => await _hotelRepository.UpdateHotel(hotelUpdateDto);

        public async Task DeleteHotel(int hotelId, int userId)
        {
            _hotelRepository.DeleteHotel(hotelId, userId);
        }
        public async Task UpdateStatus(HotelUpdateStatusDto hotelUpdateStatusDto)
        {
            _hotelRepository.UpdateStatus(hotelUpdateStatusDto);
        }
    }
}
