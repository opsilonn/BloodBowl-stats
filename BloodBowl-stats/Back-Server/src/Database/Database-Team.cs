using BloodBowl_Library;
using System;
using System.IO;
using System.Linq;

namespace Back_Server
{
    public static partial class Database
    {
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
                    if (newTeam.IsComplete)
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
                // If this Team has a valid Coach
                // If the Coach's path exists
                // -> we can write it
                if (team.coach.IsComplete && Directory.Exists(pathCoachFolder(team.coach)))
                {
                    // Get the path
                    string path = pathTeamJson(team);

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
            /// Removes a Team instance from the file database
            /// </summary>
            /// <param name="team">Team to remove</param>
            /// <returns>Whether the Removal worked or not</returns>
            public static bool Remove(Team team)
            {
                // Get the path
                string path = pathTeamJson(team);

                if(File.Exists(path))
                {
                    File.Delete(pathTeamJson(team));
                    return true;
                }
                else
                {
                    CONSOLE.WriteLine(ConsoleColor.Red, "File for Team " + team.name + " not found");
                    return false;
                }
            }



            /// <summary>
            /// Returns a Team given its name (if it exists)
            /// </summary>
            /// <param name="name"> Name of the Team we are searching for </param>
            /// <returns> the Team of a given name (if it exists) </returns>
            public static Team GetByName(string name)
            {
                // We get the Team, if any
                Team team = teams.FirstOrDefault(t => t.name == name);

                // We return it if found, otherwise we return a default instance
                return (team != null) ? team : new Team();
            }


            /// <summary>
            /// Returns a Team given its ID (if it exists)
            /// </summary>
            /// <param name="id"> ID of the Team </param>
            /// <returns> the Team of a given ID (if it exists) </returns>
            public static Team GetById(Guid id)
            {
                // We get the Team, if any
                Team team = teams.FirstOrDefault(t => t.id == id);

                // We return it if found, otherwise we return a default instance
                return (team != null) ? team : new Team();
            }
        }
    }
}
