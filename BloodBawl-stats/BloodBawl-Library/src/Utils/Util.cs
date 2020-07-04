using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBawl_Library
{
    public static class Util
    {
        /// <summary>
        /// Returns whether a string contains correct inputs
        /// </summary>
        /// <param name="input"> The string entered by the user </param>
        /// <returns> Whether the input verifies some conditions or not </returns>
        public static bool CorrectInput(string input)
        {
            // Foreach character in the string
            foreach (char c in input)
            {
                // We get the ascii value of the current char
                int unicode = c;

                // We do some verifications
                bool space = (unicode == 32);
                bool number = (48 <= unicode && unicode <= 57);
                bool letterUpper = (65 <= unicode && unicode <= 90);
                bool letterLower = (97 <= unicode && unicode <= 122);
                bool punctuation = (c == '?' || c == '!' || c == '.' || c == ',' || c == ';');
                bool specialChars = (c == '@' || c == '\'' || c == '-' || c == '<' || c == '>');


                // If the current char doesn't pas a single verification, we return false
                if (!space && !number && !letterUpper && !letterLower && !punctuation && !specialChars)
                {
                    return false;
                }
            }

            // Otherwise, we return true
            return true;
        }



        /// <summary>
        /// Returns whether or not a given string is valid according to our syntax properties : only letters, numbers and spaces
        /// </summary>
        /// <param name="input">String to analyse</param>
        /// <returns>Whether or not a given string is valid according to our syntax properties</returns>
        /// <example> "Azerty 123" is valid; "@zerty#_123" is not valid, due to the inproper characters</example>
        public static bool IsStringValid(string input)
        {
            // we iterate through the characters of the input
            foreach (char c in input)
            {
                // If any character is incorrect : return FALSE
                if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c) && !char.IsPunctuation(c))
                {
                    return false;
                }
            }

            // Otherwise : not a single character is incorrect, hense return TRUE
            return true;
        }


        /// <summary>
        /// Converts an input string to a valid one
        /// </summary>
        /// <param name="input">String given to be modified</param>
        /// <returns></returns>
        public static string CorrectString(string input)
        {
            // A char array to store and modify the input
            List<char> output = new List<char>();
            // Determines if the next char should be to uppercase
            bool nextCharToUpper = false;

            // We iterate through the input's char
            foreach (char c in input)
            {
                // If the next char should be upper : activate the trigger
                if (char.IsWhiteSpace(c) || char.IsPunctuation(c))
                {
                    nextCharToUpper = true;
                }
                // If the char is valid AND SHOULD BE UPPER : add it as upper
                else if (char.IsLetterOrDigit(c) && nextCharToUpper)
                {
                    output.Add(char.ToUpper(c));
                    nextCharToUpper = false;
                }
                // If the char is valid : add it as is
                else if (char.IsLetterOrDigit(c))
                {
                    output.Add(c);
                }
            }

            // Return the string
            return new String(output.ToArray());
        }
    }
}
