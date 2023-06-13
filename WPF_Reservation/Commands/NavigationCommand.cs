using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Reservation.Stores;
using WPF_Reservation.ViewModels;
using WPF_Reservation.Models;
using WPF_Reservation.Services;

namespace WPF_Reservation.Commands
{
    public class NavigationCommand : CommandBase
    {
        private readonly NavigationService _navigationService;

        public NavigationCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            _navigationService.Navigate(); 
        }
    }
}