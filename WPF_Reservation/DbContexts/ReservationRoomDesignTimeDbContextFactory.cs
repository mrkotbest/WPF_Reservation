using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WPF_Reservation.DbContexts
{
    public class ReservationRoomDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ReservationRoomDbContext>
    {
        public ReservationRoomDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().
                UseSqlite("Data Source=reservoom.db").Options;

            return new ReservationRoomDbContext(options);
        }
    }
}
