using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Reservation.Models
{
    class Hotel
    {
        private readonly ReservationBook _reservationBook;

        public string NameHotel { get; }

        public Hotel(string name)
        {
            NameHotel = name;
            _reservationBook = new ReservationBook();
        }

        /// <summary>
        /// Get the reservations for a user.
        /// </summary>
        /// <param name="username">A username of the user.</param>
        /// <returns>The reservations for the user.</returns>
        public IEnumerable<Reservation> GetReservationsForUser(string username)
        {
            return _reservationBook.GetReservationsForUser(username);
        }

        /// <summary>
        /// Make a reservation.
        /// </summary>
        /// <param name="reservation">The incoming reservation.</param>
        /// <exception cref="ReservationConflictException"></exception>
        public void MakeReservation(Reservation reservation)
        {
            _reservationBook.AddReservation(reservation);
        }
    }
}
