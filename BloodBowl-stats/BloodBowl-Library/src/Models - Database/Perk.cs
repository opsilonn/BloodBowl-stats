using System;
using System.Collections.Generic;
using System.Linq;


namespace BloodBowl_Library
{
    [Serializable]
    public enum Perk
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
        MonstrousMouth,
        NoHands,
        NurglesRot,
        ReallyStupid,
        Regeneration,
        RightStuff,
        SecretWeapon,
        Stab,
        Stunty,
        Swoop,
        TakeRoot,
        ThrowTeamMate,
        Timmber,
        Titchy,
        WeepingDagger,
        WildAnimal,


        // Bonus,
        BonusMovement,
        BonusArmor,
        BonusAgility,
        BonusStrength,


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




    public static partial class PerkStuff
    {
        public class PerkData
        {
            public PerkType type { get; }
            public string name { get; }
            public string description { get; }


            public PerkData(PerkType type, string name, string description)
            {
                this.type = type;
                this.name = name;
                this.description = description;
            }
        }



        // PARAMETERS

        /// <summary>
        /// Returns the type of a given Perk
        /// </summary>
        /// <param name="perk">Perk of which we want to know the type</param>
        /// <returns>The type of a given Perk</returns>
        public static PerkType type(this Perk perk)
        {
            return perk.data().type;
        }


        /// <summary>
        /// Returns the name of a given Perk
        /// </summary>
        /// <param name="perk">Perk of which we want to know the name</param>
        /// <returns>The name of a given Perk</returns>
        public static String name(this Perk perk)
        {
            return perk.data().name;
        }


        /// <summary>
        /// Returns the description of a given Perk
        /// </summary>
        /// <param name="perk">Perk of which we want to know the description</param>
        /// <returns>The description of a given Perk</returns>
        public static String description(this Perk perk)
        {
            return perk.data().description;
        }



        // DATA

