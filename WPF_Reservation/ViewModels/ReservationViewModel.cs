using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
