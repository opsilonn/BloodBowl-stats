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
        public void NewLeague(League leagueReceived)
        {
            // We initialize a new League (default has a null Id)
            League newLeague = new League();

            // We make a last verification here
            if (leagueReceived.IsComplete)
            {
                // Success !
                // We create a new Team instance from the data received
                newLeague = new League(leagueReceived.name, leagueReceived.creator);

                // We raise the event : a League has been created
                When_League_Create?.Invoke(newLeague);
            }

            // We return the id of the newly created team (error : default empty id; success : correct id)
            Net.GUID.Send(comm.GetStream(), newLeague.id);
        }




        /// <summary>
        /// Sends all Leagues related to a Coach
        /// </summary>
        /// <param name="idCoach">Id of the Coach of which we seek the Leagues</param>
        private void SendLeagues(Guid idCoach)
        {
            // We get all the Leagues a Coach is in
            List<League> leagues = Database.leagues.Where(league => league.ContainsMember(idCoach)).ToList();

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




        /// <summary>
        /// Manage the invitation of a new Coach to a League
        /// </summary>
        /// <param name="invitationReceived">InvitationCoach received</param>
        public void InviteCoachToLeague(InvitationCoach invitationReceived)
        {
            // We initialize an instance, that tells us if the received invitation is valid or not
            InvitationCoach invitationNew = new InvitationCoach();

            // First, we search the correct League in the database
            League league = Database.LEAGUE.GetById(invitationReceived.league.id);
            
            // We check if the league is valid
            if (league.IsComplete)
            {
                // Second, we check the invitor
                JobAttribution invitor = league.GetMember(invitationReceived.idInvitor);

                // we check if the invitor is in the League
                // AND if he is allowed to perform an invitation
                // AND if he is allowed to give that role
                if (invitor.IsComplete
                    && invitor.job.canAddPlayer()
                    && invitor.job.JobsItCanPropose().Contains(invitationReceived.job))
                {
                    // Third, we check the invited
                    Coach invited = Database.COACH.GetById(invitationReceived.idInvited);

                    // We verify if the invited Coach exists
                    // AND that that he is not already a member of the League
                    // AND that he hasn't already been invited with this very Job
                    // (if he has for another Job, we consider the invitation still valid)
                    if (invited.IsComplete
                        && !league.ContainsMember(invitationReceived.idInvited)
                        && !league.ContainsSimilarInvitedCoach(invitationReceived))
                    {
                        // Sounds good, we can reallocate the Invitation with server data
                        invitationNew = new InvitationCoach(league, invitor.coach, invited, invitationReceived.job);

                        // We add the invitation to the League
                        league.invitedCoaches.Add(invitationNew);

                        // We raise the event : an Invitation has been created
                        When_League_InvitationCoach_Create(invitationNew);
                    }
                }
            }

            // We return to the user whether it worked or not
            Net.BOOL.Send(comm.GetStream(), invitationNew.IsComplete);
        }


        /// <summary>
        /// Manage the acception of an invitation of a new Coach to a League
        /// </summary>
        /// <param name="invitationReceived"></param>
        public void InvitationCoachAccept(InvitationCoach invitationReceived)
        {
            // We initialize an InvitationCoach to a default instance
            // If the invitation is found to be valid, it'll transform to a regular InvitationCoach
            InvitationCoach invitationFromServer = new InvitationCoach();

            // First, we search the correct League in the database
            League league = Database.LEAGUE.GetById(invitationReceived.idLeague);

            // We check if the League is valid
            if (league.IsComplete)
            {
                // We get the invitation instance from the league
                invitationReceived = league.invitedCoaches.FirstOrDefault(invit =>
                    invit.idInvitor == invitationReceived.idInvitor
                    && invit.idInvited == invitationReceived.idInvited
                    && invit.job == invitationReceived.job);

                // If the invitation is valid
                if (invitationReceived != null && invitationReceived.IsComplete)
                {
                    // We accept the invitation
                    invitationReceived.league.AcceptInvitationCoach(invitationReceived);

                    // We raise the event : an Invitation has been accepted
                    When_League_InvitationCoach_Accept(invitationReceived);
                }
            }


            // We return to the user whether it worked or not
            Net.BOOL.Send(comm.GetStream(), (invitationReceived != null && invitationReceived.IsComplete));

        }


        /// <summary>
        /// Manage the refusal of an invitation of a new Coach to a League
        /// </summary>
        /// <param name="invitationReceived"></param>
        public void InvitationCoachRefuse(InvitationCoach invitationReceived)
        {
            // We initialize an InvitationCoach to a default instance
            // If the invitation is found to be valid, it'll transform to a regular InvitationCoach
            InvitationCoach invitationFromServer = new InvitationCoach();

            // First, we search the correct League in the database
            League league = Database.LEAGUE.GetById(invitationReceived.idLeague);

            // We check if the League is valid
            if (league.IsComplete)
            {
                // We get the invitation instance from the league
                invitationReceived = league.invitedCoaches.FirstOrDefault(invit =>
                    invit.idInvitor == invitationReceived.idInvitor
                    && invit.idInvited == invitationReceived.idInvited
                    && invit.job == invitationReceived.job);

                // If the invitation is valid
                if (invitationReceived != null && invitationReceived.IsComplete)
                {
                    // We accept the invitation
                    invitationReceived.league.RefuseInvitationCoach(invitationReceived);

                    // We raise the event : an Invitation has been refused
                    When_League_InvitationCoach_Refuse(invitationReceived);
                }
            }

            // We return to the user whether it worked or not
            Net.BOOL.Send(comm.GetStream(), (invitationReceived != null && invitationReceived.IsComplete));
        }
    }
}