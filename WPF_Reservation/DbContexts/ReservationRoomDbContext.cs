using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Reservation.DTOs;
using WPF_Reservation.Models;

namespace WPF_Reservation.DbContexts
{
    public class ReservationRoomDbContext : DbContext
    {
        public ReservationRoomDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ReservationDTO> Reservations { get; set; }
    }
}