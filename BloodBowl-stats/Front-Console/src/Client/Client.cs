using BloodBowl_Library;
using System;
using System.Collections.Generic;
using System.Net.Sockets;


namespace Front_Console
{
    public partial class Client
    {
        Coach userData;
        bool continuing;

        TcpClient comm;
        private string hostname;
        private int port;


        public Client(string h, int p)
        {
            hostname = h;
            port = p;
        }


        /// <summary>
        /// Starts the Client communication
        /// </summary>
        public void Start()
        {
            // Initializing some variables
            comm = new TcpClient(hostname, port);
            userData = new Coach();
            continuing = true;


            while (continuing)
            {
                // CONNECTION PANEL (LOGIN - SIGN-IN)
                PanelConnection();


                // CONNECTED PANEL (TOPIC - CHAT : see / add / delete)
                if (userData.IsComplete && continuing)
                {
                    PanelConnected();
                }


                // DISPLAY A GOODBYE MESSAGE
                if (!continuing)
                {
                    PanelExitSoftware();
                }
            }
        }
    }
}