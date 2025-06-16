using EntityStates;
using ExtraSkillSlots;
using MegamanEXEMod.Modules.BaseStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using RoR2;
using RoR2.Skills;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace MegamanEXEMod.Survivors.MegamanEXE.SkillStates
{
    public class SendChip : BaseSkillState
    {

        public static float BaseDuration = 2f;
        private bool shuffle = false;
        private ExtraSkillLocator extraskillLocator;


        public override void OnEnter()
        {
            extraskillLocator = base.GetComponent<ExtraSkillLocator>();

            AkSoundEngine.PostEvent(EXEStaticValues.SFXChipConfirm, this.gameObject);

            base.OnEnter();
        }

        public void ChipShuffle()
        {
            RemoveChips();

            List<SkillDef> shuffledChips = ChipSkillDefs.OrderBy(x => Random.value).ToList();

            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, shuffledChips[0], GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, shuffledChips[1], GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, shuffledChips[2], GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, shuffledChips[3], GenericSkill.SkillOverridePriority.Contextual);

            extraskillLocator.extraFirst.Reset();
            extraskillLocator.extraSecond.Reset();
            extraskillLocator.extraThird.Reset();
            extraskillLocator.extraFourth.Reset();

            shuffle = true;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (!shuffle && isAuthority)
            {
                ChipShuffle();
            }
            else
            {
                shuffle = false;
                this.outer.SetNextStateToMain();
            }


        }

        private void RemoveChips()
        {
            foreach (var chip in ChipSkillDefs)
            {
                extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, chip, GenericSkill.SkillOverridePriority.Contextual);
                extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, chip, GenericSkill.SkillOverridePriority.Contextual);
                extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, chip, GenericSkill.SkillOverridePriority.Contextual);
                extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, chip, GenericSkill.SkillOverridePriority.Contextual);
            }
        }

        private static readonly SkillDef[] ChipSkillDefs =
        {
            MegamanEXESurvivor.AirShotSkillDef,
            MegamanEXESurvivor.AquaSwrdSkillDef,
            MegamanEXESurvivor.Attack10SkillDef,
            MegamanEXESurvivor.Attack20SkillDef,
            MegamanEXESurvivor.Attack30SkillDef,

            MegamanEXESurvivor.Barr100SkillDef,
            MegamanEXESurvivor.Barr200SkillDef,
            MegamanEXESurvivor.BugFixSkillDef,

            MegamanEXESurvivor.CannonSkillDef,

            MegamanEXESurvivor.DrkBombSkillDef,
            MegamanEXESurvivor.DrkCannonSkillDef,
            MegamanEXESurvivor.DrkRecovSkillDef,
            MegamanEXESurvivor.DrkSwordSkillDef,
            MegamanEXESurvivor.DrkVulcanSkillDef,

            MegamanEXESurvivor.ElecSwrdSkillDef,

            MegamanEXESurvivor.FireSwrdSkillDef,
            MegamanEXESurvivor.FstGaugeSkillDef,

            MegamanEXESurvivor.GutPunchSkillDef,

            MegamanEXESurvivor.HiCannonSkillDef,

            MegamanEXESurvivor.InvisSkillDef,

            MegamanEXESurvivor.MCannonSkillDef,
            MegamanEXESurvivor.MiniBombSkillDef,
            MegamanEXESurvivor.MuramasaSkillDef,

            MegamanEXESurvivor.Recov300SkillDef,
            MegamanEXESurvivor.Recov50SkillDef,
            MegamanEXESurvivor.ReflectorSkillDef,

            MegamanEXESurvivor.SendChipSkillDef,
            MegamanEXESurvivor.ShokWaveSkillDef,
            MegamanEXESurvivor.ShotGunSkillDef,
            MegamanEXESurvivor.StepSwordSkillDef,
            MegamanEXESurvivor.SuprVulcSkillDef,

            MegamanEXESurvivor.ThunderSkillDef,
            MegamanEXESurvivor.VulcanSkillDef,
            MegamanEXESurvivor.YoyoSkillDef
        };

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Death;
        }

        public override void OnExit()
        {

            

            base.OnExit();
        }
    }
}