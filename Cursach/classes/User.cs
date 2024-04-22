﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursach.classes
{
    public class User
    {
        public int Id { get; set; }

        //Unique
        public string Email { get; set; }

        public string Username {  get; set; }   
        public string Salt { get; set; }
        public string Hash { get; set; }
    }
}
