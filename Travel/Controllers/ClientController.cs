using App.Services;
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
    public class ClientController : ControllerBase
    {

        private readonly IClientService _clientService;
        private readonly IEmailSender _emailSender;

        public ClientController(
            IClientService clientService,
            IEmailSender emailSender)
        {
            _clientService = clientService;
            _emailSender = emailSender;
        }

        [HttpGet("/GetAllClients/{reserveId:int}")]
        public async Task<ActionResult<PagerResponse<Client>>> GetAllClients(int reserveId)
        {
            var clients = await _clientService.GetAllClients(reserveId);
            return Ok(clients);
        }

        [HttpPost("/CreateClient")]
        public ActionResult CreateClient(ClientCreateDto clientCreateDto)
        {
            _clientService.CreateClient(clientCreateDto);
            return Ok();
        }
        [HttpPost("/CreateEmergencyContact")]
        public ActionResult CreateEmergencyContact(ContactEmergencyDto contactEmergencyDto)
        {
            _clientService.CreateEmergencyContact(contactEmergencyDto);
            return Ok();
        }
        [HttpPost("/SendEmail/{reserveId}/{email}")]
        public async Task<IActionResult> SendEmail(int reserveId, string email)
        {
            await _emailSender
                .SendEmailAsync(reserveId, email)
                .ConfigureAwait(false);

            return Ok();
        }
    }
}