        /// <summary>
        /// Data of the instance
        /// </summary>
        /// <param name="perk">Perk we are analysing</param>
        /// <returns>Data of the instance</returns>
        private static PerkData data(this Perk perk)
        {
            switch (perk)
            {
                // Skills

                // Skills- General
                case Perk.Block:
                    return new PerkData(PerkType.SkillGeneral, "Block", "While blocking or being blocked, the player is not Knocked Down on a Both Down Result.");
                case Perk.Dauntless:
                    return new PerkData(PerkType.SkillGeneral, "Dauntless", "If the player attempts to block an opponent with a higher strength score they may roll a d6 adding their own strength. If the result is higher than the opponents strength, the block is made as if the strength scores were equal.");
                case Perk.DirtyPlayer:
                    return new PerkData(PerkType.SkillGeneral, "Dirty Player", "Add 1 to an Armor or Injury roll made by a player with this skill when they make a Foul.");
                case Perk.Fend:
                    return new PerkData(PerkType.SkillGeneral, "Fend", "Opposing players may not follow up blocks made against this player, even if the player with Fend is knocked down.");
                case Perk.Frenzy:
                    return new PerkData(PerkType.SkillGeneral, "Frenzy", "A player with Frenzy must always follow up after throwing a Block at an opposing player. In addition, they must make a second block against the same opponent if the first result was Pushed Back. This skill cannot be taken by a player with the Grab skill.");
                case Perk.Kick:
                    return new PerkData(PerkType.SkillGeneral, "Kick", "When kicking the ball to the opposing team, after rolling for scatter, you may choose to halve (rounding down) the number of squares the ball scatters. To use this skill the player cannot be in a wide zone or on the line of scrimmage.");
                case Perk.KickOffReturn:
                    return new PerkData(PerkType.SkillGeneral, "Kick-off Return", "The player may move up to 3 squares after the ball has been kicked by the opposing team, but before rolling on the kick-off table. Only one player can use Kick-off Return each kick-off, and they cannot be in an opponents tackle zone or on the line of scrimmage. The player cannot move into the opponents half of the pitch, and this skill cannot be used on a touchback kick-off.");
                case Perk.PassBlock:
                    return new PerkData(PerkType.SkillGeneral, "Pass Block", "A player may move up to 3 squares immedately before an opposing player makes a pass roll. The player must end up in a position to attempt an interception, or in a tackle zone of either the player attempting the pass or the target player. This is a 'free' move out of turn, but it follows all the normal rules for moving, dodging, etc.");
                case Perk.Pro:
                    return new PerkData(PerkType.SkillGeneral, "Pro", "Once per turn, the player can try to re-roll an action without using a team re-roll. They must first roll a d6, on a 4+ they may then re-roll the action as if they used a team reroll. The player may not use a team re-roll on the action after trying to use pro, but may use one to re-roll the pro roll.");
                case Perk.Shadowing:
                    return new PerkData(PerkType.SkillGeneral, "Shadowing", "A player with Shadowing may attempt to follow an opposing player who dodges out of their tackle zone. ( dodging unit MA ) - ( shadowing unit MA ) + 2D6. If the final result is 7 or less they move into the square vacated by the opposing player. No dodge rolls are required for this action. A player may make any number of shadowing moves per turn. If the final result is 8 or more avoid Shadowing.");
                case Perk.StripBall:
                    return new PerkData(PerkType.SkillGeneral, "Strip Ball", "When a player with this skill pushes an opposing player during a Block, they will cause the opposing player to drop the ball in square they are pushed back to, even if the opposing player is not Knocked Down.");
                case Perk.SureHands:
                    return new PerkData(PerkType.SkillGeneral, "Sure Hands", "A player with Sure Hands is allowed to re-roll the result if they fail to pick up the ball. In addition, the Strip Ball skill does not work against a player with this skill.");
                case Perk.Tackle:
                    return new PerkData(PerkType.SkillGeneral, "Tackle", "Opposing player may not use their Dodge skill when attempting to dodge out of this player's tackle zone, nor may they use their Dodge skill to avoid Player Stumbles result if the attacker chooses to use Tackle.");
                case Perk.Wrestle:
                    return new PerkData(PerkType.SkillGeneral, "Wrestle", "A player with Wrestle may choose to ignore a Both Down result during a block by placing both the players prone, but no Armor rolls are made. In addition, this does not cause a turnover.");


                // Skills- Agility
                case Perk.Catch:
                    return new PerkData(PerkType.SkillAgility, "Catch", " A player with Catch can re-roll a failed catch, interception or dropped Hand Off.");
                case Perk.DivingCatch:
                    return new PerkData(PerkType.SkillAgility, "Diving Catch", "The player gets a +1 modifier to catch accurate passes and hand-offs targeted at their square, and may attempt a catch roll as normal to catch passes, kick-offs and crowd throw-ins (but not bouncing balls) landing in an adjacent square. If more than one player try to use Diving Catch at the same time neither succeeds.");
                case Perk.DivingTackle:
                    return new PerkData(PerkType.SkillAgility, "Diving Tackle", "The player may use this skill after an opposing player attempts to dodge out of any of his tackle zones. The player is placed prone in the vacated area, but does not to make an Armor or Injury roll. The opposing player must subtract 2 from their dodge roll.");
                case Perk.Dodge:
                    return new PerkData(PerkType.SkillAgility, "Dodge", "Allows a player to re-roll a failed dodge. In addition, the are only Pushed back on a \"Defender Stumbles\" result.");
                case Perk.JumpUp:
                    return new PerkData(PerkType.SkillAgility, "Jump Up", "The player may stand up without paying the usual 3 squares of movement. If they wish to make a Block action, they must successfully pass an agility roll with a +2 bonus or the Action is wasted and the player remains prone.");
                case Perk.Leap:
                    return new PerkData(PerkType.SkillAgility, "Leap", "During any form of movement the player can attempt to leap one or two squares. While leaping the player can move over other players and ignores tackle zones. The player must pass an unmodified Agility roll or be knocked down in the destination square.");
                case Perk.SideStep:
                    return new PerkData(PerkType.SkillAgility, "Side Step", "Instead of being pushed back during a block the player can choose to move into any empty adjacent square. This does not prevent the player from being kocked down.");
                case Perk.SneakyGit:
                    return new PerkData(PerkType.SkillAgility, "Sneaky Git", "During a Foul action, a player with this skill is not sent off by the referee for rolling doubles on the Armor roll if this didn't break the opponents armor.");
                case Perk.Sprint:
                    return new PerkData(PerkType.SkillAgility, "Sprint", "The player may attempt to move up to 3 extra squares rather than the normal 2 when Going For It. The coach must still roll to see if the player is Knocked Down.");
                case Perk.SureFeet:
                    return new PerkData(PerkType.SkillAgility, "Sure Feet", "A player may re-roll the result if they fail a Go For It action, once per turn.");


                // Skills- Pass
                case Perk.Accurate:
                    return new PerkData(PerkType.SkillPass, "Accurate", "The player adds 1 when attempting a Pass.");
                case Perk.DumpOff:
                    return new PerkData(PerkType.SkillPass, "Dump-Off", "If the player is holding the ball, they may attempt a quick pass immediately before getting blocked.");
                case Perk.HailMaryPass:
                    return new PerkData(PerkType.SkillPass, "Hail Mary Pass", "A player using this skill can throw the ball anywhere in the pitch, even beyond the normal range for pass. However, the pass is never accurate.");
                case Perk.Leader:
                    return new PerkData(PerkType.SkillPass, "Leader", "The player is a natural leader and earns the team a special Leader re-roll. This works in the same as team re-rolls but may only be used if the leader is on the pitch. The team can only use one leader re-roll per half.");
                case Perk.NervesOfSteel:
                    return new PerkData(PerkType.SkillPass, "Nerves Of Steel", "The player ignores modifiers for enemy tackle zones when they attempt to pass, catch or intercept.");
                case Perk.Pass:
                    return new PerkData(PerkType.SkillPass, "Pass", "The player can re-roll if he throws an inaccurate pass or fumbles.");
                case Perk.SafeThrow:
                    return new PerkData(PerkType.SkillPass, "Safe Throw", "If a pass made by this player is ever intercepted then the Safe Throw player may make an unmodified Agility roll. If this is successful the the interception is canceled.");

                // Skills- Strength
                case Perk.BreakTackle:
                    return new PerkData(PerkType.SkillStrength, "Break Tackle", "The player uses his Strength instead of his Agility when he fails a Dodge. This skill may only be used once per turn.");
                case Perk.Grab:
                    return new PerkData(PerkType.SkillStrength, "Grab", "While blocking and pushing the target backwards, the player can choose any direction for the push. This effect cannot be used as part of a Blitz. In addition the target of a Block or Blitz from this player cannot use the Side Step skill.");
                case Perk.Guard:
                    return new PerkData(PerkType.SkillStrength, "Guard", "A player with this skill assists an attacking or defensive block even if they are in another player's tackle zone.");
                case Perk.Juggernaut:
                    return new PerkData(PerkType.SkillStrength, "Juggernaut", "If the player performs a Blitz action the opposing players may not use their Fend, Stand Firm or Wrestle skills against blocks. In addition they may choose to treat a Both Down result as Pushed Back.");
                case Perk.MightyBlow:
                    return new PerkData(PerkType.SkillStrength, "Mighty Blow", "Adds +1 to any Armor or Injury roll made by a player when an opponent is hit during a block.");
                case Perk.MultipleBlock:
                    return new PerkData(PerkType.SkillStrength, "Multiple Block", "A player who is adjacent to at least two opponents can try to block the two of them at the same time. Each opponent will gain a +2 bonus in Strength to represent the extra difficulty in succeeding with this type of action.");
                case Perk.PilingOn:
                    return new PerkData(PerkType.SkillStrength, "Piling On", "After a Block, if the opponent is Knocked Down, the player can throw himself on top of them having the ability to use a Team Re-Roll for either an Armour or Injury roll. However, the player will end up prone on the ground.");
                case Perk.StandFirm:
                    return new PerkData(PerkType.SkillStrength, "StandFirm", "A player with this skill may not be pushed back as a result of Pushed Back or Knock Down during a block.");
                case Perk.StrongArm:
                    return new PerkData(PerkType.SkillStrength, "Strong Arm", "When the player tries to pass the ball the distances taken into account is reduced.");
                case Perk.ThickSkull:
                    return new PerkData(PerkType.SkillStrength, "Thick Skull", "This skill reduces the chance of a KO result by 50% to a Stunned result.");


                // Skills- Mutation
                case Perk.BigHand:
                    return new PerkData(PerkType.SkillMutation, "Big Hand", "The player ignores modifiers for enemy tackle zones or Pouring Rain weather when he attempts to pick up the ball.");
                case Perk.Claw:
                    return new PerkData(PerkType.SkillMutation, "Claw", "When an opponent is Knocked Down by the player during a block any Armor roll of 8 or more, after modifications, automatically breaks armor.");
                case Perk.DisturbingPresence:
                    return new PerkData(PerkType.SkillMutation, "Disturbing Presence", "Any player must subtract 1 from the roll while trying to pass, catch or intercept within 3 squares of the player.");
                case Perk.ExtraArms:
                    return new PerkData(PerkType.SkillMutation, "Extra arms", "A player with extra arms adds +1 to all catch, interception or pick up rolls.");
                case Perk.FoulAppearance:
                    return new PerkData(PerkType.SkillMutation, "Foul Appearance", "The player's appearance is so horrible that any opposing player must roll a d6 and get a 2 or higher to be able to block them.");
                case Perk.Horns:
                    return new PerkData(PerkType.SkillMutation, "Horns", "a player with horns gains +1 Strength during a Blitz action.");
                case Perk.PrehensileTail:
                    return new PerkData(PerkType.SkillMutation, "Prehensile Tail", "Opposing players must subtract 1 from the D6 roll if they attempt to dodge out of any of the player's tackle zones.");
                case Perk.Tentacles:
                    return new PerkData(PerkType.SkillMutation, "Tentacles", "To successfully dodge or leap away from a player with tentacles, the opposing player must pass a Strength test. 2D6 + player's ST - Tentacles player's ST. Result 5 or less can't leave the tackle zone. Action ends immediately.");
                case Perk.TwoHeads:
                    return new PerkData(PerkType.SkillMutation, "Two Heads", "Adds +1 to the roll whenever the player tries to dodge.");
                case Perk.VeryLongLegs:
                    return new PerkData(PerkType.SkillMutation, "Very Long Legs", "+1 to interception and leap. Prevents use of the Safe Throw skill.");


                // Skills- Extraordinary
                case Perk.AlwaysHungry:
                    return new PerkData(PerkType.SkillExtraordinary, "Always Hungry", "When using Throw Team-Mate, roll a D6 after he has finished moving, but before he throws his team-mate. On a 1, roll another D6. On another 1, the teammate is killed without any opportunity for recovery. If the team-mate had the ball it will scatter once from the their square.");
                case Perk.Animosity:
                    return new PerkData(PerkType.SkillExtraordinary, "Animosity", "If this player at the end of his Hand-off or Pass Action attempts to hand-off or pass the ball to a team-mate that is not the same race as the Animosity player roll a D6. On a 2+, the pass/hand-off is carried out as normal. On a 1, the player refuses to give the ball to any team-mate except one of his own race. The coach may choose to change the target of the pass/hand-off to another team-mate of the same race as the Animosity player, however no more movement is allowed for the Animosity player, so the current Action may be lost for the turn.");
                case Perk.BallAndChain:
                    return new PerkData(PerkType.SkillExtraordinary, "Ball And Chain", "Players armed with a Ball & Chain can only take Move Actions. To move or Go For It, place the throw-in template over the player facing up or down the pitch or towards either sideline. Then roll a D6 and move the player one square in the indicated direction; no Dodge roll is required if you leave a tackle zone. If this movement takes the player off the pitch, they are beaten up by the crowd in the same manner as a player who has been pushed off the pitch. Repeat this process until the player runs out of normal movement (you may GFI using the same process if you wish). If during his Move Action he would move into an occupied square then the player will throw a block following normal blocking rules against whoever is in that square, friend or foe (and it even ignores Foul Appearance!). Prone or Stunned players in an occupied square are pushed back and an Armour roll is made to see if they are injured, instead of the block being thrown at them. The player must follow up if they push back another player, and will then carry on with their move as described above. If the player is ever Knocked Down or Placed Prone roll immediately for injury (no Armour roll is required). Stunned results for any Injury rolls are always treated as KO’d. A Ball & Chain player may use the Grab skill (as if a Block Action was being used) with his blocks (if he has learned it!). A Ball & Chain player may never use the Diving Tackle, Frenzy, Kick-Off Return, Leap, Pass Block or Shadowing skills.");
                case Perk.BloodLust:
                    return new PerkData(PerkType.SkillExtraordinary, "Blood Lust", "Roll a D6 immediately after declaring an Action. On a 2+, carry out the Action as normal. On a 1, however, the Vampire is overcome by the desire to drink Human blood and must carry out the following special Action instead. If the Vampire finishes the move standing adjacent to one or more standing, Prone or Stunned Thralls from his own team, he attacks one of them. Immediately roll for injury on the Thrall who has been attacked without making an Armour roll. The injury will not cause a turnover unless the Thrall was holding the ball. If the Vampire is not able to attack a Thrall (for any reason), then he is removed from the pitch and placed in his team's Reserves box, and his team suffers a turnover. If he was holding the ball it bounces from the square he occupied when he was removed, and he will not score a Touchdown (even if he gets into the End Zone while holding the ball before being removed). If the Vampire is KO’d or suffers a Casualty before biting a Thrall, then he should be placed in the appropriate box of the Dug Out instead of being placed in the Reserves box. Note that the Vampire is allowed to pick up the ball or do anything else they could normally do while taking their action, but must bite a Thrall to avoid the turnover.");
                case Perk.Bombardier:
                    return new PerkData(PerkType.SkillExtraordinary, "Bombardier", "A coach may choose to have a Bombardier who is not Prone or Stunned throw a bomb instead of taking any other Action with the player. This does not use the team's Pass Action for the turn. The bomb is thrown using the rules for throwing the ball (including weather effects), except that the player may not move or stand up before throwing it (he needs time to light the fuse!). Intercepted bomb passes are not turnovers. Fumbles, or indeed any explosions that lead to a player on the active team being knocked over are turnovers. All skills that may be used when a ball is thrown may be used when a bomb is thrown also. A bomb may be intercepted or caught using the same rules for catching the ball, in which case the player catching it must throw it again immediately. This is a special bonus Action that takes place out of the normal sequence of play. A player holding the ball can catch or intercept and throw a bomb. The bomb explodes when it lands in an empty square or an opportunity to catch the bomb fails or is declined (i.e., bombs don’t ‘bounce’). If the bomb is fumbled it explodes in the bomb thrower’s square. If a bomb lands in the crowd, it explodes with no effect. When the bomb finally does explode any player in the same square is Knocked Down, and players in adjacent squares are Knocked Down on a roll of 4+. Players can be hit by a bomb and treated as Knocked Down even if they are already Prone or Stunned. Make Armour and Injury rolls for any players Knocked Down by the bomb. Casualties caused by a bomb do not count for Star Player points.");
                case Perk.BoneHead:
                    return new PerkData(PerkType.SkillExtraordinary, "Bone Head", "Before attempting any action they must pass a Bone-Head test (1/6 chance). If they fail this then they may not make any actions for the remainder of the turn. In addition they lose their tackle zone.");
                case Perk.Chainsaw:
                    return new PerkData(PerkType.SkillExtraordinary, "Chainsaw", "A player armed with chainsaw must attack an opponent. Roll a D6. On a roll of 2 or more the chainsaw hits the opposing player, but on a roll of 1 it kicks back. The victim rolls an Armor Value +3. Armor rolls against the chainsaw player are also made at +3. Casualties caused by the chainsaw do not earn SPP.");
                case Perk.Decay:
                    return new PerkData(PerkType.SkillExtraordinary, "Decay", "When this player suffers a Casualty result on the Injury table, roll twice on the Casualty table and apply both results. The player will only ever miss one future match as a result of his injuries, even if he suffers two results with this effect.");
                case Perk.HypnoticGaze:
                    return new PerkData(PerkType.SkillExtraordinary, "Hypnotic Gaze", "The player may use hypnotic gaze at the end of his Move Action on one opposing player who is in an adjacent square. Make an Agility roll for the player with hypnotic gaze, with a -1 modifier for each opposing tackle zone on the player with hypnotic gaze other than the victim's. If the Agility roll is successful, then the opposing player loses his tackle zones and may not catch, intercept or pass the ball, assist another player on a block or foul, or move voluntarily until the start of his next action or the drive ends. If the roll fails, then the hypnotic gaze has no effect.");
                case Perk.Loner:
                    return new PerkData(PerkType.SkillExtraordinary, "Loner", "Loners are not great team players. If they wish to use a team re-roll, they must successfully pass a Loner roll (1/2 chance). If they fail, the re-roll is lost.");
                case Perk.MonstrousMouth:
                    return new PerkData(PerkType.SkillExtraordinary, "Monstrous Mouth", "A player with a Monstrous Mouth can reroll failed Catch, Handoff and Interception rolls. In addition, the Strip Ball skill will not work against a player with a Monstrous Mouth.");
                case Perk.NoHands:
                    return new PerkData(PerkType.SkillExtraordinary, "No Hands", "The player cannot carry the ball.");
                case Perk.NurglesRot:
                    return new PerkData(PerkType.SkillExtraordinary, "Nurgle's Rot", "Any opponent killed by this player becomes a Nurgle Rotter for the player's team.");
                case Perk.ReallyStupid:
                    return new PerkData(PerkType.SkillExtraordinary, "Really Stupid", "The player has a 1/2 chance of becoming Really Stupid and may not perform any actions for the rest of the turn, in addition they lose their tackle zone. If a team mate is standing adjacent to the player, the chance to fail is reduced to a 1/6 chance.");
                case Perk.Regeneration:
                    return new PerkData(PerkType.SkillExtraordinary, "Regeneration", "If they player is injured or killed, after any attempts by the Apothecary, they have a 1/2 chance to regenerate themselves and are placed in the reserves ready for the next drive.");
                case Perk.RightStuff:
                    return new PerkData(PerkType.SkillExtraordinary, "Right Stuff", "Enables a player to be thrown by a team-mate who possesses Throw Team-Mate skill.");
                case Perk.SecretWeapon:
                    return new PerkData(PerkType.SkillExtraordinary, "Secret Weapon", "At the end of any drive in which the player took part, he will automatically be sent off by the referee.");
                case Perk.Stab:
                    return new PerkData(PerkType.SkillExtraordinary, "Stab", "A player may attack an opponent with their stabbing attack instead of throwing a block at them. Make an unmodified Armor roll for the victim. If the score is less than or equal to the victim’s Armor value then the attack has no effect. If the score beats the victim’s Armor value then they have been wounded and an unmodified Injury roll must be made. If Stab is used as part of a Blitz Action, the player cannot continue moving after using it. Casualties caused by a stabbing attack do not count for Star Player points. For normal players there is only the Dark Elf Assassin who comes with the Stab ability. There are though some star players that have Stab and if you do get enough inducement money, you will be able to hire one.");
                case Perk.Stunty:
                    return new PerkData(PerkType.SkillExtraordinary, "Stunty", "A player with Stunty may ignore any enemy tackle zone on the square he is moving to when he makes a dodge, but subtract 1 from the roll when they pass. In addition, an Injury roll of 7 made against a Stunty player is considered a KO, and an Injury roll of 9 is considered a Badly Hurt casualty. Stunties that are armed with a secret weapon are not allowed to ignore enemy tackle zones.");
                case Perk.Swoop:
                    return new PerkData(PerkType.SkillExtraordinary, "Swoop", "When a player with the Swoop skill is thrown by a player with the Throw Teammate skill, use the Throw-in template rather than the Scatter template to determine where the player lands. For each square the player scatters, their coach places the Throw in template facing toward either end zone or either sideline, rolls a d6 and moves the player 1 square in the indicated direction. Additionally, when rolling to see if the player lands on their feet (per the Right Stuff rules) add 1 to the result. When a player with both Swoop and Stunty dodges, they DO NOT ignore the tackle zones on the square they are dodging to.");
                case Perk.TakeRoot:
                    return new PerkData(PerkType.SkillExtraordinary, "Take Root", "At the start of any action, the player rolls 1D6. If a 1 is rolled, the player may not move (or Block as part of a Blitz action) until the end of the drive, or they are knocked over.");
                case Perk.ThrowTeamMate:
                    return new PerkData(PerkType.SkillExtraordinary, "Throw Team-Mate", "Enables a player to throw a team-mate who possesses Right Stuff skill.");
                case Perk.Timmber:
                    return new PerkData(PerkType.SkillExtraordinary, "Timm-ber!", "When a player with this skill attempts to stand up, friendly adjacent players that are not in enemy tackle zones may assist in the effort. For each player able to assist, add 1 to the roll to stand up... note that a 1 is always a failure. Assisting a player to stand up does not count as an Action, and players may assist even if they have already taken an Action.");
                case Perk.Titchy:
                    return new PerkData(PerkType.SkillExtraordinary, "Titchy", "The player may add 1 to any Dodge roll he attempts. On the other hand, while opponents do have to dodge to leave any of a Titchy player’s tackle zones, Titchy players are so small that they do not exert a -1 modifier when opponents dodge into any of their tackle zones.");
                case Perk.WeepingDagger:
                    return new PerkData(PerkType.SkillExtraordinary, "Weeping Dagger", "If this player inflicts a casuality durning a block, and the result of the Casuality roll is 11-38 after any re-rolls, roll a D6. On a result of 4 or more, the opposing player must miss next game. Out of the referee's sight!");
                case Perk.WildAnimal:
                    return new PerkData(PerkType.SkillExtraordinary, "Wild Animal", "The player can become uncontrollable during a match and must pass a Wild Animal test to successfully perform an action. There is a 1/2 chance that they will be able to carry out the action required. This is increased to an 5/6 chance in case of a Block or Blitz.");


                // Bonuses
                case Perk.BonusMovement:
                    return new PerkData(PerkType.BonusMovement, "Bonus Movement", "Adds 1 to the Movement's stat");
                case Perk.BonusArmor:
                    return new PerkData(PerkType.BonusArmor, "Bonus Armor", "Adds 1 to the Armor's stat");
                case Perk.BonusAgility:
                    return new PerkData(PerkType.BonusAgility, "Bonus Agility", "Adds 1 to the Agility's stat");
                case Perk.BonusStrength:
                    return new PerkData(PerkType.BonusStrength, "Bonus Strength", "Adds 1 to the Strength's stat");


                // Casualties
                case Perk.BadlyHurt:
                    return new PerkData(PerkType.CasualtyBenign, "Badly Hurt", "No long term effect.");
                case Perk.BrokenJaw:
                    return new PerkData(PerkType.CasualtyRecovery, "Broken Jaw", "Miss next game.");
                case Perk.BrokenRibs:
                    return new PerkData(PerkType.CasualtyRecovery, "Broken Ribs", "Miss next game.");
                case Perk.FracturedArm:
                    return new PerkData(PerkType.CasualtyRecovery, "Fractured arm", "Miss next game.");
                case Perk.FracturedLeg:
                    return new PerkData(PerkType.CasualtyRecovery, "Fractured Leg", "Miss next game.");
                case Perk.SmashedHand:
                    return new PerkData(PerkType.CasualtyRecovery, "Smashed Hand", "Miss next game.");
                case Perk.GougedEye:
                    return new PerkData(PerkType.CasualtyRecovery, "Gouged Eye", "Miss next game.");
                case Perk.GroinStrain:
                    return new PerkData(PerkType.CasualtyRecovery, "Groin Strain", "Miss next game.");
                case Perk.PinchedNerve:
                    return new PerkData(PerkType.CasualtyRecovery, "Pinched Nerve", "Miss next game.");
                case Perk.DamagedBack:
                    return new PerkData(PerkType.CasualtyNiggling, "DamagedBack", "Niggling injury: adds 1 to all the player's injury rolls");
                case Perk.SmashedKnee:
                    return new PerkData(PerkType.CasualtyNiggling, "Smashed Knee", "Niggling injury: adds 1 to all the player's injury rolls");
                case Perk.SmashedAnkle:
                    return new PerkData(PerkType.CasualtyMovement, "SmashedAnkle", "Loses 1 point in Movement Allowance.");
                case Perk.SmashedHip:
                    return new PerkData(PerkType.CasualtyMovement, "Smashed Hip", "Loses 1 point in Movement Allowance.");
                case Perk.FracturedSkull:
                    return new PerkData(PerkType.CasualtyArmor, "Fractured Skull", "Loses 1 point in Armour Value.");
                case Perk.SeriousConcussion:
                    return new PerkData(PerkType.CasualtyArmor, "Serious Concussion", "Loses 1 point in Armour Value.");
                case Perk.BrokenNeck:
                    return new PerkData(PerkType.CasualtyAgility, "Broken Neck", "Loses 1 point in Agility.");
                case Perk.SmashedCollarBone:
                    return new PerkData(PerkType.CasualtyStrength, "SmashedCollarBone", "Loses 1 point in Strength.");
                case Perk.Dead:
                    return new PerkData(PerkType.CasualtyDeath, "Dead", "The player is dead; he is permanently removed from the team.");


                default:
                    return new PerkData(PerkType.CasualtyAgility, "Default Effect", "Lorem ipsum dolor amet");
            }
        }
    }
}
