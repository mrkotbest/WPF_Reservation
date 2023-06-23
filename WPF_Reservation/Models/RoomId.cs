using System;

namespace WPF_Reservation.Models
{
    public class RoomId
    {
        public int FloorNumber { get; }
        public int RoomNumber { get; }

        public RoomId(int floorNumber, int roomNumber)
        {
            FloorNumber = floorNumber;
            RoomNumber = roomNumber;
        }

        public override string ToString()
        {
            return $"{FloorNumber} / {RoomNumber}";
        }

        public override bool Equals(object? obj)
        {
            return obj is RoomId roomId
                && FloorNumber == roomId.FloorNumber
                && RoomNumber == roomId.RoomNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FloorNumber, RoomNumber);
        }

        public static bool operator ==(RoomId roomId1, RoomId roomId2)
        {
            if (roomId1 is null && roomId2 is null)
                return true;

            return !(roomId1 is null) && roomId1.Equals(roomId2);
        }

        public static bool operator !=(RoomId roomId1, RoomId roomId2)
        {
            return !(roomId1 == roomId2);
        }
    }
}
