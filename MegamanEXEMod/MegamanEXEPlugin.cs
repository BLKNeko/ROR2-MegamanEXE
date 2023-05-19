using BepInEx;
using MegamanEXEMod.Modules.Survivors;
using MegamanEXEMod.SkillStates.BaseStates;
using R2API.Utils;
using RoR2;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using UnityEngine;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

//rename this namespace
namespace MegamanEXEMod
{
    [BepInDependency("com.bepis.r2api", BepInDependency.DependencyFlags.HardDependency)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [BepInPlugin(MODUID, MODNAME, MODVERSION)]
    [R2APISubmoduleDependency(new string[]
    {
        "PrefabAPI",
        "LanguageAPI",
        "SoundAPI",
        "UnlockableAPI"
    })]

    public class MegamanEXEPlugin : BaseUnityPlugin
    {
        // if you don't change these you're giving permission to deprecate the mod-
        //  please change the names to your own stuff, thanks
        //   this shouldn't even have to be said
        public const string MODUID = "com.BLKNeko.MegamanEXE";
        public const string MODNAME = "MegamanEXE";
        public const string MODVERSION = "1.0.0";

        // a prefix for name tokens to prevent conflicts- please capitalize all name tokens for convention
        public const string DEVELOPER_PREFIX = "BLKNeko";

        public static MegamanEXEPlugin instance;
        

        private void Awake()
        {
            instance = this;

            Log.Init(Logger);
            Modules.Assets.Initialize(); // load assets and read config
            Modules.Config.ReadConfig();
            Modules.States.RegisterStates(); // register states for networking
            Modules.Buffs.RegisterBuffs(); // add and register custom buffs/debuffs
            Modules.Projectiles.RegisterProjectiles(); // add and register custom projectiles
            Modules.Tokens.AddTokens(); // register name tokens
            Modules.ItemDisplays.PopulateDisplays(); // collect item display prefabs for use in our display rules

            // survivor initialization
            new MegamanEXE().Initialize();

            // now make a content pack and add it- this part will change with the next update
            new Modules.ContentPacks().Initialize();

            Hook();
        }

        private void Hook()
        {
            // run hooks here, disabling one is as simple as commenting out the line
            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;
        }

        private void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            orig(self);

            // a simple stat hook, adds armor after stats are recalculated
            if (self)
            {
                if (self.HasBuff(Modules.Buffs.armorBuff))
                {
                    self.armor += 300f;
                }

                if (self.HasBuff(Modules.Buffs.DarkDebuff))
                {
                    if (self.baseMaxHealth > 10f && SyncNetworkExe.CanDrkDrain)
                    {
                        self.baseMaxHealth -= 1f;
                        SyncNetworkExe.CanDrkDrain = false;
                    }

                    

                    switch (SyncNetworkExe.RandBugDebuf)
                    {
                        case 1:
                            self.jumpPower = 0.1f;
                            break;
                        case 2:
                            self.moveSpeed *= 0.3f;
                            break;
                        case 3:
                            self.moveSpeed *= 7f;
                            break;
                        case 4:
                            self.jumpPower *= 4f;
                            break;
                        case 5:
                            self.characterMotor.moveDirection *= -1f;
                            break;

                        default:
                            self.jumpPower = 0.1f;
                            break;
                    }


                }

                if (self.HasBuff(Modules.Buffs.Attack10Buff))
                {
                    self.damage *= 1.1f;

                }

                if (self.HasBuff(Modules.Buffs.Attack20Buff))
                {
                    self.damage *= 1.2f;

                }

                if (self.HasBuff(Modules.Buffs.Attack30Buff))
                {
                    self.damage *= 1.3f;

                }




                if (self.HasBuff(Modules.Buffs.AnxiousBuff))
                {
                    self.damage *= 0.9f;
                    self.armor *= 0.9f;
                    self.moveSpeed *= 1.2f;

                }



            }

        }
    }
}