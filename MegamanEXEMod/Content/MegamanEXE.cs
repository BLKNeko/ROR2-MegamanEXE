using BepInEx.Configuration;
using MegamanEXEMod.Modules.Characters;
using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MegamanEXEMod.Modules.Survivors
{
    internal class MegamanEXE : SurvivorBase
    {
        //used when building your character using the prefabs you set up in unity
        //don't upload to thunderstore without changing this
        public override string prefabBodyName => "MegamanEXE";

        public const string MEGAMAN_EXE_PREFIX = MegamanEXEPlugin.DEVELOPER_PREFIX + "_MEGAMAN_EXE_BODY_";

        //used when registering your survivor's language tokens
        public override string survivorTokenPrefix => MEGAMAN_EXE_PREFIX;

        internal static SkillDef TesteSkill1;
        internal static SkillDef TesteSkill2;

        internal static SkillDef AdvBarr500SkillDef;
        internal static SkillDef AdvGigaCannonSkillDef;
        internal static SkillDef AdvGreatYoyoSkillDef;
        internal static SkillDef AdvInfiniteVulcanSkillDef;
        internal static SkillDef AdvLifeSwordSkillDef;
        internal static SkillDef AirShotSkillDef;
        internal static SkillDef AquaSwrdSkillDef;
        internal static SkillDef Attack10SkillDef;
        internal static SkillDef Attack20SkillDef;
        internal static SkillDef Attack30SkillDef;
        internal static SkillDef Barr100SkillDef;
        internal static SkillDef Barr200SkillDef;
        internal static SkillDef BugFixSkillDef;
        internal static SkillDef CannonSkillDef;
        internal static SkillDef CyberSwordSkillDef;
        internal static SkillDef DrkBombSkillDef;
        internal static SkillDef DrkCannonSkillDef;
        internal static SkillDef DrkRecovSkillDef;
        internal static SkillDef DrkSwordSkillDef;
        internal static SkillDef DrkVulcanSkillDef;
        internal static SkillDef ElecSkillDef;
        internal static SkillDef FireSwrdSkillDef;
        internal static SkillDef FstGaugeSkillDef;
        internal static SkillDef GutPunchSkillDef;
        internal static SkillDef HiCannonSkillDef;
        internal static SkillDef InvisSkillDef;
        internal static SkillDef MCannonSkillDef;
        internal static SkillDef MiniBombSkillDef;
        internal static SkillDef MuramasaSkillDef;
        internal static SkillDef NoDataSkillDef;
        internal static SkillDef Recov300SkillDef;
        internal static SkillDef Recov50SkillDef;
        internal static SkillDef ReflectorSkillDef;
        internal static SkillDef SendChipSkillDef;
        internal static SkillDef ShokWaveSkillDef;
        internal static SkillDef ShotGunSkillDef;
        internal static SkillDef SuprVulcSkillDef;
        internal static SkillDef ThunderSkillDef;
        internal static SkillDef VulcanSkillDef;
        internal static SkillDef YoyoSkillDef;





        public override BodyInfo bodyInfo { get; set; } = new BodyInfo
        {
            bodyName = "MegamanEXEBody",
            bodyNameToken = MEGAMAN_EXE_PREFIX + "NAME",
            subtitleNameToken = MEGAMAN_EXE_PREFIX + "SUBTITLE",

            characterPortrait = Assets.TexMegamanExe,
            bodyColor = new Color(0.3f, 0.55f, 0.99f),

            crosshair = Modules.Assets.LoadCrosshair("Standard"),
            //crosshair = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Crosshair/GlaiveCrosshair"),
            podPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/SurvivorPod"),

            maxHealth = 100f,
            healthRegen = 1.8f,
            armor = 20f,
            moveSpeedGrowth = 0.05f,
            damage = 18f,
            shieldGrowth = 0.25f,
            jumpPowerGrowth = 0.25f,
            critGrowth = 0.15f,
            attackSpeedGrowth = 0.005f,

            jumpCount = 1,
        };

        public override CustomRendererInfo[] customRendererInfos { get; set; } = new CustomRendererInfo[] 
        {
                /*
                new CustomRendererInfo
                {
                    childName = "SwordModel",
                    material = Materials.CreateHopooMaterial("matHenry"),
                },
                new CustomRendererInfo
                {
                    childName = "GunModel",
                },
                new CustomRendererInfo
                {
                    childName = "Model",
                }
                */

                new CustomRendererInfo
                {
                    childName = "BMC",
                },
                new CustomRendererInfo
                {
                    childName = "BMM",
                },
                new CustomRendererInfo
                {
                    childName = "HML",
                },
                new CustomRendererInfo
                {
                    childName = "MMC",
                },
                new CustomRendererInfo
                {
                    childName = "EXEBuster",
                },
        };

        public override UnlockableDef characterUnlockableDef => null;

        //public override Type characterMainState => typeof(EntityStates.GenericCharacterMain);

        public override Type characterMainState => typeof(SkillStates.BaseStates.SyncNetworkExe);

        public override Type characterDeathState => typeof(SkillStates.BaseStates.DeathState);

        public override Type characterSpawnState => typeof(SkillStates.BaseStates.SpawnState);

        public override ItemDisplaysBase itemDisplays => new HenryItemDisplays();

                                                                          //if you have more than one character, easily create a config to enable/disable them like this
        public override ConfigEntry<bool> characterEnabledConfig => null; //Modules.Config.CharacterEnableConfig(bodyName);

        private static UnlockableDef masterySkinUnlockableDef;

        public override void InitializeCharacter()
        {
            base.InitializeCharacter();
        }

        public override void InitializeUnlockables()
        {
            //uncomment this when you have a mastery skin. when you do, make sure you have an icon too
            //masterySkinUnlockableDef = Modules.Unlockables.AddUnlockable<Modules.Achievements.MasteryAchievement>();
        }

        public override void InitializeHitboxes()
        {
            ChildLocator childLocator = bodyPrefab.GetComponentInChildren<ChildLocator>();

            //example of how to create a hitbox
            //Transform hitboxTransform = childLocator.FindChild("SwordHitbox");
            //Modules.Prefabs.SetupHitbox(prefabCharacterModel.gameObject, hitboxTransform, "Sword");

            //example of how to create a hitbox
            Transform hitboxTransformEXES = childLocator.FindChild("EXESwordHB");
            Modules.Prefabs.SetupHitbox(prefabCharacterModel.gameObject, hitboxTransformEXES, "EXESwordHB");
            hitboxTransformEXES.localScale = new Vector3(5f, 5f, 5f);

            Transform hitboxTransformGP = childLocator.FindChild("GutsPunchHB");
            Modules.Prefabs.SetupHitbox(prefabCharacterModel.gameObject, hitboxTransformGP, "GutsPunchHB");
            hitboxTransformGP.localScale = new Vector3(5f, 5f, 5f);

        }

        public override void InitializeSkills()
        {
            Modules.Skills.CreateSkillFamilies(bodyPrefab);
            Modules.Skills.CreateFirstExtraSkillFamily(bodyPrefab);
            Modules.Skills.CreateSecondExtraSkillFamily(bodyPrefab);
            Modules.Skills.CreateThirdExtraSkillFamily(bodyPrefab);
            Modules.Skills.CreateFourthExtraSkillFamily(bodyPrefab);
            Modules.Skills.PassiveSetup(bodyPrefab);

            string prefix = MegamanEXEPlugin.DEVELOPER_PREFIX;

            #region Primary
            //Creates a skilldef for a typical primary 
            SkillDef primarySkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo(prefix + "_MEGAMAN_EXE_BODY_PRIMARY_BUSTEREXE_NAME",
                                                                                      prefix + "_MEGAMAN_EXE_BODY_PRIMARY_BUSTEREXE_DESCRIPTION",
                                                                                      Modules.Assets.IconBusterEXE,
                                                                                      new EntityStates.SerializableEntityStateType(typeof(SkillStates.BusterEXE)),
                                                                                      "Weapon",
                                                                                      true));


            
            #endregion

            #region Secondary
            SkillDef shootSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SECONDARY_CYBERSWORD_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SECONDARY_CYBERSWORD_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SECONDARY_CYBERSWORD_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texSecondaryIcon"),
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.MiniBomb)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }
            });

            
            #endregion

            #region Utility
            SkillDef rollSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_UTILITY_SENDCHIPS_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_UTILITY_SENDCHIPS_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_UTILITY_SENDCHIPS_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texUtilityIcon"),
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.ElecSwrd)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 2f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.PrioritySkill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = false,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });

            
            #endregion

            #region Special
            SkillDef bombSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texSpecialIcon"),
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.SuprVulc)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });


            #endregion

            AdvBarr500SkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_CHIP_ADVBARR500_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_ADVBARR500_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_ADVBARR500_DESCRIPTION",
                skillIcon = Modules.Assets.IconBarr500,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.AdvBarr500)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 40f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.PrioritySkill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });

            AdvGigaCannonSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_CHIP_GIGACANNON_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_GIGACANNON_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_GIGACANNON_DESCRIPTION",
                skillIcon = Modules.Assets.IconGigaCannon,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.AdvGigaCannon)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 40f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });

            AdvGreatYoyoSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_CHIP_GREATYOYO_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_GREATYOYO_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_GREATYOYO_DESCRIPTION",
                skillIcon = Modules.Assets.IconGreatYoyo,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.AdvGreatYoyo)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 40f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });

            AdvInfiniteVulcanSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_CHIP_INFVULC_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_INFVULC_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_INFVULC_DESCRIPTION",
                skillIcon = Modules.Assets.IconInfiniteVulcan,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.AdvInfiniteVulcan)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 50,
                baseRechargeInterval = 40f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.PrioritySkill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 50,
                requiredStock = 1,
                stockToConsume = 1
            });

            AdvLifeSwordSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_CHIP_LIFESWORD_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_LIFESWORD_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_LIFESWORD_DESCRIPTION",
                skillIcon = Modules.Assets.IconLifeSword,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.AdvLifeSword)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 40f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });

            AirShotSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_DESCRIPTION",
                skillIcon = Modules.Assets.IconAirShot,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.AirShot)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 5,
                baseRechargeInterval = 15f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = true,
                rechargeStock = 5,
                requiredStock = 1,
                stockToConsume = 1
            });

            AquaSwrdSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_DESCRIPTION",
                skillIcon = Modules.Assets.IconAquaSwrd,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.AquaSwrd)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 3,
                baseRechargeInterval = 20f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 3,
                requiredStock = 1,
                stockToConsume = 1
            });

            Attack10SkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_CHIP_ATK10_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_ATK10_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_ATK10_DESCRIPTION",
                skillIcon = Modules.Assets.IconAtk10,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.Attack10)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 20f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.PrioritySkill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });

            Attack30SkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_CHIP_ATK30_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_ATK30_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_ATK10_DESCRIPTION",
                skillIcon = Modules.Assets.IconAtk10,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.Attack30)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 20f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.PrioritySkill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });

            Barr100SkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_DESCRIPTION",
                skillIcon = Modules.Assets.IconBarr100,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.Barr100)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 2,
                baseRechargeInterval = 20f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.PrioritySkill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 2,
                requiredStock = 1,
                stockToConsume = 1
            });

            Barr200SkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_CHIP_BARR200_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_BARR200_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_BARR200_DESCRIPTION",
                skillIcon = Modules.Assets.IconBarr200,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.Barr200)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 2,
                baseRechargeInterval = 20f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.PrioritySkill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 2,
                requiredStock = 1,
                stockToConsume = 1
            });

            BugFixSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_CHIP_BUGFIX_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_BUGFIX_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_BUGFIX_DESCRIPTION",
                skillIcon = Modules.Assets.IconBugFix,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.BugFix)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 20f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.PrioritySkill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });

            CannonSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_DESCRIPTION",
                skillIcon = Modules.Assets.IconCannon,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.Cannon)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 5,
                baseRechargeInterval = 15f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = true,
                rechargeStock = 5,
                requiredStock = 1,
                stockToConsume = 1
            });

            CyberSwordSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SECONDARY_CYBERSWORD_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SECONDARY_CYBERSWORD_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SECONDARY_CYBERSWORD_DESCRIPTION",
                skillIcon = Modules.Assets.IconCyberSword,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.CyberSword)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 3,
                baseRechargeInterval = 2f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });

            DrkBombSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_CHIP_DARKBOMB_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_DARKBOMB_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_DARKBOMB_DESCRIPTION",
                skillIcon = Modules.Assets.IconDrkBomb,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.DrkBomb)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 8,
                baseRechargeInterval = 15f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 8,
                requiredStock = 1,
                stockToConsume = 1
            });

            DrkCannonSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_CHIP_DARKCANNON_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_DARKCANNON_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_DARKCANNON_DESCRIPTION",
                skillIcon = Modules.Assets.IconDrkCannon,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.DrkCannon)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 5,
                baseRechargeInterval = 15f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 5,
                requiredStock = 1,
                stockToConsume = 1
            });

            DrkRecovSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_CHIP_DARKRECOV_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_DARKRECOV_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_DARKRECOV_DESCRIPTION",
                skillIcon = Modules.Assets.IconDrkRecov,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.DrkRecov)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 20f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });

            DrkSwordSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_DESCRIPTION",
                skillIcon = Modules.Assets.IconDrkSword,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.DrkSword)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 5,
                baseRechargeInterval = 15f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 5,
                requiredStock = 1,
                stockToConsume = 1
            });

            DrkVulcanSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_CHIP_DARKVULCAN_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_DARKVULCAN_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_DARKVULCAN_DESCRIPTION",
                skillIcon = Modules.Assets.IconDrkVulcan,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.DrkVulcan)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 50,
                baseRechargeInterval = 15f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.PrioritySkill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 50,
                requiredStock = 1,
                stockToConsume = 1
            });

            ElecSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_DESCRIPTION",
                skillIcon = Modules.Assets.IconElecSwrd,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.ElecSwrd)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 3,
                baseRechargeInterval = 20f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 3,
                requiredStock = 1,
                stockToConsume = 1
            });

            FireSwrdSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_DESCRIPTION",
                skillIcon = Modules.Assets.IconFireSwrd,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.FireSwrd)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 3,
                baseRechargeInterval = 20f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 3,
                requiredStock = 1,
                stockToConsume = 1
            });

            FstGaugeSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_CHIP_FSTGAUGE_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_FSTGAUGE_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_FSTGAUGE_DESCRIPTION",
                skillIcon = Modules.Assets.IconFstGauge,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.FstGauge)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 20f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });

            GutPunchSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_DESCRIPTION",
                skillIcon = Modules.Assets.IconGutPunch,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.GutPunch)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 2,
                baseRechargeInterval = 20f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 2,
                requiredStock = 1,
                stockToConsume = 1
            });

            HiCannonSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_DESCRIPTION",
                skillIcon = Modules.Assets.IconHiCannon,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.HiCannon)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 5,
                baseRechargeInterval = 15f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = true,
                rechargeStock = 5,
                requiredStock = 1,
                stockToConsume = 1
            });

            InvisSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_DESCRIPTION",
                skillIcon = Modules.Assets.IconInvis,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.Invis)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 15f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = false,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });

            MCannonSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_DESCRIPTION",
                skillIcon = Modules.Assets.IconMCannon,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.MCannon)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 3,
                baseRechargeInterval = 15f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = true,
                rechargeStock = 3,
                requiredStock = 1,
                stockToConsume = 1
            });

            MiniBombSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_DESCRIPTION",
                skillIcon = Modules.Assets.IconMiniBomb,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.MiniBomb)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 5,
                baseRechargeInterval = 15f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 5,
                requiredStock = 1,
                stockToConsume = 1
            });

            MuramasaSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_DESCRIPTION",
                skillIcon = Modules.Assets.IconMuramasa,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.Muramasa)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 15f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });

            NoDataSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_CHIP_NODATA_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_NODATA_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_NODATA_DESCRIPTION",
                skillIcon = Modules.Assets.IconNoData,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.NoData)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });

            Recov300SkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_CHIP_RECOV300_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_RECOV300_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_CHIP_RECOV300_DESCRIPTION",
                skillIcon = Modules.Assets.IconRecov300,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.Recov300)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 20f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.PrioritySkill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = false,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });

            Recov50SkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_DESCRIPTION",
                skillIcon = Modules.Assets.IconRecov50,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.Recov50)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 20f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.PrioritySkill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = false,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });

            ReflectorSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_DESCRIPTION",
                skillIcon = Modules.Assets.IconReflector,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.Reflector)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 2,
                baseRechargeInterval = 15f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = false,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 2,
                requiredStock = 1,
                stockToConsume = 1
            });

            SendChipSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_UTILITY_SENDCHIPS_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_UTILITY_SENDCHIPS_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_UTILITY_SENDCHIPS_DESCRIPTION",
                skillIcon = Modules.Assets.IconSendChip,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.SendChip)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 5f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Death,
                resetCooldownTimerOnUse = false,
                isCombatSkill = false,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });

            ShokWaveSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_DESCRIPTION",
                skillIcon = Modules.Assets.IconShockWave,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.ShokWav)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 2,
                baseRechargeInterval = 20f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.PrioritySkill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = true,
                rechargeStock = 2,
                requiredStock = 1,
                stockToConsume = 1
            });

            ShotGunSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_DESCRIPTION",
                skillIcon = Modules.Assets.IconShotgun,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.ShotGun)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 5,
                baseRechargeInterval = 15f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.PrioritySkill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = true,
                rechargeStock = 5,
                requiredStock = 1,
                stockToConsume = 1
            });

            SuprVulcSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_DESCRIPTION",
                skillIcon = Modules.Assets.IconSuprVulc,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.SuprVulc)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 30,
                baseRechargeInterval = 20f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.PrioritySkill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 30,
                requiredStock = 1,
                stockToConsume = 1
            });

            ThunderSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_DESCRIPTION",
                skillIcon = Modules.Assets.IconThunder,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.Thunder)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 3,
                baseRechargeInterval = 15f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = true,
                rechargeStock = 3,
                requiredStock = 1,
                stockToConsume = 1
            });

            VulcanSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_DESCRIPTION",
                skillIcon = Modules.Assets.IconVulcan,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.Vulcan)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 15,
                baseRechargeInterval = 15f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.PrioritySkill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = true,
                rechargeStock = 15,
                requiredStock = 1,
                stockToConsume = 1
            });

            YoyoSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillNameToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_NAME",
                skillDescriptionToken = prefix + "_MEGAMAN_EXE_BODY_SPECIAL_ADVP_DESCRIPTION",
                skillIcon = Modules.Assets.IconYoyo,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.Yoyo)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 3,
                baseRechargeInterval = 15f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = true,
                rechargeStock = 3,
                requiredStock = 1,
                stockToConsume = 1
            });




            Modules.Skills.AddPrimarySkills(bodyPrefab, primarySkillDef);
            Modules.Skills.AddSecondarySkills(bodyPrefab, CyberSwordSkillDef);
            Modules.Skills.AddUtilitySkills(bodyPrefab, SendChipSkillDef);
            Modules.Skills.AddSpecialSkills(bodyPrefab, NoDataSkillDef);

            Skills.AddFirstExtraSkill(bodyPrefab, YoyoSkillDef);
            Skills.AddSecondExtraSkill(bodyPrefab, CannonSkillDef);
            Skills.AddThirdExtraSkill(bodyPrefab, ReflectorSkillDef);
            Skills.AddFourthExtraSkill(bodyPrefab, Recov50SkillDef);
        }
        
        public override void InitializeSkins()
        {
            ModelSkinController skinController = prefabCharacterModel.gameObject.AddComponent<ModelSkinController>();
            ChildLocator childLocator = prefabCharacterModel.GetComponent<ChildLocator>();

            CharacterModel.RendererInfo[] defaultRendererinfos = prefabCharacterModel.baseRendererInfos;

            List<SkinDef> skins = new List<SkinDef>();

            #region DefaultSkin
            //this creates a SkinDef with all default fields
            SkinDef defaultSkin = Modules.Skins.CreateSkinDef(MEGAMAN_EXE_PREFIX + "DEFAULT_SKIN_NAME",
                Assets.mainAssetBundle.LoadAsset<Sprite>("texMainSkin"),
                defaultRendererinfos,
                prefabCharacterModel.gameObject);

            //these are your Mesh Replacements. The order here is based on your CustomRendererInfos from earlier
            //pass in meshes as they are named in your assetbundle
            //defaultSkin.meshReplacements = Modules.Skins.getMeshReplacements(defaultRendererinfos,
            //    "meshHenrySword",
            //    "meshHenryGun",
            //    "meshHenry");

            //add new skindef to our list of skindefs. this is what we'll be passing to the SkinController
            skins.Add(defaultSkin);
            #endregion



            
            //creating a new skindef as we did before
            SkinDef masterySkin = Modules.Skins.CreateSkinDef("_MEGAMAN_EXE_BODY_MASTERY_SKIN_NAME",
                Assets.mainAssetBundle.LoadAsset<Sprite>("texMasteryAchievement"),
                defaultRendererinfos,
                prefabCharacterModel.gameObject,
                masterySkinUnlockableDef);

            //adding the mesh replacements as above. 
            //if you don't want to replace the mesh (for example, you only want to replace the material), pass in null so the order is preserved
            masterySkin.meshReplacements = Modules.Skins.getMeshReplacements(defaultRendererinfos,
                null,
                null,//no gun mesh replacement. use same gun mesh
                null,
                null,
                null);

            //masterySkin has a new set of RendererInfos (based on default rendererinfos)
            //you can simply access the RendererInfos defaultMaterials and set them to the new materials for your skin.
            masterySkin.rendererInfos[0].defaultMaterial = Modules.Materials.CreateHopooMaterial("matMMBNDRK");
            masterySkin.rendererInfos[1].defaultMaterial = Modules.Materials.CreateHopooMaterial("matMMBNDRK");
            masterySkin.rendererInfos[2].defaultMaterial = Modules.Materials.CreateHopooMaterial("matMMBNDRK");
            masterySkin.rendererInfos[3].defaultMaterial = Modules.Materials.CreateHopooMaterial("matMMBNDRK");
            masterySkin.rendererInfos[4].defaultMaterial = Modules.Materials.CreateHopooMaterial("matMMBNDRK");

            //here's a barebones example of using gameobjectactivations that could probably be streamlined or rewritten entirely, truthfully, but it works
            //masterySkin.gameObjectActivations = new SkinDef.GameObjectActivation[]
           // {
            //    new SkinDef.GameObjectActivation
            //    {
           //         gameObject = childLocator.FindChildGameObject("GunModel"),
           //         shouldActivate = false,
           //     }
           // };
            //simply find an object on your child locator you want to activate/deactivate and set if you want to activate/deacitvate it with this skin

            skins.Add(masterySkin);
            


            //uncomment this when you have a mastery skin
            #region MasterySkin
            /*
            //creating a new skindef as we did before
            SkinDef masterySkin = Modules.Skins.CreateSkinDef(HenryPlugin.DEVELOPER_PREFIX + "_MEGAMAN_EXE_BODY_MASTERY_SKIN_NAME",
                Assets.mainAssetBundle.LoadAsset<Sprite>("texMasteryAchievement"),
                defaultRendererinfos,
                prefabCharacterModel.gameObject,
                masterySkinUnlockableDef);

            //adding the mesh replacements as above. 
            //if you don't want to replace the mesh (for example, you only want to replace the material), pass in null so the order is preserved
            masterySkin.meshReplacements = Modules.Skins.getMeshReplacements(defaultRendererinfos,
                "meshHenrySwordAlt",
                null,//no gun mesh replacement. use same gun mesh
                "meshHenryAlt");

            //masterySkin has a new set of RendererInfos (based on default rendererinfos)
            //you can simply access the RendererInfos defaultMaterials and set them to the new materials for your skin.
            masterySkin.rendererInfos[0].defaultMaterial = Modules.Materials.CreateHopooMaterial("matHenryAlt");
            masterySkin.rendererInfos[1].defaultMaterial = Modules.Materials.CreateHopooMaterial("matHenryAlt");
            masterySkin.rendererInfos[2].defaultMaterial = Modules.Materials.CreateHopooMaterial("matHenryAlt");

            //here's a barebones example of using gameobjectactivations that could probably be streamlined or rewritten entirely, truthfully, but it works
            masterySkin.gameObjectActivations = new SkinDef.GameObjectActivation[]
            {
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("GunModel"),
                    shouldActivate = false,
                }
            };
            //simply find an object on your child locator you want to activate/deactivate and set if you want to activate/deacitvate it with this skin

            skins.Add(masterySkin);
            */
            #endregion

            skinController.skins = skins.ToArray();
        }
    }
}