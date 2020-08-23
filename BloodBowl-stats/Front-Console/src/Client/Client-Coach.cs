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
                team.race.roles().ForEach(role => choiceString.Add(role.ToStringCustom()));

                // We add as a last choice the option to "Cancel"
                choiceString.Add("Cancel");

                // We create the Choice
                Choice c = new Choice("please Select a Role (last one = cancel) : ", choiceString);
                int index = c.GetChoice();

                if (index != choiceString.Count - 1)
                {
                    // We save the Role
                    Role role = team.race.roles()[index];

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
    
    

        /// <summary>
        /// Removes the current Player from its Team
        /// </summary>
        /// <param name="player">Player to remove</param>
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


        /// <summary>
        /// When a Player levels up
        /// </summary>
        /// <param name="player"></param>
        private void PlayerLevelsUp(Player player)
        {
            // We define some variables
            int dice1 = Dice.Roll6();
            int dice2 = Dice.Roll6();
            List<EffectType> types = new List<EffectType>();


            // There are 2 steps :
            // PART 1 - display some useful info to the user
            // PART 2 - Wait for the user to choose a new Effect for his Player

            // PART 1 - display some useful info to the user
            Console.Clear();
            Console.WriteLine("New level !\n\n\n\nyou rolled {0} - {1} !", dice1, dice2);

            // If the dices rolled a double :
            if(dice1 == dice2)
            {
                // Display a cool message
                Console.WriteLine("Great, a double ! you can level up in any category :");

                // Select the Effect Types the player can level up in
                bool containsMutation = player.role.effectTypes().Contains(EffectType.SkillMutation);
                types = EffectStuff.GetAllEffectTypesForLevelUp(containsMutation);
            }
            else
            {
                // Display a cool message
                Console.WriteLine("no double... you can only level up in specific categories : ");

                // Select the Effect Types the player can level up in
                types = player.role.effectTypes();
            }
            types.ForEach(type => Console.WriteLine(" - " + type));
            CONSOLE.WaitForInput();


            // PART 2 - Wait for the user to choose a new Effect for his Player
            // We initialize our variables
            bool continuing = true;
            Effect chosenEffect;
            int currentType = 0;
            int currentEffect = 0;

            List<List<Effect>> effects = new List<List<Effect>>();
            types.ForEach(type => effects.Add(EffectStuff.GetAllSkillsFromType(type)) );




            // While the user doesn't press Enter, the loop continues
            do
            {
                // We clear the console, and display a given message
                Console.Clear();

                // We display all our Types
                for(int t = 0; t < types.Count; t++)
                {
                    // Display the current EffectType
                    Console.WriteLine("\n" + types[t]);

                    // Display all its Effects (with an arrow if it is the current one)
                    for (int e = 0; e < effects[t].Count; e++)
                    {
                        // Initialize some variables
                        Effect effect = effects[t][e];
                        string arrow = (currentType == t && currentEffect == e) ? "\t --> " : "\t     ";
                        ConsoleColor color = (player.effectsAll.Contains(effect)) ? ConsoleColor.Red : ConsoleColor.Blue;

                        // Display the result
                        CONSOLE.WriteLine(color, arrow + effect.name());
                    }
                }


                // We read the input
                ConsoleKeyInfo input = Console.ReadKey();
                bool goToLastEffect = false;


                // If it is an Array key (UP or DOWN), we modify our index accordingly
                if (input.Key == ConsoleKey.UpArrow)
                {
                    currentEffect--;

                    // If the index goes too low, we change type
                    if (currentEffect < 0)
                    {
                        currentType--;
                        goToLastEffect = true;
                    }
                }
                if (input.Key == ConsoleKey.DownArrow)
                {
                    currentEffect++;

                    // If the index goes too high, we change type
                    if (currentEffect > effects[currentType].Count - 1)
                    {
                        currentType++;
                        currentEffect = 0;
                    }
                }
                // If it is a LEFT Array key : go to the previous type
                if (input.Key == ConsoleKey.LeftArrow)
                {
                    currentType--;
                    currentEffect = 0;
                }
                // If it is a RIGHT Array key : go to the next type
                if (input.Key == ConsoleKey.RightArrow)
                {
                    currentType++;
                    currentEffect = 0;
                }



                // If the type is too high / too low, we change it accordingly
                if (currentType < 0)
                {
                    currentType = types.Count - 1;
                }
                if (currentType > types.Count - 1)
                {
                    currentType = 0;
                }

                // If needed : we replace the effect counter to the last index of the current list
                if(goToLastEffect)
                {
                    currentEffect = effects[currentType].Count - 1;
                }


                // We check if we end the loop
                if(input.Key == ConsoleKey.Enter)
                {
                    // We set the chosen Effect
                    chosenEffect = effects[currentType][currentEffect];

                    Console.Clear();
                    // We display a message, whether the Effect can be chosen or not
                    if (player.effectsAll.Contains(chosenEffect))
                    {
                        CONSOLE.WriteLine(ConsoleColor.Red, "You cannot choose the Effect " + chosenEffect.name() + " :\n\nyour Player already has it !!");
                    }
                    else
                    {
                        CONSOLE.WriteLine(ConsoleColor.Green, "You have chosen the Effect " + chosenEffect.name() + " !!");
                        continuing = false;
                    }

                    CONSOLE.WaitForInput();
                }
            }
            while (continuing);
        }
    }
}