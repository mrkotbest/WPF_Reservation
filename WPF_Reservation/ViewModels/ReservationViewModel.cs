using System;
using WPF_Reservation.Models;

namespace WPF_Reservation.ViewModels
{
    public class ReservationViewModel : ViewModelBase
    {
        private readonly Reservation _reservation;

        public string? RoomId => _reservation.RoomId?.ToString();
        public string? Username => _reservation.Username;
        public DateTime StartDate => _reservation.StartDate;
        public DateTime EndDate => _reservation.EndDate;

        public ReservationViewModel(Reservation reservation)
        {
            _reservation = reservation;
        }
    }
}
