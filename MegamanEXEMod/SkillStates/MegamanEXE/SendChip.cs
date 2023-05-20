using EntityStates;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using ExtraSkillSlots;
using MegamanEXEMod.Modules.Survivors;
using MegamanEXEMod.Modules;

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

            Util.PlaySound(Sounds.SFXChipConfirm, base.gameObject);

            base.OnEnter();
        }

        public void ChipShuffle()
        {

            //unsetoverrida all skills

            //unset primary
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.AirShotSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.AquaSwrdSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.Barr100SkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.CannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.DrkSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.ElecSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.FireSwrdSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.HiCannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.InvisSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.MCannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.MiniBombSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.MuramasaSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.Recov50SkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.ShokWaveSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.ShotGunSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.SuprVulcSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.ThunderSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.VulcanSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.YoyoSkillDef, GenericSkill.SkillOverridePriority.Contextual);



            //unset sec
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.AirShotSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.AquaSwrdSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.Barr100SkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.CannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.DrkSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.ElecSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.FireSwrdSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.HiCannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.InvisSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.MCannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.MiniBombSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.MuramasaSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.Recov50SkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.ShokWaveSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.ShotGunSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.SuprVulcSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.ThunderSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.VulcanSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.YoyoSkillDef, GenericSkill.SkillOverridePriority.Contextual);

            //unset tri
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.AirShotSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.Barr100SkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.CannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.DrkSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.ElecSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.FireSwrdSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.HiCannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.AquaSwrdSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.InvisSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.MCannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.MiniBombSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.MuramasaSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.Recov50SkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.ShokWaveSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.ShotGunSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.SuprVulcSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.ThunderSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.VulcanSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, MegamanEXE.YoyoSkillDef, GenericSkill.SkillOverridePriority.Contextual);

            //unset four
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.AirShotSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.CannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.DrkSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.ElecSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.FireSwrdSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.Barr100SkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.HiCannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.AquaSwrdSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.InvisSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.MCannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.MiniBombSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.MuramasaSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.Recov50SkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.ShokWaveSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.ShotGunSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.SuprVulcSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.ThunderSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.VulcanSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.YoyoSkillDef, GenericSkill.SkillOverridePriority.Contextual);




            SkillId2 = Random.Range(1, 21);
            SkillId3 = Random.Range(1, 21);
            SkillId1 = Random.Range(1, 21);
            SkillId4 = Random.Range(1, 21);

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
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.AirShotSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to skill2");
                    break;
                case 2:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.CannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to sword");
                    break;
                case 3:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 4:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.DrkSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 5:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.ElecSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 6:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.FireSwrdSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 7:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.Barr100SkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 8:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.AquaSwrdSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 9:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.InvisSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 10:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.MCannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 11:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.MiniBombSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 12:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.MuramasaSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 13:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.Recov50SkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 14:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.ShokWaveSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 15:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.ShotGunSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 16:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.SuprVulcSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 17:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.ThunderSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 18:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.VulcanSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 19:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.YoyoSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 20:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.MiniBombSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                default:
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, MegamanEXE.DrkSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to default");
                    break;
            }

            switch (SkillId2)
            {
                case 1:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.AirShotSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to skill2");
                    break;
                case 2:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.CannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to sword");
                    break;
                case 3:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 4:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.DrkSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 5:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.ElecSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 6:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.FireSwrdSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 7:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.Barr100SkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 8:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.AquaSwrdSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 9:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.InvisSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 10:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.MCannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 11:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.MiniBombSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 12:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.MuramasaSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 13:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.Recov50SkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 14:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.ShokWaveSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 15:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.ShotGunSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 16:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.SuprVulcSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 17:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.ThunderSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 18:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.VulcanSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 19:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.YoyoSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 20:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.MiniBombSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                default:
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, MegamanEXE.DrkSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to default");
                    break;
            }

            switch (SkillId3)
            {
                case 1:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.AirShotSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to skill2");
                    break;
                case 2:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.CannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to sword");
                    break;
                case 3:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 4:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.DrkSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 5:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.ElecSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 6:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.FireSwrdSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 7:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.Barr100SkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 8:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.AquaSwrdSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 9:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.InvisSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 10:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.MCannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 11:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.MiniBombSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 12:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.MuramasaSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 13:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.Recov50SkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 14:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.ShokWaveSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 15:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.ShotGunSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 16:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.SuprVulcSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 17:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.ThunderSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 18:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.VulcanSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 19:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.YoyoSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 20:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.MiniBombSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                default:
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, MegamanEXE.DrkSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to default");
                    break;
            }

            switch (SkillId4)
            {
                case 1:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.AirShotSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to skill2");
                    break;
                case 2:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.CannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to sword");
                    break;
                case 3:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 4:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.DrkSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 5:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.ElecSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 6:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.FireSwrdSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 7:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.Barr100SkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 8:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.AquaSwrdSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 9:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.InvisSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 10:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.MCannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 11:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.MiniBombSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 12:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.MuramasaSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 13:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.Recov50SkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 14:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.ShokWaveSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 15:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.ShotGunSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 16:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.SuprVulcSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 17:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.ThunderSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 18:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.VulcanSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 19:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.YoyoSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                case 20:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.MiniBombSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to bomb");
                    break;
                default:
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, MegamanEXE.DrkSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                    Debug.Log("SkillId1 to default");
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