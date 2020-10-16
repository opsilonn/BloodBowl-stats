using BloodBowl_Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Front_Console
{
    public partial class Client
    {
        /// <summary>
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
                        PanelLeagues();
                        break;

                    case 2:
                        NewLeague();
                        break;

                    case 3:
                        SeeInvitationsCoach();
                        break;

                    // TEAM
                    case 4:
                        PanelTeams();
                        break;

                    case 5:
                        NewTeam();
                        break;

                    // LOG OUT
                    case 6:
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
        /// Panel displaying all the Leagues related to an user
        /// </summary>
        private void PanelLeagues()
        {
            bool continuingLeagues = true;


            while (continuingLeagues)
            {
                // We ask to get all the Leagues the user is in
                Net.COMMUNICATION.Send(comm.GetStream(), new Communication(
                    Instructions.League_GetAllForCoach,
                    userData.id
                    ));

                // We get the Leagues
                List<League> leagues = Net.LIST_LEAGUE.Receive(comm.GetStream());


                if(leagues.Count == 0)
                {
                    CONSOLE.WriteLine(ConsoleColor.Red, PrefabMessages.LEAGUE_NONE_ARE_AVAILABLE);
                    CONSOLE.WaitForInput();
                    continuingLeagues = false;
                }
                else
                {
                    // CHOICE
                    // We dynamically create a List containing all the League's name
                    List<string> choiceString = leagues.Select(league => league.name).ToList();

                    // We add as a last choice the option to "Go Back"
                    choiceString.Add(PrefabMessages.SELECTION_GO_BACK);

                    // We create the Choice
                    Choice choice = new Choice(PrefabMessages.SELECTION_LEAGUE, choiceString);
                    int index = choice.GetChoice();

                    if (index != choiceString.Count - 1)
                    {
                        // We continue to browse Leagues depending on future decisions
                        PanelLeague(leagues[index]);
                    }
                    else
                    { 
                        // We end browsing Leagues NOW
                        continuingLeagues = false;
                    }
                }
            }
        }


        /// <summary>
        /// A panel displaying all options a user can perform with a given League
        /// </summary>
        /// <param name="league">League instance of which we display the options</param>
        /// <returns>Whether the user wants to keep browing Leagues afterward</returns>
        private void PanelLeague(League league)
        {
            bool continuingLeague = true;

            while (continuingLeague)
            {
                // We create a list containing all the choices
                List<string> choiceStrings = new List<string>();

                // Add choice to see the League's data
                choiceStrings.Add(PrefabMessages.SELECTION_SEE_DATA);

                // Add choice to see the Members
                choiceStrings.Add(PrefabMessages.SELECTION_LEAGUE_SEE_MEMBERS);

                // if the user is allowed to manage member : Add choice to Invite / Remove a Member
                if(league.members.FirstOrDefault(member => member.coach.id == userData.id).job.canManageMember())
                {
                    choiceStrings.Add(PrefabMessages.SELECTION_LEAGUE_INVITE_MEMBER);
                    choiceStrings.Add(PrefabMessages.SELECTION_LEAGUE_REMOVE_MEMBER);
                }

                // Add choice to leave the League
                choiceStrings.Add(PrefabMessages.SELECTION_LEAGUE_LEAVE);

                // Add choice to go back
                choiceStrings.Add(PrefabMessages.SELECTION_GO_BACK);


                // We create the Choice
                Choice c = new Choice("please Select an action for " + league.name + " (last one = leave) : ", choiceStrings);

                // We get the user's choice STRING !!!
                string choice = choiceStrings[c.GetChoice()];

                // Depending on his choice :
                switch(choice)
                {
                    // Display basic data
                    case PrefabMessages.SELECTION_SEE_DATA:
                        DisplayLeague(league);
                        break;

                    // See Members
                    case PrefabMessages.SELECTION_LEAGUE_SEE_MEMBERS:
                        DisplayLeagueMembers(league);
                        break;

                    // Invite Member
                    case PrefabMessages.SELECTION_LEAGUE_INVITE_MEMBER:
                        InviteToLeague(league);
                        break;

                    // Remove Member
                    case PrefabMessages.SELECTION_LEAGUE_REMOVE_MEMBER:
                        RemoveMemberFromLeague(league);
                        break;

                    // Leave League
                    case PrefabMessages.SELECTION_LEAGUE_LEAVE:
                        // we continue inversely to whether we could depart from the League
                        continuingLeague = !CoachLeaveLeague(league);
                        break;

                    // Go Back
                    case PrefabMessages.SELECTION_GO_BACK:
                        continuingLeague = false;
                        break;

                    default:
                        CONSOLE.WriteLine(ConsoleColor.Red, "Error at the League " + league.name);
                        break;
                }
            }
        }




        /// <summary>
        /// Panel displaying all the Teams of the user
        /// </summary>
        private void PanelTeams()
        {
            bool continuingTeams = true;

            while (continuingTeams)
            {
                // if there is no team, display an error message
                if(userData.teams.Count == 0)
                {
                    CONSOLE.WriteLine(ConsoleColor.Red, PrefabMessages.TEAM_NONE_ARE_AVAILABLE);
                    CONSOLE.WaitForInput();
                    continuingTeams = false;
                }
                else
                {
                    // CHOICE
                    // We dynamically create a List containing all the Team's name
                    List<string> choiceString = new List<string>();
                    userData.teams.ForEach(team => choiceString.Add(team.shortDescription));

                    // We add as a last choice the option to "Go Back"
                    choiceString.Add(PrefabMessages.SELECTION_GO_BACK);

                    // We create the Choice
                    Choice choice = new Choice(PrefabMessages.SELECTION_TEAM, choiceString);
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
        }


        /// <summary>
        /// A panel displaying all options a user can perform with a given Team
        /// </summary>
        /// <param name="team">Team instance of which we display the options</param>
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
                        // we continue inversely to whether we could delete the Team
                        continuingTeam = !DeleteTeam(team);
                        break;

                    // Go Back
                    case 4:
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
                choiceString.Add(PrefabMessages.SELECTION_GO_BACK);

                // We create the Choice
                Choice c = new Choice(PrefabMessages.SELECTION_PLAYER, choiceString);
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
                // We create a list containing all the choices
                List<string> choiceStrings = new List<string>();
                choiceStrings.Add(PrefabMessages.SELECTION_SEE_DATA);

                if (player.hasNewLevel)
                {
                    choiceStrings.Add(PrefabMessages.SELECTION_PLAYER_NEW_LEVEL);
                }

                // We add another choice : "Remove Player"
                choiceStrings.Add(PrefabMessages.SELECTION_PLAYER_REMOVE);

                // We add a last choice: "Go Back"
                choiceStrings.Add(PrefabMessages.SELECTION_GO_BACK);


                // We create the Choice
                Choice c = new Choice("please Select an action for " + player.name + " (last one = leave) : ", choiceStrings);

                // We get the user's choice STRING !!!
                string choice = choiceStrings[c.GetChoice()];


                // Depending on his choice :
                switch (choice)
                {
                    // Display basic data
                    case PrefabMessages.SELECTION_SEE_DATA:
                        Console.WriteLine(player);
                        CONSOLE.WaitForInput();
                        break;

                    // New Level
                    case PrefabMessages.SELECTION_PLAYER_NEW_LEVEL:
                        PlayerLevelsUp(player);
                        break;

                    // Go Back
                    case PrefabMessages.SELECTION_PLAYER_REMOVE:
                        RemovePlayer(player);
                        break;

                    // Go Back
                    case PrefabMessages.SELECTION_GO_BACK:
                        continuingPlayer = false;
                        break;

                    default:
                        CONSOLE.WriteLine(ConsoleColor.Red, "Error at the Player " + player.name);
                        break;
                }
            }
        }
    }
}