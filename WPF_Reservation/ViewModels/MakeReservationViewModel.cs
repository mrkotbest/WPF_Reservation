using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class MakeReservationViewModel : ViewModelBase, INotifyDataErrorInfo
    {
		private string _username;
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

                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));

                if (EndDate < StartDate)
                {
                    AddError("The START DATE cannot be after the END DATE.", nameof(StartDate));
                }
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

                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));

                if (EndDate < StartDate)
                {
					AddError("The END DATE cannot be before the START DATE.", nameof(EndDate));
                }
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

		private readonly Dictionary<string, List<string>> _propertyNameToErrors;
        public bool HasErrors => _propertyNameToErrors.Any();
		public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public MakeReservationViewModel(HotelStore hotelStore, NavigationService reservationViewNavigationService)
        {
            SubmitCommand = new MakeReservationCommand(this, hotelStore, reservationViewNavigationService);
            CancelCommand = new NavigationCommand(reservationViewNavigationService);
            
			_propertyNameToErrors = new Dictionary<string, List<string>>();
        }

        public IEnumerable GetErrors(string propertyName)
        {
			return _propertyNameToErrors.GetValueOrDefault(propertyName, new List<string>());
        }
		private void AddError(string errorMessage, string propertyName)
		{
			if (!_propertyNameToErrors.ContainsKey(propertyName))
			{
				_propertyNameToErrors.Add(propertyName, new List<string>());
			}

			_propertyNameToErrors[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }

        private void ClearErrors(string propertyName)
        {
            _propertyNameToErrors.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }

		private void OnErrorsChanged(string propertyName)
		{
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}