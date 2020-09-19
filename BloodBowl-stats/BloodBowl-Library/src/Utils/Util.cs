using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBowl_Library
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
        public static string ConvertToCorrectString(string input)
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


        public static int Compute(string s, string t)
        {
            if (string.IsNullOrEmpty(s))
            {
                if (string.IsNullOrEmpty(t))
                    return 0;
                return t.Length;
            }

            if (string.IsNullOrEmpty(t))
            {
                return s.Length;
            }

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // initialize the top and right of the table to 0, 1, 2, ...
            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 1; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    int min1 = d[i - 1, j] + 1;
                    int min2 = d[i, j - 1] + 1;
                    int min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }
            return d[n, m];
        }







        /// <summary>
        /// Calculate percentage similarity of two strings
        /// <param name="source">Source String to Compare with</param>
        /// <param name="target">Targeted String to Compare</param>
        /// <returns>Return Similarity between two strings from 0 to 1.0</returns>
        /// </summary>
        public static double CalculateSimilarity(this string source, string target)
        {
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            int stepsToSame = ComputeLevenshteinDistance(source, target);
            return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }
        /// <summary>
        /// Returns the number of steps required to transform the source string
        /// into the target string.
        /// </summary>
        static int ComputeLevenshteinDistance(string source, string target)
        {
            if ((source == null) || (target == null)) return 0;
            if ((source.Length == 0) || (target.Length == 0)) return 0;
            if (source == target) return source.Length;

            int sourceWordCount = source.Length;
            int targetWordCount = target.Length;

            // Step 1
            if (sourceWordCount == 0)
                return targetWordCount;

            if (targetWordCount == 0)
                return sourceWordCount;

            int[,] distance = new int[sourceWordCount + 1, targetWordCount + 1];

            // Step 2
            for (int i = 0; i <= sourceWordCount; distance[i, 0] = i++) ;
            for (int j = 0; j <= targetWordCount; distance[0, j] = j++) ;

            for (int i = 1; i <= sourceWordCount; i++)
            {
                for (int j = 1; j <= targetWordCount; j++)
                {
                    // Step 3
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 4
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceWordCount, targetWordCount];
        }
    }
}
