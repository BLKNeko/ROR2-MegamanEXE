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


        //CHIP DAMAGES


        public static readonly float XMidChargeDamageCoefficient = EXEConfig.midChargeMultiplierFloat.Value;

        public static readonly float XFullChargeDamageCoefficient = EXEConfig.fullChargeMultiplierFloat.Value;

        public const float EXEBusterDamageCoefficient = 1f;



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