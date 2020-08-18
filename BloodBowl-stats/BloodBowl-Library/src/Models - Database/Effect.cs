using System;
using System.Collections.Generic;
using System.Linq;


namespace BloodBowl_Library
{
    [Serializable]
    public enum EffectType
    {
        // Skills
        SkillGeneral,
        SkillAgility,
        SkillPass,
        SkillStrength,
        SkillMutation,
        SkillExtraordinary,

        // Bonus
        BonusMovement,
        BonusStrength,
        BonusAgility,
        BonusArmor,

        // Casualty
        CasualtyBenine,
        CasualtyMovement,
        CasualtyStrength,
        CasualtyAgility,
        CasualtyArmor,
        CasualtyDeath
    }



    [Serializable]
    public enum Effect
    {
        // Skills
        // Skills - General
        Block,
        Dauntless,
        DirtyPlayer,
        Fend,
        Frenzy,
        Kick,
        KickOffReturn,
        PassBlock,
        Pro,
        Shadowing,
        StripBall,
        SureHands,
        Tackle,
        Wrestle,

        // Skills - Agility
        Catch,
        DivingCatch,
        DivingTackle,
        Dodge,
        JumpUp,
        Leap,
        SideStep,
        SneakyGit,
        Sprint,
        SureFeet,

        // Skills - Pass
        Accurate,
        DumpOff,
        HailMaryPass,
        Leader,
        NervesOfSteel,
        Pass,
        SafeThrow,

        // Skills - Strength
        BreakTackle,
        Grab,
        Guard,
        Juggernaut,
        MightyBlow,
        MultipleBlock,
        PilingOn,
        StandFirm,
        StrongArm,
        ThickSkull,

        // Skills - Mutation
        BigHand,
        Claw,
        DisturbingPresence,
        ExtraArms,
        FoulAppearance,
        Horns,
        PrehensileTail,
        Tentacles,
        TwoHeads,
        VeryLongLegs,

        // Skills - Extraordinary
        AlwaysHungry,
        Animosity,
        BallAndChain,
        BloodLust,
        Bombardier,
        BoneHead,
        Chainsaw,
        Decay,
        HypnoticGaze,
        Loner,
        NoHands,
        NurglesRot,
        ReallyStupid,
        Regeneration,
        RightStuff,
        SecretWeapon,
        Stab,
        Stunty,
        TakeRoot,
        ThrowTeamMate,
        Titchy,
        WeepingDagger,
        WildAnimal,


        // Bonus,
        BonusMovement,
        BonusStrength,
        BonusAgility,
        BonusArmor,


        // Casualties
        BadlyHurt,
        BrokenJaw,
        BrokenRibs,
        FracturedArm,
        FracturedLeg,
        SmashedHand,
        GougedEye,
        GroinStrain,
        PinchedNerve,
        DamagedBack,
        SmashedKnee,
        SmashedAnkle,
        SmashedHip,
        FracturedSkull,
        SeriousConcussion,
        BrokenNeck,
        SmashedCollarBone,


        // Death :)
        Dead
    }


    public static class EffectStuff
    {
        public class EffectData
        {
            public EffectType type { get; }
            public string name { get; }
            public string description { get; }


            public EffectData(EffectType type, string name, string description)
            {
                this.type = type;
                this.name = name;
                this.description = description;
            }
        }

        /// <summary>
        /// Data of the instance
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Data of the instance</returns>
        private static EffectData data(this Effect effect)
        {
            switch (effect)
            {
                // Skills

                // Skills- General
                case Effect.Block:
                    return new EffectData(EffectType.SkillGeneral, "Block", "While blocking or being blocked, the player is not Knocked Down on a Both Down Result.");
                case Effect.Dauntless:
                    return new EffectData(EffectType.SkillGeneral, "Dauntless", "If the player attempts to block an opponent with a higher strength score they may roll a d6 adding their own strength. If the result is higher than the opponents strength, the block is made as if the strength scores were equal.");
                case Effect.DirtyPlayer:
                    return new EffectData(EffectType.SkillGeneral, "Dirty Player", "Add 1 to an Armor or Injury roll made by a player with this skill when they make a Foul.");
                case Effect.Fend:
                    return new EffectData(EffectType.SkillGeneral, "Fend", "Opposing players may not follow up blocks made against this player, even if the player with Fend is knocked down.");
                case Effect.Frenzy:
                    return new EffectData(EffectType.SkillGeneral, "Frenzy", "A player with Frenzy must always follow up after throwing a Block at an opposing player. In addition, they must make a second block against the same opponent if the first result was Pushed Back. This skill cannot be taken by a player with the Grab skill.");
                case Effect.Kick:
                    return new EffectData(EffectType.SkillGeneral, "Kick", "When kicking the ball to the opposing team, after rolling for scatter, you may choose to halve (rounding down) the number of squares the ball scatters. To use this skill the player cannot be in a wide zone or on the line of scrimmage.");
                case Effect.KickOffReturn:
                    return new EffectData(EffectType.SkillGeneral, "Kick-off Return", "The player may move up to 3 squares after the ball has been kicked by the opposing team, but before rolling on the kick-off table. Only one player can use Kick-off Return each kick-off, and they cannot be in an opponents tackle zone or on the line of scrimmage. The player cannot move into the opponents half of the pitch, and this skill cannot be used on a touchback kick-off.");
                case Effect.PassBlock:
                    return new EffectData(EffectType.SkillGeneral, "Pass Block", "A player may move up to 3 squares immedately before an opposing player makes a pass roll. The player must end up in a position to attempt an interception, or in a tackle zone of either the player attempting the pass or the target player. This is a 'free' move out of turn, but it follows all the normal rules for moving, dodging, etc.");
                case Effect.Pro:
                    return new EffectData(EffectType.SkillGeneral, "Pro", "Once per turn, the player can try to re-roll an action without using a team re-roll. They must first roll a d6, on a 4+ they may then re-roll the action as if they used a team reroll. The player may not use a team re-roll on the action after trying to use pro, but may use one to re-roll the pro roll.");
                case Effect.Shadowing:
                    return new EffectData(EffectType.SkillGeneral, "Shadowing", "A player with Shadowing may attempt to follow an opposing player who dodges out of their tackle zone. ( dodging unit MA ) - ( shadowing unit MA ) + 2D6. If the final result is 7 or less they move into the square vacated by the opposing player. No dodge rolls are required for this action. A player may make any number of shadowing moves per turn. If the final result is 8 or more avoid Shadowing.");
                case Effect.StripBall:
                    return new EffectData(EffectType.SkillGeneral, "Strip Ball", "When a player with this skill pushes an opposing player during a Block, they will cause the opposing player to drop the ball in square they are pushed back to, even if the opposing player is not Knocked Down.");
                case Effect.SureHands:
                    return new EffectData(EffectType.SkillGeneral, "Sure Hands", "A player with Sure Hands is allowed to re-roll the result if they fail to pick up the ball. In addition, the Strip Ball skill does not work against a player with this skill.");
                case Effect.Tackle:
                    return new EffectData(EffectType.SkillGeneral, "Tackle", "Opposing player may not use their Dodge skill when attempting to dodge out of this player's tackle zone, nor may they use their Dodge skill to avoid Player Stumbles result if the attacker chooses to use Tackle.");
                case Effect.Wrestle:
                    return new EffectData(EffectType.SkillGeneral, "Wrestle", "A player with Wrestle may choose to ignore a Both Down result during a block by placing both the players prone, but no Armor rolls are made. In addition, this does not cause a turnover.");

                default:
                    return new EffectData(EffectType.CasualtyAgility, "Default Effect", "Lorem ipsum dolor amet");
            }
        }


