using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_Reservation.DbContexts;
using WPF_Reservation.Exceptions;
using WPF_Reservation.Models;
using WPF_Reservation.Services;
using WPF_Reservation.Services.ReservationConflictValidators;
using WPF_Reservation.Services.ReservationCreators;
using WPF_Reservation.Services.ReservationProviders;
using WPF_Reservation.Stores;
using WPF_Reservation.ViewModels;
using WPF_Reservation.Views;

namespace WPF_Reservation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Data Source=reservoom.db";
        private readonly Hotel _hotel;
        private readonly NavigationStore _navigationStore;
        private readonly ReservationRoomDbContextFactory _reservationRoomDbContextFactory;

        public App()
        {
            _reservationRoomDbContextFactory = new(CONNECTION_STRING);
            IReservationProvider reservationProvider = new DatabaseReservationProvider(_reservationRoomDbContextFactory);
            IReservationCreator reservationCreator = new DatabaseReservationCreator(_reservationRoomDbContextFactory);
            IReservationConflictValidator reservationConflictValidator = new DatabaseReservationConflictValidator(_reservationRoomDbContextFactory);

            ReservationBook reservationBook = new ReservationBook(reservationProvider, reservationCreator, reservationConflictValidator);
            
            _hotel = new Hotel("For Adults", reservationBook);
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            /*Hotel hotel = new Hotel("Only for adults");

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

            IEnumerable<Reservation> reservations = hotel.GetAllReservations();*/
            
            using (ReservationRoomDbContext dbContext = _reservationRoomDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }
            
            _navigationStore.CurrentViewModel = CreateReservationListingViewModel();

            MainWindow mainWindow = new() { DataContext = new MainViewModel(_navigationStore) };
            mainWindow.Show();

            base.OnStartup(e);
        }
        private MakeReservationViewModel CreateMakeReservationViewModel()
        {
            return new MakeReservationViewModel(_hotel, new NavigationService(_navigationStore, CreateReservationListingViewModel));
        }

        private ReservationListingViewModel CreateReservationListingViewModel()
        {
            return ReservationListingViewModel.LoadViewModel(_hotel, new NavigationService(_navigationStore, CreateMakeReservationViewModel));
        }
    }
}