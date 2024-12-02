using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TicTacToe_With_SignalR.Data;
using TicTacToe_With_SignalR.Models;

namespace TicTacToe_With_SignalR_Api.Hubs
{
    public class RoomHub :Hub
    {
        private readonly AppDbContext _context;

        public RoomHub(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Room>> RoomList() 
        {

           var list = await _context.Rooms.ToListAsync();

            return list;
        }
        public async Task<string> CreateRoom(string roomName) 
        {
            Room room = new Room();
            room.Name = roomName;
            room.CreatedUserId = Context.ConnectionId;
            room.Id = Guid.NewGuid().ToString();

            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("CreateRoom", room);
            return "succes";
        }
        public async Task<List<RoomUser>> RoomUserList(string roomId)
        {
            var roomList = await _context.RoomUsers.Where(x => x.RoomId == roomId).ToListAsync();

            return roomList ?? new List<RoomUser>();
        }

    }
}
