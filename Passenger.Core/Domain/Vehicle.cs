using System;

namespace Passenger.Core.Domain
{
    public class Vehicle
    {
        public string Brand { get; protected set; }
        public string Name { get; protected set; }
        public int Seats { get; protected set; }

        public Vehicle (string brand, string name, int seats) 
        {
            SetBrand(brand);
            SetName(name);
            SetSeats(seats);
        }

        public void SetBrand(string brand)
        {
            if (string.IsNullOrWhiteSpace(brand))
            {
                throw new Exception("Please provide valid data.");
            }
            if (Brand == brand)
            {
                return;
            }
            Brand = brand;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Please provide valid data.");
            }
            if (Name == name)
            {
                return;
            }
            Name = name;
        }

        public void SetSeats(int seats)
        {
           
            if (seats < 0) 
            {
                throw new Exception("Seats must be greater than 0.");
            }

            if (seats > 9) 
            {
                throw new Exception("You can not provide more than 9 seats");
            }

            if (Seats == seats)
            {
                return;
            }
            Seats = seats;
        }
    }

   
}