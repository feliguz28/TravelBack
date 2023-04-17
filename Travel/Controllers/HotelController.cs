using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel.Dtos;
using Travel.Models;
using Travel.Services;

namespace Travel.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {

        private readonly IHotelService _hotelService;

        public HotelController(
            IHotelService hotelService)
        {
            _hotelService = hotelService;
        }


        [HttpGet("/GetHotelById/{hotelId:int}")]
        public Task<Hotel> Get(int hotelId)=> _hotelService.GetHotelById(hotelId);

        [HttpGet("/GetAllHotelsMain")]
        public async Task<ActionResult<PagerResponse<Hotel>>> GetAllHotels([FromQuery] PagerRequest pager)
        {
            var hotels = await _hotelService.GetAllHotels(pager);
            return Ok(hotels);
        }

        [HttpPost("/CreateHotel")]
        public async Task<Hotel> CreateHotel(HotelCreateDto hotelCreateDto)
        {
            var createdHotel = await _hotelService.CreateHotel(hotelCreateDto);
            return createdHotel;         
        }

        [HttpPut("/UpdateHotel")]
        public async Task<Hotel> UpdateHotel(HotelUpdateDto hotelUpdateDto)
        {
            var result = await _hotelService.UpdateHotel(hotelUpdateDto);
            return result;
        }

        [HttpDelete("/DeleteHotel/{userId:int}/{hotelId:int}")]
        public ActionResult DeleteHotel(int hotelId, int userId)
        {
            _hotelService.DeleteHotel(hotelId, userId);
            return Ok();
        }

        [HttpPut("/UpdateHotelStatus")]
        public ActionResult UpdateHotelStatus(HotelUpdateStatusDto hotelUpdateStatusDto)
        {
            _hotelService.UpdateStatus(hotelUpdateStatusDto);
            return Ok();
        }

    }
}
