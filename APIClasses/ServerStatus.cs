﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClasses
{
    public class ServerStatus
    {
        public string Status;
        public string Reason;

        public ServerStatus()
        {
            Status = "Denied";
            Reason = "Authentication error";
        }
    }
}
