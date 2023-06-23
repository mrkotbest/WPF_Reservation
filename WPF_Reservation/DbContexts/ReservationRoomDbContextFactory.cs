using Microsoft.EntityFrameworkCore;

namespace WPF_Reservation.DbContexts
{
    public class ReservationRoomDbContextFactory
    {
        private readonly string? _connectionString;

        public ReservationRoomDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ReservationRoomDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().
                UseSqlite(_connectionString).Options;

            return new ReservationRoomDbContext(options);
        }
    }
}
