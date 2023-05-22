using BepInEx;
using MegamanEXEMod.Modules.Survivors;
using MegamanEXEMod.SkillStates.BaseStates;
using R2API;
using R2API.Utils;
using RoR2;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Networking;

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

        static DamageAPI.ModdedDamageType DMGAPI = DamageAPI.ReserveDamageType();


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

            On.RoR2.HealthComponent.TakeDamage += CheckDMG;
        }


        private static void CheckDMG(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo info)
        {
            //if (info.HasModdedDamageType(DamageTypes.yourNameHere)

            if (!info.attacker.name.Contains("MegamanEXE"))
            {

                SyncNetworkExe.EmotionValue--;

                Debug.Log("elf.body.name:" + self.body.name);
                Debug.Log("self.body.isLocalPlayer:" + self.body.isLocalPlayer);
                Debug.Log("self.name:" + self.name);



                SyncNetworkExe.DamageReceived += info.damage;

                Debug.Log("DamageReceived:" + SyncNetworkExe.DamageReceived);
                Debug.Log("attacker:" + info.attacker);
                Debug.Log("damage:" + info.damage);
                Debug.Log("position:" + info.position);

                SyncNetworkExe.Hurt = true;


            }

            orig(self, info);

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
                            self.damage = 1f;
                            break;
                        case 6:

                            if (NetworkServer.active)
                            {
                                self.AddHelfireDuration(1f);
                            }

                            break;
                        case 7:

                            if (NetworkServer.active)
                            {
                                self.hideCrosshair = true;
                            }

                            break;
                        case 8:

                            if (NetworkServer.active)
                            {
                                self.AddHelfireDuration(2f);
                            }

                            break;
                        case 9:

                            if (NetworkServer.active)
                            {
                                self.AddTimedBuff(RoR2Content.Buffs.Weak, 5f);
                            }

                            break;

                        default:
                            self.jumpPower = 0.1f;
                            break;
                    }


                }
                else
                {
                    self.hideCrosshair = false;
                }

                if (self.HasBuff(Modules.Buffs.EvilBuff))
                {
                    self.damage *= 1.3f;
                    self.regen *= 0.25f;


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
                            self.damage = 1f;
                            break;
                        case 6:

                            if (NetworkServer.active)
                            {
                                self.hideCrosshair = true;
                            }

                            break;
                        case 7:

                            if (NetworkServer.active)
                            {
                                self.AddTimedBuff(RoR2Content.Buffs.Weak, 5f);
                            }

                            break;
                        case 8:

                            if (NetworkServer.active)
                            {
                                self.skillLocator.secondary.RemoveAllStocks();
                            }

                            break;
                        case 9:

                            if (NetworkServer.active)
                            {
                                self.skillLocator.utility.RemoveAllStocks();
                            }

                            break;

                        default:
                            self.jumpPower = 0.1f;
                            break;
                    }

                }
                else
                {
                    self.hideCrosshair = false;
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

                if (self.HasBuff(Modules.Buffs.FullSyncBuff))
                {
                    self.damage *= 2f;
                    self.crit *= 2f;
                    self.critHeal += (self.baseMaxHealth * 0.25f);
                    self.moveSpeed *= 1.4f;
                    self.regen *= 1.5f;

                }

                if (self.HasBuff(Modules.Buffs.RageBuff))
                {
                    self.damage *= 2.5f;

                }


                if (self.HasBuff(Modules.Buffs.AnxiousBuff))
                {
                    self.damage *= 0.9f;
                    self.armor *= 0.8f;
                    self.moveSpeed *= 1.25f;

                }



            }

        }
    }
}