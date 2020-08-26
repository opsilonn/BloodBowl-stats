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



        // PARAMETERS

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
                    return new EffectData(EffectType.SkillAgility, "Sure Feet", "A player may re-roll the result if they fail a Go For It action, once per turn.");


                // Skills- Pass
                case Effect.Accurate:
                    return new EffectData(EffectType.SkillPass, "Accurate", "The player adds 1 when attempting a Pass.");
                case Effect.DumpOff:
                    return new EffectData(EffectType.SkillPass, "Dump-Off", "If the player is holding the ball, they may attempt a quick pass immediately before getting blocked.");
                case Effect.HailMaryPass:
                    return new EffectData(EffectType.SkillPass, "Hail Mary Pass", "A player using this skill can throw the ball anywhere in the pitch, even beyond the normal range for pass. However, the pass is never accurate.");
                case Effect.Leader:
                    return new EffectData(EffectType.SkillPass, "Leader", "The player is a natural leader and earns the team a special Leader re-roll. This works in the same as team re-rolls but may only be used if the leader is on the pitch. The team can only use one leader re-roll per half.");
                case Effect.NervesOfSteel:
                    return new EffectData(EffectType.SkillPass, "Nerves Of Steel", "The player ignores modifiers for enemy tackle zones when they attempt to pass, catch or intercept.");
                case Effect.Pass:
                    return new EffectData(EffectType.SkillPass, "Pass", "The player can re-roll if he throws an inaccurate pass or fumbles.");
                case Effect.SafeThrow:
                    return new EffectData(EffectType.SkillPass, "Safe Throw", "If a pass made by this player is ever intercepted then the Safe Throw player may make an unmodified Agility roll. If this is successful the the interception is canceled.");

                // Skills- Strength
                case Effect.BreakTackle:
                    return new EffectData(EffectType.SkillStrength, "Break Tackle", "The player uses his Strength instead of his Agility when he fails a Dodge. This skill may only be used once per turn.");
                case Effect.Grab:
                    return new EffectData(EffectType.SkillStrength, "Grab", "While blocking and pushing the target backwards, the player can choose any direction for the push. This effect cannot be used as part of a Blitz. In addition the target of a Block or Blitz from this player cannot use the Side Step skill.");
                case Effect.Guard:
                    return new EffectData(EffectType.SkillStrength, "Guard", "A player with this skill assists an attacking or defensive block even if they are in another player's tackle zone.");
                case Effect.Juggernaut:
                    return new EffectData(EffectType.SkillStrength, "Juggernaut", "If the player performs a Blitz action the opposing players may not use their Fend, Stand Firm or Wrestle skills against blocks. In addition they may choose to treat a Both Down result as Pushed Back.");
                case Effect.MightyBlow:
                    return new EffectData(EffectType.SkillStrength, "Mighty Blow", "Adds +1 to any Armor or Injury roll made by a player when an opponent is hit during a block.");
                case Effect.MultipleBlock:
                    return new EffectData(EffectType.SkillStrength, "Multiple Block", "A player who is adjacent to at least two opponents can try to block the two of them at the same time. Each opponent will gain a +2 bonus in Strength to represent the extra difficulty in succeeding with this type of action.");
                case Effect.PilingOn:
                    return new EffectData(EffectType.SkillStrength, "Piling On", "After a Block, if the opponent is Knocked Down, the player can throw himself on top of them having the ability to use a Team Re-Roll for either an Armour or Injury roll. However, the player will end up prone on the ground.");
                case Effect.StandFirm:
                    return new EffectData(EffectType.SkillStrength, "StandFirm", "A player with this skill may not be pushed back as a result of Pushed Back or Knock Down during a block.");
                case Effect.StrongArm:
                    return new EffectData(EffectType.SkillStrength, "Strong Arm", "When the player tries to pass the ball the distances taken into account is reduced.");
                case Effect.ThickSkull:
                    return new EffectData(EffectType.SkillStrength, "Thick Skull", "This skill reduces the chance of a KO result by 50% to a Stunned result.");


                // Skills- Mutation
                case Effect.BigHand:
                    return new EffectData(EffectType.SkillMutation, "Big Hand", "The player ignores modifiers for enemy tackle zones or Pouring Rain weather when he attempts to pick up the ball.");
                case Effect.Claw:
                    return new EffectData(EffectType.SkillMutation, "Claw", "When an opponent is Knocked Down by the player during a block any Armor roll of 8 or more, after modifications, automatically breaks armor.");
                case Effect.DisturbingPresence:
                    return new EffectData(EffectType.SkillMutation, "Disturbing Presence", "Any player must subtract 1 from the roll while trying to pass, catch or intercept within 3 squares of the player.");
                case Effect.ExtraArms:
                    return new EffectData(EffectType.SkillMutation, "Extra arms", "A player with extra arms adds +1 to all catch, interception or pick up rolls.");
                case Effect.FoulAppearance:
                    return new EffectData(EffectType.SkillMutation, "Foul Appearance", "The player's appearance is so horrible that any opposing player must roll a d6 and get a 2 or higher to be able to block them.");
                case Effect.Horns:
                    return new EffectData(EffectType.SkillMutation, "Horns", "a player with horns gains +1 Strength during a Blitz action.");
                case Effect.PrehensileTail:
                    return new EffectData(EffectType.SkillMutation, "Prehensile Tail", "Opposing players must subtract 1 from the D6 roll if they attempt to dodge out of any of the player's tackle zones.");
                case Effect.Tentacles:
                    return new EffectData(EffectType.SkillMutation, "Tentacles", "To successfully dodge or leap away from a player with tentacles, the opposing player must pass a Strength test. 2D6 + player's ST - Tentacles player's ST. Result 5 or less can't leave the tackle zone. Action ends immediately.");
                case Effect.TwoHeads:
                    return new EffectData(EffectType.SkillMutation, "Two Heads", "Adds +1 to the roll whenever the player tries to dodge.");
                case Effect.VeryLongLegs:
                    return new EffectData(EffectType.SkillMutation, "Very Long Legs", "+1 to interception and leap. Prevents use of the Safe Throw skill.");


                // Skills- Extraordinary
                case Effect.AlwaysHungry:
                    return new EffectData(EffectType.SkillExtraordinary, "Always Hungry", "When using Throw Team-Mate, roll a D6 after he has finished moving, but before he throws his team-mate. On a 1, roll another D6. On another 1, the teammate is killed without any opportunity for recovery. If the team-mate had the ball it will scatter once from the their square.");
                case Effect.Animosity:
                    return new EffectData(EffectType.SkillExtraordinary, "Animosity", "If this player at the end of his Hand-off or Pass Action attempts to hand-off or pass the ball to a team-mate that is not the same race as the Animosity player roll a D6. On a 2+, the pass/hand-off is carried out as normal. On a 1, the player refuses to give the ball to any team-mate except one of his own race. The coach may choose to change the target of the pass/hand-off to another team-mate of the same race as the Animosity player, however no more movement is allowed for the Animosity player, so the current Action may be lost for the turn.");
                case Effect.BallAndChain:
                    return new EffectData(EffectType.SkillExtraordinary, "Ball And Chain", "Players armed with a Ball & Chain can only take Move Actions. To move or Go For It, place the throw-in template over the player facing up or down the pitch or towards either sideline. Then roll a D6 and move the player one square in the indicated direction; no Dodge roll is required if you leave a tackle zone. If this movement takes the player off the pitch, they are beaten up by the crowd in the same manner as a player who has been pushed off the pitch. Repeat this process until the player runs out of normal movement (you may GFI using the same process if you wish). If during his Move Action he would move into an occupied square then the player will throw a block following normal blocking rules against whoever is in that square, friend or foe (and it even ignores Foul Appearance!). Prone or Stunned players in an occupied square are pushed back and an Armour roll is made to see if they are injured, instead of the block being thrown at them. The player must follow up if they push back another player, and will then carry on with their move as described above. If the player is ever Knocked Down or Placed Prone roll immediately for injury (no Armour roll is required). Stunned results for any Injury rolls are always treated as KO’d. A Ball & Chain player may use the Grab skill (as if a Block Action was being used) with his blocks (if he has learned it!). A Ball & Chain player may never use the Diving Tackle, Frenzy, Kick-Off Return, Leap, Pass Block or Shadowing skills.");
                case Effect.BloodLust:
                    return new EffectData(EffectType.SkillExtraordinary, "Blood Lust", "Roll a D6 immediately after declaring an Action. On a 2+, carry out the Action as normal. On a 1, however, the Vampire is overcome by the desire to drink Human blood and must carry out the following special Action instead. If the Vampire finishes the move standing adjacent to one or more standing, Prone or Stunned Thralls from his own team, he attacks one of them. Immediately roll for injury on the Thrall who has been attacked without making an Armour roll. The injury will not cause a turnover unless the Thrall was holding the ball. If the Vampire is not able to attack a Thrall (for any reason), then he is removed from the pitch and placed in his team's Reserves box, and his team suffers a turnover. If he was holding the ball it bounces from the square he occupied when he was removed, and he will not score a Touchdown (even if he gets into the End Zone while holding the ball before being removed). If the Vampire is KO’d or suffers a Casualty before biting a Thrall, then he should be placed in the appropriate box of the Dug Out instead of being placed in the Reserves box. Note that the Vampire is allowed to pick up the ball or do anything else they could normally do while taking their action, but must bite a Thrall to avoid the turnover.");
                case Effect.Bombardier:
                    return new EffectData(EffectType.SkillExtraordinary, "Bombardier", "A coach may choose to have a Bombardier who is not Prone or Stunned throw a bomb instead of taking any other Action with the player. This does not use the team's Pass Action for the turn. The bomb is thrown using the rules for throwing the ball (including weather effects), except that the player may not move or stand up before throwing it (he needs time to light the fuse!). Intercepted bomb passes are not turnovers. Fumbles, or indeed any explosions that lead to a player on the active team being knocked over are turnovers. All skills that may be used when a ball is thrown may be used when a bomb is thrown also. A bomb may be intercepted or caught using the same rules for catching the ball, in which case the player catching it must throw it again immediately. This is a special bonus Action that takes place out of the normal sequence of play. A player holding the ball can catch or intercept and throw a bomb. The bomb explodes when it lands in an empty square or an opportunity to catch the bomb fails or is declined (i.e., bombs don’t ‘bounce’). If the bomb is fumbled it explodes in the bomb thrower’s square. If a bomb lands in the crowd, it explodes with no effect. When the bomb finally does explode any player in the same square is Knocked Down, and players in adjacent squares are Knocked Down on a roll of 4+. Players can be hit by a bomb and treated as Knocked Down even if they are already Prone or Stunned. Make Armour and Injury rolls for any players Knocked Down by the bomb. Casualties caused by a bomb do not count for Star Player points.");
                case Effect.BoneHead:
                    return new EffectData(EffectType.SkillExtraordinary, "Bone Head", "Before attempting any action they must pass a Bone-Head test (1/6 chance). If they fail this then they may not make any actions for the remainder of the turn. In addition they lose their tackle zone.");
                case Effect.Chainsaw:
                    return new EffectData(EffectType.SkillExtraordinary, "Chainsaw", "A player armed with chainsaw must attack an opponent. Roll a D6. On a roll of 2 or more the chainsaw hits the opposing player, but on a roll of 1 it kicks back. The victim rolls an Armor Value +3. Armor rolls against the chainsaw player are also made at +3. Casualties caused by the chainsaw do not earn SPP.");
                case Effect.Decay:
                    return new EffectData(EffectType.SkillExtraordinary, "Decay", "When this player suffers a Casualty result on the Injury table, roll twice on the Casualty table and apply both results. The player will only ever miss one future match as a result of his injuries, even if he suffers two results with this effect.");
                case Effect.HypnoticGaze:
                    return new EffectData(EffectType.SkillExtraordinary, "Hypnotic Gaze", "The player may use hypnotic gaze at the end of his Move Action on one opposing player who is in an adjacent square. Make an Agility roll for the player with hypnotic gaze, with a -1 modifier for each opposing tackle zone on the player with hypnotic gaze other than the victim's. If the Agility roll is successful, then the opposing player loses his tackle zones and may not catch, intercept or pass the ball, assist another player on a block or foul, or move voluntarily until the start of his next action or the drive ends. If the roll fails, then the hypnotic gaze has no effect.");
                case Effect.Loner:
                    return new EffectData(EffectType.SkillExtraordinary, "Loner", "Loners are not great team players. If they wish to use a team re-roll, they must successfully pass a Loner roll (1/2 chance). If they fail, the re-roll is lost.");
                case Effect.MonstrousMouth:
                    return new EffectData(EffectType.SkillExtraordinary, "Monstrous Mouth", "A player with a Monstrous Mouth can reroll failed Catch, Handoff and Interception rolls. In addition, the Strip Ball skill will not work against a player with a Monstrous Mouth.");
                case Effect.NoHands:
                    return new EffectData(EffectType.SkillExtraordinary, "No Hands", "The player cannot carry the ball.");
                case Effect.NurglesRot:
                    return new EffectData(EffectType.SkillExtraordinary, "Nurgle's Rot", "Any opponent killed by this player becomes a Nurgle Rotter for the player's team.");
                case Effect.ReallyStupid:
                    return new EffectData(EffectType.SkillExtraordinary, "Really Stupid", "The player has a 1/2 chance of becoming Really Stupid and may not perform any actions for the rest of the turn, in addition they lose their tackle zone. If a team mate is standing adjacent to the player, the chance to fail is reduced to a 1/6 chance.");
                case Effect.Regeneration:
                    return new EffectData(EffectType.SkillExtraordinary, "Regeneration", "If they player is injured or killed, after any attempts by the Apothecary, they have a 1/2 chance to regenerate themselves and are placed in the reserves ready for the next drive.");
                case Effect.RightStuff:
                    return new EffectData(EffectType.SkillExtraordinary, "Right Stuff", "Enables a player to be thrown by a team-mate who possesses Throw Team-Mate skill.");
                case Effect.SecretWeapon:
                    return new EffectData(EffectType.SkillExtraordinary, "Secret Weapon", "At the end of any drive in which the player took part, he will automatically be sent off by the referee.");
                case Effect.Stab:
                    return new EffectData(EffectType.SkillExtraordinary, "Stab", "A player may attack an opponent with their stabbing attack instead of throwing a block at them. Make an unmodified Armor roll for the victim. If the score is less than or equal to the victim’s Armor value then the attack has no effect. If the score beats the victim’s Armor value then they have been wounded and an unmodified Injury roll must be made. If Stab is used as part of a Blitz Action, the player cannot continue moving after using it. Casualties caused by a stabbing attack do not count for Star Player points. For normal players there is only the Dark Elf Assassin who comes with the Stab ability. There are though some star players that have Stab and if you do get enough inducement money, you will be able to hire one.");
                case Effect.Stunty:
                    return new EffectData(EffectType.SkillExtraordinary, "Stunty", "A player with Stunty may ignore any enemy tackle zone on the square he is moving to when he makes a dodge, but subtract 1 from the roll when they pass. In addition, an Injury roll of 7 made against a Stunty player is considered a KO, and an Injury roll of 9 is considered a Badly Hurt casualty. Stunties that are armed with a secret weapon are not allowed to ignore enemy tackle zones.");
                case Effect.Swoop:
                    return new EffectData(EffectType.SkillExtraordinary, "Swoop", "When a player with the Swoop skill is thrown by a player with the Throw Teammate skill, use the Throw-in template rather than the Scatter template to determine where the player lands. For each square the player scatters, their coach places the Throw in template facing toward either end zone or either sideline, rolls a d6 and moves the player 1 square in the indicated direction. Additionally, when rolling to see if the player lands on their feet (per the Right Stuff rules) add 1 to the result. When a player with both Swoop and Stunty dodges, they DO NOT ignore the tackle zones on the square they are dodging to.");
                case Effect.TakeRoot:
                    return new EffectData(EffectType.SkillExtraordinary, "Take Root", "At the start of any action, the player rolls 1D6. If a 1 is rolled, the player may not move (or Block as part of a Blitz action) until the end of the drive, or they are knocked over.");
                case Effect.ThrowTeamMate:
                    return new EffectData(EffectType.SkillExtraordinary, "Throw Team-Mate", "Enables a player to throw a team-mate who possesses Right Stuff skill.");
                case Effect.Timmber:
                    return new EffectData(EffectType.SkillExtraordinary, "Timm-ber!", "When a player with this skill attempts to stand up, friendly adjacent players that are not in enemy tackle zones may assist in the effort. For each player able to assist, add 1 to the roll to stand up... note that a 1 is always a failure. Assisting a player to stand up does not count as an Action, and players may assist even if they have already taken an Action.");
                case Effect.Titchy:
                    return new EffectData(EffectType.SkillExtraordinary, "Titchy", "The player may add 1 to any Dodge roll he attempts. On the other hand, while opponents do have to dodge to leave any of a Titchy player’s tackle zones, Titchy players are so small that they do not exert a -1 modifier when opponents dodge into any of their tackle zones.");
                case Effect.WeepingDagger:
                    return new EffectData(EffectType.SkillExtraordinary, "Weeping Dagger", "If this player inflicts a casuality durning a block, and the result of the Casuality roll is 11-38 after any re-rolls, roll a D6. On a result of 4 or more, the opposing player must miss next game. Out of the referee's sight!");
                case Effect.WildAnimal:
                    return new EffectData(EffectType.SkillExtraordinary, "Wild Animal", "The player can become uncontrollable during a match and must pass a Wild Animal test to successfully perform an action. There is a 1/2 chance that they will be able to carry out the action required. This is increased to an 5/6 chance in case of a Block or Blitz.");


                // Bonuses
                case Effect.BonusMovement:
                    return new EffectData(EffectType.BonusMovement, "Bonus Movement", "Adds 1 to the Movement's stat");
                case Effect.BonusArmor:
                    return new EffectData(EffectType.BonusArmor, "Bonus Armor", "Adds 1 to the Armor's stat");
                case Effect.BonusAgility:
                    return new EffectData(EffectType.BonusAgility, "Bonus Agility", "Adds 1 to the Agility's stat");
                case Effect.BonusStrength:
                    return new EffectData(EffectType.BonusStrength, "Bonus Strength", "Adds 1 to the Strength's stat");


                // Casualties
                case Effect.BadlyHurt:
                    return new EffectData(EffectType.CasualtyBenign, "Badly Hurt", "No long term effect.");
                case Effect.BrokenJaw:
                    return new EffectData(EffectType.CasualtyRecovery, "Broken Jaw", "Miss next game.");
                case Effect.BrokenRibs:
                    return new EffectData(EffectType.CasualtyRecovery, "Broken Ribs", "Miss next game.");
                case Effect.FracturedArm:
                    return new EffectData(EffectType.CasualtyRecovery, "Fractured arm", "Miss next game.");
                case Effect.FracturedLeg:
                    return new EffectData(EffectType.CasualtyRecovery, "Fractured Leg", "Miss next game.");
                case Effect.SmashedHand:
                    return new EffectData(EffectType.CasualtyRecovery, "Smashed Hand", "Miss next game.");
                case Effect.GougedEye:
                    return new EffectData(EffectType.CasualtyRecovery, "Gouged Eye", "Miss next game.");
                case Effect.GroinStrain:
                    return new EffectData(EffectType.CasualtyRecovery, "Groin Strain", "Miss next game.");
                case Effect.PinchedNerve:
                    return new EffectData(EffectType.CasualtyRecovery, "Pinched Nerve", "Miss next game.");
                case Effect.DamagedBack:
                    return new EffectData(EffectType.CasualtyNiggling, "DamagedBack", "Niggling injury: adds 1 to all the player's injury rolls");
                case Effect.SmashedKnee:
                    return new EffectData(EffectType.CasualtyNiggling, "Smashed Knee", "Niggling injury: adds 1 to all the player's injury rolls");
                case Effect.SmashedAnkle:
                    return new EffectData(EffectType.CasualtyMovement, "SmashedAnkle", "Loses 1 point in Movement Allowance.");
                case Effect.SmashedHip:
                    return new EffectData(EffectType.CasualtyMovement, "Smashed Hip", "Loses 1 point in Movement Allowance.");
                case Effect.FracturedSkull:
                    return new EffectData(EffectType.CasualtyArmor, "Fractured Skull", "Loses 1 point in Armour Value.");
                case Effect.SeriousConcussion:
                    return new EffectData(EffectType.CasualtyArmor, "Serious Concussion", "Loses 1 point in Armour Value.");
                case Effect.BrokenNeck:
                    return new EffectData(EffectType.CasualtyAgility, "Broken Neck", "Loses 1 point in Agility.");
                case Effect.SmashedCollarBone:
                    return new EffectData(EffectType.CasualtyStrength, "SmashedCollarBone", "Loses 1 point in Strength.");
                case Effect.Dead:
                    return new EffectData(EffectType.CasualtyDeath, "Dead", "The player is dead; he is permanently removed from the team.");


                default:
                    return new EffectData(EffectType.CasualtyAgility, "Default Effect", "Lorem ipsum dolor amet");
            }
        }
    }
}
