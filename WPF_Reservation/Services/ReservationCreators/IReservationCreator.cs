using System.Threading.Tasks;
using WPF_Reservation.Models;

namespace WPF_Reservation.Services.ReservationCreators
{
    public interface IReservationCreator
    {
        Task CreateReservation(Reservation reservation);
    }
}
