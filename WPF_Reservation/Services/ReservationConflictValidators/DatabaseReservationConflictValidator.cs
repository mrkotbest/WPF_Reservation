using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WPF_Reservation.DbContexts;
using WPF_Reservation.DTOs;
using WPF_Reservation.Models;

namespace WPF_Reservation.Services.ReservationConflictValidators
{
    public class DatabaseReservationConflictValidator : IReservationConflictValidator
    {
        private readonly ReservationRoomDbContextFactory _dbContextFactory;

        public DatabaseReservationConflictValidator(ReservationRoomDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Reservation> GetConflictingReservation(Reservation reservation)
        {
            using (ReservationRoomDbContext context = _dbContextFactory.CreateDbContext())
            {
                ReservationDTO reservationDTO = await context.Reservations
                    .Where(r => r.FloorNumber == reservation.RoomId.FloorNumber)
                    .Where(r => r.RoomNumber == reservation.RoomId.RoomNumber)
                    .Where(r => r.EndDate > reservation.StartDate)
                    .Where(r => r.StartDate < reservation.EndDate)
                    .FirstOrDefaultAsync();

                if (reservationDTO == null) return null;

                return ToReservation(reservationDTO);
            };
        }

        private static Reservation ToReservation(ReservationDTO r)
        {
            return new Reservation(new RoomId(r.FloorNumber, r.RoomNumber), r.Username, r.StartDate, r.EndDate);
        }
    }
}