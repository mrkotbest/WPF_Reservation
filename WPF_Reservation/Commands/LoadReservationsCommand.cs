using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Reservation.Models;
using WPF_Reservation.Stores;
using WPF_Reservation.ViewModels;

namespace WPF_Reservation.Commands
{
    public class LoadReservationsCommand : AsyncCommandBase
    {
        private readonly ReservationListingViewModel _reservationListingViewModel;
        private readonly HotelStore _hotelStore;

        public LoadReservationsCommand(ReservationListingViewModel reservationListingViewModel, HotelStore hotelStore)
        {
            _reservationListingViewModel = reservationListingViewModel;
            _hotelStore = hotelStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _hotelStore.Load();

                _reservationListingViewModel.UpdateReservations(_hotelStore.Reservations);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load reservations.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
