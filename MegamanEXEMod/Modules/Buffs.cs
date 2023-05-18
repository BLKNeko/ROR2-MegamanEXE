using RoR2;
using System.Collections.Generic;
using UnityEngine;

namespace MegamanEXEMod.Modules
{
    public static class Buffs
    {
        // armor buff gained during roll
        internal static BuffDef armorBuff;
        internal static BuffDef DarkSwordDebuff;
        internal static BuffDef Attack10Buff;
        internal static BuffDef Attack20Buff;
        internal static BuffDef Attack30Buff;

        internal static BuffDef FullSyncBuff;
        internal static BuffDef NormalBuff;
        internal static BuffDef RageBuff;
        internal static BuffDef AnxiousBuff;
        internal static BuffDef EvilBuff;

        internal static void RegisterBuffs()
        {
            armorBuff = AddNewBuff("HenryArmorBuff",
                LegacyResourcesAPI.Load<BuffDef>("BuffDefs/HiddenInvincibility").iconSprite, 
                Color.white, 
                false, 
                false);

            Attack10Buff = AddNewBuff("Attack10Buff",
                MegamanEXEMod.Modules.Assets.IconAtk10,
                Color.white,
                false,
                false);

            Attack20Buff = AddNewBuff("Attack20Buff",
                LegacyResourcesAPI.Load<BuffDef>("BuffDefs/HiddenInvincibility").iconSprite,
                Color.white,
                false,
                false);

            Attack30Buff = AddNewBuff("Attack30Buff",
                MegamanEXEMod.Modules.Assets.IconAtk30,
                Color.white,
                false,
                false);

            DarkSwordDebuff = AddNewBuff("DarkSwordDebuff",
                MegamanEXEMod.Modules.Assets.IconDrkSword,
                Color.white,
                false,
                false);


            FullSyncBuff = AddNewBuff("FullSyncBuff",
                MegamanEXEMod.Modules.Assets.IconFullSync,
                Color.white,
                false,
                false);

            NormalBuff = AddNewBuff("NormalBuff",
                MegamanEXEMod.Modules.Assets.IconNormal,
                Color.white,
                false,
                false);

            RageBuff = AddNewBuff("RageBuff",
                MegamanEXEMod.Modules.Assets.IconRage,
                Color.white,
                false,
                false);

            AnxiousBuff = AddNewBuff("AnxiousBuff",
                MegamanEXEMod.Modules.Assets.IconAnxious,
                Color.white,
                false,
                false);

            EvilBuff = AddNewBuff("EvilBuff",
                MegamanEXEMod.Modules.Assets.IconEvil,
                Color.white,
                false,
                false);

        }

        // simple helper method
        internal static BuffDef AddNewBuff(string buffName, Sprite buffIcon, Color buffColor, bool canStack, bool isDebuff)
        {
            BuffDef buffDef = ScriptableObject.CreateInstance<BuffDef>();
            buffDef.name = buffName;
            buffDef.buffColor = buffColor;
            buffDef.canStack = canStack;
            buffDef.isDebuff = isDebuff;
            buffDef.eliteDef = null;
            buffDef.iconSprite = buffIcon;

            Modules.Content.AddBuffDef(buffDef);

            return buffDef;
        }
    }
}