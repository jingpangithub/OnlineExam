using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace OnlineExam.Codes
{
    public class MyHub : Hub
    {
        public void SendMessage(string name, string message)
        {
            Clients.All.receiveMessage(name, message);
        }
    }
}