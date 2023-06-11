using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_Reservation.Exceptions;
using WPF_Reservation.Models;

namespace WPF_Reservation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Hotel hotel = new Hotel("Only for adults");

            try
            {
                hotel.MakeReservation(new Reservation(new RoomId(1, 2),
                    "Gendalf Gray",
                    new DateTime(2000, 1, 1),
                    new DateTime(2000, 1, 2)));

                hotel.MakeReservation(new Reservation(new RoomId(1, 3),
                    "Gendalf Gray",
                    new DateTime(2000, 1, 3),
                    new DateTime(2000, 1, 4)));
            }
            catch (ReservationConflictException ex)
            {
                throw ex;
            }

            IEnumerable<Reservation> reservations = hotel.GetAllReservations();

            base.OnStartup(e);
        }
    }
}
 