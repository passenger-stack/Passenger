namespace Passenger.Infrastructure.Commands.Drivers
{
    public class DeleteDriverRoute : AuthenticatedCommandBase
    {
        public string Name { get; set; }
    }
}