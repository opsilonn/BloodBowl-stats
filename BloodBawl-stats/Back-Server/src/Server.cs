using BloodBawl_Library;
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
                Receiver receiver = new Receiver(comm);

                // ... J'initialise quelques events...
                receiver.When_Server_Exit += EndingCommunication;
                receiver.When_Server_LogIn += LogIn;
                receiver.When_Server_LogOff += LogOut;

                receiver.When_Coach_Create += CoachCreate;
                /*
                receiver.When_Topic_Create += TopicCreate;
                receiver.When_Chat_Create += ChatCreate;
                receiver.When_Message_Create += MessageCreate;

                receiver.When_Member_Join += MemberJoin;
                receiver.When_Member_Leave += MemberLeave;
                */

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


        /*
        private void TopicCreate(Topic newTopic)
        {
            // Adding the new objects to the database representation
            Database.topics.Add(newTopic);

            // Creating an XML file
            Database.TOPIC.Write(newTopic);
        }
        private void ChatCreate(Chat newChat)
        {
            // Adding the new objects to the database representation
            Database.chats.Add(newChat);

            // Creating an XML file
            Database.CHAT.Write(newChat);
        }
        private void MessageCreate(MessageCreation messageCreation)
        {
            // We initialize some variables to clarify the code
            Structure structureHosting = messageCreation.structure;
            Message newMessage = messageCreation.message;

            // Adding the new objects to the database representation (new Message + update the Structure's list of Message's ID)
            structureHosting.ID_messages.Add(newMessage.ID);
            Database.messages.Add(newMessage);

            // Updating the XML files
            Database.STRUCTURE.Write(structureHosting);
            Database.MESSAGE.Write(newMessage);
        }
        private void MemberJoin(Structure structureHostingNewMember, Profile newMember)
        {
            // Adding the new objects to the database representation (adding the Profile's ID to the Structure)
            structureHostingNewMember.ID_members.Add(newMember.ID);

            // Updating the XML files
            Database.STRUCTURE.Write(structureHostingNewMember);
        }
        private void MemberLeave(Structure structureNoLongerHostingMember, Profile oldMember)
        {
            // If we remove the last member of a Chat (count = 1 last member) : we delete it
            if (structureNoLongerHostingMember is Chat && structureNoLongerHostingMember.numberOfMembers == 1)
            {
                // Removing the Chat from the database representation
                Database.chats.Remove((Chat)structureNoLongerHostingMember);

                // Deleting the xml file
                Database.CHAT.Delete((Chat)structureNoLongerHostingMember);
            }
            else
            {
                // Removing the Profile's ID from the database representation (removing the Profile's ID of the Structure)
                structureNoLongerHostingMember.ID_members.Remove(oldMember.ID);

                // Updating the xml file
                Database.STRUCTURE.Write(structureNoLongerHostingMember);
            }
        }
        */


        /// <summary>
        /// Display some data about the number of users, whether logged in just in the connection menu
        /// </summary>
        private void DisplaySituation()
        {
            CONSOLE.WriteLine(ConsoleColor.Magenta, "Number of clients Logged In / Number of clients Total : " + cptCoachessLoggedIn + "/ " + receivers.Count);
        }
    }
}
