﻿using BepInEx.Configuration;
using UnityEngine;

namespace MegamanEXEMod.Modules
{
    public static class Config
    {
        public static void ReadConfig()
        {

        }

        // this helper automatically makes config entries for disabling survivors
        public static ConfigEntry<bool> CharacterEnableConfig(string characterName, string description = "Set to false to disable this character", bool enabledDefault = true) {

            return MegamanEXEPlugin.instance.Config.Bind<bool>("General",
                                                          "Enable " + characterName,
                                                          enabledDefault,
                                                          description);
        }
    }
}