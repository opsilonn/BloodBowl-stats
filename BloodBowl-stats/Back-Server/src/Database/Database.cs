using BloodBowl_Library;
using System.Collections.Generic;
using System.IO;


namespace Back_Server
{
    public static partial class Database
    {
        public static List<Coach> coaches;
        public static List<Credentials> credentials;
        public static List<Team> teams;
        public static List<League> leagues;
        public static List<Competition> competitions;



        // Some useful stuff
        private static string rootPath = @"../../database";
        private static string rootPathCoaches { get { return rootPath + "/Coaches"; } }
        private static string rootPathLeagues { get { return rootPath + "/Leagues"; } }

        private static string pathCoachFolder(Coach coach) { return rootPathCoaches + "/" + coach.name; }
        private static string pathCoachJson(Coach coach) { return rootPathCoaches + "/" + coach.name + "/coach_" + coach.name + ".json"; }
        private static string pathCoachJson(DirectoryInfo di) { return rootPathCoaches + "/" + di.Name + "/coach_" + di.Name + ".json"; }

        private static string pathTeamFolder(Team team) { return pathCoachFolder(team.coach); }
        private static string pathTeamJson(Team team) { return pathTeamFolder(team) + "/team_" + team.name + ".json"; }

        private static string pathLeagueFolder(League league) { return rootPathLeagues + "/" + league.name; }
        private static string pathLeagueJson(League league) { return rootPathLeagues + "/" + league.name + "/league_" + league.name + ".json"; }
        private static string pathLeagueJson(DirectoryInfo di) { return rootPathLeagues + "/" + di.Name + "/league_" + di.Name + ".json"; }

        private static string pathCompetitionFolder(Competition competition) { return pathLeagueFolder(competition.league) + "/" + competition.name; }
        private static string pathCompetitionJson(Competition competition) { return pathCompetitionFolder(competition) + "/" + competition.name + ".json"; }


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
            competitions = new List<Competition>();

            // Filling the lists
            COACH.ReadAll();
            LEAGUE.ReadAll();
        }
    }
}