﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.classes
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string SenderUsername { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
