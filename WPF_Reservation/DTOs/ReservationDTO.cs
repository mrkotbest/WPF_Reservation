using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Reservation.DTOs
{
    public class ReservationDTO
    {
        [Key]
        public Guid Id { get; set; }
        public int FloorNumber { get; set; }
        public int RoomNumber { get; set; }
        public string Username { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
