using System;
using System.Runtime.Serialization;
using WPF_Reservation.Models;

namespace WPF_Reservation.Exceptions
{
    public class ReservationConflictException : Exception
    {
        public Reservation ExistingReservation { get; }
        public Reservation IncomingReservation { get; }

        public ReservationConflictException(Reservation existingReservation, Reservation incomingReservation)
        {
            ExistingReservation = existingReservation;
            IncomingReservation = incomingReservation;
        }

        public ReservationConflictException()
        {
        }

        public ReservationConflictException(string? message) : base(message)
        {
        }

        public ReservationConflictException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ReservationConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
