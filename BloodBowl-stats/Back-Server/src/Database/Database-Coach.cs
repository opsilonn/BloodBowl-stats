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
                DirectoryInfo di = new DirectoryInfo(rootPathCoaches);

                try
                {
                    // Foreach folder, we extract its Coach
                    foreach (DirectoryInfo directory in di.GetDirectories())
                    {
                        // Reading the Coach's data
                        Read(directory);
                    }
                }
                catch (Exception)
                {
                    CONSOLE.WriteLine(ConsoleColor.Magenta, "\nCOULD NOT READ THE COACHES");
                }
            }



            /// <summary>
            /// Gets a folder containing all data of a Coach, and returns a corresponding Coach instance
            /// </summary>
            /// <param name="di">Directory Info where all the Coach's data is stored</param>
            /// <returns>Whether the method worked or not</returns>
            private static bool Read(DirectoryInfo di)
            {
                // Creating a new default Coach (for security purposes)
                Coach newCoach = new Coach();
                Credentials newCredentials = new Credentials();

                try
                {
                    // We get the COACH file
                    string coachPath = pathCoachData(di); // String.Format("{0}\\coach_{1}.json", directory.FullName, directory.Name);

                    // We read the json
                    string json = System.IO.File.ReadAllText(coachPath);

                    // We fill our instances according to the JSON file
                    newCoach = Coach.Deserialize(json);
                    newCredentials = Credentials.Deserialize(json);

                    // For all the Team files in the Directory
                    foreach (FileInfo file in di.GetFiles("team_*.json"))
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
                        newCoach.teams.Sort((x, y) => x.dateCreation.CompareTo(y.dateCreation));

                        // We add them to their respective lists
                        coaches.Add(newCoach);
                        credentials.Add(newCredentials);

                        // It worked !
                        return true;
                    }
                }
                catch (Exception)
                {
                    CONSOLE.WriteLine(ConsoleColor.Red, "ERROR WITH COACH : " + di.Name);
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


                    // We initialize a default instance of a CoachWithPassword
                    CoachWithPassword coachWithPassword = new CoachWithPassword();

                    // Is the instance a regular Coach, or a CoachWithPassword ?
                    // If it is a CoachWithPassword, then we're good to go
                    if (coach is CoachWithPassword)
                    {
                        coachWithPassword = (CoachWithPassword)coach;
                    }
                    // If it is a regular Coach, we need to get its password !!
                    else
                    {
                        // To begin with, we need to get the credentials
                        Credentials credentials = CREDENTIALS.GetById(coach.id);

                        // Then, we put all the data into a single instance : a Coach With a Password
                        coachWithPassword = new CoachWithPassword(coach, credentials);
                    }


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
    }
}
