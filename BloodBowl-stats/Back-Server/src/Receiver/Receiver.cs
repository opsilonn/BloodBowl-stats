using BloodBowl_Library;
using System;
using System.Net.Sockets;


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
        public event LeagueEvent When_Member_Leaves_League;

        public delegate void LeagueInvitationCoachEvent(InvitationCoach invitation);
        public event LeagueInvitationCoachEvent When_League_InvitationCoach_Create;
        public event LeagueInvitationCoachEvent When_League_InvitationCoach_Accept;
        public event LeagueInvitationCoachEvent When_League_InvitationCoach_Refuse;

        public delegate void LeagueExpulsionCoachEvent(ExpulsionCoach expelled);
        public event LeagueExpulsionCoachEvent When_League_Expel_Coach;

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
                        LogIn((Credentials)content);
                        break;

                    case Instructions.SignIn:
                        SignIn((CoachWithPassword)content);
                        break;

                    case Instructions.LogOut:
                        LogOut();
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
                        NewTeam((Team)content);
                        break;

                    case Instructions.Team_AddPlayer:
                        NewPlayer((Player)content);
                        break;

                    case Instructions.Team_RemovePlayer:
                        RemovePlayer((Player)content);
                        break;



                    // PLAYER
                    case Instructions.Player_LevelUp:
                        PlayerLevelsUp((Player)content);
                        break;



                    // LEAGUE
                    case Instructions.League_New:
                        NewLeague((League)content);
                        break;

                    case Instructions.League_GetAllForCoach:
                        SendLeagues((Guid)content);
                        break;

                    case Instructions.League_GetMembersData:
                        SendLeagueMembers((Guid)content);
                        break;

                    case Instructions.League_InviteCoachCreate:
                        InviteCoachToLeague((InvitationCoach)content);
                        break;

                    case Instructions.League_InviteCoachAccept:
                        InvitationCoachAccept((InvitationCoach)content);
                        break;

                    case Instructions.League_InviteCoachRefuse:
                        InvitationCoachRefuse((InvitationCoach)content);
                        break;

                    case Instructions.League_RemoveCoach:
                        RemoveCoachFromLeague((ExpulsionCoach)content);
                        break;

                    case Instructions.League_Coach_Leave:
                        LeagueLeaveCoach((League)content);
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