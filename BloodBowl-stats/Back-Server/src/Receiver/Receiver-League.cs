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
        /// <summary>
        /// Creates a Team
        /// </summary>
        /// <param name="leagueReceived">League's data send by the client</param>
        public League NewLeague(League leagueReceived)
        {
            // We initialize a new League (default has a null Id)
            League newLeague = new League();

            // We make a last verification here
            if (leagueReceived.IsComplete)
            {
                // Success !
                // We create a new Team instance from the data receiveds
                newLeague = new League(leagueReceived.name, leagueReceived.idCreator);
            }

            // We return the id of the newly created team (error : default empty id; success : correct id)
            Net.GUID.Send(comm.GetStream(), newLeague.id);


            // We return the new Team
            return newLeague;
        }




        /// <summary>
        /// Sends all Leagues related to a Coach
        /// </summary>
        /// <param name="idCoach">Id of the Coach of which we seek the Leagues</param>
        private void SendLeagues(Guid idCoach)
        {
            // We get all the Leagues a Coach is in
            List<League> leagues = Database.leagues.Where(league => league.ContainsCoach(idCoach)).ToList();

            // We send the Leagues
            Net.LIST_LEAGUE.Send(comm.GetStream(), leagues);
        }




        /// <summary>
        /// Sends all Coaches that belong to a League
        /// </summary>
        /// <param name="idLeague">Id of the League of which we seek the >Coaches</param>
        private void SendLeagueMembers(Guid idLeague)
        {
            // We initialize a list
            List<Coach> coaches = new List<Coach>();

            // We get the League
            League league = Database.LEAGUE.GetById(idLeague);

            // get all the Coaches in a League
            league.members.ForEach(member => coaches.Add(Database.COACH.GetById(member.idCoach)));

            // We remove all occurences of an incomplete Coach
            coaches.RemoveAll(coach => !coach.IsComplete);

            // We send the Leagues
            Net.LIST_COACH.Send(comm.GetStream(), coaches);
        }
    }
}