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


        public async Task<string> InRoomUser(string roomId)
        {
            var room = _context.RoomUsers.FirstOrDefault(x=> x.RoomId == roomId && x.FirstPlayerUserId != null );
            if (room != null)
            {
                room.SecondPlayerUserId = Context.ConnectionId;
                 _context.RoomUsers.Update(room);
                await Clients.All.SendAsync("InRoomUser", room);
            }
            else
            {
                RoomUser roomUser = new RoomUser();
                roomUser.RoomId = roomId;
                roomUser.FirstPlayerUserId = Context.ConnectionId;
                roomUser.SecondPlayerUserId = "";

                var result = _context.RoomUsers.Add(roomUser);
                await Clients.All.SendAsync("InRoomUser", roomUser);
            }

            await _context.SaveChangesAsync();


            return "succes";

        }
    }
}
