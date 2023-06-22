﻿using LoadingSpinnerControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    string connectionString = hostContext.Configuration.GetConnectionString("Default");

                    services.AddSingleton(new ReservationRoomDbContextFactory(connectionString));
                    services.AddSingleton<IReservationProvider, DatabaseReservationProvider>();
                    services.AddSingleton<IReservationCreator, DatabaseReservationCreator>();
                    services.AddSingleton<IReservationConflictValidator, DatabaseReservationConflictValidator>();

                    services.AddTransient<ReservationBook>();
                    services.AddSingleton((s) => new Hotel("For Adults", s.GetRequiredService<ReservationBook>()));

                    services.AddTransient((s) => CreateReservationListingViewModel(s));
                    services.AddSingleton<Func<ReservationListingViewModel>>((s) => () => s.GetRequiredService<ReservationListingViewModel>());
                    services.AddSingleton<NavigationService<ReservationListingViewModel>>();

                    services.AddTransient<MakeReservationViewModel>();   
                    services.AddSingleton<Func<MakeReservationViewModel>>((s) => () => s.GetRequiredService<MakeReservationViewModel>());
                    services.AddSingleton<NavigationService<MakeReservationViewModel>>();


                    services.AddSingleton<HotelStore>();
                    services.AddSingleton<NavigationStore>();

                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton(s => new MainWindow() { DataContext = s.GetRequiredService<MainViewModel>() });
                })
                .Build();
        }

        private ReservationListingViewModel CreateReservationListingViewModel(IServiceProvider services)
        {
            return ReservationListingViewModel.LoadViewModel(
                services.GetRequiredService<HotelStore>(),
                services.GetRequiredService<NavigationService<MakeReservationViewModel>>()
                );
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            ReservationRoomDbContextFactory reservationRoomDbContextFactory = _host.Services.GetRequiredService<ReservationRoomDbContextFactory>();

            using (ReservationRoomDbContext dbContext = reservationRoomDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            NavigationService<ReservationListingViewModel> navigationService = _host.Services.GetRequiredService<NavigationService<ReservationListingViewModel>>();
            navigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();

            base.OnExit(e);
        }
    }
}