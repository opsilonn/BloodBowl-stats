using System;
using System.Collections.Generic;
using System.Linq;

namespace BloodBowl_Library
{
    [Serializable]
    public enum Effect
    {
        // Skills
        // Skills - General
        SkillGeneralBlock,
        SkillGeneralDauntless,
        SkillGeneral,
        SkillGeneralirtyPlayer,
        SkillGeneralFend,
        SkillGeneralFrenzy,
        SkillGeneralKick,
        SkillGeneralKickOffReturn,
        SkillGeneralPassBlock,
        SkillGeneralPro,
        SkillGeneralShadowing,
        SkillGeneralStripBall,
        SkillGeneralSureHands,
        SkillGeneralTackle,
        SkillGeneralWrestle,
        // Skills - Agility
        SkillAgilityCatch,
        SkillAgilityDivingCatch,
        SkillAgilityDivingTackle,
        SkillAgilityDodge,
        SkillAgilityJumpUp,
        SkillAgilityLeap,
        SkillAgilitySideStep,
        SkillAgilitySneakyGit,
        SkillAgilitySprint,
        SkillAgilitySureFeet,
        // Skills - Pass
        SkillPassingAccurate,
        SkillPassingDumpOff,
        SkillPassingHailMaryPass,
        SkillPassingLeader,
        SkillPassingNervesOfSteel,
        SkillPassingPass,
        SkillPassingSafeThrow,
        // Skills - Strength
        SkillStrengthBreakTackle,
        SkillStrengthGrab,
        SkillStrengthGuard,
        SkillStrengthJuggernaut,
        SkillStrengthMightyBlow,
        SkillStrengthMultipleBlock,
        SkillStrengthPilingOn,
        SkillStrengthStandFirm,
        SkillStrengthStrongArm,
        SkillStrengthThickSkull,
        // Skills - Mutation
        SkillMutationBigHand,
        SkillMutationClaw,
        SkillMutationDisturbingPresence,
        SkillMutationExtraArms,
        SkillMutationFoulAppearance,
        SkillMutationHorns,
        SkillMutationPrehensileTail,
        SkillMutationTentacles,
        SkillMutationTwoHeads,
        SkillMutationVeryLongLegs,
        // Skills - Extraordinary
        SkillExtraordinaryAlwaysHungry,
        SkillExtraordinaryAnimosity,
        SkillExtraordinaryBallAndChain,
        SkillExtraordinaryBloodLust,
        SkillExtraordinaryBombardier,
        SkillExtraordinaryBoneHead,
        SkillExtraordinaryChainsaw,
        SkillExtraordinaryDecay,
        SkillExtraordinaryHypnoticGaze,
        SkillExtraordinaryLoner,
        SkillExtraordinaryNoHands,
        SkillExtraordinaryNurglesRot,
        SkillExtraordinaryReallyStupid,
        SkillExtraordinaryRegeneration,
        SkillExtraordinaryRightStuff,
        SkillExtraordinarySecretWeapon,
        SkillExtraordinaryStab,
        SkillExtraordinaryStunty,
        SkillExtraordinaryTakeRoot,
        SkillExtraordinaryThrowTeamMate,
        SkillExtraordinaryTitchy,
        SkillExtraordinaryWeepingDagger,
        SkillExtraordinaryWildAnimal,

        // Bonus,
        BonusMovement,
        BonusStrength,
        BonusAgility,
        BonusArmor,

        // Casualties
        CasualtyBadlyHurt,
        CasualtyBrokenJaw,
        CasualtyBrokenRibs,
        CasualtyFracturedArm,
        CasualtyFracturedLeg,
        CasualtySmashedHand,
        CasualtyGougedEye,
        CasualtyGroinStrain,
        CasualtyPinchedNerve,
        CasualtyDamagedBack,
        CasualtySmashedKnee,
        CasualtySmashedAnkle,
        CasualtySmashedHip,
        CasualtyFracturedSkull,
        CasualtySeriousConcussion,
        CasualtyBrokenNeck,
        CasualtySmashedCollarBone,


        // Death :)
        Dead
    }


    public static class EffectStuff
    {
        private static string SKILL = "Skill";
        private static string SKILL_GENERAL = "SkillGeneral";
        private static string SKILL_AGILITY = "SkillAgility";
        private static string SKILL_PASS = "SkillPass";
        private static string SKILL_STRENGTH = "SkillStrength";
        private static string SKILL_MUTATION = "SkillMutation";
        private static string SKILL_EXTRAORDINARY = "SkillExtraordinary";
        private static string CASULTY = "Casualty";


        private static string getName(this Effect effect)
        {
            return Enum.GetName(typeof(Effect), effect);
        }


        /// <summary>
        /// Returns whether an Effect is a Skill or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Skill or not</returns>
        public static bool isSkill(this Effect effect)
        {
            return effect.getName().StartsWith(SKILL);
        }


        /// <summary>
        /// Returns whether an Effect is a General Skill or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a General Skill or not</returns>
        public static bool isSkillGeneral(this Effect effect)
        {
            return effect.getName().StartsWith(SKILL_GENERAL);
        }


