using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBowl_Library;


namespace Back_Server
{
    public partial class Receiver
    {
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


        /// <summary>
        /// Searches a Coach by name, and returns up to 10 of the best matches
        /// </summary>
        /// <param name="name">name of the Coach we seek</param>
        public void SearchCoachByName(string name)
        {
            // Writing a Log in the Console
            CONSOLE.WriteLine(ConsoleColor.Yellow, "Coach's name : " + name);

            // We create a list list that
            // - orders all the Coach by similitude to the searched name
            // - takes only the 10 first results
            List<Coach> listCoaches = Database.coaches.OrderByDescending(c => name.CalculateSimilarity(c.name)).Take(10).ToList();


            // We send the default Coach if no match was found / the correct one if a match was found
            Net.LIST_COACH.Send(comm.GetStream(), listCoaches);
        }

        /// <summary>
        /// Searches a Coach by name, and returns up to 10 of the best matches (while not including the user)
        /// </summary>
        /// <param name="name">name of the Coach we seek</param>
        public void SearchCoachByNameExceptSelf(string name)
        {
            // Writing a Log in the Console
            CONSOLE.WriteLine(ConsoleColor.Yellow, "Coach's name : " + name);

            // We create a list list that
            // - does not contain
            // - orders all the Coach by similitude to the searched name
            // - takes only the 10 first results
            List<Coach> listCoaches = Database.coaches.
                Where(c => c.id != userCoach.id).
                OrderByDescending(c => name.CalculateSimilarity(c.name)).Take(10).ToList();


            // We send the default Coach if no match was found / the correct one if a match was found
            Net.LIST_COACH.Send(comm.GetStream(), listCoaches);
        }


        /// <summary>
        /// Sends to the Client all the InvitationCoach the user has
        /// </summary>
        /// <param name="coachId">Id of the Coach</param>
        public void GetInvitationsCoach(Guid coachId)
        {
            // We initialize a list
            List<InvitationCoach> invitations = new List<InvitationCoach>();

            // Foreach League, we add to the list the corresponding Invitations
            foreach (League league in Database.leagues)
            {
                List<InvitationCoach> currentInvits = league.invitedCoaches.Where(invit => invit.idInvited == coachId).ToList();
                invitations.AddRange(currentInvits);
            }

            // We send the list of InvitationCoach to the user

            invitations.ForEach(i => Console.WriteLine(i.league));
            Net.LIST_INVITATION_COACH.Send(comm.GetStream(), invitations);
        }




        // TEAM


        /// <summary>
        /// Creates a Team
        /// </summary>
        /// <param name="teamReceived">Team's data send by the client</param>
        public void NewTeam(Team teamReceived)
        {
            // We initialize a new Team
            Team newTeam = new Team();

            // ...Let's say we do some verification here...

            // The user does NOT have any Team with the same name AND same race
            // (same name, different race OR same race, different name is accepted)
            if (!userCoach.teams.Any(team =>
                team.name == teamReceived.name
                && team.race == teamReceived.race))
            {
                // Success !
                // We create a new Team instance from the data receiveds
                newTeam = new Team(teamReceived.name, teamReceived.description, teamReceived.race, teamReceived.coach);

                // We save it into the representation of the user's data
                userCoach.teams.Add(newTeam);

                // We save it into the Database's list
                Database.teams.Add(newTeam);

                // We raise the event : a Team has been created
                When_Team_Create?.Invoke(newTeam);
            }

            // We return the id of the newly created team (error : default empty id; success : correct id)
            Net.GUID.Send(comm.GetStream(), newTeam.id);
        }


        /// <summary>
        /// Deletes a Team of the user's list
        /// </summary>
        /// <param name="teamReceived">Team to delete</param>
        public void DeleteTeam(Team teamReceived)
        {
            // We get the team from the user's data
            Team teamToDelete = userCoach.teams.FirstOrDefault(t => t.id == teamReceived.id);

            // If we found a Team
            if(teamToDelete != null)
            {
                // We remove it from the user's data
                userCoach.teams.Remove(teamToDelete);

                // We raise the event : a Team has been deleted
                When_Team_Delete?.Invoke(teamToDelete);
            }

            // We return to the user whether the deletion was successful or not
            Net.BOOL.Send(comm.GetStream(), (teamToDelete != null));
        }




