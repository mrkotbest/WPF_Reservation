﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WPF_Reservation.Models;

namespace WPF_Reservation.Services.ReservationProviders
{
    public interface IReservationProvider
    {
        Task<IEnumerable<Reservation>> GetAllReservations();
    }
}
