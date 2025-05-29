using BepInEx.Configuration;
using MegamanEXEMod.Modules;

namespace MegamanEXEMod.Survivors.MegamanEXE
{
    public static class EXEConfig
    {

        public static ConfigEntry<bool> enableVoiceBool;
        public static ConfigEntry<int> enableEXEFootstep;
        public static ConfigEntry<float> midChargeMultiplierFloat;
        public static ConfigEntry<float> fullChargeMultiplierFloat;

        public static ConfigEntry<bool> NaviChatBool;
        public static ConfigEntry<float> NaviChatCooldown;

        public static void Init()
        {
            string section = "MegamanEXE";

            enableVoiceBool = Config.BindAndOptions(
                section,
                "EnableVoice",
                true,
                "At certain moments or when using a skill, X may talk or scream. If you prefer to disable this feature, you can turn it off here.");

            enableEXEFootstep = Config.BindAndOptions(
                section,
                "Enable EXE Footstep",
                1,
                0,
                2,
                "Megaman EXE footstep SFX. \n\n 0 = OFF \n\n 1 = Comand Mission SFX \n\n 2 = MegamanX8 SFX");

            midChargeMultiplierFloat = Config.BindAndOptions(
                section,
                "MidChargeDamageMultiplier",
                1.8f,
                1.5f,
                3f,
                "This is the medium charge damage multiplier.");

            fullChargeMultiplierFloat = Config.BindAndOptions(
                section,
                "FullChargeDamageMultiplier",
                3f,
                2f,
                5f,
                "This is the full charge damage multiplier.");

            NaviChatBool = Config.BindAndOptions(
                section,
                "Enable NetNavi Chat",
                true,
                "At certain moments your NetNavi will talk to you, giving Hints, warnings, sharing thougths or just chat a little.");

            NaviChatCooldown = Config.BindAndOptions(
                section,
                "NaviChatCooldown",
                150f,
                10f,
                900f,
                "This is the time between every chat message from the normal NetNavi state. \n\n WILL NOT WORK IF NETNAVICHAT IS DISABLED");
        }
    }
}
