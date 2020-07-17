using BloodBowl_Library;
using System;
using System.Collections.Generic;
using System.Net.Sockets;


namespace Front_Console
{
    class Client
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
                PANEL_CONNECTION();


                // CONNECTED PANEL (TOPIC - CHAT : see / add / delete)
                if (userData.IsComplete && continuing)
                {
                    PANEL_CONNECTED();
                }


                // DISPLAY A GOODBYE MESSAGE
                if (!continuing)
                {
                    PANEL_EXITSOFTWARE();
                }
            }
        }



        // PANELS

        /// <summary>
        /// A panel displaying all data about connecting to the program (Login / Sign-in)
        /// </summary>
        private void PANEL_CONNECTION()
        {
            int choice = Choice_Prefabs.CHOICE_CONNECTION.GetChoice();
            switch (choice)
            {
                case 0:
                    LogIn();
                    break;

                case 1:
                    SignIn();
                    break;

                case 2:
                    continuing = false;
                    break;

                default:
                    Console.WriteLine("Error at the Login / Sign in");
                    break;
            }
        }


        /// <summary>
        /// A panel displaying all options a user can perform while logged to the program (Topic / Chat)
        /// </summary>
        private void PANEL_CONNECTED()
        {
            bool continuingConnection = true;

            while (continuingConnection)
            {
                int choice = Choice_Prefabs.CHOICE_CONNECTED.GetChoice();
                switch (choice)
                {
                    // COACH
                    case 0:
                        DisplayCoach(userData);
                        break;

                    // LEAGUE
                    case 1:
                        CONSOLE.WriteLine(ConsoleColor.Magenta, "Not yet implemented !");
                        break;

                    case 2:
                        CONSOLE.WriteLine(ConsoleColor.Magenta, "Not yet implemented !");
                        break;

                    // TEAM
                    case 3:
                        PANEL_TEAMS();
                        break;

                    case 4:
                        NewTeam();
                        break;

                    // LOG OUT
                    case 5:
                        LogOut();
                        continuingConnection = false;
                        break;

                    default:
                        Console.WriteLine("Error at the Main menu");
                        break;
                }

                CONSOLE.WaitForInput();
            }
        }


        /// <summary>
        /// A panel saying goodbye to the user
        /// </summary>
        private void PANEL_EXITSOFTWARE()
        {
            // Displaying a farewell message
            Console.Clear();
            CONSOLE.WriteLine(ConsoleColor.Blue, "\n\n\n      Goodbye !! We hope to see you soon :)");

            // Warning the server we leave the software
            Instructions instruction = Instructions.Exit_Software;
            Object content = null;
            Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));


            // Environment.Exit(0);
        }



        /// <summary>
        /// Panel displaying all the Teams of the user
        /// </summary>
        private void PANEL_TEAMS()
        {
            bool continuingTeams = true;


            while (continuingTeams)
            {
                // CHOICE
                // We dynamically create a List containing all the topic's name
                List<string> choiceString = new List<string>();
                foreach (Team team in userData.teams)
                {
                    choiceString.Add(team.name);
                }
                
                // We add as a last choice the option to "Go Back"
                choiceString.Add("Go Back");

                // We create the Choice
                Choice choice = new Choice("please Select a Team (last one = leave) : ", choiceString);
                int index = choice.GetChoice();

                if (index != choiceString.Count - 1)
                {
                    PANEL_TEAM(userData.teams[index]);
                }
                else
                {
                    continuingTeams = false;
                }
            }
        }


        /// <summary>
        /// A panel displaying all options a user can perform while logged to the program (Topic / Chat)
        /// </summary>
        private void PANEL_TEAM(Team team)
        {
            bool continuingTeam = true;

            while (continuingTeam)
            {
                int choice = Choice_Prefabs.CHOICE_TEAM.GetChoice();
                switch (choice)
                {
                    // COACH
                    case 0:
                        DisplayTeam(team);
                        break;

                    // LEAGUE
                    case 1:
                        continuingTeam = false;
                        break;

                    default:
                        Console.WriteLine("Error at the detailled Team menu");
                        break;
                }

                CONSOLE.WaitForInput();
            }
        }



        // METHODS - CONNECTION

        /// <summary>
        /// Gets the Credentials of the user, and connects him / makes him retry accordingly
        /// </summary>
        private void LogIn()
        {
            bool continueLogin = true;
            string errorMessage = "";

            do
            {
                // Displaying some messages
                Console.Clear();
                CONSOLE.WriteLine(ConsoleColor.Blue, "\n   Enter empty fields to leave the LOGIN");


                // Displays a message if the credentials are incorrect
                CONSOLE.WriteLine(ConsoleColor.Red, errorMessage);


                // USERNAME
                Console.Write("\n Please enter your name : ");
                string name = Console.ReadLine();


                // PASSWORD
                Console.Write("\n Please enter your password : ");
                string password = Console.ReadLine();


                // All the fields are empty : go back to the menu
                if (name.Length == 0 && password.Length == 0)
                {
                    continueLogin = false;
                }
                // If at least one field is empty : ERROR
                else if (name.Length == 0 || password.Length == 0)
                {
                    errorMessage = PrefabMessages.INCOMPLETE_FIELDS;
                }
                // If at least one field is too long : ERROR
                else if (name.Length > PrefabMessages.INPUT_MAXSIZE_COACH_NAME || password.Length > PrefabMessages.INPUT_MAXSIZE_COACH_PASSWORD)
                {
                    errorMessage = PrefabMessages.INCORRECT_INPUT_SIZE;
                }
                // If at least one field has one incorrect character : ERROR
                else if (!Util.CorrectInput(name) || !Util.CorrectInput(password))
                {
                    errorMessage = PrefabMessages.INCORRECT_INPUT_CHARACTER;
                }
                // Otherwise : verify with the server
                else
                {
                    // Sending the credentials
                    Instructions instruction = Instructions.LogIn;
                    Object content = new Credentials(name, password);
                    Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));

                    // Receiving the response ID
                    userData = Net.COACH.Receive(comm.GetStream());

                    // Match found, we proceed forward
                    if (userData.IsComplete)
                    {
                        continueLogin = false;
                    }
                    // If the ID is Empty : there was no match found, we reset the login
                    else
                    {
                        errorMessage = PrefabMessages.LOGIN_FAILURE;
                    }
                }
            }
            while (continueLogin);
        }


        /// <summary>
        /// Gets the newly created Coach's data of the user, and connects him / makes him retry accordingly
        /// </summary>
        private void SignIn()
        {
            bool continueSignin = true;
            string errorMessage = "";

            while (continueSignin)
            {
                // Displaying some messages
                Console.Clear();
                CONSOLE.WriteLine(ConsoleColor.Blue, "\n   Enter empty fields to leave the SIGN-IN");


                // Displays a message if the credentials are incorrect
                CONSOLE.WriteLine(ConsoleColor.Red, errorMessage);


                // USERNAME
                Console.Write("\n Please enter your username : ");
                string name = Console.ReadLine();


                // PASSWORD
                Console.Write("\n Please enter your password : ");
                string password = Console.ReadLine();


                // PASSWORD - VERIFICATION
                Console.Write("\n Please verify your password : ");
                string passwordVerif = Console.ReadLine();


                // EMAIL
                Console.Write("\n Please enter your email : ");
                string email = Console.ReadLine();


                // All the fields are empty : go back to the menu
                if (name.Length == 0 && password.Length == 0 && passwordVerif.Length == 0 && email.Length == 0)
                {
                    continueSignin = false;
                }
                // If at least one field is empty : ERROR
                else if (name.Length == 0 || password.Length == 0 || passwordVerif.Length == 0 || email.Length == 0)
                {
                    errorMessage = PrefabMessages.INCOMPLETE_FIELDS;
                }
                // The password and its verification do not match : ERROR
                else if (password != passwordVerif)
                {
                    errorMessage = PrefabMessages.SIGNIN_PASSWORD_DONT_MATCH;
                }
                // NOW, we don't have to verify the PasswordVerif field anymore !
                // If at least one field is too long : ERROR
                else if (name.Length > PrefabMessages.INPUT_MAXSIZE_COACH_NAME
                    || password.Length > PrefabMessages.INPUT_MAXSIZE_COACH_PASSWORD
                    || email.Length > PrefabMessages.INPUT_MAXSIZE_COACH_EMAIL)
                {
                    errorMessage = PrefabMessages.INCORRECT_INPUT_SIZE;
                }
                // If at least one field has one incorrect character : ERROR
                else if (!Util.CorrectInput(name) || !Util.CorrectInput(password) || !Util.CorrectInput(email))
                {
                    errorMessage = PrefabMessages.INCORRECT_INPUT_CHARACTER;
                }
                // Otherwise : verify with the server
                else
                {
                    // Sending the new Coach (with password !)
                    Instructions instruction = Instructions.SignIn;
                    Object content = new CoachWithPassword(name, password, email);
                    Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));

                    // We get the Coach data back
                    // CORRECT :  regular data
                    // INCORRECT : default data
                    userData = Net.COACH.Receive(comm.GetStream());


                    // the data is complete : successful Sign in
                    if (userData.IsComplete)
                    {
                        continueSignin = false;
                    }
                    // If the data is incomplete : the profile's name and/or email is already taken, we reset the sign-in
                    {
                        errorMessage = PrefabMessages.SIGNIN_FAILURE;
                    }
                }
            }
        }


        /// <summary>
        /// Ensures the user Logs Out of the Softare (but remains to the Log In / Sign In menu)
        /// </summary>
        private void LogOut()
        {
            // Display a Farewell message
            CONSOLE.WriteLine(ConsoleColor.Blue, "\n\n     Goodbye ! We hope to see you soon :)\n\n");

            // Resetting the user's Data : no user is logged in anymore
            userData = new Coach();

            // Warning the server we log out
            Instructions instruction = Instructions.LogOut;
            Object content = null;
            Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));
        }




        // METHODS - CONNECTED

        /// <summary>
        /// Display all data about a Coach
        /// </summary>
        private void DisplayCoach(Coach coach)
        {
            // Display a Coach's data
            Console.Clear();
            Console.WriteLine(coach.ToString());
        }

        /// <summary>
        /// Display all data about a Team
        /// </summary>
        private void DisplayTeam(Team team)
        {
            // Display a Coach's data
            Console.Clear();
            Console.WriteLine(team.ToString());
        }




        /// <summary>
        /// Creates a new Team
        /// </summary>
        private void NewTeam()
        {
            bool continueNewTeam = true;
            string errorMessage = "";

            while (continueNewTeam)
            {
                // Displaying some messages
                Console.Clear();
                CONSOLE.WriteLine(ConsoleColor.Blue, "\n   Enter empty fields to leave the Creation of the new TEAM");


                // Displays a message if the credentials are incorrect
                CONSOLE.WriteLine(ConsoleColor.Red, errorMessage);


                // NAME
                Console.Write("\n Please enter the name of the Team : ");
                string name = Console.ReadLine();


                // DESCRIPTION
                Console.Write("\n Please enter the description of the Team  : ");
                string description = Console.ReadLine();


                // All the fields are empty : go back to the menu
                if (name.Length == 0 && description.Length == 0)
                {
                    continueNewTeam = false;
                }
                // One of the field is empty : error
                else if (name.Length == 0 || description.Length == 0)
                {
                    errorMessage = PrefabMessages.INCOMPLETE_FIELDS;
                }
                else if (name.Length > PrefabMessages.INPUT_MAXSIZE_STRUCTURE_NAME
                    || description.Length > PrefabMessages.INPUT_MAXSIZE_STRUCTURE_DESCRIPTION)
                {
                    errorMessage = PrefabMessages.INCORRECT_INPUT_SIZE;
                }
                // If at least one field has one incorrect character : ERROR
                else if (!Util.CorrectInput(name) || !Util.CorrectInput(description))
                {
                    errorMessage = PrefabMessages.INCORRECT_INPUT_CHARACTER;
                }
                // Otherwise : continue the protocol by choosing a Race
                else
                {
                    Race[] races = (Race[])Enum.GetValues(typeof(Race));

                    // CHOICE
                    // We dynamically create a List containing all the Race's name
                    List<string> choiceString = new List<string>();
                    foreach (Race r in races)
                    {
                        choiceString.Add(r.name());
                    }

                    // We add as a last choice the option to cancel the protocol
                    choiceString.Add("Cancel the creation");


                    // We create the Choice
                    Choice choice = new Choice("please Select a Race (last one = cancel) : ", choiceString);
                    int index = choice.GetChoice();

                    // The user chose a Race
                    if (index != choiceString.Count - 1)
                    {
                        // We get the race chose by the user
                        Race race = races[index];

                        // Sending the new Team
                        Instructions instruction = Instructions.Team_New;
                        Team newTeam = new Team(name, description, race, userData);
                        Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, newTeam));

                        // We receive whether the creation was successful or not
                        if (Net.BOOL.Receive(comm.GetStream()))
                        {
                            userData.teams.Add(newTeam);
                            CONSOLE.WriteLine(ConsoleColor.Green, PrefabMessages.TOPIC_CREATION_SUCCESS);

                            // Ending the loop
                            continueNewTeam = false;
                        }
                        else
                        {
                            errorMessage = PrefabMessages.CHAT_CREATION_FAILURE;
                        }
                    }
                    // The user chose to leave
                    else
                    {
                        continueNewTeam = false;
                    }
                }
            }
        }
    }
}