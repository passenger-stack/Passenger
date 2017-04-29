using System;
using System.Threading.Tasks;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.Services;

namespace Passenger.Infrastructure.Handlers.Users
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IUserService _userService;
        
        public CreateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(CreateUser command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), command.Email, 
                command.Username, command.Password, command.Role);
        }
    }
}