using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursach.classes
{
    //many to many between users
    public class BlackList
    {
        //who added the user to their blacklist
        public int BlockerId {  get; set; } 
        public User Blocker { get; set; }
        //who was added to blacklist
        public int BlockedId { get; set; }
        public User Blocked { get; set; }
    }
}
