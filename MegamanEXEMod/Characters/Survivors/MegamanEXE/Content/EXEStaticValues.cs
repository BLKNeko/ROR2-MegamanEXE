using MegamanEXEMod.Modules;
using System;
using UnityEngine;

namespace MegamanEXEMod.Survivors.MegamanEXE
{
    public static class EXEStaticValues
    {
        public const float swordDamageCoefficient = 2.8f;

        public const float gunDamageCoefficient = 4.2f;

        public const float bombDamageCoefficient = 16f;

        public const float EXEBusterDamageCoefficient = 1f;

        public const float CyberSwordDamageCoefficient = 3f;


        //CHIP DAMAGES


        public static readonly float XMidChargeDamageCoefficient = EXEConfig.midChargeMultiplierFloat.Value;

        public static readonly float XFullChargeDamageCoefficient = EXEConfig.fullChargeMultiplierFloat.Value;

        

        public const float AdvAirShotSkillDefDamageCoefficient = 12f; 
        public const float AdvGigaCannonSkillDefDamageCoefficient = 30f; 
        public const float AdvGreatYoyoSkillDefDamageCoefficient = 10f; 
        public const float AdvInfiniteVulcanSkillDefDamageCoefficient = 1.4f; 
        public const float AdvLifeSwordSkillDefDamageCoefficient = 40f; 

        public const float AirShotSkillDefDamageCoefficient = 1.2f; 
        public const float AquaSwrdSkillDefDamageCoefficient = 5.75f; 

        public const float CannonSkillDefDamageCoefficient = 1.5f; 

        public const float DrkBombSkillDefDamageCoefficient = 20f; 
        public const float DrkCannonSkillDefDamageCoefficient = 30f; 
        public const float DrkSwordSkillDefDamageCoefficient = 40f; 
        public const float DrkVulcanSkillDefDamageCoefficient = 2.5f; 

        public const float ElecSwrdSkillDefDamageCoefficient = 5.25f; 

        public const float FireSwrdSkillDefDamageCoefficient = 5.5f; 

        public const float GutPunchSkillDefDamageCoefficient = 4f; 
        public const float GutPnchShotSkillDefDamageCoefficient = 3f; 

        public const float HiCannonSkillDefDamageCoefficient = 2.25f; 

        public const float MCannonSkillDefDamageCoefficient = 3.4f; 
        public const float MiniBombSkillDefDamageCoefficient = 1.5f; 
        public const float MuramasaSkillDefDamageCoefficient = 1f; 


        public const float ReflectorSkillDefDamageCoefficient = 3f; 

        public const float ShokWaveSkillDefDamageCoefficient = 1.8f; 
        public const float ShotGunSkillDefDamageCoefficient = 1.4f; 
        public const float SpreaderSkillDefDamageCoefficient = 1.5f; 
        public const float StepSwordSkillDefDamageCoefficient = 5.5f; 
        public const float SuprVulcSkillDefDamageCoefficient = 1.5f; 

        public const float ThunderSkillDefDamageCoefficient = 1.45f; 

        public const float VulcanSkillDefDamageCoefficient = 1.2f; 

        public const float YoyoSkillDefDamageCoefficient = 1.5f;



        // SOUNDS

        public static readonly string BusterCharging = "Play_BusterCharging";
        public static readonly string BusterCharged = "Play_BusterCharged";
        public static readonly string BusterEXE = "Play_BusterEXE";
        public static readonly string SwordSwing = "Play_SwordSwing";
        public static readonly string SFXRecov = "Play_Recover";
        public static readonly string SFXInvis = "Play_TurnInvisible";
        public static readonly string SFXRedHP = "Play_RedHP";
        public static readonly string SFXCanon = "Play_Cannon";
        public static readonly string SFXBarrier = "Play_Barrier";
        public static readonly string SFXChipConfirm = "Play_ChipConfirm";
        public static readonly string SFXVulcan = "Play_Vulcan";
        public static readonly string SFXThunder = "Play_Thunder";
        public static readonly string SFXDeleted = "Play_Megaman_Deleted_vocals";
        public static readonly string SFXBugFix = "Play_EXE_BugFix";
        public static readonly string SFXGun = "Play_EXE_gun";
        public static readonly string SFXDenied = "Play_EXEDenied";
        public static readonly string SFXTossItem = "Play_EXETossItem";
        public static readonly string SFXAreaGrab = "Play_EXEAreaGrab";
        public static readonly string SFXBombExplosion = "Play_EXEBombExplosion";


    }
}