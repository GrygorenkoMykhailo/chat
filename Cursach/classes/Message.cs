using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursach.classes
{
    public class Message
    {
        public int Id { get; set; } 
        public string Content { get; set; }

        //chat fk
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
