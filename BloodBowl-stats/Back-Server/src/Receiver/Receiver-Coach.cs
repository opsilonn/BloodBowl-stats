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


        /// <summary>
        /// Removes a Player
        /// </summary>
        /// <param name="playerReceived">Player we are removing</param>
        /// <returns>The Player removed (if the process didn't work, we send a default instance)</returns>
        public Player RemovePlayer(Player playerReceived)
        {
            // We initialize the Player we are removing
            Player playerToRemove = new Player();

            // We get the Team he is in
            Team team = GetTeamById(playerReceived.team.id);

            // If the Team is valid
            if (team.IsComplete)
            {
                // We get all the Players of the given Id
                // OF COURSE THERE IS ONLY ONE !! but hey, that's how functional programming works ^^'
                List<Player> ListPlayersWithGivenId = team.players.Where(p => p.id == playerReceived.id).ToList();

                // If we found at least one Player (a.k.a. : if we found the ONLY Player)
                if (ListPlayersWithGivenId.Count != 0)
                {
                    // We set the Player instance accordingly
                    playerToRemove = ListPlayersWithGivenId[0];

                    // We remove the Player from its Team
                    team.players.Remove(playerToRemove);
                }

            }

            // We return to the Client whether the operation worked
            Net.BOOL.Send(comm.GetStream(), playerToRemove.IsComplete);

            // We return to the Server whether the operation worked
            return playerToRemove;
        }


        /// <summary>
        /// Manage the selection of a new Perk for a Player that is leveling up
        /// </summary>
        /// <param name="player">Player that is leveling up</param>
        /// <returns>Perk chosen for the Player leveling up (if invalid, returns null)</returns>
        public Perk? PlayerLevelsUp(Player player)
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
            Perk? effectReceived = Net.PERK.Receive(comm.GetStream());



            // We check if the Perk received is valid
            // By default, we think it is false, so we search to find the Perk in the valid one. If we find it, then it is valid !
            bool isValid = false;
            foreach (List<Perk> currentList in perks)
            {
                foreach (Perk effect in currentList)
                {
                    // if we find the Perk : good !
                    if (effect == effectReceived)
                    {
                        isValid = true;
                        break;
                    }
                }

                // We stop itirating once it is found
                if (isValid)
                {
                    break;
                }
            }

            // We return to the user whether the Perk was accepted or not
            Net.BOOL.Send(comm.GetStream(), isValid);

            // If reached, return the effect received
            return (isValid) ? effectReceived : null;
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