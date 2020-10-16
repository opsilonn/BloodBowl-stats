using BloodBowl_Library;
using System;
using System.IO;
using System.Linq;


namespace Back_Server
{
    public static partial class Database
    {
        /// <summary>
        /// All methods used with the class Competition linked to the Database
        /// </summary>
        [assembly: InternalsVisibleTo("LEAGUE")]
        public static class COMPETITION
        {
            /// <summary>
            /// Reads a given JSON file containing a Competition, and adds it to the database
            /// </summary>
            /// <param name="file">File to read</param>
            internal static Competition Read(FileInfo file)
            {
                // Creating a new default Competition (for security purposes)
                Competition newCompetition = new Competition();

                try
                {
                    // We read the file
                    string json = System.IO.File.ReadAllText(file.FullName);
                    newCompetition = Competition.Deserialize(json);

                    // Ensuring we have completed fields
                    if (newCompetition.IsComplete)
                    {
                        // We give the reference to the Competition to each match
                        // newCompetition.players.ForEach(player => player.team = newCompetition);

                        // returning the new Competition
                        return newCompetition;
                    }
                }
                catch (Exception)
                {
                }

                // otherwise, return null
                return newCompetition;
            }


            /// <summary>
            /// Writes a Competition structure into a JSON file
            /// </summary>
            /// <param name="competition">Competition to transcribe into a JSON file</param>
            /// <returns> Whether the save worked or not</returns>
            public static bool Write(Competition competition)
            {
                // If this Competition has a valid League
                // If the League's path exists
                // -> we can write it
                Console.WriteLine(competition.league.IsComplete);
                Console.WriteLine(Directory.Exists(pathLeagueFolder(competition.league)));
                if (competition.league.IsComplete && Directory.Exists(pathLeagueFolder(competition.league)))
                {
                    // Determine whether the directory exists : if not, we create it.
                    if (!Directory.Exists(pathCompetitionFolder(competition)))
                    {
                        Directory.CreateDirectory(pathCompetitionFolder(competition));
                    }

                    // Get the json path
                    string path = pathCompetitionJson(competition);

                    // Convert the instance into a string
                    string json = competition.Serialize();

                    // Write the JSON into the file
                    System.IO.File.WriteAllText(path, json);

                    // Return that the save has been completed
                    return true;
                }

                // If reached : something did not work
                return false;
            }


            /// <summary>
            /// Returns a Competition given its name (if it exists)
            /// </summary>
            /// <param name="name"> Name of the Competition we are searching for </param>
            /// <returns> the Competition of a given name (if it exists) </returns>
            public static Competition GetByName(string name)
            {
                // We get the Competition, if any
                Competition competition = competitions.FirstOrDefault(c => c.name == name);

                // We return it if found, otherwise we return a default instance
                return (competition != null) ? competition : new Competition();
            }


            /// <summary>
            /// Returns a Competition given its ID (if it exists)
            /// </summary>
            /// <param name="id"> ID of the Competition </param>
            /// <returns> the Competition of a given ID (if it exists) </returns>
            public static Competition GetById(Guid id)
            {
                // We get the Competition, if any
                Competition competition = competitions.FirstOrDefault(c => c.id == id);

                // We return it if found, otherwise we return a default instance
                return (competition != null) ? competition : new Competition();
            }
        }
    }
}
