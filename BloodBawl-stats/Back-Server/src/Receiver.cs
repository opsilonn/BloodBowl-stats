using BloodBawl_Library;
using System;
using System.Collections.Generic;
using System.Net.Sockets;


namespace Back_Server
{
    public class Receiver
    {
        public TcpClient comm;

        // Creating a Delegate called CommunicationWithTheServer, and an event using it
        public delegate void CommunicationWithTheServer(TcpClient comm);
        public event CommunicationWithTheServer When_Server_Exit;
        public event CommunicationWithTheServer When_Server_LogIn;
        public event CommunicationWithTheServer When_Server_LogOff;

        // Creating a Delegate about creating some structures, and events using them
        public delegate void CreatingCoach(CoachWithPassword newCoach);
        public event CreatingCoach When_Coach_Create;




        /// <summary>
        /// Constructor of the Class
        /// </summary>
        /// <param name="s"></param>
        public Receiver(TcpClient s)
        {
            comm = s;
        }


        /// <summary>
        /// Server's HUB : it does stuff.
        /// </summary>
        public void DoOperation()
        {
            while (true)
            {
                // We receive a Communication from the client
                Communication communication = Net.COMMUNICATION.Receive(comm.GetStream());

                // We extract the data
                Instructions instruction = communication.instruction;
                Object content = communication.content;

                // We display the instruction
                CONSOLE.WriteLine(ConsoleColor.Cyan, "\nINSTRUCTION : " + instruction);

                // According to the instruction, we'll do a specific action
                switch (instruction)
                {
                    case Instructions.Exit_Software:
                        // We raise the event : a user left the software
                        When_Server_Exit?.Invoke(comm);
                        break;




                    // CREDENTIALS
                    case Instructions.LogIn:
                        if (LogIn((Credentials)content))
                        {
                            // We raise the event : a user has logged in
                            When_Server_LogIn?.Invoke(comm);
                        }
                        break;

                    case Instructions.SignIn:
                        // We get the profile Signed In
                        CoachWithPassword newCoach = SignIn((CoachWithPassword)content);
                        
                        // If the Profile's ID is not Empty (means that the Sign In was successful)
                        if (newCoach.id != Guid.Empty)
                        {
                            // We raise the event : a user has logged in
                            When_Server_LogIn?.Invoke(comm);

                            // We raise the event : a Profile has been created
                            When_Coach_Create(newCoach);
                        }
                        break;

                    case Instructions.LogOut:
                        // We raise the event : a user has logged off
                        When_Server_LogOff?.Invoke(comm);
                        break;




                    // COACH
                    /*
                    case Instructions.Profile_GetAll:
                        SendAllProfiles();
                        break;
                        */

                    case Instructions.Coach_GetById:
                        GetCoachById((Guid)content);
                        break;
                    /*
                case Instructions.Profile_GetByName:
                    GetProfileByName((string)content);
                    break;
                    */

                    // TEAM
                    case Instructions.Team_GetAllFromCoach:
                        GetTeamsByCoachId((Guid)content);
                        break;

                    // otherwise : Error (should not occur, but we're not taking any chance)
                    default:
                        CONSOLE.WriteLine(ConsoleColor.Red, "Message instruction not understood : " + instruction);
                        break;
                }
            }
        }






        // COACH

        /// <summary>
        /// Sends all Coaches to the Client-Side
        /// </summary>
        public void SendAllCoaches()
        {
            // We send all the Coaches
            Net.COACH.SendMany(comm.GetStream(), Database.coaches);
        }


        /// <summary>
        /// Sends a Coach of which we know the id
        /// </summary>
        /// <param name="id">Id of the Coach we seek</param>
        public void GetCoachById(Guid id)
        {
            // Writing a Log in the Console
            CONSOLE.WriteLine(ConsoleColor.Yellow, "Coach's id : " + id);

            // We send the default Coach if no match was found / the correct one if a match was found
            Net.COACH.Send(comm.GetStream(), Database.COACH.GetById(id));
        }


        /// <summary>
        /// Sends a Coach of which we know the name
        /// </summary>
        /// <param name="name">name of the Coach we seek</param>
        public void GetCoachByName(string name)
        {
            // Writing a Log in the Console
            CONSOLE.WriteLine(ConsoleColor.Yellow, "Coach's name : " + name);

            // We send the default Coach if no match was found / the correct one if a match was found
            Net.COACH.Send(comm.GetStream(), Database.COACH.GetByName(name));
        }







        // CREDENTIALS


        /// <summary>
        /// Server-side verification for the Login
        /// Returns an instance of the Profile logged in (if it didn't work, its ID = -1)
        /// </summary>
        /// <param name="credentials">Credentials of the user</param>
        /// <returns> Whether the Login was successful or not</returns>
        public bool LogIn(Credentials credentials)
        {
            // We extract the credential's data
            string name = credentials.name;
            string password = credentials.password;

            // We create a integer representing the ID of the user ;
            // By default, its ID is negative (= not an actual profile)
            Guid ID_toReturn = Guid.Empty;

            // We iterate through all the Database's Profiles
            foreach (Credentials credential in Database.credentials)
            {
                // If we found a match : BINGO
                if (name == credential.name && password == credential.password)
                {
                    ID_toReturn = credential.id;
                    break;
                }
            }

            // We send the default Profile if no match was found / the correct one if a match was found
            Net.GUID.Send(comm.GetStream(), ID_toReturn);


            // Returns whether we succeeded or not in Logging in
            return ID_toReturn != Guid.Empty;
        }


        /// <summary>
        /// Server-side verification for the Sign-in
        /// Returns an instance of the newly created Coach (if it didn't work, its ID = -1)
        /// </summary>
        /// <param name="coachReceived">Coach's data of the user trying to Sign in</param>
        /// <returns> The newly created Coach (with password), if successful; otherwise, a default one</returns>
        public CoachWithPassword SignIn(CoachWithPassword coachReceived)
        {
            // We extract the profile's data
            string name = coachReceived.name;
            string password = coachReceived.password;
            string email = coachReceived.email;

            CoachWithPassword coachToReturn = new CoachWithPassword();

            // We create a bool repertoring whether or not a Profile already has the same data as the new user
            // By default, this is false
            bool isTaken = false;


            // We iterate through all the Database's Profiles
            foreach (Coach coach in Database.coaches)
            {
                // If we found a match : ERROR !
                if (name == coach.name || email == coach.email)
                {
                    isTaken = true;
                    break;
                }
            }

            // If the Credentials don't exist : we can create a new Profile
            if (!isTaken)
            {
                // We create the new Profile
                coachToReturn = new CoachWithPassword(name, email, password);
            }


            // We send the ID of the Profile (Empty = SignIn failed)
            Net.GUID.Send(comm.GetStream(), coachToReturn.id);

            // We return the profile created
            return coachToReturn;
        }




        // TEAM


        /// <summary>
        /// Sends a Coach of which we know the id
        /// </summary>
        /// <param name="coachId">Id of the Coach we seek</param>
        public void GetTeamsByCoachId(Guid coachId)
        {
            // Writing a Log in the Console
            CONSOLE.WriteLine(ConsoleColor.Yellow, "Teams from Coach's id : " + coachId);

            // We send the list of Teams from that coach
            Net.TEAM.SendMany(comm.GetStream(), Database.COACH.GetTeams(coachId));
        }
    }
}
