using System;
using System.Collections.Generic;

namespace Passenger.Core.Domain
{
    public class Route
    {
        public string Name { get; protected set; }
        public Node Start { get; protected set; }
        public Node End { get; protected set; }
        public double Length { get; protected set; }

        protected Route()
        {
        }

        protected Route(string name, Node start, Node end, double length)
        {
            Name = name;
            Start = start;
            End = end;
            Length = length;
        }

        public static Route Create(string name, Node start, Node end, double length)
            => new Route(name, start, end, length);
    }
}