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

    }
}