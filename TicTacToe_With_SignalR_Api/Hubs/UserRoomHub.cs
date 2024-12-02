using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TicTacToe_With_SignalR.Data;
using TicTacToe_With_SignalR.Models;

namespace TicTacToe_With_SignalR_Api.Hubs
{
    public class UserRoomHub : Hub
    {

        private readonly AppDbContext _context;
        public UserRoomHub(AppDbContext context)
        {
            _context = context;
        }


        public async Task<string> InRoomUser(string roomId, string userId)
        {
            var room = _context.RoomUsers.FirstOrDefault(x=> x.RoomId == roomId && x.FirstPlayerUserId != null && x.FirstPlayerUserId != Context.ConnectionId );
            var roomResult = _context.RoomUsers.FirstOrDefault(x=> x.RoomId == roomId && x.FirstPlayerUserId != null && x.SecondPlayerUserId !=null );

            if (roomResult != null)
            {
                return "false";
            }

            if (room != null)
            {
                //connectionId coktrol et
                room.SecondPlayerUserId = userId;
                 _context.RoomUsers.Update(room);
                await Clients.All.SendAsync("InRoomUser", room);
            }
            else
            {
                RoomUser roomUser = new RoomUser();
                roomUser.RoomId = roomId;
                roomUser.FirstPlayerUserId = userId;

                var result = _context.RoomUsers.Add(roomUser);
                await Clients.All.SendAsync("InRoomUser", roomUser);
            }

            await _context.SaveChangesAsync();


            return "succes";

        }
    }

    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Identity?.Name; // Kullanıcı adını veya başka bir benzersiz kimliği döndürün
        }
    }
}
