using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursach.classes
{
    public class BlackList
    {
        public int Id { get; set; }
        //blacklist owner fk
        public int UserId { get; set; } 
        public User user {  get; set; } 
        //blocked users
        public List<User> Users { get; set; }
    }
}
