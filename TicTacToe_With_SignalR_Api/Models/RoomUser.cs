namespace TicTacToe_With_SignalR.Models
{
    public class RoomUser
    {
        public int Id { get; set; }
        public string RoomId { get; set; }
        public Room Room { get; set; }  
        public string FirstPlayerUserId { get; set; }
        public string SecondPlayerUserId { get; set; }


    }
}
