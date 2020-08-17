using BloodBowl_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Front_Console
{
    public partial class Client
    {
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
                    // We create a list of all the Races
                    List<Race> races = RaceStuff.GetAllRaces();

                    // CHOICE
                    // We dynamically create a List containing all the Race's name
                    List<string> choiceString = new List<string>();
                    races.ForEach(r => choiceString.Add(r.name()));

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

                        // We receive the id of the Team from the server
                        Guid idReceived = Net.GUID.Receive(comm.GetStream());

                        // We see if the id received is valid
                        if (idReceived != Guid.Empty)
                        {
                            // We set the received id
                            newTeam.id = idReceived;

                            // We add the Player to the user's team
                            userData.teams.Add(newTeam);

                            // We display a success message
                            CONSOLE.WriteLine(ConsoleColor.Green, PrefabMessages.TEAM_CREATION_SUCCESS);

                            // Ending the loop
                            continueNewTeam = false;
                        }
                        else
                        {
                            errorMessage = PrefabMessages.TEAM_CREATION_FAILURE;
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

        
        /// <summary>
        /// Creates a new Player
        /// </summary>
        /// <param name="team">Team the Player is in</param>
        private void NewPlayer(Team team)
        {
            bool continueNewPlayer = true;
            bool continueNewPlayerInputs = true;
            string errorMessage = "";

            do
            {
                // CHOICE
                // We dynamically create a List containing all the roles names
                List<string> choiceString = new List<string>();
                team.race.playerRoles().ForEach(role => choiceString.Add(role.ToStringCustom()));

                // We add as a last choice the option to "Cancel"
                choiceString.Add("Cancel");

                // We create the Choice
                Choice c = new Choice("please Select a Role (last one = cancel) : ", choiceString);
                int index = c.GetChoice();

                if (index != choiceString.Count - 1)
                {
                    // We save the Role
                    Role role = team.race.playerRoles()[index];

                    // if the role chosen is too cost-expensive
                    if(role.price() > team.money)
                    {
                        CONSOLE.WriteLine(ConsoleColor.Red, PrefabMessages.NOT_ENOUGH_MONEY);
                        CONSOLE.WaitForInput();
                    }
                    // if the player can afford it : continue
                    else
                    {
                        // Inputs
                        do
                        {
                            Console.Clear();

                            // Displays a message if the credentials are incorrect
                            CONSOLE.WriteLine(ConsoleColor.Red, errorMessage);

                            // NAME
                            Console.Write("\n Please enter the {0}'s name (empty = go back) : ", role.name());
                            string name = Console.ReadLine();


                            // All the fields are empty : go back to the menu
                            if (name.Length == 0)
                            {
                                continueNewPlayerInputs = false;
                            }
                            // If at least one field is too long : ERROR
                            else if (name.Length > PrefabMessages.INPUT_MAXSIZE_COACH_NAME)
                            {
                                errorMessage = PrefabMessages.INCORRECT_INPUT_SIZE;
                            }
                            // If at least one field has one incorrect character : ERROR
                            else if (!Util.CorrectInput(name))
                            {
                                errorMessage = PrefabMessages.INCORRECT_INPUT_CHARACTER;
                            }
                            // Otherwise : verify with the server
                            else
                            {
                                // Sending the new Player
                                Instructions instruction = Instructions.Team_AddPlayer;
                                Player newPlayer = new Player(name, role, team);
                                Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, newPlayer));

                                // We receive the id of the Player from the server
                                Guid idReceived = Net.GUID.Receive(comm.GetStream());

                                // We see if the id received is valid
                                if (idReceived != Guid.Empty)
                                {
                                    // We set the received id
                                    newPlayer.id = idReceived;

                                    // We add the Player to the user's team
                                    team.players.Add(newPlayer);

                                    // We make the transaction (the team has less money now)
                                    team.money -= role.price();

                                    // We display a success message
                                    CONSOLE.WriteLine(ConsoleColor.Green, PrefabMessages.PLAYER_CREATION_SUCCESS);

                                    // Ending the loops
                                    continueNewPlayerInputs = false;
                                    continueNewPlayer = false;
                                }
                                else
                                {
                                    errorMessage = PrefabMessages.PLAYER_CREATION_FAILURE;
                                }
                            }
                        }
                        while (continueNewPlayerInputs);
                    }
                }
                else
                {
                    continueNewPlayer = false;
                }
            }
            while (continueNewPlayer);
        }
    
    

        private void RemovePlayer(Player player)
        {
            // Little verification : we ask the user to validate his action
            if (Choice_Prefabs.CHOICE_REMOVEPLAYER.GetChoice() == 0)
            {
                // Asking to remove the Player
                Instructions instruction = Instructions.Team_RemovePlayer;
                Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, player));

                // We receive whether it work or not
                if(Net.BOOL.Receive(comm.GetStream()))
                {
                    // We remove the Player from its Team
                    player.team.players.Remove(player);

                    // We display a message accordingly
                    CONSOLE.WriteLine(ConsoleColor.Green, "Player " + player.name + " removed !");
                }
                else
                {
                    // We display a message accordingly
                    CONSOLE.WriteLine(ConsoleColor.Red, "Player " + player.name + "was not removed...");
                }
                CONSOLE.WaitForInput();
            }
        }
    }
}
