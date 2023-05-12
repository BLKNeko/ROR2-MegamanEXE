using EntityStates;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using ExtraSkillSlots;
using MegamanEXEMod.Modules.Survivors;

namespace MegamanEXEMod.SkillStates
{
    public class SendChip : BaseSkillState
    {

        public static float BaseDuration = 2f;
        private bool shuffle = false;
        private ExtraSkillLocator extraskillLocator;
        private float SkillId1;
        private float SkillId2;
        private float SkillId3;
        private float SkillId4;


        public override void OnEnter()
        {
            extraskillLocator = base.GetComponent<ExtraSkillLocator>();

            
            base.OnEnter();
        }

        public void ChipShuffle()
        {

            //unsetoverrida all skills

            //unset primary
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.TesteSkill2, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.MiniBombSkillDef, GenericSkill.SkillOverridePriority.Contextual);

            //unset sec
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.TesteSkill2, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.MiniBombSkillDef, GenericSkill.SkillOverridePriority.Contextual);

            //unset tri
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.TesteSkill2, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.MiniBombSkillDef, GenericSkill.SkillOverridePriority.Contextual);

            //unset four
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.TesteSkill2, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.MiniBombSkillDef, GenericSkill.SkillOverridePriority.Contextual);




            SkillId1 = Random.Range(1, 4);
            SkillId2 = Random.Range(1, 4);
            SkillId3 = Random.Range(1, 4);
            SkillId4 = Random.Range(1, 4);

            Debug.Log("SkillId1 Value:");
            Debug.Log(SkillId1);
            Debug.Log("SkillId2 Value:");
            Debug.Log(SkillId3);
            Debug.Log("SkillId3 Value:");
            Debug.Log(SkillId3);
            Debug.Log("SkillId4 Value:");
            Debug.Log(SkillId4);

            //extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);

            switch (SkillId1)
            {
                case 1:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.TesteSkill2, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to skill2");
                    break;
                case 2:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to sword");
                    break;
                case 3:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.MiniBombSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                default:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to default");
                    break;
            }

            switch (SkillId2)
            {
                case 1:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.TesteSkill2, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId2 to skill2");
                    break;
                case 2:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId2 to sword");
                    break;
                case 3:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.MiniBombSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId2 to bomb");
                    break;
                default:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId2 to default");
                    break;
            }

            switch (SkillId3)
            {
                case 1:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.TesteSkill2, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId3 to skill2");
                    break;
                case 2:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId3 to sword");
                    break;
                case 3:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.MiniBombSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId3 to bomb");
                    break;
                default:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId3 to default");
                    break;
            }

            switch (SkillId4)
            {
                case 1:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.TesteSkill2, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId4 to skill2");
                    break;
                case 2:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId4 to sword");
                    break;
                case 3:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.MiniBombSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId4 to bomb");
                    break;
                default:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId4 to default");
                    break;
            }


            extraskillLocator.extraFirst.Reset();
            extraskillLocator.extraSecond.Reset();
            extraskillLocator.extraThird.Reset();
            extraskillLocator.extraFourth.Reset();


            shuffle = true;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (!shuffle)
            {
                ChipShuffle();
            }
            else
            {
                shuffle = false;
                this.outer.SetNextStateToMain();
            }


        }

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