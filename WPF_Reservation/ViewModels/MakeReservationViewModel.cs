using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Reservation.Commands;
using WPF_Reservation.Models;
using WPF_Reservation.Services;
using WPF_Reservation.Stores;

namespace WPF_Reservation.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase
    {
		private string? _username;
		public string Username
		{
			get => _username;
			set
			{
				_username = value;
				OnPropertyChanged(nameof(Username));
			}
		}

		private int _floorNumber;
		public int FloorNumber
		{
			get => _floorNumber;
			set
			{
				_floorNumber = value;
				OnPropertyChanged(nameof(FloorNumber));
			}
		}

		private int _roomNumber;
		public int RoomNumber
		{
			get => _roomNumber;

			set
			{
				_roomNumber = value;
				OnPropertyChanged(nameof(RoomNumber));
			}
		}

		private DateTime _startDate = new DateTime(2023, 6, 1);
		public DateTime  StartDate
		{
			get => _startDate;
			set
			{
				_startDate = value;
				OnPropertyChanged(nameof(StartDate));
			}
		}

        private DateTime _endDate = new DateTime(2023, 6, 30);
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

		public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public MakeReservationViewModel(Hotel hotel, NavigationService reservationViewNavigationService)
        {
			SubmitCommand = new MakeReservationCommand(this, hotel, reservationViewNavigationService);
			CancelCommand = new NavigationCommand(reservationViewNavigationService);
        }
    }
}