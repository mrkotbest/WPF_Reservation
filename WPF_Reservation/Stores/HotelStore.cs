using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Reservation.Models;

namespace WPF_Reservation.Stores
{
    public class HotelStore
    {
        private readonly Hotel _hotel;
        private readonly List<Reservation> _reservations;
        private Lazy<Task> _initLazy;

        public IEnumerable<Reservation> Reservations => _reservations;

        public event Action<Reservation> ReservationMade;

        public HotelStore(Hotel hotel)
        {
            _hotel = hotel;
            _initLazy = new Lazy<Task>(Initialize);
            _reservations = new List<Reservation>();
        }

        public async Task Load()
        {
            await _initLazy.Value;
        }

        public async Task MakeReservation(Reservation reservation)
        {
            await _hotel.MakeReservation(reservation);

            _reservations.Add(reservation);

            OnReservationMade(reservation);
        }

        private void OnReservationMade(Reservation reservation)
        {
            ReservationMade?.Invoke(reservation);
        }

        private async Task Initialize()
        {
            IEnumerable<Reservation> reservations = await _hotel.GetAllReservations();

            _reservations.Clear();
            _reservations.AddRange(reservations);
        }
    }
}
