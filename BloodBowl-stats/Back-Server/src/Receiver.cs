using BloodBowl_Library;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Linq;


namespace Back_Server
{
    public class Receiver
    {
        private Server server;
        public TcpClient comm;

        // Creating a Delegate called CommunicationWithTheServer, and an event using it
        public delegate void CommunicationWithTheServer(TcpClient comm);
        public event CommunicationWithTheServer When_Server_Exit;
        public event CommunicationWithTheServer When_Server_LogIn;
        public event CommunicationWithTheServer When_Server_LogOff;

        // Creating a Delegate about creating some structures, and events using them
        public delegate void CreatingCoach(CoachWithPassword newCoach);
        public event CreatingCoach When_Coach_Create;

        public delegate void CreatingTeam(Team newTeam);
        public event CreatingTeam When_Team_Create;

        public delegate void PlayerEvent(Player player);
        public event PlayerEvent When_Player_Create;
        public event PlayerEvent When_Player_Remove;

        public Coach userCoach;



        /// <summary>
        /// Constructor of the Class
        /// </summary>
        /// <param name="server">Reference of the server</param>
        /// <param name="comm">Communication</param>
        public Receiver(Server server, TcpClient comm)
        {
            this.server = server;
            this.comm = comm;
            userCoach = new Coach();
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
                        LogOut();
                        // We raise the event : a user has logged off
                        When_Server_LogOff?.Invoke(comm);
                        break;




                    // COACH
                    case Instructions.Coach_GetById:
                        GetCoachById((Guid)content);
                        break;



                    // TEAM
                    case Instructions.Team_New:
                        // We get the newly created Team
                        Team newTeam = NewTeam((Team)content);

                        // If the Team can be created (ID is not empty)
                        if (newTeam.IsComplete)
                        {
                            // We raise the event : a Team has been created
                            When_Team_Create?.Invoke(newTeam);
                        }
                        break;

                    case Instructions.Team_AddPlayer:
                        // We get the newly created Player
                        Player newPlayer = NewPlayer((Player)content);

                        // If the Player can be created (ID is not empty)
                        if (newPlayer.IsComplete)
                        {
                            // We raise the event : a Player has been created
                            When_Player_Create?.Invoke(newPlayer);
                        }
                        break;

                    case Instructions.Team_RemovePlayer:
                        // We get the Player to remove
                        Player playerToRemove = (Player)content;

                        // We remove him from the database
                        bool playerRemovalWorked = RemovePlayer(playerToRemove);

                        // If the removal was successful)
                        if (playerRemovalWorked)
                        {
                            // We raise the event : a Player has been removed
                            When_Player_Remove?.Invoke(playerToRemove);
                        }
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
                    // We check that no one with these credentials is already logged
                    if (!credential.isLogged)
                    {
                        // We set the credential's data for the user being logged to TRUE
                        credential.isLogged = true;

                        // We save the ID found
                        ID_toReturn = credential.id;

                        // We save the Coach representing the user's data
                        userCoach = Database.COACH.GetById(ID_toReturn);
                    }

                    // whatever the result is, we end the loop, since we found the credentials, successful or not
                    break;
                }
            }

            // We send the default Profile if no match was found / the correct one if a match was found
            Net.COACH.Send(comm.GetStream(), userCoach);


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

            // If the Credentials don't exist : we can create a new Coach
            if (!isTaken)
            {
                // We create a new Coach representing the user's data
                userCoach = new Coach(name, email);
            }

            // We send the Coach (Empty id = SignIn failed)
            Net.COACH.Send(comm.GetStream(), userCoach);

            // We return the profile created
            return new CoachWithPassword(userCoach, password);
        }


        /// <summary>
        /// Server-side that handles all the Log out business
        /// </summary>
        public void LogOut()
        {
            // We reset the "isLogged" parameter from the user's credentials
            Database.CREDENTIALS.GetById(userCoach.id).isLogged = false;

            // We reset the user's data
            userCoach = new Coach();
        }



        // TEAM


        /// <summary>
        /// Creates a Team
        /// </summary>
        /// <param name="teamReceived">Team's data send by the client</param>
        public Team NewTeam(Team teamReceived)
        {
            // We initialize a new Team
            Team newTeam = new Team();

            // ...Let's say we do some verification here...

            if (true)
            {
                // Success !
                // We create a new Team instance from the data receiveds
                newTeam = new Team(teamReceived.name, teamReceived.description, teamReceived.race, teamReceived.coach);

                // we save it into the representation of the user's data
                userCoach.teams.Add(newTeam);
            }

            // We return the id of the newly created team (error : default empty id; success : correct id)
            Net.GUID.Send(comm.GetStream(), newTeam.id);


            // We return the new Team
            return newTeam;
        }




        // Player


        /// <summary>
        /// Creates a Player
        /// </summary>
        /// <param name="playerReceived">Player's data send by the client</param>
        public Player NewPlayer(Player playerReceived)
        {
            // We initialize a new Player
            Player newPlayer = new Player();
            bool isValid = true;

            // We get the Team to add the player in
            Team teamToAddIn = GetTeamById(playerReceived.team.id);


            // ...Let's say we do some verification here...
            // We check that :
            // - the Team to put the player in is valid
            // - the Team has enough money
            isValid = (teamToAddIn.IsComplete) && (teamToAddIn.money >= playerReceived.role.price());

            if (isValid)
            {
                // Success !
                // We create a new Player instance from the data receiveds
                newPlayer = new Player(playerReceived.name, playerReceived.role, teamToAddIn);

                // we save it into the representation of the user's data
                teamToAddIn.players.Add(newPlayer);

                teamToAddIn.money -= newPlayer.role.price();
            }

            // We return the id of the newly created team (error : default empty id; success : correct id)
            Net.GUID.Send(comm.GetStream(), newPlayer.id);


            // We return the new Player
            return newPlayer;
        }


        public bool RemovePlayer(Player player)
        {
            // We remove the player from its team
            GetTeamById(player.team.id).players.Remove(player);

            // We return whether the operation worked
            Net.BOOL.Send(comm.GetStream(), true);


            return true;
        }



        private Team GetTeamById(Guid id)
        {
            foreach (Team team in userCoach.teams)
            {
                if(team.id == id)
                {
                    return team;
                }
            }

            return new Team();
            // return userCoach.teams.Where(team => team.id == id).ToList()[0];
        }
    }
}
