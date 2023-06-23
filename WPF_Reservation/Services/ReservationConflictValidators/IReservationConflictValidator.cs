using System.Threading.Tasks;
using WPF_Reservation.Models;

namespace WPF_Reservation.Services.ReservationConflictValidators
{
    public interface IReservationConflictValidator
    {
        Task<Reservation> GetConflictingReservation(Reservation reservation);
    }
}
