using System;


namespace BloodBowl_Library
{
    [Serializable]
    public enum Effect
    {
        // Skills
        SkillsBegin,
        SkillBlock,
        SkillsEnd,

        // Wounds
        WoundsBegin,
        WoundBrokenBone,
        WoundsEnd,

        // Death :)
        Dead
    }


    public static class EffectStuff
    {
        /// <summary>
        /// Returns whether an Effect is a Skill or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Skill or not</returns>
        public static bool isSkill(this Effect effect)
        {
            return Effect.SkillsBegin < effect && effect < Effect.SkillsEnd;
        }


        /// <summary>
        /// Returns whether an Effect is a Wound or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Skill or not</returns>
        public static bool isWound(this Effect effect)
        {
            return Effect.WoundsBegin < effect && effect < Effect.WoundsEnd;
        }
    }
}
