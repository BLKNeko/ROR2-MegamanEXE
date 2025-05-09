using RoR2;
using MegamanEXEMod.Modules.Achievements;

namespace MegamanEXEMod.Survivors.Henry.Achievements
{
    //automatically creates language tokens "ACHIEVMENT_{identifier.ToUpper()}_NAME" and "ACHIEVMENT_{identifier.ToUpper()}_DESCRIPTION" 
    [RegisterAchievement(identifier, unlockableIdentifier, null, 10, null)]
    public class HenryMasteryAchievement : BaseMasteryAchievement
    {
        public const string identifier = MegamanEXESurvivor.HENRY_PREFIX + "masteryAchievement";
        public const string unlockableIdentifier = MegamanEXESurvivor.HENRY_PREFIX + "masteryUnlockable";

        public override string RequiredCharacterBody => MegamanEXESurvivor.instance.bodyName;

        //difficulty coeff 3 is monsoon. 3.5 is typhoon for grandmastery skins
        public override float RequiredDifficultyCoefficient => 3;
    }
}