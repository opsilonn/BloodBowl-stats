using BloodBowl_Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Back_Server
{
    public static partial class Database
    {
        public static List<Coach> coaches;
        public static List<Credentials> credentials;
        public static List<Team> teams;
        public static List<League> leagues;



        // Some useful stuff
        private static string rootPath = @"../../database";
        private static string rootPathCoaches { get { return rootPath + "/Coaches"; } }
        private static string rootPathLeagues { get { return rootPath + "/Leagues"; } }

        private static string pathCoach(Coach coach) { return rootPathCoaches + "/" + coach.name; }
        private static string pathCoachData(Coach coach) { return rootPathCoaches + "/" + coach.name + "/coach_" + coach.name + ".json"; }
        private static string pathCoachData(DirectoryInfo di) { return rootPathCoaches + "/" + di.Name + "/coach_" + di.Name + ".json"; }

        private static string pathTeam(Team team) { return pathCoach(team.coach); }
        private static string pathTeamData(Team team) { return pathTeam(team) + "/team_" + team.name + ".json"; }

        private static string pathLeague(League league) { return rootPathLeagues + "/" + league.name; }
        private static string pathLeagueData(League league) { return rootPathLeagues + "/" + league.name + "/league_" + league.name + ".json"; }
        private static string pathLeagueData(DirectoryInfo di) { return rootPathLeagues + "/" + di.Name + "/league_" + di.Name + ".json"; }



        /// <summary>
        /// Fills the database by reading all files
        /// </summary>
        public static void FillDatabase()
        {
            // Initializing the variables
            coaches = new List<Coach>();
            credentials = new List<Credentials>();
            teams = new List<Team>();
            leagues = new List<League>();

            // Filling the lists
            COACH.ReadAll();
            LEAGUE.ReadAll();
        }
    }
}
