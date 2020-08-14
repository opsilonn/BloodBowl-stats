using BloodBowl_Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_Server
{
    public static class Database
    {
        public static List<Coach> coaches;
        public static List<Credentials> credentials;
        public static List<Team> teams;



        // Some useful stuff
        private static string sourcePath = @"../../database";
        private static string pathCoaches { get { return sourcePath + "/Coaches"; } }
        private static string pathCoach(Coach coach) { return pathCoaches + "/" + coach.name; }
        private static string pathCoachData(Coach coach) { return pathCoaches + "/" + coach.name + "/coach_" + coach.name + ".json"; }
        private static string pathTeam(Team team) { return pathCoach(team.coach); }
        private static string pathTeamData(Team team) { return pathTeam(team) + "/team_" + team.name + ".json"; }



        /// <summary>
        /// Fills the database by reading all files
        /// </summary>
        public static void FillDatabase()
        {
            // Initializing the variables
            coaches = new List<Coach>();
            credentials = new List<Credentials>();
            teams = new List<Team>();

            // Filling the lists
            COACH.ReadAll();
        }



        /// <summary>
        /// All methods used with the class Coach linked to the Database
        /// </summary>
        public static class COACH
        {
            /// <summary>
            /// Goes through all the folders containing Coach's data, and add them to the database accordingly
            /// </summary>
            public static void ReadAll()
            {
                // We get all the folders in the COACH section
                DirectoryInfo di = new DirectoryInfo(pathCoaches);

                // Foreach folder, we extract its Coach
                foreach (DirectoryInfo directory in di.GetDirectories())
                {
                    // Reading the Coach's data
                    Read(directory);
                }
            }



            /// <summary>
            /// Gets a folder containing all data of a Coach, and returns a corresponding Coach instance
            /// </summary>
            /// <param name="directory">Directory where all the Coach's data is stored</param>
            /// <returns>Whether the method worked or not</returns>
            private static bool Read(DirectoryInfo directory)
            {
                // Creating a new default Coach (for security purposes)
                Coach newCoach = new Coach();
                Credentials newCredentials = new Credentials();

                try
                {
                    // We get the COACH file
                    string coachPath = String.Format("{0}\\coach_{1}.json", directory.FullName, directory.Name);

                    // We read the json
                    string json = System.IO.File.ReadAllText(coachPath);

                    // We fill our instances according to the JSON file
                    newCoach = Coach.Deserialize(json);
                    newCredentials = Credentials.Deserialize(json);

                    // For all the Team files in the Directory
                    foreach (FileInfo file in directory.GetFiles("team_*.json"))
                    {
                        // We read the Team
                        Team newTeam = TEAM.Read(file);

                        // We add it if correct
                        if (newTeam.IsComplete)
                        {
                            // We set the Coach and Team values respectively in both instances
                            newCoach.teams.Add(newTeam);
                            newTeam.coach = newCoach;

                            // We add the team to the List
                            teams.Add(newTeam);
                        }
                    }

                    // If the instances are complete (all fields are OK)
                    if (newCoach.IsComplete && newCredentials.IsComplete)
                    {
                        // We sort the teams in chronological order (older first, then younger)
                        teams.Sort((x, y) => x.dateCreation.CompareTo(y.dateCreation));

                        // We add them to their respective lists
                        coaches.Add(newCoach);
                        credentials.Add(newCredentials);

                        // It worked !
                        return true;
                    }
                }
                catch (Exception)
                {
                }

                // It didn't work.
                return false;
            }


            /// <summary>
            /// Writes a Coach structure into a JSON file (more precisely, both a Coach and its password, contained in the according Credentials instance
            /// </summary>
            /// <param name="coach">Coach to transcribe into a JSON file</param>
            public static bool Write(Coach coach)
            {
                try
                {
                    // Get the folder's path
                    string pathFolder = pathCoach(coach);

                    // Determine whether the directory exists : if not, we create it.
                    if (!Directory.Exists(pathFolder))
                    {
                        Directory.CreateDirectory(pathFolder);
                    }

                    // We also need the password !
                    // Hence, we need to get the credentials
                    Credentials credentials = CREDENTIALS.GetById(coach.id);

                    // We put all the data into a single instance : a Coach With a Password
                    CoachWithPassword coachWithPassword = new CoachWithPassword(coach, credentials);


                    // Get the JSON file's path
                    string pathJson = pathCoachData(coach);

                    // Convert the instance into a string
                    string json = coachWithPassword.Serialize();

                    // Write the JSON into the file
                    System.IO.File.WriteAllText(pathJson, json);
                    
                    // It worked !
                    return true;
                }
                catch (Exception)
                {
                }

                // It didn't work.
                return false;
            }


            /// <summary>
            /// Returns a Coach given its name (if it exists)
            /// </summary>
            /// <param name="name"> Name of the Coach we are searching for </param>
            /// <returns> the Coach of a given name (if it exists) </returns>
            public static Coach GetByName(string name)
            {
                // We iterate through all the profiles
                foreach (Coach coach in coaches)
                {
                    // If we find a similar Coach in the Database
                    if (coach.name == name)
                    {
                        return coach;
                    }
                }

                // otherwise, we return default Coach
                return new Coach();
            }


            /// <summary>
            /// Returns a Coach given its ID (if it exists)
            /// </summary>
            /// <param name="id"> ID of the Coach </param>
            /// <returns> the Coach of a given ID (if it exists) </returns>
            public static Coach GetById(Guid id)
            {
                // We iterate through all the profiles
                foreach (Coach coach in coaches)
                {
                    // If we find a similar Coach in the Database
                    if (coach.id == id)
                    {
                        return coach;
                    }
                }

                // otherwise, we return default Coach
                return new Coach();
            }
        }



        /// <summary>
        /// All methods used with the class Credential linked to the Database
        /// </summary>
        public static class CREDENTIALS
        {
            /// <summary>
            /// Returns a Credentials given its ID (if it exists)
            /// </summary>
            /// <param name="id"> ID of the CoCredentialsach </param>
            /// <returns> the Credentials of a given ID (if it exists) </returns>
            public static Credentials GetById(Guid id)
            {
                // We iterate through all the profiles
                foreach (Credentials credential in credentials)
                {
                    // If we find a similar Coach in the Database
                    if (credential.id == id)
                    {
                        return credential;
                    }
                }

                // otherwise, we return default Coach
                return new Credentials();
            }
        }




        /// <summary>
        /// All methods used with the class Team linked to the Database
        /// </summary>
        [assembly: InternalsVisibleTo("COACH")]
        public static class TEAM
        {
            /// <summary>
            /// Reads a given JSON file containing a Team, and adds it to the database
            /// </summary>
            /// <param name="file">File to read</param>
            internal static Team Read(FileInfo file)
            {
                // Creating a new default Team (for security purposes)
                Team newTeam = new Team();

                try
                {
                    // We read the file
                    string json = System.IO.File.ReadAllText(file.FullName);
                    newTeam = Team.Deserialize(json);

                    // Ensuring we have completed fields
                    if (newTeam.id != Guid.Empty && newTeam.name != "")
                    {
                        // We give the reference to the Team to each player
                        newTeam.players.ForEach(player => player.team = newTeam);

                        // returning the new Team
                        return newTeam;
                    }
                }
                catch (Exception)
                {
                }

                // otherwise, return null
                return newTeam;
            }


            /// <summary>
            /// Writes a Team structure into a JSON file
            /// </summary>
            /// <param name="team">Team to transcribe into a JSON file</param>
            /// <returns> Whether the save worked or not</returns>
            public static bool Write(Team team)
            {
                // If this team has a valid coach
                // If the coach's path exists
                // -> we can write it
                if (team.coach.IsComplete && Directory.Exists(pathTeam(team)))
                {
                    // Get the path
                    string path = pathTeamData(team);

                    // Convert the instance into a string
                    string json = team.Serialize();

                    // Write the JSON into the file
                    System.IO.File.WriteAllText(path, json);

                    // Return that the save has been completed
                    return true;
                }

                // If reached : something did not work
                return false;
            }


            /// <summary>
            /// Returns a Team given its name (if it exists)
            /// </summary>
            /// <param name="name"> Name of the Team we are searching for </param>
            /// <returns> the Team of a given name (if it exists) </returns>
            public static Team GetByName(string name)
            {
                // We iterate through all the profiles
                foreach (Team team in teams)
                {
                    // If we find a similar Team in the Database
                    if (team.name == name)
                    {
                        return team;
                    }
                }

                // otherwise, we return null
                return null;
            }


            /// <summary>
            /// Returns a Team given its ID (if it exists)
            /// </summary>
            /// <param name="id"> ID of the Team </param>
            /// <returns> the Team of a given ID (if it exists) </returns>
            public static Team GetById(Guid id)
            {
                // We iterate through all the profiles
                foreach (Team team in teams)
                {
                    // If we find a similar Coach in the Database
                    if (team.id == id)
                    {
                        return team;
                    }
                }

                // otherwise, we return null
                return null;
            }
        }
    }
}
