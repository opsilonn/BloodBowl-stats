using System;


namespace BloodBowl_Library
{
    [Serializable]
    public enum Effect
    {
        // Skills
        SkillsBegin,
        SkillBlock,
        SkillDodge,
        SkillsEnd,

        // Bonus,
        BonusMovement,
        BonusStrength,
        BonusAgility,
        BonusArmor,

        // Casualties
        CasualtiesBegin,

        CasualtiesBadlyHurt,
        CasualtiesBrokenJaw,
        CasualtiesBrokenRibs,
        CasualtiesFracturedArm,
        CasualtiesFracturedLeg,
        CasualtiesSmashedHand,
        CasualtiesGougedEye,
        CasualtiesGroinStrain,
        CasualtiesPinchedNerve,
        CasualtiesDamagedBack,
        CasualtiesSmashedKnee,
        CasualtiesSmashedAnkle,
        CasualtiesSmashedHip,
        CasualtiesFracturedSkull,
        CasualtiesSeriousConcussion,
        CasualtiesBrokenNeck,
        CasualtiesSmashedCollarBone,

        CasualtiesEnd,

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
        /// Returns whether an Effect is a Casualty or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Casualty or not</returns>
        public static bool isCasualty(this Effect effect)
        {
            return Effect.CasualtiesBegin < effect && effect < Effect.CasualtiesEnd;
        }


        /// <summary>
        /// Returns whether an Effect is a Casualty decreasing movement or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Casualty decreasing movement or not</returns>
        public static bool isCasualtyMovement(this Effect effect)
        {
            return effect == Effect.CasualtiesSmashedAnkle || effect == Effect.CasualtiesSmashedHip;
        }


        /// <summary>
        /// Returns whether an Effect is a Casualty decreasing strength or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Casualty decreasing strength or not</returns>
        public static bool isCasualtyStrength(this Effect effect)
        {
            return effect == Effect.CasualtiesSmashedCollarBone;
        }


        /// <summary>
        /// Returns whether an Effect is a Casualty decreasing agility or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Casualty decreasing agility or not</returns>
        public static bool isCasualtyAgility(this Effect effect)
        {
            return effect == Effect.CasualtiesBrokenNeck;
        }


        /// <summary>
        /// Returns whether an Effect is a Casualty decreasing armor or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Casualty decreasing armor or not</returns>
        public static bool isCasualtyArmor(this Effect effect)
        {
            return effect == Effect.CasualtiesFracturedSkull || effect == Effect.CasualtiesSeriousConcussion;
        }
    }
}
