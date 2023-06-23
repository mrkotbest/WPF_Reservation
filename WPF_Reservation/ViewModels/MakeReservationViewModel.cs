using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using WPF_Reservation.Commands;
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

				ClearErrors(nameof(Username));

				if (!HasUsername)
				{
					AddError("Username cannot be empty.", nameof(Username));
				}

				OnPropertyChanged(nameof(CanCreateReservation));
			}
		}

		private int _floorNumber = 1;
		public int FloorNumber
		{
			get => _floorNumber;
			set
			{
				_floorNumber = value;
				OnPropertyChanged(nameof(FloorNumber));

				ClearErrors(nameof(FloorNumber));

				if (!HasFloorNumberGreaterThanZero)
				{
					AddError("Floor number must be greater than zero.", nameof(FloorNumber));
				}

                OnPropertyChanged(nameof(CanCreateReservation));
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

                if (!HasStartDateBeforeEndDate)
                {
                    AddError("The start date cannot be after the end date.", nameof(StartDate));
                }

				OnPropertyChanged(nameof(CanCreateReservation));
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

                if (!HasStartDateBeforeEndDate)
                {
					AddError("The end date cannot be before the start date.", nameof(EndDate));
                }

                OnPropertyChanged(nameof(CanCreateReservation));
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

		private readonly Dictionary<string, List<string>> _propertyNameToErrors;
        public bool HasErrors => _propertyNameToErrors.Any();
		public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

		public bool CanCreateReservation =>
			HasUsername &&
			HasFloorNumberGreaterThanZero &&
			HasStartDateBeforeEndDate &&
			!HasErrors;

		private bool HasUsername => !string.IsNullOrEmpty(Username);
		private bool HasFloorNumberGreaterThanZero => FloorNumber > 0;
		private bool HasStartDateBeforeEndDate => StartDate < EndDate;
		private bool HasSubmitErrorMessage => !string.IsNullOrEmpty(SubmitErrorMessage);

		private string _submitErrorMessage;
		public string SubmitErrorMessage
		{
			get => _submitErrorMessage;
			set
			{
				_submitErrorMessage = value;
				OnPropertyChanged(nameof(SubmitErrorMessage));
				OnPropertyChanged(nameof(HasSubmitErrorMessage));
			}
		}

		private bool _isSubmitting;
		public bool IsSubmitting
		{
			get => _isSubmitting;
			set
			{
				_isSubmitting = value;
				OnPropertyChanged(nameof(IsSubmitting));
			}
		}

		public MakeReservationViewModel(HotelStore hotelStore, NavigationService<ReservationListingViewModel> reservationViewNavigationService)
        {
            SubmitCommand = new MakeReservationCommand(this, hotelStore, reservationViewNavigationService);
            CancelCommand = new NavigationCommand<ReservationListingViewModel>(reservationViewNavigationService);
            
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