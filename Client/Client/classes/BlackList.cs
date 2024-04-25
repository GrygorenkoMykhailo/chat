using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.classes
{
    public class BlackList
    {
        public int Id { get; set; }
        public int BlockerId { get; set; }
        public User Blocker { get; set; }
        public int BlockedId { get; set; }
        public User Blocked { get; set; }
    }
}
