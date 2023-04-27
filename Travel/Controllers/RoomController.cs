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
    public class RoomController : ControllerBase
    {

        private readonly IRoomService _roomService;

        public RoomController(
            IRoomService roomService)
        {
            _roomService = roomService;
        }


        [HttpGet("/GetRoomById/{roomId:int}")]
        public Task<Room> Get(int roomId) => _roomService.GetRoomById(roomId);

        [HttpGet("/GetAllRoomsByIdHotel/{hotelId:int}")]
        public async Task<ActionResult<PagerResponse<Room>>> GetAllRoomsByIdHotel([FromQuery] PagerRequest pager, int hotelId)
        {
            var rooms = await _roomService.GetAllRoomsByIdHotel(pager, hotelId);
            return Ok(rooms);
        }

        [HttpGet("/GetAllRoomsAvailable/{DateCheckIn}/{DateCheckOut}/{Location}/{NumberPerson}")]
        public async Task<ActionResult<PagerResponse<RoomAvailable>>> GetAllRoomsAvailable( DateTime DateCheckIn, DateTime DateCheckOut, string Location, int NumberPerson, [FromQuery] PagerRequest pager)
        {
            RoomAvailableDto roomAvailableDto = new RoomAvailableDto();
            roomAvailableDto.DateCheckIn = DateCheckIn;
            roomAvailableDto.DateCheckOut = DateCheckOut;
            roomAvailableDto.Location = Location;
            roomAvailableDto.NumberPerson = NumberPerson;
            var rooms = await _roomService.GetAllRoomsAvailable(pager, roomAvailableDto);
            return Ok(rooms);
        }

        [HttpPost("/CreateRoom")]
        public async Task<Room> CreateRoom(RoomCreateDto roomCreateDto)
        {
            var createdRoom = await _roomService.CreateRoom(roomCreateDto);
            return createdRoom;         
        }

        [HttpPut("/UpdateRoom")]
        public async Task<Room> UpdateRoom(RoomUpdateDto roomUpdateDto)
        {
            var result = await _roomService.UpdateRoom(roomUpdateDto);
            return result;
        }

        [HttpDelete("/DeleteRoom/{userId:int}/{roomId:int}")]
        public ActionResult DeleteRoom(int roomId, int userId)
        {
            _roomService.DeleteRoom(roomId, userId);
            return Ok();
        }

        [HttpPut("/UpdateRoomStatus")]
        public ActionResult UpdateRoomStatus(RoomUpdateStatusDto roomUpdateStatusDto)
        {
            _roomService.UpdateStatus(roomUpdateStatusDto);
            return Ok();
        }

    }
}
