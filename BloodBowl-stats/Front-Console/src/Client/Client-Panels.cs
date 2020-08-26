using BloodBowl_Library;
using System;
using System.Collections.Generic;

namespace Front_Console
{
    public partial class Client
    {/// <summary>
     /// A panel displaying all data about connecting to the program (Login / Sign-in)
     /// </summary>
        private void PanelConnection()
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
        private void PanelConnected()
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
                        CONSOLE.WaitForInput();
                        break;

                    case 2:
                        CONSOLE.WriteLine(ConsoleColor.Magenta, "Not yet implemented !");
                        CONSOLE.WaitForInput();
                        break;

                    // TEAM
                    case 3:
                        PanelTeams();
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
            }
        }


        /// <summary>
        /// A panel saying goodbye to the user
        /// </summary>
        private void PanelExitSoftware()
        {
            // Displaying a farewell message
            Console.Clear();
            CONSOLE.WriteLine(ConsoleColor.Blue, "\n\n\n      Goodbye !! We hope to see you soon :)");

            // Warning the server we leave the software
            Instructions instruction = Instructions.Exit_Software;
            Object content = null;
            Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));


            // Exit the software
            Environment.Exit(0);
        }


        /// <summary>
        /// Panel displaying all the Teams of the user
        /// </summary>
        private void PanelTeams()
        {
            bool continuingTeams = true;


            while (continuingTeams)
            {
                // CHOICE
                // We dynamically create a List containing all the topic's name
                List<string> choiceString = new List<string>();
                userData.teams.ForEach(team => choiceString.Add(team.name));

                // We add as a last choice the option to "Go Back"
                choiceString.Add("Go Back");

                // We create the Choice
                Choice choice = new Choice("please Select a Team (last one = leave) : ", choiceString);
                int index = choice.GetChoice();

                if (index != choiceString.Count - 1)
                {
                    PanelTeam(userData.teams[index]);
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
        private void PanelTeam(Team team)
        {
            bool continuingTeam = true;

            while (continuingTeam)
            {
                int choice = Choice_Prefabs.CHOICE_TEAM.GetChoice();
                switch (choice)
                {
                    // display data
                    case 0:
                        DisplayTeam(team);
                        break;

                    // manage Players
                    case 1:
                        ManagePlayers(team);
                        break;

                    // buy Players
                    case 2:
                        NewPlayer(team);
                        break;

                    // Go Back
                    case 3:
                        continuingTeam = false;
                        break;

                    default:
                        Console.WriteLine("Error at the detailled Team menu");
                        break;
                }
            }
        }



        /// <summary>
        /// Manage the menu of the Players of a given Team
        /// </summary>
        /// <param name="team">Team of which we are analysing the Players</param>
        private void ManagePlayers(Team team)
        {
            bool continuingPlayers = true;

            while (continuingPlayers)
            {
                // CHOICE
                // We dynamically create a List containing all the players names
                List<string> choiceString = new List<string>();
                team.players.ForEach(player => choiceString.Add(player.name));

                // We add as a last choice the option to "Go Back"
                choiceString.Add("Go Back");

                // We create the Choice
                Choice c = new Choice("please Select a Player (last one = leave) : ", choiceString);
                int index = c.GetChoice();

                if (index != choiceString.Count - 1)
                {
                    ManagePlayer(team.players[index]);
                }
                else
                {
                    continuingPlayers = false;
                }
            }
        }


        /// <summary>
        /// Manage a given Player
        /// </summary>
        /// <param name="player">Player we are managing</param>
        private void ManagePlayer(Player player)
        {
            bool continuingPlayer = true;

            while (continuingPlayer)
            {
                // CHOICE
                // We dynamically create a List containing all the players names
                List<string> choiceString = new List<string>();
                choiceString.Add("See Data");

                if (player.hasNewLevel)
                {
                    choiceString.Add("New Level !");
                }

                // We add another choice : "Remove Player"
                choiceString.Add("Remove Player");

                // We add a last choice: "Go Back"
                choiceString.Add("Go Back");

                // We create the Choice
                Choice c = new Choice("please Select an action for " + player.name + " (last one = leave) : ", choiceString);
                int index = c.GetChoice();


                // Since we have a dynamic attribution of choices, the on-going program is quite... messy

                // If it is the first choice : SEE DATA
                if (index == 0)
                {
                    Console.WriteLine(player);
                    CONSOLE.WaitForInput();
                }
                // If it is the second choice AND the player has a new level : NEW LEVEL
                else if (index == 1 && player.hasNewLevel)
                {
                    PlayerLevelsUp(player);
                }
                // If it is the last choice : GO BACK
                else if(index == choiceString.Count-1)
                {
                    continuingPlayer = false;
                }
                // Otherwise : REMOVE PLAYER
                else
                {
                    RemovePlayer(player);
                }
            }
        }
    }
}