using Travel.Dtos;
using Travel.Models;
using Travel.Repositories;

namespace Travel.Services
{
    public interface IReserveService
    {
        Task<int> CreateReserve(ReserveCreateDto reserveCreateDto);
        Task<PagerResponse<Reserve>> GetAllReserve(PagerRequest pager);
    }
    public class ReserveService : IReserveService
    {
        private readonly IReserveRepository _reserveRepository;
        public ReserveService(IReserveRepository reserveRepository)
        {
            _reserveRepository = reserveRepository;
        }

        public async Task<int> CreateReserve(ReserveCreateDto reserveCreateDto) => await _reserveRepository.CreateReserve(reserveCreateDto);
        public async Task<PagerResponse<Reserve>> GetAllReserve(PagerRequest pager)
        {
            var reserve = await _reserveRepository.GetAllReserve(pager);
            return reserve;
        }
    }
}
