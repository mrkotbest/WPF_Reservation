using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPF_Reservation.DbContexts;
using WPF_Reservation.DTOs;
using WPF_Reservation.Models;

namespace WPF_Reservation.Services.ReservationProviders
{
    public class DatabaseReservationProvider : IReservationProvider
    {
        private readonly ReservationRoomDbContextFactory _dbContextFactory;

        public DatabaseReservationProvider(ReservationRoomDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            using (ReservationRoomDbContext context = _dbContextFactory.CreateDbContext())
            {
                await Task.Delay(3000);
                
                IEnumerable<ReservationDTO> dto = await context.Reservations.ToListAsync();

                return dto.Select(r => ToReservation(r));
            }
        }

        private static Reservation ToReservation(ReservationDTO dto)
        {
            return new Reservation(new RoomId(dto.FloorNumber, dto.RoomNumber),
                dto.Username,
                dto.StartDate,
                dto.EndDate);
        }
    }
}
