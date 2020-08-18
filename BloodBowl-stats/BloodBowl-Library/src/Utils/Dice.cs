using System;


namespace BloodBowl_Library
{
    public static class Dice
    {
        private static Random rnd = new Random();


        public static int Roll6() { return rnd.Next(6) + 1; }
        public static int Roll8() { return rnd.Next(8) + 1; }
        public static int RollX(int x) { return (x > 0) ? rnd.Next(x) + 1 : 0; }
    }
}