using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBowl_Library;


namespace Front_Console
{
    public partial class Client
    {

        /// <summary>
        /// Display all data about a Team
        /// </summary>
        /// <param name="league">League instance to display</param>
        private void DisplayLeague(League league)
        {
            // Display a Coach's data
            Console.Clear();
            Console.WriteLine(league.ToString());
            CONSOLE.WaitForInput();
        }



        /// <summary>
        /// Display all members of a League
        /// </summary>
        /// <param name="league">League instance to display</param>
        private void DisplayMembers(League league)
        {
            bool continueMembers = true;

            while (continueMembers)
            {
                // We create a list with all the league's members, ordered in hierarchical position
                List<JobAttribution> members = league.members.OrderBy(member => member.job).ToList();

                // If there is no member : cancel
                if (members.Count == 0)
                {
                    // Display error message
                    CONSOLE.WriteLine(ConsoleColor.Red, PrefabMessages.LEAGUE_HAS_NO_COACH);
                    CONSOLE.WaitForInput();

                    // end loop
                    continueMembers = false;
                }
                else
                {
                    // CHOICE
                    // We dynamically create a List containing all the Coaches names
                    List<string> choiceString = new List<string>();
                    members.ForEach(member => choiceString.Add(member.job + " - " + member.coach.name));

                    // We add as a last choice the option to "Go Back"
                    choiceString.Add(PrefabMessages.SELECTION_GO_BACK);


                    // We create the Choice
                    Choice c = new Choice(PrefabMessages.SELECTION_PLAYER, choiceString);
                    int index = c.GetChoice();


                    if (index != choiceString.Count - 1)
                    {
                        Console.WriteLine("you have chosen : " + members[index].coach.name);
                        CONSOLE.WaitForInput();
                        // ManagePlayer(members[index]);
                    }
                    else
                    {
                        continueMembers = false;
                    }
                }
            }
        }



        /// <summary>
        /// Creation of a new League
        /// </summary>
        private void NewLeague()
        {
            bool continueNewLeague = true;
            string errorMessage = "";

            while (continueNewLeague)
            {
                // Displaying some messages
                Console.Clear();
                CONSOLE.WriteLine(ConsoleColor.Blue, "\n   Enter empty fields to leave the Creation of the new LEAGUE");


                // Displays a message if the credentials are incorrect
                CONSOLE.WriteLine(ConsoleColor.Red, errorMessage);


                // NAME
                Console.Write("\n Please enter the name of the League : ");
                string name = Console.ReadLine();


                // All the fields are empty : go back to the menu
                if (name.Length == 0)
                {
                    continueNewLeague = false;
                }
                // One of the field is empty : error
                /*
                else if (name.Length == 0)
                {
                    errorMessage = PrefabMessages.INCOMPLETE_FIELDS;
                }
                */
                // input is too long : ERROR
                else if (name.Length > PrefabMessages.INPUT_MAXSIZE_NAME)
                {
                    errorMessage = PrefabMessages.INCORRECT_INPUT_SIZE;
                }
                // If at least one field has one incorrect character : ERROR
                else if (!Util.CorrectInput(name))
                {
                    errorMessage = PrefabMessages.INCORRECT_INPUT_CHARACTER;
                }
                // Otherwise : continue the protocol
                else
                {
                    // Sending the new League
                    Instructions instruction = Instructions.League_New;
                    League newLeague = new League(name, userData);
                    Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, newLeague));

                    // We receive the id of the League from the server
                    Guid idReceived = Net.GUID.Receive(comm.GetStream());

                    // We see if the id received is valid
                    if (idReceived != Guid.Empty)
                    {
                        // We set the received id
                        newLeague.id = idReceived;

                        // We display a success message
                        CONSOLE.WriteLine(ConsoleColor.Green, PrefabMessages.LEAGUE_CREATION_SUCCESS);

                        // Ending the loop
                        continueNewLeague = false;

                        CONSOLE.WaitForInput();

                        // We redirect the user to the League Panel
                        PanelLeague(newLeague);
                    }
                    else
                    {
                        errorMessage = PrefabMessages.LEAGUE_CREATION_FAILURE;
                    }
                }
            }
        }



        /// <summary>
        /// Invite a new Member to the League
        /// </summary>
        /// <param name="league">League where we are inviting the Player to</param>
        public void InviteToLeague(League league)
        {

            bool continueInvitation = true;
            string errorMessage = "";

            while (continueInvitation)
            {
                // Displaying some messages
                Console.Clear();
                CONSOLE.WriteLine(ConsoleColor.Blue, "\n   Enter empty fields to leave the INVITATION of a new Member :");

                // Displays a message if the credentials are incorrect
                CONSOLE.WriteLine(ConsoleColor.Red, errorMessage);

                // NAME
                Console.Write("\n Please enter the name of the user you want to invite to this League : ");
                string name = Console.ReadLine();


                // All the fields are empty : go back to the menu
                if (name.Length == 0)
                {
                    continueInvitation = false;
                }
                // input is too long : ERROR
                else if (name.Length > PrefabMessages.INPUT_MAXSIZE_NAME)
                {
                    errorMessage = PrefabMessages.INCORRECT_INPUT_SIZE;
                }
                // If at least one field has one incorrect character : ERROR
                else if (!Util.CorrectInput(name))
                {
                    errorMessage = PrefabMessages.INCORRECT_INPUT_CHARACTER;
                }
                // Otherwise : continue the protocol
                else
                {
                    // Sending the name of the Coach
                    Instructions instruction = Instructions.Player_SearchByName;
                    Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, name));

                    // We receive the list of potential matches
                    List<Coach> listCoaches = Net.LIST_COACH.Receive(comm.GetStream());


                    // CHOICE
                    // We dynamically create a List containing all the Coaches names
                    List<string> choiceString = new List<string>();
                    listCoaches.ForEach(coach => choiceString.Add(coach.name));

                    // We add as a last choice the option to "Go Back"
                    choiceString.Add(PrefabMessages.SELECTION_GO_BACK);


                    // We create the Choice
                    Choice c = new Choice(PrefabMessages.SELECTION_COACH, choiceString);
                    int index = c.GetChoice();


                    if (index != choiceString.Count - 1)
                    {
                        Coach selectedCoach = listCoaches[index];

                        // We check if the user didn't invite himself
                        if(selectedCoach.id == userData.id)
                        {
                            CONSOLE.WriteLine(ConsoleColor.Red, PrefabMessages.LEAGUE_INVITATION_COACH_SELF);
                        }
                        else
                        {
                            // Sending the ID of the Coach we invite
                            instruction = Instructions.League_InviteCoach;
                            InvitationCoach invitationCoach = new InvitationCoach(league, userData, selectedCoach);
                            Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, invitationCoach));

                            // We receive whether it worked or not
                            if(Net.BOOL.Receive(comm.GetStream()))
                            {
                                // We add the invitation to the League
                                league.invitedCoaches.Add(invitationCoach);

                                // We display a message accordingly
                                CONSOLE.WriteLine(ConsoleColor.Green, PrefabMessages.LEAGUE_INVITATION_COACH_SUCCESS);
                                continueInvitation = false;
                            }
                            else
                            {
                                // We display a message accordingly
                                CONSOLE.WriteLine(ConsoleColor.Red, PrefabMessages.LEAGUE_INVITATION_COACH_FAILURE);
                            }
                        }

                        CONSOLE.WaitForInput();
                    }
                }
            }
        }
    }
}