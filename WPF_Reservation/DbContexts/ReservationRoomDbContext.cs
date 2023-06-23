using Microsoft.EntityFrameworkCore;
using WPF_Reservation.DTOs;

namespace WPF_Reservation.DbContexts
{
    public class ReservationRoomDbContext : DbContext
    {
        public ReservationRoomDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ReservationDTO> Reservations { get; set; }
    }
}