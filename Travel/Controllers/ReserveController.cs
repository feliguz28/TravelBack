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
    public class ReserveController : ControllerBase
    {

        private readonly IReserveService _reserveService;

        public ReserveController(
            IReserveService reserveService)
        {
            _reserveService = reserveService;
        }


        [HttpPost("/CreateReserve")]
        public async Task<int> CreateReserve(ReserveCreateDto reserveCreateDto)
        {
            var createdReserve = await _reserveService.CreateReserve(reserveCreateDto);
            return createdReserve;         
        }

        [HttpGet("/GetAllReserveMain")]
        public async Task<ActionResult<PagerResponse<Reserve>>> GetAllHotels([FromQuery] PagerRequest pager)
        {
            var reserve = await _reserveService.GetAllReserve(pager);
            return Ok(reserve);
        }
    }
}
