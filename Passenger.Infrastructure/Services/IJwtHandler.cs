using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public interface IJwtHandler
    {
         JwtDto CreateToken(string email, string role);
    }
}