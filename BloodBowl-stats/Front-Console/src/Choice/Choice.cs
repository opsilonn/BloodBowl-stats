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
            ConsoleKeyInfo input;
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
                ConsoleColor color = ConsoleColor.White;

                foreach (string s in choices)
                {
                    if (index == currentChoice)
                        Console.Write("     --> ");
                    else
                        Console.Write("         ");

                    // We change the color to Blue if we reach the final statement (usually one saying "Go back" or "Log out")
                    if (index == MAX)
                    {
                        color = ConsoleColor.Blue;
                    }

                    CONSOLE.WriteLine(color, choices[index++] + "\n");
                }


                // We read the input
                input = Console.ReadKey();


                // If it is an Array key (UP or DOWN), we modify our choice accordingly
                if (input.Key == ConsoleKey.UpArrow)
                    currentChoice--;
                if (input.Key == ConsoleKey.DownArrow)
                    currentChoice++;
                // If it is a LEFT Array key : go to the first choice (index = 0)
                if (input.Key == ConsoleKey.LeftArrow)
                    currentChoice = 0;
                // If it is a RIGHT Array key : go to the last choice (index = MAX)
                if (input.Key == ConsoleKey.RightArrow)
                    currentChoice = MAX;


                // If the value goes too low / too high, it goes to the other extreme
                if (currentChoice < MIN)
                    currentChoice = MAX;
                if (currentChoice > MAX)
                    currentChoice = MIN;
            }
            while (input.Key != ConsoleKey.Enter);


            // We return the value
            return currentChoice;
        }
    }
}
