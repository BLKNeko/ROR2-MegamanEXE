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

            string desc = "Henry is a skilled fighter who makes use of a wide arsenal of weaponry to take down his foes.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Sword is a good all-rounder while Boxing Gloves are better for laying a beatdown on more powerful foes." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Pistol is a powerful anti air, with its low cooldown and high damage." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Roll has a lingering armor buff that helps to use it aggressively." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Bomb can be used to wipe crowds with ease." + Environment.NewLine + Environment.NewLine;

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
            LanguageAPI.Add(prefix + "PRIMARY_BUSTEREXE_DESCRIPTION", Helpers.agilePrefix + $"Swing forward for <style=cIsDamage>{100f * StaticValues.swordDamageCoefficient}% damage</style>.");
            #endregion

            #region Secondary
            LanguageAPI.Add(prefix + "SECONDARY_CYBERSWORD_NAME", "Cyber Sword");
            LanguageAPI.Add(prefix + "SECONDARY_CYBERSWORD_DESCRIPTION", Helpers.agilePrefix + $"Fire a handgun for <style=cIsDamage>{100f * StaticValues.gunDamageCoefficient}% damage</style>.");
            #endregion

            #region Utility
            LanguageAPI.Add(prefix + "UTILITY_SENDCHIPS_NAME", "Send Chip");
            LanguageAPI.Add(prefix + "UTILITY_SENDCHIPS_DESCRIPTION", "Send <style=cIsUtility>4</style> random battle chips.");
            #endregion

            #region Special
            LanguageAPI.Add(prefix + "SPECIAL_ADVP_NAME", "Advanced Program");
            LanguageAPI.Add(prefix + "SPECIAL_ADVP_DESCRIPTION", "If a correct order of Battle Chips are used, this skill changes for an Advanced Program.");
            #endregion




            LanguageAPI.Add(prefix + "CHIP_NODATA_NAME", "Advanced Program");
            LanguageAPI.Add(prefix + "CHIP_NODATA_DESCRIPTION", "If a correct order of Battle Chips are used, this skill changes for an Advanced Program.");


            #region Achievements
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_ACHIEVEMENT_NAME", "Henry: Mastery");
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_ACHIEVEMENT_DESC", "As Henry, beat the game or obliterate on Monsoon.");
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_UNLOCKABLE_NAME", "Henry: Mastery");
            #endregion
            #endregion
        }
    }
}