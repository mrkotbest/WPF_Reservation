using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
