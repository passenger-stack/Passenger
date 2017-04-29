using System;
using System.Collections.Generic;

namespace Passenger.Core.Domain
{
    public class Route
    {
        public string Name { get; protected set; }
        public Node Start { get; protected set; }
        public Node End { get; protected set; }

        protected Route()
        {
        }

        protected Route(string name, Node start, Node end)
        {
            Name = name;
            Start = start;
            End = end;
        }

        public static Route Create(string name, Node start, Node end)
            => new Route(name, start, end);
    }
}