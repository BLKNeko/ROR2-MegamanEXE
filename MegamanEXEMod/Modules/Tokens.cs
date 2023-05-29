using R2API;
using System;

namespace MegamanEXEMod.Modules
{
    internal static class Tokens
    {
        internal static void AddTokens()
        {
            #region Henry
            string prefix = MegamanEXEPlugin.DEVELOPER_PREFIX + "_MEGAMAN_EXE_BODY_";

            string desc = "Megaman.EXE's emotions will change depending on how you play.\n\n";
            desc = desc + "< ! > Killing enemies and causing damage to them will increase Megaman's mood and make him enter FullSynchro state. In this state, Megaman is stronger and deals 2x damage.\n\n";
            desc = desc + "< ! > Taking hits and missing attacks will make Megaman anxious. In this state, Megaman is weaker but faster.\n\n";
            desc = desc + "< ! > When Megaman takes a certain amount of damage, he will enter an enraged state, causing 2x damage.\n\n";
            desc = desc + "<color=#681da1>< ! > Dark chips are super powerful but cause various bugs in Megaman's data.\n\n";
            desc = desc + "< ! > The continuous use of Dark Chips will cause Megaman to enter an Evil State, transforming into 'DarkMegaman' with a lot of expected bugs in this state.<color=#CCD3E0>\n\n";
            desc = desc + "< ! > Most of the bugs will be removed over time, but the BugFix chip can take care of most of them instantly.\n\n";
            desc = desc + "<color=#de9528>< ! > Use your battle chips with Program Advance in mind. They will appear in the 'special skill slot' with a golden icon. Try using any sword battle chips 3 times or cannon 3 times.\n\n";
            desc = desc + "< ! > There are a total of 5 Program Advances available.\n\n";

            /*
            string desc = "Megaman.EXE's emotions will change depending on how you play.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Kill enemies and cause damage to them will increase megaman's mood and make him enter FullSynchro state, in this state mageman is stronger and cause 2x damage" + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Take hits and miss attacks will make Megaman Anxious, on this state mageman is weaker, but faster." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > When megaman take a certain amount of damage, he will enter anraged state, causing 2x damage." + Environment.NewLine + Environment.NewLine;
            desc = desc + "<color=#681da1>< ! > Dark chips are super powerfull, but cause various bugs on Megaman's data." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > The continuou use of DarkChips will cause Megaman to enter on Evil State, he will became 'DarkMegaman' and a lot of bugs are expected on this state.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Most of the bugs will be removed by time, but BugFix chip can take cara o most of then in no time." + Environment.NewLine + Environment.NewLine;
            desc = desc + "<color=#de9528>< ! > Use your battle chips with Program Advance in mind, they will apear on 'special skill slot' with a golden icon, try on use any sword battlechips 3 times or cannon 3 times." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > there a total of 5 Programa Advance available." + Environment.NewLine + Environment.NewLine;
            */

            string outro = "..Great Job Megaman, Jack Out!";
            string outroFailure = "...Megaman data...Deleted.";

            LanguageAPI.Add(prefix + "NAME", "Megaman.EXE");
            LanguageAPI.Add(prefix + "DESCRIPTION", desc);
            LanguageAPI.Add(prefix + "SUBTITLE", "Jack In Megaman!");
            LanguageAPI.Add(prefix + "LORE", "Megaman.EXE");
            LanguageAPI.Add(prefix + "OUTRO_FLAVOR", outro);
            LanguageAPI.Add(prefix + "OUTRO_FAILURE", outroFailure);

            #region Skins
            LanguageAPI.Add(prefix + "DEFAULT_SKIN_NAME", "Default");
            LanguageAPI.Add(prefix + "MASTERY_SKIN_NAME", "Alternate");
            #endregion

            #region Passive
            LanguageAPI.Add(prefix + "PASSIVE_NAME", "Emotion Syncronization");
            LanguageAPI.Add(prefix + "PASSIVE_DESCRIPTION", "Depending on your operation and battle chips used, Megaman emotions will change, keep track on Buff icons");
            #endregion

            #region Primary
            LanguageAPI.Add(prefix + "PRIMARY_BUSTEREXE_NAME", "Buster EXE");
            LanguageAPI.Add(prefix + "PRIMARY_BUSTEREXE_DESCRIPTION", "Shoot for <style=cIsDamage>100% damage</style> and charge for <style=cIsDamage>150% damage</style>.");
            #endregion

            #region Secondary
            LanguageAPI.Add(prefix + "SECONDARY_CYBERSWORD_NAME", "Cyber Sword");
            LanguageAPI.Add(prefix + "SECONDARY_CYBERSWORD_DESCRIPTION", "Swing with a CyberSword for<style=cIsDamage>300% damage</style>.");
            #endregion

            #region Utility
            LanguageAPI.Add(prefix + "UTILITY_SENDCHIPS_NAME", "Send Chip");
            LanguageAPI.Add(prefix + "UTILITY_SENDCHIPS_DESCRIPTION", "Send <style=cIsUtility>4</style> random battle chips.");
            #endregion

            #region Special
            LanguageAPI.Add(prefix + "SPECIAL_ADVP_NAME", "Program Advance");
            LanguageAPI.Add(prefix + "SPECIAL_ADVP_DESCRIPTION", "If a correct order of Battle Chips are used, this skill changes for an Advanced Program.");
            #endregion




            LanguageAPI.Add(prefix + "CHIP_NODATA_NAME", "Program Advance");
            LanguageAPI.Add(prefix + "CHIP_NODATA_DESCRIPTION", "If a correct order of Battle Chips are used, this skill changes for an Advanced Program.");



            LanguageAPI.Add(prefix + "CHIP_ADVBARR500_NAME", "Advanced Barrier 500");
            LanguageAPI.Add(prefix + "CHIP_ADVBARR500_DESCRIPTION", "Add Barrier 500");

            LanguageAPI.Add(prefix + "CHIP_GIGACANNON_NAME", "Advanced GigaCannon");
            LanguageAPI.Add(prefix + "CHIP_GIGACANNON_DESCRIPTION", "If a correct order of Battle Chips are used, this skill changes for an Advanced Program.");

            LanguageAPI.Add(prefix + "CHIP_GREATYOYO_NAME", "Advanced GreatYoyo");
            LanguageAPI.Add(prefix + "CHIP_GREATYOYO_DESCRIPTION", "If a correct order of Battle Chips are used, this skill changes for an Advanced Program.");

            LanguageAPI.Add(prefix + "CHIP_INFVULC_NAME", "Advanced Infinite Vulcan");
            LanguageAPI.Add(prefix + "CHIP_INFVULC_DESCRIPTION", "If a correct order of Battle Chips are used, this skill changes for an Advanced Program.");

            LanguageAPI.Add(prefix + "CHIP_LIFESWORD_NAME", "Advanced Life Sword");
            LanguageAPI.Add(prefix + "CHIP_LIFESWORD_DESCRIPTION", "If a correct order of Battle Chips are used, this skill changes for an Advanced Program.");

            LanguageAPI.Add(prefix + "CHIP_AIRSHOT_NAME", "AirShot");
            LanguageAPI.Add(prefix + "CHIP_AIRSHOT_DESCRIPTION", "Air shot with high knockback");

            LanguageAPI.Add(prefix + "CHIP_AQUASWRD_NAME", "Aqua Sword");
            LanguageAPI.Add(prefix + "CHIP_AQUASWRD_DESCRIPTION", "If a correct order of Battle Chips are used, this skill changes for an Advanced Program.");

            LanguageAPI.Add(prefix + "CHIP_ATK10_NAME", "ATK10");
            LanguageAPI.Add(prefix + "CHIP_ATK10_DESCRIPTION", "Gain +10% of damage");

            LanguageAPI.Add(prefix + "CHIP_ATK30_NAME", "ATK30");
            LanguageAPI.Add(prefix + "CHIP_ATK30_DESCRIPTION", "If a correct order of Battle Chips are used, this skill changes for an Advanced Program.");

            LanguageAPI.Add(prefix + "CHIP_BARR100_NAME", "Barr100");
            LanguageAPI.Add(prefix + "CHIP_BARR100_DESCRIPTION", "chip");

            LanguageAPI.Add(prefix + "CHIP_BARR200_NAME", "Barr200");
            LanguageAPI.Add(prefix + "CHIP_BARR200_DESCRIPTION", "chip");

            LanguageAPI.Add(prefix + "CHIP_BUGFIX_NAME", "BugFix");
            LanguageAPI.Add(prefix + "CHIP_BUGFIX_DESCRIPTION", "Remove most bugs and evil points.");

            LanguageAPI.Add(prefix + "CHIP_CANNON_NAME", "Cannon");
            LanguageAPI.Add(prefix + "CHIP_CANNON_DESCRIPTION", "A power shot with a cannon arm.");

            LanguageAPI.Add(prefix + "CHIP_DARKBOMB_NAME", "Dark Bomb");
            LanguageAPI.Add(prefix + "CHIP_DARKBOMB_DESCRIPTION", "chip");

            LanguageAPI.Add(prefix + "CHIP_DARKCANNON_NAME", "Dark Cannon");
            LanguageAPI.Add(prefix + "CHIP_DARKCANNON_DESCRIPTION", "chip");

            LanguageAPI.Add(prefix + "CHIP_DARKRECOV_NAME", "Dark Recov");
            LanguageAPI.Add(prefix + "CHIP_DARKRECOV_DESCRIPTION", "Recover <style=cIsUtility>1000 HP</style> and a little extra if posible.");

            LanguageAPI.Add(prefix + "CHIP_DRKSWRD_NAME", "Dark Sword");
            LanguageAPI.Add(prefix + "CHIP_DRKSWRD_DESCRIPTION", "Powerfull Dark Sword");

            LanguageAPI.Add(prefix + "CHIP_DARKVULCAN_NAME", "Dark Vulcan");
            LanguageAPI.Add(prefix + "CHIP_DARKVULCAN_DESCRIPTION", "chip");

            LanguageAPI.Add(prefix + "CHIP_ELECSWRD_NAME", "Elec Sword");
            LanguageAPI.Add(prefix + "CHIP_ELECSWRD_DESCRIPTION", "chip");

            LanguageAPI.Add(prefix + "CHIP_FIRESWRD_NAME", "Fire Sword");
            LanguageAPI.Add(prefix + "CHIP_FIRESWRD_DESCRIPTION", "chip");

            LanguageAPI.Add(prefix + "CHIP_FSTGAUGE_NAME", "FstGauge");
            LanguageAPI.Add(prefix + "CHIP_FSTGAUGE_DESCRIPTION", "Instante remove 'SendChip' cooldown.");

            LanguageAPI.Add(prefix + "CHIP_GUTPUNCH_NAME", "Gut Punch");
            LanguageAPI.Add(prefix + "CHIP_GUTPUNCH_DESCRIPTION", "chip");

            LanguageAPI.Add(prefix + "CHIP_HICANNON_NAME", "HiCannon");
            LanguageAPI.Add(prefix + "CHIP_HICANNON_DESCRIPTION", "chip");

            LanguageAPI.Add(prefix + "CHIP_INVIS_NAME", "Invis");
            LanguageAPI.Add(prefix + "CHIP_INVIS_DESCRIPTION", "chip");

            LanguageAPI.Add(prefix + "CHIP_MCANNON_NAME", "MCannon");
            LanguageAPI.Add(prefix + "CHIP_MCANNON_DESCRIPTION", "chip");

            LanguageAPI.Add(prefix + "CHIP_MINIBOMB_NAME", "Mini Bomb");
            LanguageAPI.Add(prefix + "CHIP_MINIBOMB_DESCRIPTION", "Small granades");

            LanguageAPI.Add(prefix + "CHIP_MURAMASA_NAME", "Muramasa");
            LanguageAPI.Add(prefix + "CHIP_MURAMASA_DESCRIPTION", "chip");

            LanguageAPI.Add(prefix + "CHIP_RECOV300_NAME", "Recov300");
            LanguageAPI.Add(prefix + "CHIP_RECOV300_DESCRIPTION", "chip");

            LanguageAPI.Add(prefix + "CHIP_RECOV50_NAME", "Recov50");
            LanguageAPI.Add(prefix + "CHIP_RECOV50_DESCRIPTION", "Recover <style=cIsUtility>50 HP</style>.");

            LanguageAPI.Add(prefix + "CHIP_SHOKWAVE_NAME", "ShokWave");
            LanguageAPI.Add(prefix + "CHIP_SHOKWAVE_DESCRIPTION", "chip");

            LanguageAPI.Add(prefix + "CHIP_SHOTGUN_NAME", "Shotgun");
            LanguageAPI.Add(prefix + "CHIP_SHOTGUN_DESCRIPTION", "chip");

            LanguageAPI.Add(prefix + "CHIP_STEPSWORD_NAME", "Step Sword");
            LanguageAPI.Add(prefix + "CHIP_STEPSWORD_DESCRIPTION", "Perform a dash and attack if enemy is on range");

            LanguageAPI.Add(prefix + "CHIP_SUPRVULC_NAME", "Supr Vulcan");
            LanguageAPI.Add(prefix + "CHIP_SUPRVULC_DESCRIPTION", "chip");

            LanguageAPI.Add(prefix + "CHIP_THUNDER_NAME", "Thunder");
            LanguageAPI.Add(prefix + "CHIP_THUNDER_DESCRIPTION", "chip");

            LanguageAPI.Add(prefix + "CHIP_VULCAN_NAME", "Vulcan");
            LanguageAPI.Add(prefix + "CHIP_VULCAN_DESCRIPTION", "Fast gattling shots.");

            LanguageAPI.Add(prefix + "CHIP_YOYO_NAME", "Yoyo");
            LanguageAPI.Add(prefix + "CHIP_YOYO_DESCRIPTION", "chip");






            #region Achievements
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_ACHIEVEMENT_NAME", "Henry: Mastery");
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_ACHIEVEMENT_DESC", "As Henry, beat the game or obliterate on Monsoon.");
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_UNLOCKABLE_NAME", "Henry: Mastery");
            #endregion
            #endregion
        }
    }
}