        /// <summary>
        /// Returns whether an Effect is an Agility Skill or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is an Agility Skill or not</returns>
        public static bool isSkillAgility(this Effect effect)
        {
            return effect.getName().StartsWith(SKILL_AGILITY);
        }


        /// <summary>
        /// Returns whether an Effect is a Pass Skill or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Pass Skill or not</returns>
        public static bool isSkillPass(this Effect effect)
        {
            return effect.getName().StartsWith(SKILL_PASS);
        }


        /// <summary>
        /// Returns whether an Effect is a Strength Skill or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Strength Skill or not</returns>
        public static bool isSkillStrength(this Effect effect)
        {
            return effect.getName().StartsWith(SKILL_STRENGTH);
        }


        /// <summary>
        /// Returns whether an Effect is a Mutation Skill or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Mutation Skill or not</returns>
        public static bool isSkillMutation(this Effect effect)
        {
            return effect.getName().StartsWith(SKILL_MUTATION);
        }


        /// <summary>
        /// Returns whether an Effect is an Extraordinary Skill or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is an Extraordinary Skill or not</returns>
        public static bool isSkillExtraordinary(this Effect effect)
        {
            return effect.getName().StartsWith(SKILL_EXTRAORDINARY);
        }


        /// <summary>
        /// Returns whether an Effect is a Casualty or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Casualty or not</returns>
        public static bool isCasualty(this Effect effect)
        {
            return effect.getName().StartsWith(CASULTY);
        }


        /// <summary>
        /// Returns whether an Effect is a Casualty decreasing movement or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Casualty decreasing movement or not</returns>
        public static bool isCasualtyMovement(this Effect effect)
        {
            return effect == Effect.CasualtySmashedAnkle || effect == Effect.CasualtySmashedHip;
        }


        /// <summary>
        /// Returns whether an Effect is a Casualty decreasing strength or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Casualty decreasing strength or not</returns>
        public static bool isCasualtyStrength(this Effect effect)
        {
            return effect == Effect.CasualtySmashedCollarBone;
        }


        /// <summary>
        /// Returns whether an Effect is a Casualty decreasing agility or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Casualty decreasing agility or not</returns>
        public static bool isCasualtyAgility(this Effect effect)
        {
            return effect == Effect.CasualtyBrokenNeck;
        }


        /// <summary>
        /// Returns whether an Effect is a Casualty decreasing armor or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Casualty decreasing armor or not</returns>
        public static bool isCasualtyArmor(this Effect effect)
        {
            return effect == Effect.CasualtyFracturedSkull || effect == Effect.CasualtySeriousConcussion;
        }





        /// <summary>
        /// Returns an array of all the Effects
        /// </summary>
        /// <returns>An array of all the Effects</returns>
        public static List<Effect> GetAllEffects()
        {
            return Enum.GetValues(typeof(Effect)).Cast<Effect>().ToList();
        }


        /// <summary>
        /// Returns an array of all the Skill Effects
        /// </summary>
        /// <returns>An array of all the Skill Effects</returns>
        public static List<Effect> GetAllEffectsSkills()
        {
            return GetAllEffects().Where(effect => effect.isSkill()).ToList();
        }


        /// <summary>
        /// Returns an array of all the General Skill Effects
        /// </summary>
        /// <returns>An array of all the General Skill Effects</returns>
        public static List<Effect> GetAllEffectsSkillsGeneral()
        {
            return GetAllEffects().Where(effect => effect.isSkillGeneral()).ToList();
        }


        /// <summary>
        /// Returns an array of all the Agility Skill Effects
        /// </summary>
        /// <returns>An array of all the Agility Skill Effects</returns>
        public static List<Effect> GetAllEffectsSkillsAgility()
        {
            return GetAllEffects().Where(effect => effect.isSkillAgility()).ToList();
        }


        /// <summary>
        /// Returns an array of all the Pass Skill Effects
        /// </summary>
        /// <returns>An array of all the Pass Skill Effects</returns>
        public static List<Effect> GetAllEffectsSkillsPass()
        {
            return GetAllEffects().Where(effect => effect.isSkillPass()).ToList();
        }


        /// <summary>
        /// Returns an array of all the Strength Skill Effects
        /// </summary>
        /// <returns>An array of all the Strength Skill Effects</returns>
        public static List<Effect> GetAllEffectsSkillsStrength()
        {
            return GetAllEffects().Where(effect => effect.isSkillStrength()).ToList();
        }


        /// <summary>
        /// Returns an array of all the Mutation Skill Effects
        /// </summary>
        /// <returns>An array of all the Mutation Skill Effects</returns>
        public static List<Effect> GetAllEffectsSkillsMutation()
        {
            return GetAllEffects().Where(effect => effect.isSkillMutation()).ToList();
        }


        /// <summary>
        /// Returns an array of all the Extraordinary Skill Effects
        /// </summary>
        /// <returns>An array of all the Extraordinary Skill Effects</returns>
        public static List<Effect> GetAllEffectsSkillsExtraordinary()
        {
            return GetAllEffects().Where(effect => effect.isSkillExtraordinary()).ToList();
        }
    }
}
