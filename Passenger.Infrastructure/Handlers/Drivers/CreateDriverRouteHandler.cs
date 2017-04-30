using System.Threading.Tasks;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Services;

namespace Passenger.Infrastructure.Handlers.Drivers
{
    public class CreateDriverRouteHandler : ICommandHandler<CreateDriverRoute>
    {
        private readonly IDriverRouteService _driverRouteService;

        public CreateDriverRouteHandler(IDriverRouteService driverRouteService)
        {
            _driverRouteService = driverRouteService;
        }

        public async Task HandleAsync(CreateDriverRoute command)
        {
            await _driverRouteService.AddAsync(command.UserId, command.Name,
                command.StartLatitude, command.StartLongitude, 
                command.EndLatitude, command.EndLongitude);
        }        
    }
}