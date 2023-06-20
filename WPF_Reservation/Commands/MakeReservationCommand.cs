using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Reservation.Exceptions;
using WPF_Reservation.Models;
using WPF_Reservation.Services;
using WPF_Reservation.Stores;
using WPF_Reservation.ViewModels;

namespace WPF_Reservation.Commands
{
    public class MakeReservationCommand : AsyncCommandBase
    {
        private readonly HotelStore _hotelStore;
        private readonly MakeReservationViewModel _makeReservationViewModel;
        private readonly NavigationService<ReservationListingViewModel> _reservationViewNavigationService;

        public MakeReservationCommand(MakeReservationViewModel makeReservationViewModel,
            HotelStore hotelStore,
            NavigationService<ReservationListingViewModel> reservationViewNavigationService)
        {
            _hotelStore = hotelStore;
            _makeReservationViewModel = makeReservationViewModel;

            _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;

            _reservationViewNavigationService = reservationViewNavigationService;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_makeReservationViewModel.Username) &&
                _makeReservationViewModel.FloorNumber > 0 &&
                base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Reservation reservation = new Reservation(
                new RoomId(_makeReservationViewModel.FloorNumber, _makeReservationViewModel.RoomNumber),
                _makeReservationViewModel.Username,
                _makeReservationViewModel.StartDate,
                _makeReservationViewModel.EndDate);

            try
            {
                await _hotelStore.MakeReservation(reservation);

                MessageBox.Show("Successfully reserved room.", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                _reservationViewNavigationService.Navigate();
            }
            catch (ReservationConflictException)
            {
                MessageBox.Show("This room is already taken.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to make reservation.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MakeReservationViewModel.Username)
                || e.PropertyName == nameof(MakeReservationViewModel.FloorNumber))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
