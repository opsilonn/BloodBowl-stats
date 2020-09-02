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
            // EffectStuff.GetAllEffects().ForEach(effect => Console.WriteLine(effect.isSkill()));
            Database.leagues.ForEach(league => Console.WriteLine(league));
            // ... And now, no more testing

            // Creating the server's console Interface
            Console.Write("Launching the server...");
            Server server = new Server(8976);
            server.Start();
        }
    }
}