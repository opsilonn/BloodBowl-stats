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




    public static partial class EffectStuff
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



        // DATA

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


                // Skills- Agility
                case Effect.Catch:
                    return new EffectData(EffectType.SkillAgility, "Catch", " A player with Catch can re-roll a failed catch, interception or dropped Hand Off.");
                case Effect.DivingCatch:
                    return new EffectData(EffectType.SkillAgility, "Diving Catch", "The player gets a +1 modifier to catch accurate passes and hand-offs targeted at their square, and may attempt a catch roll as normal to catch passes, kick-offs and crowd throw-ins (but not bouncing balls) landing in an adjacent square. If more than one player try to use Diving Catch at the same time neither succeeds.");
                case Effect.DivingTackle:
                    return new EffectData(EffectType.SkillAgility, "Diving Tackle", "The player may use this skill after an opposing player attempts to dodge out of any of his tackle zones. The player is placed prone in the vacated area, but does not to make an Armor or Injury roll. The opposing player must subtract 2 from their dodge roll.");
                case Effect.Dodge:
                    return new EffectData(EffectType.SkillAgility, "Dodge", "Allows a player to re-roll a failed dodge. In addition, the are only Pushed back on a \"Defender Stumbles\" result.");
                case Effect.JumpUp:
                    return new EffectData(EffectType.SkillAgility, "Jump Up", "The player may stand up without paying the usual 3 squares of movement. If they wish to make a Block action, they must successfully pass an agility roll with a +2 bonus or the Action is wasted and the player remains prone.");
                case Effect.Leap:
                    return new EffectData(EffectType.SkillAgility, "Leap", "During any form of movement the player can attempt to leap one or two squares. While leaping the player can move over other players and ignores tackle zones. The player must pass an unmodified Agility roll or be knocked down in the destination square.");
                case Effect.SideStep:
                    return new EffectData(EffectType.SkillAgility, "Side Step", "Instead of being pushed back during a block the player can choose to move into any empty adjacent square. This does not prevent the player from being kocked down.");
                case Effect.SneakyGit:
                    return new EffectData(EffectType.SkillAgility, "Sneaky Git", "During a Foul action, a player with this skill is not sent off by the referee for rolling doubles on the Armor roll if this didn't break the opponents armor.");
                case Effect.Sprint:
                    return new EffectData(EffectType.SkillAgility, "Sprint", "The player may attempt to move up to 3 extra squares rather than the normal 2 when Going For It. The coach must still roll to see if the player is Knocked Down.");
                case Effect.SureFeet:
                    return new EffectData(EffectType.SkillAgility, "Sure Feet", " A player may re-roll the result if they fail a Go For It action, once per turn.");

                /*
                case Effect.Catch:
                    return new EffectData(EffectType.SkillAgility, "", "");
                */



                default:
                    return new EffectData(EffectType.CasualtyAgility, "Default Effect", "Lorem ipsum dolor amet");
            }
        }
    }
}
