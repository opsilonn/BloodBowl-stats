using BloodBowl_Library;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace Back_Server
{
    public class Server
    {
        private int port;

        List<Receiver> receivers;
        private int cptCoachessLoggedIn;

        public Server(int port)
        {
            this.port = port;
        }



        public void Start()
        {
            Console.Write(" Server launched !\n\n");

            // We create and initialize a Listener
            TcpListener listener = new TcpListener(new IPAddress(new byte[] { 127, 0, 0, 1 }), port);
            listener.Start();


            cptCoachessLoggedIn = 0;
            receivers = new List<Receiver>();


            // Filling the database
            // Database.FillDatabase();


            while (true)
            {
                // Coucou, j'écoute si un Client se connecte
                TcpClient comm = listener.AcceptTcpClient();

                // Oh tiens, un client s'est connecté
                CONSOLE.WriteLine(ConsoleColor.DarkGreen, "Connection established @" + comm);

                
                // Je récupère ses infos...
                Receiver receiver = new Receiver(this, comm);

                // ... J'initialise quelques events...
                receiver.When_Server_Exit += EndingCommunication;
                receiver.When_Server_LogIn += LogIn;
                receiver.When_Server_LogOff += LogOut;

                receiver.When_Coach_Create += CoachCreate;
                receiver.When_Team_Create += TeamCreate;
                receiver.When_Player_Create += PlayerCreate;
                receiver.When_Player_Remove += PlayerRemove;
                receiver.When_Player_LevelsUp += PlayerLevelsUp;

                receiver.When_League_Create += LeagueCreate;
                receiver.When_League_InvitationCoach_Create += LeagueInvitationCoachCreate;
                receiver.When_League_InvitationCoach_Accept += LeagueInvitationCoachAccept;
                receiver.When_League_InvitationCoach_Refuse += LeagueInvitationCoachRefuse;


                // ... et je lance une connexion avec le serveur
                new Thread(receiver.DoOperation).Start();

                // Je garde la connexion en mémoire dans une Liste de Receiver
                receivers.Add(receiver);

                // On affiche les informations dans la console
                DisplaySituation();

                // Et hop, je retourne écouter si un Client se connecte...
            }
        }


        /// <summary>
        /// When a Coach logs in
        /// </summary>
        /// <param name="communication"> communication Logged in </param>
        private void LogIn(TcpClient communication)
        {
            // A client has logged in
            CONSOLE.WriteLine(ConsoleColor.Green, "Logged In @" + communication);

            cptCoachessLoggedIn++;

            DisplaySituation();
        }


        /// <summary>
        /// When a Client logs off
        /// </summary>
        /// <param name="communication"> communication Logged Off </param>
        private void LogOut(TcpClient communication)
        {
            // A client has logged off, but is still present
            CONSOLE.WriteLine(ConsoleColor.Red, "Logged Out @" + communication);

            cptCoachessLoggedIn--;

            DisplaySituation();
        }


        /// <summary>
        /// When a Client stops the communication with the server
        /// </summary>
        /// <param name="communication"></param>
        private void EndingCommunication(TcpClient communication)
        {
            // A client has disconnected the software
            CONSOLE.WriteLine(ConsoleColor.DarkRed, "Connection stopped @" + communication);

            // We remove the Receiver communication from our list
            foreach (Receiver receiver in receivers)
            {
                if (receiver.comm == communication)
                {
                    receivers.Remove(receiver);
                    break;
                }
            }

            DisplaySituation();
        }


        /// <summary>
        /// When a Client signs-in
        /// </summary>
        /// <param name="newCoach">Data of the newly created Coach</param>
        private void CoachCreate(CoachWithPassword newCoach)
        {
            // We convert the Coach (with a password) into a regular Coach
            Coach coach = new Coach(newCoach);

            // Adding the new objects to the database representation
            Database.coaches.Add(coach);

            // Creating an JSON file in the database
            Database.COACH.Write(newCoach);
        }
        private void TeamCreate(Team newTeam)
        {
            // Adding the new objects to the database representation
            Database.teams.Add(newTeam);

            // Creating a JSON file
            Database.TEAM.Write(newTeam);
        }
        private void PlayerCreate(Player newPlayer)
        {
            // Updating the JSON file
            Database.TEAM.Write(newPlayer.team);
        }
        private void PlayerRemove(Player playerRemoved)
        {
            // Updating the JSON file
            Database.TEAM.Write(playerRemoved.team);
        }
        private void PlayerLevelsUp(Player player)
        {
            // Updating the JSON file
            Database.TEAM.Write(player.team);
        }
        private void LeagueCreate(League league)
        {
            // Updating the JSON file
            Database.LEAGUE.Write(league);
        }
        private void LeagueInvitationCoachCreate(InvitationCoach ia)
        {
            // Updating the JSON file
            Database.LEAGUE.Write(ia.league);
        }
        private void LeagueInvitationCoachAccept(InvitationCoach ia)
        {
            // Updating the JSON file
            Database.LEAGUE.Write(ia.league);
        }
        private void LeagueInvitationCoachRefuse(InvitationCoach ia)
        {
            // Updating the JSON file
            Database.LEAGUE.Write(ia.league);
        }


        /// <summary>
        /// Display some data about the number of users, whether logged in just in the connection menu
        /// </summary>
        private void DisplaySituation()
        {
            CONSOLE.WriteLine(ConsoleColor.Magenta, "Number of clients Logged In / Number of clients Total : " + cptCoachessLoggedIn + "/ " + receivers.Count);
        }
    }
}
