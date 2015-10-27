using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace QLearning
{
    class TcpServer
    {
        private IPAddress ipAddr;
        private int port;

        public TcpServer(IPAddress ip, int mPort)
        {
            ipAddr = ip;
            port = mPort;
        }

        public void Listen()
        {
            
        }
    }
}
