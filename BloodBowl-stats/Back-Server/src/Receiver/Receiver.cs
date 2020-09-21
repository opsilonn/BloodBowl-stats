using BloodBowl_Library;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Linq;


namespace Back_Server
{
    public partial class Receiver
    {
        private Server server;
        public TcpClient comm;

        // Creating a Delegate called CommunicationWithTheServer, and an event using it
        public delegate void CommunicationWithTheServer(TcpClient comm);
        public event CommunicationWithTheServer When_Server_Exit;
        public event CommunicationWithTheServer When_Server_LogIn;
        public event CommunicationWithTheServer When_Server_LogOff;

        // Creating a Delegate about creating some structures, and events using them
        public delegate void CreatingCoach(CoachWithPassword newCoach);
        public event CreatingCoach When_Coach_Create;

        public delegate void CreatingTeam(Team newTeam);
        public event CreatingTeam When_Team_Create;

        public delegate void PlayerEvent(Player player);
        public event PlayerEvent When_Player_Create;
        public event PlayerEvent When_Player_Remove;
        public event PlayerEvent When_Player_LevelsUp;

        public delegate void LeagueEvent(League league);
        public event LeagueEvent When_League_Create;

        public delegate void LeagueInvitationCoachEvent(InvitationCoach invitation);
        public event LeagueInvitationCoachEvent When_League_InvitationCoach_Create;
        public event LeagueInvitationCoachEvent When_League_InvitationCoach_Accept;
        public event LeagueInvitationCoachEvent When_League_InvitationCoach_Refuse;

        /*
        public delegate void LeagueInvitationTeamEvent(InvitationTeam invitation);
        public event LeagueInvitationTeamEvent When_League_InvitationTeam_Create;
        public event LeagueInvitationTeamEvent When_League_InvitationTeam_Accept;
        public event LeagueInvitationTeamEvent When_League_InvitationTeam_Refuse;
        */ 


        // Data of the User currently logged in
        public Coach userCoach;



        /// <summary>
        /// Constructor of the Class
        /// </summary>
        /// <param name="server">Reference of the server</param>
        /// <param name="comm">Communication</param>
        public Receiver(Server server, TcpClient comm)
        {
            this.server = server;
            this.comm = comm;
            userCoach = new Coach();
        }


        /// <summary>
        /// Server's HUB : it does stuff.
        /// </summary>
        public void DoOperation()
        {
            while (true)
            {
                // We receive a Communication from the client
                Communication communication = Net.COMMUNICATION.Receive(comm.GetStream());

                // We extract the data
                Instructions instruction = communication.instruction;
                Object content = communication.content;

                // We display the instruction
                CONSOLE.WriteLine(ConsoleColor.Cyan, "\nINSTRUCTION : " + instruction);

                // According to the instruction, we'll do a specific action
                switch (instruction)
                {
                    case Instructions.Exit_Software:
                        // We raise the event : a user left the software
                        When_Server_Exit?.Invoke(comm);
                        break;




                    // CREDENTIALS
                    case Instructions.LogIn:
                        if (LogIn((Credentials)content))
                        {
                            // We raise the event : a user has logged in
                            When_Server_LogIn?.Invoke(comm);
                        }
                        break;

                    case Instructions.SignIn:
                        // We get the profile Signed In
                        CoachWithPassword newCoach = SignIn((CoachWithPassword)content);

                        // If the Profile's ID is not Empty (means that the Sign In was successful)
                        if (newCoach.id != Guid.Empty)
                        {
                            // We raise the event : a user has logged in
                            When_Server_LogIn?.Invoke(comm);

                            // We raise the event : a Profile has been created
                            When_Coach_Create(newCoach);
                        }
                        break;

                    case Instructions.LogOut:
                        LogOut();
                        // We raise the event : a user has logged off
                        When_Server_LogOff?.Invoke(comm);
                        break;



                    // COACH
                    case Instructions.Coach_GetById:
                        GetCoachById((Guid)content);
                        break;

                    case Instructions.Coach_GetInvitations:
                        GetInvitationsCoach((Guid)content);
                        break;

                    case Instructions.Player_SearchByName:
                        SearchCoachByName((string)content);
                        break;



                    // TEAM
                    case Instructions.Team_New:
                        // We get the newly created Team
                        Team newTeam = NewTeam((Team)content);

                        // If the Team can be created (ID is not empty)
                        if (newTeam.IsComplete)
                        {
                            // We raise the event : a Team has been created
                            When_Team_Create?.Invoke(newTeam);
                        }
                        break;

                    case Instructions.Team_AddPlayer:
                        // We get the newly created Player
                        Player newPlayer = NewPlayer((Player)content);

                        // If the Player can be created (ID is not empty)
                        if (newPlayer.IsComplete)
                        {
                            // We raise the event : a Player has been created
                            When_Player_Create?.Invoke(newPlayer);
                        }
                        break;

                    case Instructions.Team_RemovePlayer:
                        // We get the Player to remove
                        Player playerToRemove = (Player)content;

                        // We remove him from the database
                        playerToRemove = RemovePlayer(playerToRemove);

                        // If the removal was successful)
                        if (playerToRemove.IsComplete)
                        {
                            // We raise the event : a Player has been removed
                            When_Player_Remove?.Invoke(playerToRemove);
                        }
                        break;



                    // PLAYER
                    case Instructions.Player_LevelUp:
                        // We get the Player to level up
                        Player player = (Player)content;

                        // We add a new Perk to the player
                        Perk? newPerk = PlayerLevelsUp(player);

                        // If a new effect was chosen
                        if (newPerk != null)
                        {
                            // We add the effect to the Player
                            player.perks.Add((Perk)newPerk);

                            // We raise the event : an Perk has been added
                            When_Player_LevelsUp?.Invoke(player);
                        }
                        break;



                    // LEAGUE
                    case Instructions.League_New:
                        // We get the newly created League
                        League newLeague = NewLeague((League)content);

                        // If the Team can be created (ID is not empty)
                        if (newLeague.IsComplete)
                        {
                            // We raise the event : a League has been created
                            When_League_Create?.Invoke(newLeague);
                        }
                        break;

                    case Instructions.League_GetAllForCoach:
                        SendLeagues((Guid)content);
                        break;

                    case Instructions.League_GetMembersData:
                        SendLeagueMembers((Guid)content);
                        break;

                    case Instructions.League_InviteCoachCreate:
                        InvitationCoach invitationCoach = (InvitationCoach)content;
                        if (InviteCoachToLeague(invitationCoach))
                        {
                            // We set the Invitation League to th eone of our Database (otherwise, there is conflict...)
                            invitationCoach.league = Database.LEAGUE.GetById(invitationCoach.league.id);

                            // We add the invitation to the League
                            invitationCoach.league.invitedCoaches.Add(invitationCoach);

                            // We raise the event : an Invitation has been created
                            When_League_InvitationCoach_Create(invitationCoach);
                        }
                        break;

                    case Instructions.League_InviteCoachAccept:
                        InvitationCoach invitationCoachToAccept = (InvitationCoach)content;
                        if (InvitationCoachAccepted(invitationCoachToAccept))
                        {
                            // We accept the invitation
                            invitationCoachToAccept.league.AcceptInvitationCoach(invitationCoachToAccept);

                            // We raise the event : an Invitation has been created
                            When_League_InvitationCoach_Accept(invitationCoachToAccept);
                        }
                        break;


                    // otherwise : Error (should not occur, but we're not taking any chance)
                    default:
                        CONSOLE.WriteLine(ConsoleColor.Red, "Message instruction not understood : " + instruction);
                        break;
                }
            }
        }
    }
}