using System;
using MegamanEXEMod.Modules;
using MegamanEXEMod.Survivors.MegamanEXE.Achievements;

namespace MegamanEXEMod.Survivors.MegamanEXE
{
    public static class EXETokens
    {
        public static void Init()
        {
            //AddHenryTokens();
            AddEXETokens();

            ////uncomment this to spit out a lanuage file with all the above tokens that people can translate
            ////make sure you set Language.usingLanguageFolder and printingEnabled to true
            //Language.PrintOutput("MegamanEXE.txt");
            ////refer to guide on how to build and distribute your mod with the proper folders
        }

        public static void AddEXETokens()
        {
            string prefix = MegamanEXESurvivor.MMEXE_PREFIX;

            string desc = "Megaman.EXE's emotions shift depending on how you play.\n\n";
            desc += "< ! > Killing enemies and dealing damage will boost Megaman's morale and trigger FullSynchro. In this state, Megaman becomes stronger and deals 2x damage.\n\n";
            desc += "< ! > Taking hits or missing attacks will make Megaman anxious. In this state, he's weaker but faster.\n\n";
            desc += "< ! > When Megaman takes a certain amount of damage, he will enter an enraged state, also dealing 2x damage.\n\n";
            desc += "<color=#681da1>< ! > Dark Chips are extremely powerful but introduce various bugs into Megaman's data.\n\n";
            desc += "< ! > Repeated use of Dark Chips will trigger the Evil State, transforming Megaman into 'DarkMegaman' with several known malfunctions.<color=#CCD3E0>\n\n";
            desc += "< ! > Most bugs will wear off over time, but the BugFix chip can instantly remove most of them.\n\n";
            desc += "<color=#de9528>< ! > Use your Battle Chips with Program Advance in mind. These appear in the special skill slot with a golden icon. Try using any Sword chips 3 times, or Cannons 3 times.\n\n";
            desc += "< ! > A total of 6 Program Advances are available.\n\n";

            string outro = "..Great job, Megaman. Jack out!";
            string outroFailure = "...Megaman's data... Deleted.";

            Language.Add(prefix + "NAME", "MegamanEXE");
            Language.Add(prefix + "DESCRIPTION", desc);
            Language.Add(prefix + "SUBTITLE", "Jack in, Megaman!");
            Language.Add(prefix + "LORE", "A NetNavi who fights for justice in the digital world. Megaman.EXE now takes the battle to new data fields.");
            Language.Add(prefix + "OUTRO_FLAVOR", outro);
            Language.Add(prefix + "OUTRO_FAILURE", outroFailure);

            #region Skins
            Language.Add(prefix + "MMEXE_SKIN_NAME", "Megaman.EXE");
            Language.Add(prefix + "PROTO_SKIN_NAME", "Protoman.EXE");
            Language.Add(prefix + "ROLL_SKIN_NAME", "Roll.EXE");
            Language.Add(prefix + "BASS_SKIN_NAME", "Bass.EXE");
            Language.Add(prefix + "DIVEEXE_SKIN_NAME", "Megaman.EXE Dive");
            #endregion

            #region Passive
            Language.Add(prefix + "PASSIVE_NAME", "Emotion Synchronization");
            Language.Add(prefix + "PASSIVE_DESCRIPTION", "Depending on your performance and the chips used, Megaman’s emotions will change. Keep an eye on the buff icons.");

            #endregion

            #region Primary
            Language.Add(prefix + "_MEGAMAN_EXE_BODY_PRIMARY_EXEBUSTER_NAME", "Buster EXE");
            Language.Add(prefix + "_MEGAMAN_EXE_BODY_PRIMARY_EXEBUSTER_DESCRIPTION", $"A chargeable Buster that deals <style=cIsDamage>{100f * EXEStaticValues.EXEBusterDamageCoefficient}% damage</style>.");

            #endregion

            #region Secondary
            Language.Add(prefix + "_MEGAMAN_EXE_BODY_SECONDARY_CYBERSWORD_NAME", "Cyber Sword");
            Language.Add(prefix + "_MEGAMAN_EXE_BODY_SECONDARY_CYBERSWORD_DESCRIPTION", $"Slash with a Cyber Sword, dealing <style=cIsDamage>{100f * EXEStaticValues.CyberSwordDamageCoefficient}% damage</style>.");
            
            #endregion

            #region Utility
            Language.Add(prefix + "_MEGAMAN_EXE_BODY_UTILITY_SENDCHIPS_NAME", "Send Chip");
            Language.Add(prefix + "_MEGAMAN_EXE_BODY_UTILITY_SENDCHIPS_DESCRIPTION", "Send <style=cIsUtility>4</style> random Battle Chips.");
            #endregion

            #region Special
            Language.Add(prefix + "_MEGAMAN_EXE_BODY_CHIP_NODATA_NAME", "Program Advance");
            Language.Add(prefix + "_MEGAMAN_EXE_BODY_CHIP_NODATA_DESCRIPTION", $"When the correct combination of Battle Chips is used, this skill changes into a powerful Program Advance.");

            #endregion

            //CHIPS ---

            #region ADVChips

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_ADVAIRSHOT_NAME", "Adv. AirShot");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_ADVAIRSHOT_DESCRIPTION", "Program Advance: Fires a powerful air burst that knocks enemies back.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_ADVBARR500_NAME", "Adv. Barrier 500");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_ADVBARR500_DESCRIPTION", "Program Advance: Deploys a massive 500 HP barrier.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_ADVGIGACANNON_NAME", "Adv. GigaCannon");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_ADVGIGACANNON_DESCRIPTION", "Program Advance: Fires a massive energy blast shot.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_ADVGREATYOYO_NAME", "Adv. Great Yoyo");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_ADVGREATYOYO_DESCRIPTION", "Program Advance: Launches three powerful yoyo attack that hits multiple times.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_ADVINFINITEVULCAN_NAME", "Adv. Infinite Vulcan");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_ADVINFINITEVULCAN_DESCRIPTION", "Program Advance: Rapidly fires an endless stream of vulcan shots.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_ADVLIFESWORD_NAME", "Adv. LifeSword");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_ADVLIFESWORD_DESCRIPTION", "Program Advance: Summons the legendary sword that cuts through reality.");


            #endregion

            #region Chips

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_AIRSHOT_NAME", "AirShot");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_AIRSHOT_DESCRIPTION", "Fires a blast of air that knocks enemies back.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_AQUASWRD_NAME", "Aqua Sword");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_AQUASWRD_DESCRIPTION", "A sword imbued with Aqua element, freezing enemies.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_ATTACK10_NAME", "Attack +10");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_ATTACK10_DESCRIPTION", "Increases the power for a short period of time by 10%.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_ATTACK20_NAME", "Attack +20");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_ATTACK20_DESCRIPTION", "Increases the power for a short period of time by 20%.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_ATTACK30_NAME", "Attack +30");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_ATTACK30_DESCRIPTION", "Increases the power for a short period of time by 30%.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_BARR100_NAME", "Barrier 100");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_BARR100_DESCRIPTION", "Deploys a protective barrier that absorbs 100 damage.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_BARR200_NAME", "Barrier 200");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_BARR200_DESCRIPTION", "Deploys a protective barrier that absorbs 200 damage.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_BUGFIX_NAME", "BugFix");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_BUGFIX_DESCRIPTION", "Instantly removes all current data bugs from Megaman.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_CANNON_NAME", "Cannon");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_CANNON_DESCRIPTION", "Fires a straight projectile that deals heavy damage.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_DRKBOMB_NAME", "Dark Bomb");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_DRKBOMB_DESCRIPTION", "Dark Chip: Throws a bomb causing area damage and data corruption.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_DRKCANNON_NAME", "Dark Cannon");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_DRKCANNON_DESCRIPTION", "Dark Chip: Fires a corrupted cannon shot.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_DRKPLUS77_NAME", "Dark Plus77");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_DRKPLUS77_DESCRIPTION", "Dark Chip: Greatly increases damage at the cost of bugs.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_DRKRECOV_NAME", "Dark Recover");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_DRKRECOV_DESCRIPTION", "Dark Chip: Recovers HP but introduces multiple bugs.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_DRKSWORD_NAME", "Dark Sword");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_DRKSWORD_DESCRIPTION", "Dark Chip: A corrupted sword with devastating power.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_DRKVULCAN_NAME", "Dark Vulcan");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_DRKVULCAN_DESCRIPTION", "Dark Chip: Fires multiple corrupted bullets in rapid succession.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_ELECSWRD_NAME", "Elec Sword");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_ELECSWRD_DESCRIPTION", "A sword imbued with electricity, shocks enemies on contact.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_FIRESWRD_NAME", "Fire Sword");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_FIRESWRD_DESCRIPTION", "A blazing sword that burns enemies with fire element.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_FSTGAUGE_NAME", "Fast Gauge");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_FSTGAUGE_DESCRIPTION", "Speeds up the Battle Chip gauge for faster chip selection.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_GUTPUNCH_NAME", "Gut Punch");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_GUTPUNCH_DESCRIPTION", "A heavy punch that knocks enemies back.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_GUTPNCHSHOT_NAME", "Gut Punch Shot");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_GUTPNCHSHOT_DESCRIPTION", "Punch followed by a short-range blast.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_HICANNON_NAME", "HiCannon");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_HICANNON_DESCRIPTION", "Stronger version of Cannon, fires a powerful blast.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_INVIS_NAME", "Invis");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_INVIS_DESCRIPTION", "Grants temporary invincibility to avoid enemy attacks.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_MCANNON_NAME", "MCannon");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_MCANNON_DESCRIPTION", "Mid-tier Cannon, stronger than Cannon, weaker than HiCannon.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_MINIBOMB_NAME", "MiniBomb");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_MINIBOMB_DESCRIPTION", "Tosses a small bomb that explodes after some time.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_MURAMASA_NAME", "Muramasa");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_MURAMASA_DESCRIPTION", "Deals damage based on Megaman's missing HP.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_RECOV300_NAME", "Recover 300");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_RECOV300_DESCRIPTION", "Restores 300 HP.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_RECOV50_NAME", "Recover 50");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_RECOV50_DESCRIPTION", "Restores 50 HP.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_REFLECTOR_NAME", "Reflector");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_REFLECTOR_DESCRIPTION", "Reflects projectiles back at enemies.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_SHOKWAVE_NAME", "Shockwave");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_SHOKWAVE_DESCRIPTION", "Sends a shockwave along the ground that pierces through enemies.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_SHOTGUN_NAME", "Shotgun");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_SHOTGUN_DESCRIPTION", "Fires a wide blast that hits multiple targets.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_SPREADER_NAME", "Spreader");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_SPREADER_DESCRIPTION", "Fires a spread shot that damages enemies in an area.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_STEPSWORD_NAME", "Step Sword");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_STEPSWORD_DESCRIPTION", "Quickly dashes forward and slashes with a sword.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_SUPRVULC_NAME", "Super Vulcan");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_SUPRVULC_DESCRIPTION", "Fires a long volley of rapid bullets.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_THUNDER_NAME", "Thunder");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_THUNDER_DESCRIPTION", "Sends a thunderbolt at a nearby enemy.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_VULCAN_NAME", "Vulcan");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_VULCAN_DESCRIPTION", "Fires a rapid burst forward.");

            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_YOYO_NAME", "Yoyo");
            Language.Add($"{prefix}_MEGAMAN_EXE_BODY_CHIP_YOYO_DESCRIPTION", "Launches a yoyo that hits multiple times.");


            #endregion


            #region Achievements
            Language.Add(Tokens.GetAchievementNameToken(HenryMasteryAchievement.identifier), "MegamanEXE: Mastery");
            Language.Add(Tokens.GetAchievementDescriptionToken(HenryMasteryAchievement.identifier), "As MegamanEXE, beat the game or obliterate on Monsoon.");
            #endregion
        }

        public static void AddHenryTokens()
        {
            string prefix = MegamanEXESurvivor.MMEXE_PREFIX;

            string desc = "MegamanEXE is a skilled fighter who makes use of a wide arsenal of weaponry to take down his foes.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine
             + "< ! > Sword is a good all-rounder while Boxing Gloves are better for laying a beatdown on more powerful foes." + Environment.NewLine + Environment.NewLine
             + "< ! > Pistol is a powerful anti air, with its low cooldown and high damage." + Environment.NewLine + Environment.NewLine
             + "< ! > Roll has a lingering armor buff that helps to use it aggressively." + Environment.NewLine + Environment.NewLine
             + "< ! > Bomb can be used to wipe crowds with ease." + Environment.NewLine + Environment.NewLine;

            string outro = "..and so he left, searching for a new identity.";
            string outroFailure = "..and so he vanished, forever a blank slate.";

            Language.Add(prefix + "NAME", "MegamanEXE");
            Language.Add(prefix + "DESCRIPTION", desc);
            Language.Add(prefix + "SUBTITLE", "The Chosen One");
            Language.Add(prefix + "LORE", "sample lore");
            Language.Add(prefix + "OUTRO_FLAVOR", outro);
            Language.Add(prefix + "OUTRO_FAILURE", outroFailure);

            #region Skins
            Language.Add(prefix + "MASTERY_SKIN_NAME", "Alternate");
            #endregion

            #region Passive
            Language.Add(prefix + "PASSIVE_NAME", "MegamanEXE passive");
            Language.Add(prefix + "PASSIVE_DESCRIPTION", "Sample text.");
            #endregion

            #region Primary
            Language.Add(prefix + "PRIMARY_SLASH_NAME", "Sword");
            Language.Add(prefix + "PRIMARY_SLASH_DESCRIPTION", Tokens.agilePrefix + $"Swing forward for <style=cIsDamage>{100f * EXEStaticValues.swordDamageCoefficient}% damage</style>.");
            #endregion

            #region Secondary
            Language.Add(prefix + "SECONDARY_GUN_NAME", "Handgun");
            Language.Add(prefix + "SECONDARY_GUN_DESCRIPTION", Tokens.agilePrefix + $"Fire a handgun for <style=cIsDamage>{100f * EXEStaticValues.gunDamageCoefficient}% damage</style>.");
            #endregion

            #region Utility
            Language.Add(prefix + "UTILITY_ROLL_NAME", "Roll");
            Language.Add(prefix + "UTILITY_ROLL_DESCRIPTION", "Roll a short distance, gaining <style=cIsUtility>300 armor</style>. <style=cIsUtility>You cannot be hit during the roll.</style>");
            #endregion

            #region Special
            Language.Add(prefix + "SPECIAL_BOMB_NAME", "Bomb");
            Language.Add(prefix + "SPECIAL_BOMB_DESCRIPTION", $"Throw a bomb for <style=cIsDamage>{100f * EXEStaticValues.bombDamageCoefficient}% damage</style>.");
            #endregion

            #region Achievements
            Language.Add(Tokens.GetAchievementNameToken(HenryMasteryAchievement.identifier), "MegamanEXE: Mastery");
            Language.Add(Tokens.GetAchievementDescriptionToken(HenryMasteryAchievement.identifier), "As MegamanEXE, beat the game or obliterate on Monsoon.");
            #endregion
        }
    }
}
