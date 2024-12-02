using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TicTacToe_With_SignalR.Models;

namespace TicTacToe_With_SignalR.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Room>  Rooms { get; set; }
        public DbSet<RoomUser>  RoomUsers { get; set; }
    }
}
