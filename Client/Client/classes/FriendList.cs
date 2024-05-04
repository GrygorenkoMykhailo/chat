using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Server.classes;

namespace Server.classes
{
    public class FriendList
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public User User { get; set; }
        public int FriendID { get; set; }   
        public User Friend { get; set; }
    }
}
