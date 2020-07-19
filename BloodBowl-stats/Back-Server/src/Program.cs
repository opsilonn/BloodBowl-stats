using BloodBowl_Library;
using System;
using System.Collections.Generic;
using System.Linq;

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


            /*
            Team team = new Team();
            Player player = new Player();

            player.role = PlayerRole.HumanBlitzer;
            player.effects.Add(Effect.SkillDodge);

            team.players.Add(player);
            Console.WriteLine(team.Serialize());
            player.effectsAll.ForEach(effect => Console.WriteLine(effect.ToString()));
            */


            // Creating the server's console Interface
            Console.Write("Launching the server...");
            Server server = new Server(8976);
            server.Start();
        }
    }
}