        // Player


        /// <summary>
        /// Creates a Player
        /// </summary>
        /// <param name="playerReceived">Player's data send by the client</param>
        public void NewPlayer(Player playerReceived)
        {
            // We initialize a new Player
            Player newPlayer = new Player();

            // We get the Team to add the player in
            Team teamToAddIn = GetTeamById(playerReceived.team.id);


            // ...Let's say we do some verification here...
            // We check that :
            // - the Team to put the player in is valid
            // - the Team has enough money
            if (teamToAddIn.IsComplete
                && teamToAddIn.money >= playerReceived.role.price())
            {
                // Success !
                // We create a new Player instance from the data receiveds
                newPlayer = new Player(playerReceived.name, playerReceived.role, teamToAddIn);

                // we save it into the representation of the user's data
                teamToAddIn.players.Add(newPlayer);

                // We withdraw the correct amount of money from the Team
                teamToAddIn.money -= newPlayer.role.price();

                // We raise the event : a Player has been created
                When_Player_Create?.Invoke(newPlayer);
            }

            // We return the id of the newly created team (error : default empty id; success : correct id)
            Net.GUID.Send(comm.GetStream(), newPlayer.id);
        }


        /// <summary>
        /// Removes a Player
        /// </summary>
        /// <param name="playerReceived">Player we are removing</param>
        public void RemovePlayer(Player playerReceived)
        {
            // We initialize the Player we are removing
            Player playerToRemove = new Player();

            // We get the Team he is in
            Team team = GetTeamById(playerReceived.team.id);

            // If the Team is valid
            if (team.IsComplete)
            {
                // We get the Player with the given Id
                playerToRemove = team.players.FirstOrDefault(p => p.id == playerReceived.id);

                // If we found the Player
                if (playerToRemove != null)
                {
                    // We remove the Player from its Team
                    team.players.Remove(playerToRemove);

                    // We raise the event : a Player has been removed
                    When_Player_Remove?.Invoke(playerToRemove);
                }
            }

            // We return to the Client whether the operation worked
            Net.BOOL.Send(comm.GetStream(), (playerToRemove != null && playerToRemove.IsComplete));
        }


        /// <summary>
        /// Manage the selection of a new Perk for a Player that is leveling up
        /// </summary>
        /// <param name="player">Player that is leveling up</param>
        public void PlayerLevelsUp(Player player)
        {
            // We define the dices
            int dice1 = Dice.Roll6();
            int dice2 = Dice.Roll6();

            // We send their results
            Net.INT.Send(comm.GetStream(), dice1);
            Net.INT.Send(comm.GetStream(), dice2);

            // While we wait for the result...
            // Select the Perk Types the player can level up in
            List<PerkType> types = PerkStuff.GetPerkTypesForLevelUp(player.role, dice1, dice2);
            List<List<Perk>> perks = PerkStuff.GetPerksForLevelUp(types);

            // We receive the Perk chosen
            Perk perkReceived = Net.PERK.Receive(comm.GetStream());


            // We check if the Perk received is valid
            // By default, we think it is false, so we search to find the Perk in the valid one. If we find it, then it is valid !
            bool isValid = false;
            foreach (List<Perk> currentList in perks)
            {
                // We check if the current list of Perks contain the received Perk
                isValid = currentList.Contains(perkReceived);

                // We stop itirating once it is found
                if (isValid)
                {
                    // We add the effect to the Player
                    player.perks.Add(perkReceived);

                    // We raise the event : an Perk has been added
                    When_Player_LevelsUp?.Invoke(player);

                    // We break the loop
                    break;
                }
            }

            // We return to the user whether the Perk was accepted or not
            Net.BOOL.Send(comm.GetStream(), isValid);
        }



        private Team GetTeamById(Guid id)
        {
            foreach (Team team in userCoach.teams)
            {
                if (team.id == id)
                {
                    return team;
                }
            }

            return new Team();
            // return userCoach.teams.Where(team => team.id == id).ToList()[0];
        }
    }
}