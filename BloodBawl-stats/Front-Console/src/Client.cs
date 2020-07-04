using BloodBawl_Library;
using System;
using System.Collections.Generic;
using System.Net.Sockets;


namespace Front_Console
{
    class Client
    {
        Guid id_user;
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
            id_user = Guid.Empty;
            continuing = true;


            while (continuing)
            {
                // CONNECTION PANEL (LOGIN - SIGN-IN)
                PANEL_CONNECTION();


                // CONNECTED PANEL (TOPIC - CHAT : see / add / delete)
                if (id_user != Guid.Empty && continuing)
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
                        DisplayCoach(id_user);
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
                        CONSOLE.WriteLine(ConsoleColor.Magenta, "Not yet implemented !");
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
                // GETTING THE VARIABLES

                // Asking for all the Teams
                Instructions instruction = Instructions.Team_GetAllFromCoach;
                Object content = id_user;
                Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));

                // Receiving all the Topics
                List<Team> teams = Net.TEAM.ReceiveMany(comm.GetStream());


                // CHOICE


                // We dynamically create a List containing all the topic's name
                List<string> choiceString = new List<string>();
                foreach (Team team in teams)
                {
                    choiceString.Add(team.name);
                }
                
                // We add as a last choice the option to "Go Back"
                choiceString.Add("Go Back");

                // We create the Choice
                Choice choice = new Choice("please Select a topic (last one = leave) : ", choiceString);
                int index = choice.GetChoice();

                if (index != choiceString.Count - 1)
                {
                    // DisplayTopic(topics[index].ID);
                }
                else
                {
                    continuingTeams = false;
                }
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
                    id_user = Net.GUID.Receive(comm.GetStream());

                    // If the ID is Empty : there was no match found, we reset the login
                    if (id_user == Guid.Empty)
                    {
                        errorMessage = PrefabMessages.LOGIN_FAILURE;
                    }
                    // Match found, we proceed forward
                    else
                    {
                        continueLogin = false;
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

                    // We get an ID back
                    // CORRECT :  regular ID
                    // INCORRECT : empty ID
                    id_user = Net.GUID.Receive(comm.GetStream());


                    // If the ID is Empty : the profile's name and/or email is already taken, we reset the sign-in
                    if (id_user == Guid.Empty)
                    {
                        errorMessage = PrefabMessages.SIGNIN_FAILURE;
                    }
                    // otherwise : SignIn successful, we proceed forward
                    else
                    {
                        continueSignin = false;
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

            // Setting the ID to -1 : no user is logged in anymore
            id_user = Guid.Empty;

            // Warning the server we log out
            Instructions instruction = Instructions.LogOut;
            Object content = null;
            Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));
        }




        // METHODS - CONNECTED

        /// <summary>
        /// Display all data about the Profile logged-in
        /// </summary>
        private void DisplayCoach(Guid id)
        {
            // Display a Coach's data
            Console.Clear();
            Console.WriteLine(Net.COACH.GetByID(comm, id).ToString());
        }
    }
}