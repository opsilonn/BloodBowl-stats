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
        /// <summary>
        /// All methods used with the class League linked to the Database
        /// </summary>
        public static class LEAGUE
        {
            /// <summary>
            /// Goes through all the folders containing League's data, and add them to the database accordingly
            /// </summary>
            public static void ReadAll()
            {
                // We get all the folders in the LEAGUE section
                DirectoryInfo di = new DirectoryInfo(rootPathLeagues);

                try
                {
                    // Foreach folder, we extract its League
                    foreach (DirectoryInfo directory in di.GetDirectories())
                    {
                        // Reading the League's data
                        Read(directory);
                    }
                }
                catch(Exception)
                {
                    CONSOLE.WriteLine(ConsoleColor.Magenta, "\nCOULD NOT READ THE LEAGUES");
                }
            }



            /// <summary>
            /// Gets a folder containing all data of a League, and returns a corresponding League instance
            /// </summary>
            /// <param name="di">Directory Info where all the League's data is stored</param>
            /// <returns>Whether the method worked or not</returns>
            private static bool Read(DirectoryInfo di)
            {
                // Creating a new default League (for security purposes)
                League newLeague = new League();

                try
                {
                    // We get the LEAGUE file
                    string leaguePath = pathLeagueData(di); // String.Format("{0}\\league_{1}.json", directoryInfo.FullName, directoryInfo.Name);

                    // We read the json
                    string json = System.IO.File.ReadAllText(leaguePath);

                    // We fill our instances according to the JSON file
                    newLeague = League.Deserialize(json);

                    // For all the Team files in the Directory
                    /*
                    foreach (FileInfo file in directory.GetFiles("team_*.json"))
                    {
                        // We read the Team
                        Team newTeam = TEAM.Read(file);

                        // We add it if correct
                        if (newTeam.IsComplete)
                        {
                            // We set the Coach and Team values respectively in both instances
                            newLeague.teams.Add(newTeam);
                            newTeam.coach = newLeague;

                            // We add the team to the List
                            teams.Add(newTeam);
                        }
                    }
                    */
                    // We associate to each Member the correct link to its coach data
                    newLeague.members.ForEach(member => member.coach = COACH.GetById(member.idCoach));

                    // We remove all the useless links
                    newLeague.members.RemoveAll(member => !member.IsComplete);

                    // If the instance is complete (all fields are OK)
                    if (newLeague.IsComplete)
                    {
                        // We add it to its respective list
                        leagues.Add(newLeague);

                        // It worked !
                        return true;
                    }
                }
                catch (Exception)
                {
                    CONSOLE.WriteLine(ConsoleColor.Red, "\nERROR WITH LEAGUE : " + di.Name);
                }

                // It didn't work.
                return false;
            }


            /// <summary>
            /// Writes a Coach structure into a JSON file (more precisely, both a Coach and its password, contained in the according Credentials instance
            /// </summary>
            /// <param name="league">League to transcribe into a JSON file</param>
            public static bool Write(League league)
            {
                try
                {
                    // Get the folder's path
                    string pathFolder = pathLeague(league);

                    // Determine whether the directory exists : if not, we create it.
                    if (!Directory.Exists(pathFolder))
                    {
                        Directory.CreateDirectory(pathFolder);
                    }

                    // Get the JSON file's path
                    string pathJson = pathLeagueData(league);

                    // Convert the instance into a string
                    string json = league.Serialize();

                    // Write the JSON into the file
                    System.IO.File.WriteAllText(pathJson, json);

                    // It worked !
                    return true;
                }
                catch (Exception)
                {
                    Console.WriteLine("ERROR while writing the League {0} - {1} into the database", league.id, league.name);
                }

                // It didn't work.
                return false;
            }


            /// <summary>
            /// Returns a League given its name (if it exists)
            /// </summary>
            /// <param name="name"> Name of the League we are searching for </param>
            /// <returns> the League of a given name (if it exists) </returns>
            public static League GetByName(string name)
            {
                // We iterate through all the profiles
                foreach (League league in leagues)
                {
                    // If we find a similar Coach in the Database
                    if (league.name == name)
                    {
                        return league;
                    }
                }

                // otherwise, we return default League
                return new League();
            }


            /// <summary>
            /// Returns a League given its ID (if it exists)
            /// </summary>
            /// <param name="id"> ID of the League </param>
            /// <returns> the League of a given ID (if it exists) </returns>
            public static League GetById(Guid id)
            {
                // We iterate through all the profiles
                foreach (League league in leagues)
                {
                    // If we find a similar League in the Database
                    if (league.id == id)
                    {
                        return league;
                    }
                }

                // otherwise, we return default League
                return new League();
            }
        }
    }
}
