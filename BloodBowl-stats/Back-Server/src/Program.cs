using BloodBowl_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;


namespace Back_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // Reading the database
            Console.Write("Reading the Database... ");
            Database.FillDatabase();
            Console.WriteLine("Database acquired !");

            // Want to do some test server-side ? do it here !
            // HERE HERE HERE
            // ... And now, no more testing

            /*
            InvitationCoach invitation = new InvitationCoach(Database.leagues[0], Database.coaches[0], Job.Player, Database.coaches[0]);
            Console.WriteLine(invitation.Serialize());
            */

            // Creating the server's console Interface
            Console.Write("Launching the server...");
            Server server = new Server(8976);
            server.Start();
        }
    }
}