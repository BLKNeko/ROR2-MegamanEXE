using MegamanEXEMod.SkillStates;
using MegamanEXEMod.SkillStates.BaseStates;
using System.Collections.Generic;
using System;

namespace MegamanEXEMod.Modules
{
    public static class States
    {
        internal static void RegisterStates()
        {
            Modules.Content.AddEntityState(typeof(BaseMeleeAttack));
            Modules.Content.AddEntityState(typeof(SlashCombo));

            Modules.Content.AddEntityState(typeof(Shoot));

            Modules.Content.AddEntityState(typeof(Roll));

            Modules.Content.AddEntityState(typeof(ThrowBomb));


            Modules.Content.AddEntityState(typeof(AdvBarr500));
            Modules.Content.AddEntityState(typeof(AdvGigaCannon));
            Modules.Content.AddEntityState(typeof(AdvGreatYoyo));
            Modules.Content.AddEntityState(typeof(AdvInfiniteVulcan));
            Modules.Content.AddEntityState(typeof(AdvLifeSword));
            Modules.Content.AddEntityState(typeof(AirShot));
            Modules.Content.AddEntityState(typeof(AquaSwrd));
            Modules.Content.AddEntityState(typeof(Attack10));
            Modules.Content.AddEntityState(typeof(Attack30));
            Modules.Content.AddEntityState(typeof(Barr100));
            Modules.Content.AddEntityState(typeof(Barr200));
            Modules.Content.AddEntityState(typeof(BugFix));
            Modules.Content.AddEntityState(typeof(BusterEXE));
            Modules.Content.AddEntityState(typeof(Cannon));
            Modules.Content.AddEntityState(typeof(CyberSword));
            Modules.Content.AddEntityState(typeof(DrkBomb));
            Modules.Content.AddEntityState(typeof(DrkCannon));
            Modules.Content.AddEntityState(typeof(DrkRecov));
            Modules.Content.AddEntityState(typeof(DrkSword));
            Modules.Content.AddEntityState(typeof(DrkVulcan));
            Modules.Content.AddEntityState(typeof(ElecSwrd));
            Modules.Content.AddEntityState(typeof(FireSwrd));
            Modules.Content.AddEntityState(typeof(FstGauge));
            Modules.Content.AddEntityState(typeof(GutPunch));
            Modules.Content.AddEntityState(typeof(HiCannon));
            Modules.Content.AddEntityState(typeof(Invis));
            Modules.Content.AddEntityState(typeof(MCannon));
            Modules.Content.AddEntityState(typeof(MiniBomb));
            Modules.Content.AddEntityState(typeof(Muramasa));
            Modules.Content.AddEntityState(typeof(NoData));
            Modules.Content.AddEntityState(typeof(Recov300));
            Modules.Content.AddEntityState(typeof(Recov50));
            Modules.Content.AddEntityState(typeof(Reflector));
            Modules.Content.AddEntityState(typeof(SendChip));
            Modules.Content.AddEntityState(typeof(ShokWav));
            Modules.Content.AddEntityState(typeof(ShotGun));
            Modules.Content.AddEntityState(typeof(StepSwordAttack));
            Modules.Content.AddEntityState(typeof(StepSwordDash));
            Modules.Content.AddEntityState(typeof(SuprVulc));
            Modules.Content.AddEntityState(typeof(Thunder));
            Modules.Content.AddEntityState(typeof(Vulcan));
            Modules.Content.AddEntityState(typeof(Yoyo));


        }
    }
}