        // Methods

        /// <summary>
        /// Returns the type of a given Effect
        /// </summary>
        /// <param name="effect">Effect of which we want to know the type</param>
        /// <returns>The type of a given Effect</returns>
        public static EffectType type(this Effect effect)
        {
            return effect.data().type;
        }


        /// <summary>
        /// Returns the name of a given Effect
        /// </summary>
        /// <param name="effect">Effect of which we want to know the name</param>
        /// <returns>The name of a given Effect</returns>
        public static String name(this Effect effect)
        {
            return effect.data().name;
        }


        /// <summary>
        /// Returns the description of a given Effect
        /// </summary>
        /// <param name="effect">Effect of which we want to know the description</param>
        /// <returns>The description of a given Effect</returns>
        public static String description(this Effect effect)
        {
            return effect.data().description;
        }




        // Determination

        /// <summary>
        /// Returns whether an Effect is a Skill or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Skill or not</returns>
        public static bool isSkill(this Effect effect)
        {
            return effect.type().ToString().StartsWith("Skill");
        }


        /// <summary>
        /// Returns whether an Effect is a General Skill or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a General Skill or not</returns>
        public static bool isSkillGeneral(this Effect effect)
        {
            return effect.type() == EffectType.SkillGeneral;
        }


        /// <summary>
        /// Returns whether an Effect is an Agility Skill or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is an Agility Skill or not</returns>
        public static bool isSkillAgility(this Effect effect)
        {
            return effect.type() == EffectType.SkillAgility;
        }


        /// <summary>
        /// Returns whether an Effect is a Pass Skill or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Pass Skill or not</returns>
        public static bool isSkillPass(this Effect effect)
        {
            return effect.type() == EffectType.SkillPass;
        }


        /// <summary>
        /// Returns whether an Effect is a Strength Skill or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Strength Skill or not</returns>
        public static bool isSkillStrength(this Effect effect)
        {
            return effect.type() == EffectType.SkillStrength;
        }


        /// <summary>
        /// Returns whether an Effect is a Mutation Skill or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Mutation Skill or not</returns>
        public static bool isSkillMutation(this Effect effect)
        {
            return effect.type() == EffectType.SkillMutation;
        }


        /// <summary>
        /// Returns whether an Effect is an Extraordinary Skill or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is an Extraordinary Skill or not</returns>
        public static bool isSkillExtraordinary(this Effect effect)
        {
            return effect.type() == EffectType.SkillExtraordinary;
        }


        /// <summary>
        /// Returns whether an Effect is a Casualty or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Casualty or not</returns>
        public static bool isCasualty(this Effect effect)
        {
            return effect.type().ToString().StartsWith("Casualty");
        }


        /// <summary>
        /// Returns whether an Effect is a Casualty decreasing movement or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Casualty decreasing movement or not</returns>
        public static bool isCasualtyMovement(this Effect effect)
        {
            return effect.type() == EffectType.CasualtyMovement;
        }


        /// <summary>
        /// Returns whether an Effect is a Casualty decreasing strength or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Casualty decreasing strength or not</returns>
        public static bool isCasualtyStrength(this Effect effect)
        {
            return effect.type() == EffectType.CasualtyStrength;
        }


        /// <summary>
        /// Returns whether an Effect is a Casualty decreasing agility or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Casualty decreasing agility or not</returns>
        public static bool isCasualtyAgility(this Effect effect)
        {
            return effect.type() == EffectType.CasualtyAgility;
        }


        /// <summary>
        /// Returns whether an Effect is a Casualty decreasing armor or not
        /// </summary>
        /// <param name="effect">Effect we are analysing</param>
        /// <returns>Whether an Effect is a Casualty decreasing armor or not</returns>
        public static bool isCasualtyArmor(this Effect effect)
        {
            return effect.type() == EffectType.CasualtyArmor;
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
