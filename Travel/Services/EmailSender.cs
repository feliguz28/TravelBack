using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Travel.Models;
using Travel.Repositories;

namespace App.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(int reserveId, string email);
    }
    public class EmailSender : IEmailSender
    {
        private readonly IReserveRepository _reserveRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IRoomRepository _roomRepository;
        public EmailSender(IReserveRepository reserveRepository, IClientRepository clientRepository, IRoomRepository roomRepository)
        {
            _reserveRepository = reserveRepository;
            _clientRepository = clientRepository;
            _roomRepository = roomRepository;
        }
        public async Task SendEmailAsync(int reserveId, string email)
        {
            EmailSenderOptions options = new EmailSenderOptions()
            {
                Port = 587,
                Email ="pruebacorreo9519@outlook.com",
                Password="Colombia2023*",
                EnableSsl = true,
                Host = "smtp.office365.com"
            };

            Reserve reserve = await _reserveRepository.GetReserveById(reserveId);
            EmergencyContact contact =  await _clientRepository.GetEmergencyContact(reserveId);
            Room room = _roomRepository.GetRoomById(reserve.RoomId);
            PagerResponse<Client> clients = await _clientRepository.GetAllClients(reserveId);
            var client = clients.Items.FirstOrDefault();


            string subject = "Gracias por su reservacion";
            string message = @$"<style>
                            .default{{background-color: rgb(109, 195, 252);
                                      border-radius: 5px;
                                      margin-top: 10px;
                                      font-family: Verdana, Geneva, Tahoma, sans-serif;}}
                            td{{text-align: center;
                                background-color: rgb(238, 255, 197);
                                border-radius: 4px;}}
                            th{{text-align: center;}}
                            </style>
                            <h1>Reserve Data</h1>
                            <table class=""default"">
                                <th>CheckIn</th>
                                <th>CheckOut</th>
                                <th>Number Person</th>
                                <th>Location</th>
                                <th>Room Name</th>
                                <th>Room Name</th>
                              <tr>
                                <td>{reserve.DateCheckIn}</td>
                                <td>{reserve.DateCheckOut}</td>
                                <td>{reserve.NumberPersons}</td>
                                <td>{reserve.Location}</td>
                                <td>{room.RoomName}</td>
                                <td>{reserve.Value}</td>
                              </tr>
                            </table>
                            <hr>
                            <h1>User Data</h1>
                            <table class=""default"">
                                <th>First Name</th>
                                <th>Last Name</th>
                                <th>Gender</th>
                                <th>Document</th>
                              <tr>
                                <td>{client.FirstName}</td>
                                <td>{client.LastName}</td>
                                <td>{client.Gender}</td>
                                <td>{client.Document}</td>
                              </tr>
                            </table>
                            <hr>
                            <h1>Contact Emergency</h1>
                            <table class=""default"">
                                <th>First Name</th>
                                <th>Last Name</th>
                                <th>ContactNumber</th>
                              <tr>
                                <td>{contact.FirstName}</td>
                                <td>{contact.LastName}</td>
                                <td>{contact.ContactNumber}</td>
                              </tr>
                            </table>";


            SmtpClient Cliente = new SmtpClient()
            {
                Host = options.Host,
                Port = options.Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(options.Email, options.Password),
                EnableSsl = options.EnableSsl,
            };

            var correo = new MailMessage(from: options.Email, to: email, subject: subject,message);
            correo.IsBodyHtml = true;
            var result = Cliente.SendMailAsync(correo);
            return ;
        }
    }
}