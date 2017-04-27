namespace Passenger.Infrastructure.Commands.Accounts
{
    public class Login : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}