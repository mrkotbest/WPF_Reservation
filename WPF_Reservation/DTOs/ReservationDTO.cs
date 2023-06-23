﻿using System;
using System.ComponentModel.DataAnnotations;

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
