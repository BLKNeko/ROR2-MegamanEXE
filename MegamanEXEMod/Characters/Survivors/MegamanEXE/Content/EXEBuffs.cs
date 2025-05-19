using RoR2;
using UnityEngine;

namespace MegamanEXEMod.Survivors.MegamanEXE
{
    public static class EXEBuffs
    {
        // armor buff gained during roll
        public static BuffDef armorBuff;

        
        internal static BuffDef Attack10Buff;
        internal static BuffDef Attack20Buff;
        internal static BuffDef Attack30Buff;
        internal static BuffDef ReflectorBuff;

        internal static BuffDef FullSyncBuff;
        internal static BuffDef NormalBuff;
        internal static BuffDef RageBuff;
        internal static BuffDef AnxiousBuff;
        internal static BuffDef EvilBuff;

        internal static BuffDef DarkDebuff1;
        internal static BuffDef DarkDebuff2;
        internal static BuffDef DarkDebuff3;
        internal static BuffDef DarkDebuff4;
        internal static BuffDef DarkDebuff5;
        internal static BuffDef DarkDebuff6;
        internal static BuffDef DarkDebuff7;
        internal static BuffDef DarkDebuff8;
        internal static BuffDef DarkDebuff9;

        public static void Init(AssetBundle assetBundle)
        {
            armorBuff = Modules.Content.CreateAndAddBuff("HenryArmorBuff",
                LegacyResourcesAPI.Load<BuffDef>("BuffDefs/HiddenInvincibility").iconSprite,
                Color.white,
                false,
                false);

            Attack10Buff = Modules.Content.CreateAndAddBuff("Attack10Buff",
                EXEAssets.IconAtk10,
                Color.white,
                false,
                false);

            Attack30Buff = Modules.Content.CreateAndAddBuff("Attack30Buff",
                EXEAssets.IconAtk30,
                Color.white,
                false,
                false);

            ReflectorBuff = Modules.Content.CreateAndAddBuff("ReflectorBuff",
                EXEAssets.IconAtk30,
                Color.white,
                false,
                false);

            FullSyncBuff = Modules.Content.CreateAndAddBuff("FullSyncBuff",
                EXEAssets.IconFullSync,
                Color.white,
                false,
                false);

            NormalBuff = Modules.Content.CreateAndAddBuff("NormalBuff",
                EXEAssets.IconNormal,
                Color.white,
                false,
                false);

            RageBuff = Modules.Content.CreateAndAddBuff("RageBuff",
                EXEAssets.IconRage,
                Color.white,
                false,
                false);

            AnxiousBuff = Modules.Content.CreateAndAddBuff("AnxiousBuff",
                EXEAssets.IconAnxious,
                Color.white,
                false,
                false);

            EvilBuff = Modules.Content.CreateAndAddBuff("EvilBuff",
                EXEAssets.IconEvil,
                Color.white,
                false,
                true);

            DarkDebuff1 = Modules.Content.CreateAndAddBuff("DarkDebuff1",
                EXEAssets.IconDrkDebuff,
                Color.white,
                false,
                true);

            DarkDebuff2 = Modules.Content.CreateAndAddBuff("DarkDebuff2",
                EXEAssets.IconDrkDebuff,
                Color.white,
                false,
                true);

            DarkDebuff3 = Modules.Content.CreateAndAddBuff("DarkDebuff3",
                EXEAssets.IconDrkDebuff,
                Color.white,
                false,
                true);

            DarkDebuff4 = Modules.Content.CreateAndAddBuff("DarkDebuff4",
                EXEAssets.IconDrkDebuff,
                Color.white,
                false,
                true);

            DarkDebuff5 = Modules.Content.CreateAndAddBuff("DarkDebuff5",
                EXEAssets.IconDrkDebuff,
                Color.white,
                false,
                true);

            DarkDebuff6 = Modules.Content.CreateAndAddBuff("DarkDebuff6",
                EXEAssets.IconDrkDebuff,
                Color.white,
                false,
                true);

            DarkDebuff7 = Modules.Content.CreateAndAddBuff("DarkDebuff7",
                EXEAssets.IconDrkDebuff,
                Color.white,
                false,
                true);

            DarkDebuff8 = Modules.Content.CreateAndAddBuff("DarkDebuff8",
                EXEAssets.IconDrkDebuff,
                Color.white,
                false,
                true);

            DarkDebuff9 = Modules.Content.CreateAndAddBuff("DarkDebuff9",
                EXEAssets.IconDrkDebuff,
                Color.white,
                false,
                true);

        }
    }
}
