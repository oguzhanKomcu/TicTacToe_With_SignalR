using Microsoft.AspNetCore.SignalR;
using TicTacToe_With_SignalR.Data;
using TicTacToe_With_SignalR.Models;

namespace TicTacToe_With_SignalR.Hubs
{
    public class UserHub : Hub
    {
        private readonly AppDbContext _context;

        public UserHub(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> RegisterUser(string userName, string userId)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = userName,
                ConnectionId = userId
            };

            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Hata mesajını loglayın veya konsola yazdırın
                Console.WriteLine(ex.Message);
                // Daha fazla hata bilgisi için ex.InnerException'ı kontrol edin
            }

            return "succes";
        }
    }
}
