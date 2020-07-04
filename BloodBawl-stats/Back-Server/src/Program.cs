using System;


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

            // Creating the server's console Interface
            Console.Write("Launching the server...");
            Server server = new Server(8976);
            server.Start();
        }
    }
}