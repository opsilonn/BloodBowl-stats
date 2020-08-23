using BloodBowl_Library;
using System;
using System.Collections.Generic;


namespace Front_Console
{
    /// <summary>
    /// Structure of a Choice : a message and several propositions
    /// </summary>
    public class Choice
    {
        public string message;
        public List<string> choices;

        /// <summary>
        /// Default constructor of the instance
        /// </summary>
        public Choice()
        {
            message = "";
            choices = new List<string>();
        }


        /// <summary>
        /// Complete constructor of the instance
        /// </summary>
        /// <param name="message"> Message of the instance </param>
        /// <param name="choices"> List of choices of the instance </param>
        public Choice(string message, List<string> choices)
        {
            this.message = message;
            this.choices = choices;
        }

        public int GetChoice()
        {
            // We initialize our variables
            bool continuing = true;
            int currentChoice = 0;
            int MIN = 0;
            int MAX = choices.Count - 1;


            // While the user doesn't press Enter, the loop continues
            do
            {
                // We clear the console, and display a given message
                Console.Clear();
                CONSOLE.WriteLine(ConsoleColor.Blue, message + "\n\n");


                // We display all our choices (and we highlight the current choice)
                int index = 0;

                foreach (string s in choices)
                {
                    if (index == currentChoice)
                        Console.Write("     --> ");
                    else
                        Console.Write("         ");

                    // We set the color to Blue if we reach the final statement (usually one saying "Go back" or "Log out")
                    ConsoleColor color = (index == MAX) ? ConsoleColor.Blue : ConsoleColor.White;


                    CONSOLE.WriteLine(color, choices[index++] + "\n");
                }


                // We read the input
                switch(Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        currentChoice--;
                        // If the value goes too low, it goes to the other extreme
                        if (currentChoice < MIN)
                            currentChoice = MAX;
                        break;

                    case ConsoleKey.DownArrow:
                        currentChoice++;
                        // If the value goes too high, it goes to the other extreme
                        if (MAX < currentChoice)
                            currentChoice = MIN;
                        break;

                    case ConsoleKey.LeftArrow:
                        currentChoice = MIN;
                        break;

                    case ConsoleKey.RightArrow:
                        currentChoice = MAX;
                        break;

                    case ConsoleKey.Enter:
                        continuing = false;
                        break;

                    case ConsoleKey.Escape:
                        continuing = false;
                        currentChoice = MAX;
                        break;
                }
            }
            while (continuing);


            // We return the value
            return currentChoice;
        }
    }
}
