using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Reservation.Models;

namespace WPF_Reservation.ViewModels
{
    public class ReservationListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ReservationViewModel> _reservations;

        public IEnumerable<ReservationViewModel> Reservations => _reservations;
        public ICommand MakeReservationCommand { get; }

        public ReservationListingViewModel()
        {
            _reservations = new ObservableCollection<ReservationViewModel>();
            _reservations.Add(new ReservationViewModel(new Reservation(new RoomId(1, 2), "SingletonSean", DateTime.Now, DateTime.Now.AddDays(1))));
            _reservations.Add(new ReservationViewModel(new Reservation(new RoomId(3, 2), "Rayan Gosling", DateTime.Now.AddDays(3), DateTime.Now.AddDays(5))));
            _reservations.Add(new ReservationViewModel(new Reservation(new RoomId(2, 4), "Pomella IdiNaxui", DateTime.Now.AddDays(2), DateTime.Now.AddDays(10))));
            
        }
    }
}
