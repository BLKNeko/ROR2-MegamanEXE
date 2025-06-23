using BepInEx.Configuration;
using EmotesAPI;
using EntityStates.AffixVoid;
using MegamanEXEMod.Modules;
using MegamanEXEMod.Modules.BaseStates;
using MegamanEXEMod.Modules.Characters;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using MegamanEXEMod.Survivors.MegamanEXE.SkillStates;
using RoR2;
using RoR2.Projectile;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MegamanEXEMod.Survivors.MegamanEXE
{
    public class MegamanEXESurvivor : SurvivorBase<MegamanEXESurvivor>
    {
        //used to load the assetbundle for this character. must be unique
        public override string assetBundleName => "megamanexebundle"; //if you do not change this, you are giving permission to deprecate the mod

        //the name of the prefab we will create. conventionally ending in "Body". must be unique
        public override string bodyName => "MegamanEXEBody"; //if you do not change this, you get the point by now

        //name of the ai master for vengeance and goobo. must be unique
        public override string masterName => "MegamanEXEMonsterMaster"; //if you do not

        //the names of the prefabs you set up in unity that we will use to build your character
        public override string modelPrefabName => "mdlMegamanEXE";
        public override string displayPrefabName => "MegamanEXEDisplay";

        public const string MMEXE_PREFIX = MegamanEXEPlugin.DEVELOPER_PREFIX + "_MMEXE_";

        //used when registering your survivor's language tokens
        public override string survivorTokenPrefix => MMEXE_PREFIX;

        internal bool setupEmoteSkeleton = false;
        private float EXETakeDamageValue = 0f;
        private CharacterMaster EXEMaster;


        //Skill Defs

        internal static SkillDef BusterEXESkillDef;

        internal static SkillDef TurretTestSkillDef;

        internal static SteppedSkillDef CyberSwordSkillDef;

        internal static SkillDef AdvAirShotSkillDef;
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
        internal static SkillDef DrkBombSkillDef;
        internal static SkillDef DrkCannonSkillDef;
        internal static SkillDef DrkPlus77SkillDef;
        internal static SkillDef DrkRecovSkillDef;
        internal static SkillDef DrkSwordSkillDef;
        internal static SkillDef DrkVulcanSkillDef;
        internal static SkillDef ElecSwrdSkillDef;
        internal static SkillDef FireSwrdSkillDef;
        internal static SkillDef FstGaugeSkillDef;
        internal static SkillDef GutPunchSkillDef;
        internal static SkillDef GutPnchShotSkillDef;
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
        internal static SkillDef SpreaderSkillDef;
        internal static SkillDef StepSwordSkillDef;
        internal static SkillDef SuprVulcSkillDef;
        internal static SkillDef ThunderSkillDef;
        internal static SkillDef VulcanSkillDef;
        internal static SkillDef YoyoSkillDef;


        public override BodyInfo bodyInfo => new BodyInfo
        {
            bodyName = bodyName,
            bodyNameToken = MMEXE_PREFIX + "NAME",
            subtitleNameToken = MMEXE_PREFIX + "SUBTITLE",

            characterPortrait = EXEAssets.IconEXETex,
            bodyColor = new Color(0.3f, 0.55f, 0.99f),
            sortPosition = 100,

            crosshair = Asset.LoadCrosshair("Standard"),
            podPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/SurvivorPod"),

            maxHealth = 100f,
            healthRegen = 1.5f,
            healthGrowth = 25f,
            regenGrowth = 0.2f,
            armor = 25f,
            armorGrowth = 1.5f,
            moveSpeedGrowth = 0.05f,
            damage = 10f,
            shieldGrowth = 0.25f,
            critGrowth = 0.15f,
            attackSpeedGrowth = 0.005f,

            jumpPowerGrowth = 0.25f,
            jumpCount = 1,
        };

        public override CustomRendererInfo[] customRendererInfos => new CustomRendererInfo[]
        {
                new CustomRendererInfo
                {
                    childName = "EXEBodyMesh",
                    material = assetBundle.LoadMaterial("matMMEXE"),
                },
                new CustomRendererInfo
                {
                    childName = "EXEHandLMesh",
                    material = assetBundle.LoadMaterial("matMMEXE"),
                },
                new CustomRendererInfo
                {
                    childName = "EXEHandRMesh",
                    material = assetBundle.LoadMaterial("matMMEXE"),
                },
                new CustomRendererInfo
                {
                    childName = "EXEBuster",
                    material = assetBundle.LoadMaterial("matMMEXE"),
                },
                new CustomRendererInfo
                {
                    childName = "ProtoBuster",
                    material = assetBundle.LoadMaterial("matProtoBuster"),
                },
                new CustomRendererInfo
                {
                    childName = "RollBuster",
                    material = assetBundle.LoadMaterial("matRBuster"),
                },
                new CustomRendererInfo
                {
                    childName = "BassBuster",
                    material = assetBundle.LoadMaterial("matBassEXE"),
                },
                new CustomRendererInfo
                {
                    childName = "CYSword",
                    material = assetBundle.LoadMaterial("matEXESword"),
                },
                new CustomRendererInfo
                {
                    childName = "EXEMask",
                    material = assetBundle.LoadMaterial("matMMEXE"),
                },
                new CustomRendererInfo
                {
                    childName = "GutsPunch",
                    material = assetBundle.LoadMaterial("matGutsPunch"),
                },
                new CustomRendererInfo
                {
                    childName = "DiveEXESword",
                    material = assetBundle.LoadMaterial("matDVEXE"),
                },
                new CustomRendererInfo
                {
                    childName = "DiveEXEBuster",
                    material = assetBundle.LoadMaterial("matDVEXE"),
                }
        };

        public override UnlockableDef characterUnlockableDef => EXEUnlockables.characterUnlockableDef;
        
        public override ItemDisplaysBase itemDisplays => new EXEItemDisplays();

        //set in base classes
        public override AssetBundle assetBundle { get; protected set; }

        public override GameObject bodyPrefab { get; protected set; }
        public override CharacterBody prefabCharacterBody { get; protected set; }
        public override GameObject characterModelObject { get; protected set; }
        public override CharacterModel prefabCharacterModel { get; protected set; }
        public override GameObject displayPrefab { get; protected set; }

        public override void Initialize()
        {
            //uncomment if you have multiple characters
            //ConfigEntry<bool> characterEnabled = Config.CharacterEnableConfig("Survivors", "MegamanEXE");

            //if (!characterEnabled.Value)
            //    return;

            base.Initialize();
        }

        public override void InitializeCharacter()
        {
            //need the character unlockable before you initialize the survivordef
            EXEUnlockables.Init();

            base.InitializeCharacter();

            EXEConfig.Init();
            EXEStates.Init();
            EXETokens.Init();

            EXEAssets.Init(assetBundle);
            EXEBuffs.Init(assetBundle);

            InitializeEntityStateMachines();
            InitializeSkills();
            InitializeSkins();
            InitializeCharacterMaster();

            AdditionalBodySetup();

            AddHooks();
        }

        private void AdditionalBodySetup()
        {
            AddHitboxes();
            bodyPrefab.AddComponent<EXEBaseComponent>();
            //bodyPrefab.AddComponent<HuntressTrackerComopnent>();
            //anything else here
        }

        public void AddHitboxes()
        {
            //example of how to create a HitBoxGroup. see summary for more details
            //Prefabs.SetupHitBoxGroup(characterModelObject, "EXESwordGroup", "EXESwordHitbox");

            ChildLocator childLocator = bodyPrefab.GetComponentInChildren<ChildLocator>();
            GameObject model = childLocator.gameObject;

            Transform hitboxTransform = childLocator.FindChild("EXESwordHitbox");
            Prefabs.SetupHitBoxGroup(model, "EXESwordGroup", "EXESwordHitbox");
            //hitboxTransform.localScale = new Vector3(5.2f, 5.2f, 5.2f);
            hitboxTransform.localScale = new Vector3(6f, 6f, 6f);

        }

        public override void InitializeEntityStateMachines() 
        {
            //clear existing state machines from your cloned body (probably commando)
            //omit all this if you want to just keep theirs
            Prefabs.ClearEntityStateMachines(bodyPrefab);

            //the main "Body" state machine has some special properties
            Prefabs.AddMainEntityStateMachine(bodyPrefab, "Body", typeof(EntityStates.GenericCharacterMain), typeof(EntityStates.SpawnTeleporterState));
            //if you set up a custom main characterstate, set it up here
            //don't forget to register custom entitystates in your HenryStates.cs

            bodyPrefab.GetComponent<CharacterDeathBehavior>().deathState = new EntityStates.SerializableEntityStateType(typeof(DeathStateEXE));
            bodyPrefab.GetComponent<EntityStateMachine>().initialStateType = new EntityStates.SerializableEntityStateType(typeof(SpawnStateEXE));

            Prefabs.AddEntityStateMachine(bodyPrefab, "Weapon");
            Prefabs.AddEntityStateMachine(bodyPrefab, "Weapon2");
        }

        #region skills
        public override void InitializeSkills()
        {
            //remove the genericskills from the commando body we cloned
            Skills.ClearGenericSkills(bodyPrefab);
            //add our own
            Skills.CreateFirstExtraSkillFamily(bodyPrefab);
            Skills.CreateSecondExtraSkillFamily(bodyPrefab);
            Skills.CreateThirdExtraSkillFamily(bodyPrefab);
            Skills.CreateFourthExtraSkillFamily(bodyPrefab);
            //AddPassiveSkill();

            CreateSkillDefs();

            AddPrimarySkills();
            AddSecondarySkills();
            AddUtiitySkills();
            AddSpecialSkills();

            AddExtraFirstSkills();
            AddExtraSecondSkills();
            AddExtraThirdSkills();
            AddExtraFourthSkills();

        }

        private void CreateSkillDefs()
        {
            BusterEXESkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "BusterEXE",
                skillNameToken = MMEXE_PREFIX + "WEAPON_ZSABER_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "WEAPON_ZSABER_DESCRIPTION",
                skillIcon = EXEAssets.IconBusterEXE,

                activationState = new EntityStates.SerializableEntityStateType(typeof(BusterEXE)),
                activationStateMachineName = "Weapon",
                interruptPriority = EntityStates.InterruptPriority.Skill,

                baseRechargeInterval = 0f,
                baseMaxStock = 1,

                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = true,
                dontAllowPastMaxStocks = false,
                mustKeyPress = false,
                beginSkillCooldownOnSkillEnd = false,

                isCombatSkill = true,
                canceledFromSprinting = false,
                cancelSprintingOnActivation = false,
                forceSprintDuringState = false,
            });

            TurretTestSkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "BusterEXE",
                skillNameToken = MMEXE_PREFIX + "WEAPON_ZSABER_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "WEAPON_ZSABER_DESCRIPTION",
                //skillIcon = EXEAssets.IconBusterEXE,

                activationState = new EntityStates.SerializableEntityStateType(typeof(EXETurret)),
                activationStateMachineName = "Weapon",
                interruptPriority = EntityStates.InterruptPriority.Skill,

                baseRechargeInterval = 2f,
                baseMaxStock = 5,

                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = true,
                dontAllowPastMaxStocks = false,
                mustKeyPress = true,
                beginSkillCooldownOnSkillEnd = false,

                isCombatSkill = true,
                canceledFromSprinting = false,
                cancelSprintingOnActivation = false,
                forceSprintDuringState = false,
            });

            CyberSwordSkillDef = Skills.CreateSkillDef<SteppedSkillDef>(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_SECONDARY_CYBERSWORD_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_SECONDARY_CYBERSWORD_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_SECONDARY_CYBERSWORD_DESCRIPTION",
                skillIcon = EXEAssets.IconCyberSword,

                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.CySwordSlashCombo1)),
                activationStateMachineName = "Weapon",
                interruptPriority = EntityStates.InterruptPriority.Skill,

                baseMaxStock = 3,
                baseRechargeInterval = 2f,

                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });
            CyberSwordSkillDef.stepCount = 2;
            CyberSwordSkillDef.stepGraceDuration = 0.5f;

            NoDataSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_NODATA_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_NODATA_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_NODATA_DESCRIPTION",
                skillIcon = EXEAssets.IconNoData,
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
                isCombatSkill = false,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });

            SendChipSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_UTILITY_SENDCHIPS_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_UTILITY_SENDCHIPS_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_UTILITY_SENDCHIPS_DESCRIPTION",
                skillIcon = EXEAssets.IconSendChip,
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

            #region ADVPROG

            AdvAirShotSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_ADVAIRSHOT_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_ADVAIRSHOT_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_ADVAIRSHOT_DESCRIPTION",
                skillIcon = EXEAssets.IconAdvAirShot,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.AdvAirShot)),
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

            AdvBarr500SkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_ADVBARR500_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_ADVBARR500_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_ADVBARR500_DESCRIPTION",
                skillIcon = EXEAssets.IconBarr500,
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
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_GIGACANNON_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_GIGACANNON_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_GIGACANNON_DESCRIPTION",
                skillIcon = EXEAssets.IconGigaCannon,
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
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_GREATYOYO_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_GREATYOYO_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_GREATYOYO_DESCRIPTION",
                skillIcon = EXEAssets.IconGreatYoyo,
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
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_INFVULC_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_INFVULC_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_INFVULC_DESCRIPTION",
                skillIcon = EXEAssets.IconInfiniteVulcan,
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
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_LIFESWORD_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_LIFESWORD_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_LIFESWORD_DESCRIPTION",
                skillIcon = EXEAssets.IconLifeSword,
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

            #endregion

            #region DRKCHIP

            DrkBombSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_DARKBOMB_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_DARKBOMB_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_DARKBOMB_DESCRIPTION",
                skillIcon = EXEAssets.IconDrkBomb,
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
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_DARKCANNON_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_DARKCANNON_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_DARKCANNON_DESCRIPTION",
                skillIcon = EXEAssets.IconDrkCannon,
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

            DrkPlus77SkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_DRKPLUS77_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_DRKPLUS77_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_DRKPLUS77_DESCRIPTION",
                //skillIcon = EXEAssets.IconRecov300,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.DrkPlus77)),
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

            DrkRecovSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_DARKRECOV_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_DARKRECOV_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_DARKRECOV_DESCRIPTION",
                skillIcon = EXEAssets.IconDrkRecov,
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

            DrkSwordSkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_DRKSWRD_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_DRKSWRD_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_DRKSWRD_DESCRIPTION",
                skillIcon = EXEAssets.IconDrkSword,

                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.DrkSword)),
                activationStateMachineName = "Weapon",
                interruptPriority = EntityStates.InterruptPriority.Skill,

                baseMaxStock = 5,
                baseRechargeInterval = 15f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                
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
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_DARKVULCAN_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_DARKVULCAN_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_DARKVULCAN_DESCRIPTION",
                skillIcon = EXEAssets.IconDrkVulcan,
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

            #endregion

            #region BUFFCHIPS

            Attack10SkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_ATK10_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_ATK10_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_ATK30_DESCRIPTION",
                skillIcon = EXEAssets.IconAtk10,
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
                isCombatSkill = false,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });

            Attack20SkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_ATK20_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_ATK20_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_ATK20_DESCRIPTION",
                skillIcon = EXEAssets.IconAtk20,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.Attack20)),
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

            Attack30SkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_ATK30_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_ATK30_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_ATK10_DESCRIPTION",
                skillIcon = EXEAssets.IconAtk30,
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
                isCombatSkill = false,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });

            Barr100SkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_BARR100_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_BARR100_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_BARR100_DESCRIPTION",
                skillIcon = EXEAssets.IconBarr100,
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
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_BARR200_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_BARR200_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_BARR200_DESCRIPTION",
                skillIcon = EXEAssets.IconBarr200,
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
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_BUGFIX_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_BUGFIX_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_BUGFIX_DESCRIPTION",
                skillIcon = EXEAssets.IconBugFix,
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

            FstGaugeSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_FSTGAUGE_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_FSTGAUGE_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_FSTGAUGE_DESCRIPTION",
                skillIcon = EXEAssets.IconFstGauge,
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

            InvisSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_INVIS_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_INVIS_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_INVIS_DESCRIPTION",
                skillIcon = EXEAssets.IconInvis,
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

            Recov300SkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_RECOV300_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_RECOV300_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_RECOV300_DESCRIPTION",
                skillIcon = EXEAssets.IconRecov300,
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
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_RECOV50_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_RECOV50_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_RECOV50_DESCRIPTION",
                skillIcon = EXEAssets.IconRecov50,
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
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_REFLECTOR_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_REFLECTOR_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_REFLECTOR_DESCRIPTION",
                skillIcon = EXEAssets.IconReflector,
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

            #endregion

            #region SwordChips

            AquaSwrdSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_AQUASWRD_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_AQUASWRD_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_AQUASWRD_DESCRIPTION",
                skillIcon = EXEAssets.IconAquaSwrd,
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

            ElecSwrdSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_ELECSWRD_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_ELECSWRD_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_ELECSWRD_DESCRIPTION",
                skillIcon = EXEAssets.IconElecSwrd,
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
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_FIRESWRD_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_FIRESWRD_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_FIRESWRD_DESCRIPTION",
                skillIcon = EXEAssets.IconFireSwrd,
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

            GutPunchSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_GUTPUNCH_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_GUTPUNCH_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_GUTPUNCH_DESCRIPTION",
                skillIcon = EXEAssets.IconGutPunch,
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

            MuramasaSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_MURAMASA_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_MURAMASA_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_MURAMASA_DESCRIPTION",
                skillIcon = EXEAssets.IconMuramasa,
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

            StepSwordSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_STEPSWORD_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_STEPSWORD_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_STEPSWORD_DESCRIPTION",
                skillIcon = EXEAssets.IconStepSword,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.StepSwordDash)),
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

            #endregion

            #region BUSTERCHIPS

            AirShotSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_AIRSHOT_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_AIRSHOT_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_AIRSHOT_DESCRIPTION",
                skillIcon = EXEAssets.IconAirShot,
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

            CannonSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_CANNON_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_CANNON_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_CANNON_DESCRIPTION",
                skillIcon = EXEAssets.IconCannon,
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

            GutPnchShotSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_GUTSPNCHSHOT_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_GUTSPNCHSHOT_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_GUTSPNCHSHOT_DESCRIPTION",
                skillIcon = EXEAssets.IconGutPunchShot,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.GutsPnchShot)),
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

            HiCannonSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_HICANNON_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_HICANNON_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_HICANNON_DESCRIPTION",
                skillIcon = EXEAssets.IconHiCannon,
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

            MCannonSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_MCANNON_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_MCANNON_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_MCANNON_DESCRIPTION",
                skillIcon = EXEAssets.IconMCannon,
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

            ShokWaveSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_SHOKWAVE_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_SHOKWAVE_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_SHOKWAVE_DESCRIPTION",
                skillIcon = EXEAssets.IconShockWave,
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
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_SHOTGUN_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_SHOTGUN_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_SHOTGUN_DESCRIPTION",
                skillIcon = EXEAssets.IconShotgun,
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

            SpreaderSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_SPREADER_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_SPREADER_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_SPREADER_DESCRIPTION",
                skillIcon = EXEAssets.IconSpreader,
                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.Spreader)),
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
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_SUPRVULC_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_SUPRVULC_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_SUPRVULC_DESCRIPTION",
                skillIcon = EXEAssets.IconSuprVulc,
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
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_THUNDER_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_THUNDER_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_THUNDER_DESCRIPTION",
                skillIcon = EXEAssets.IconThunder,
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
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_VULCAN_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_VULCAN_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_VULCAN_DESCRIPTION",
                skillIcon = EXEAssets.IconVulcan,
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
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_YOYO_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_YOYO_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_YOYO_DESCRIPTION",
                skillIcon = EXEAssets.IconYoyo,
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

            #endregion

            #region BOMBCHIPS

            MiniBombSkillDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_MINIBOMB_NAME",
                skillNameToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_MINIBOMB_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "_MEGAMAN_EXE_BODY_CHIP_MINIBOMB_DESCRIPTION",
                skillIcon = EXEAssets.IconMiniBomb,
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

            #endregion

        }

        //skip if you don't have a passive
        //also skip if this is your first look at skills
        private void AddPassiveSkill()
        {
            //option 1. fake passive icon just to describe functionality we will implement elsewhere
            bodyPrefab.GetComponent<SkillLocator>().passiveSkill = new SkillLocator.PassiveSkill
            {
                enabled = true,
                skillNameToken = MMEXE_PREFIX + "PASSIVE_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "PASSIVE_DESCRIPTION",
                keywordToken = "KEYWORD_STUNNING",
                icon = assetBundle.LoadAsset<Sprite>("texPassiveIcon"),
            };

            //option 2. a new SkillFamily for a passive, used if you want multiple selectable passives
            GenericSkill passiveGenericSkill = Skills.CreateGenericSkillWithSkillFamily(bodyPrefab, "PassiveSkill");
            SkillDef passiveSkillDef1 = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "HenryPassive",
                skillNameToken = MMEXE_PREFIX + "PASSIVE_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "PASSIVE_DESCRIPTION",
                keywordTokens = new string[] { "KEYWORD_AGILE" },
                skillIcon = assetBundle.LoadAsset<Sprite>("texPassiveIcon"),

                //unless you're somehow activating your passive like a skill, none of the following is needed.
                //but that's just me saying things. the tools are here at your disposal to do whatever you like with

                //activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.Shoot)),
                //activationStateMachineName = "Weapon1",
                //interruptPriority = EntityStates.InterruptPriority.Skill,

                //baseRechargeInterval = 1f,
                //baseMaxStock = 1,

                //rechargeStock = 1,
                //requiredStock = 1,
                //stockToConsume = 1,

                //resetCooldownTimerOnUse = false,
                //fullRestockOnAssign = true,
                //dontAllowPastMaxStocks = false,
                //mustKeyPress = false,
                //beginSkillCooldownOnSkillEnd = false,

                //isCombatSkill = true,
                //canceledFromSprinting = false,
                //cancelSprintingOnActivation = false,
                //forceSprintDuringState = false,

            });
            Skills.AddSkillsToFamily(passiveGenericSkill.skillFamily, passiveSkillDef1);
        }

        //if this is your first look at skilldef creation, take a look at Secondary first
        private void AddPrimarySkills()
        {
            Skills.CreateGenericSkillWithSkillFamily(bodyPrefab, SkillSlot.Primary);

            //the primary skill is created using a constructor for a typical primary
            //it is also a SteppedSkillDef. Custom Skilldefs are very useful for custom behaviors related to casting a skill. see ror2's different skilldefs for reference
            SteppedSkillDef primarySkillDef1 = Skills.CreateSkillDef<SteppedSkillDef>(new SkillDefInfo
                (
                    "HenrySlash",
                    MMEXE_PREFIX + "PRIMARY_SLASH_NAME",
                    MMEXE_PREFIX + "PRIMARY_SLASH_DESCRIPTION",
                    assetBundle.LoadAsset<Sprite>("texPrimaryIcon"),
                    new EntityStates.SerializableEntityStateType(typeof(SkillStates.BusterEXE)),
                    "Weapon",
                    true
                ));
            //custom Skilldefs can have additional fields that you can set manually
            primarySkillDef1.stepCount = 2;
            primarySkillDef1.stepGraceDuration = 0.5f;

            //Skills.AddPrimarySkills(bodyPrefab, primarySkillDef1);
            Skills.AddPrimarySkills(bodyPrefab, BusterEXESkillDef);
        }

        private void AddSecondarySkills()
        {
            Skills.CreateGenericSkillWithSkillFamily(bodyPrefab, SkillSlot.Secondary);

            //here is a basic skill def with all fields accounted for
            SkillDef secondarySkillDef1 = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "HenryGun",
                skillNameToken = MMEXE_PREFIX + "SECONDARY_GUN_NAME",
                skillDescriptionToken = MMEXE_PREFIX + "SECONDARY_GUN_DESCRIPTION",
                keywordTokens = new string[] { "KEYWORD_AGILE" },
                skillIcon = assetBundle.LoadAsset<Sprite>("texSecondaryIcon"),

                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.Shoot)),
                activationStateMachineName = "Weapon2",
                interruptPriority = EntityStates.InterruptPriority.Skill,

                baseRechargeInterval = 1f,
                baseMaxStock = 1,

                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = true,
                dontAllowPastMaxStocks = false,
                mustKeyPress = false,
                beginSkillCooldownOnSkillEnd = false,

                isCombatSkill = true,
                canceledFromSprinting = false,
                cancelSprintingOnActivation = false,
                forceSprintDuringState = false,

            });

            //Skills.AddSecondarySkills(bodyPrefab, secondarySkillDef1);
            Skills.AddSecondarySkills(bodyPrefab, CyberSwordSkillDef);
        }

        private void AddUtiitySkills()
        {
            Skills.CreateGenericSkillWithSkillFamily(bodyPrefab, SkillSlot.Utility);

            //here's a skilldef of a typical movement skill.
            //SkillDef utilitySkillDef1 = Skills.CreateSkillDef(new SkillDefInfo
            //{
            //    skillName = "HenryRoll",
            //    skillNameToken = MMEXE_PREFIX + "UTILITY_ROLL_NAME",
            //    skillDescriptionToken = MMEXE_PREFIX + "UTILITY_ROLL_DESCRIPTION",
            //    skillIcon = assetBundle.LoadAsset<Sprite>("texUtilityIcon"),

            //    activationState = new EntityStates.SerializableEntityStateType(typeof(Roll)),
            //    activationStateMachineName = "Body",
            //    interruptPriority = EntityStates.InterruptPriority.PrioritySkill,

            //    baseRechargeInterval = 4f,
            //    baseMaxStock = 1,

            //    rechargeStock = 1,
            //    requiredStock = 1,
            //    stockToConsume = 1,

            //    resetCooldownTimerOnUse = false,
            //    fullRestockOnAssign = true,
            //    dontAllowPastMaxStocks = false,
            //    mustKeyPress = false,
            //    beginSkillCooldownOnSkillEnd = false,

            //    isCombatSkill = false,
            //    canceledFromSprinting = false,
            //    cancelSprintingOnActivation = false,
            //    forceSprintDuringState = true,
            //});

            Skills.AddUtilitySkills(bodyPrefab, SendChipSkillDef);
        }

        private void AddSpecialSkills()
        {
            Skills.CreateGenericSkillWithSkillFamily(bodyPrefab, SkillSlot.Special);

            //a basic skill. some fields are omitted and will just have default values
            //SkillDef specialSkillDef1 = Skills.CreateSkillDef(new SkillDefInfo
            //{
            //    skillName = "HenryBomb",
            //    skillNameToken = MMEXE_PREFIX + "SPECIAL_BOMB_NAME",
            //    skillDescriptionToken = MMEXE_PREFIX + "SPECIAL_BOMB_DESCRIPTION",
            //    skillIcon = assetBundle.LoadAsset<Sprite>("texSpecialIcon"),

            //    activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.ThrowBomb)),
            //    //setting this to the "weapon2" EntityStateMachine allows us to cast this skill at the same time primary, which is set to the "weapon" EntityStateMachine
            //    activationStateMachineName = "Weapon2", interruptPriority = EntityStates.InterruptPriority.Skill,

            //    baseMaxStock = 1,
            //    baseRechargeInterval = 10f,

            //    isCombatSkill = true,
            //    mustKeyPress = false,
            //});

            Skills.AddSpecialSkills(bodyPrefab, NoDataSkillDef);
        }
        #endregion skills

        #region extraskills

        private void AddExtraFirstSkills()
        {
            Skills.AddFirstExtraSkill(bodyPrefab, TurretTestSkillDef);
            Skills.AddFirstExtraSkill(bodyPrefab, DrkSwordSkillDef);
            Skills.AddFirstExtraSkill(bodyPrefab, AdvInfiniteVulcanSkillDef);
            Skills.AddFirstExtraSkill(bodyPrefab, DrkBombSkillDef);
            Skills.AddFirstExtraSkill(bodyPrefab, DrkCannonSkillDef);
            Skills.AddFirstExtraSkill(bodyPrefab, GutPnchShotSkillDef);
        }
        private void AddExtraSecondSkills()
        {
            Skills.AddSecondExtraSkill(bodyPrefab, AquaSwrdSkillDef);
            Skills.AddSecondExtraSkill(bodyPrefab, ElecSwrdSkillDef);
            Skills.AddSecondExtraSkill(bodyPrefab, FireSwrdSkillDef);
            Skills.AddSecondExtraSkill(bodyPrefab, GutPunchSkillDef);
            Skills.AddSecondExtraSkill(bodyPrefab, SpreaderSkillDef);
        }
        private void AddExtraThirdSkills()
        {
            Skills.AddThirdExtraSkill(bodyPrefab, AirShotSkillDef);
            Skills.AddThirdExtraSkill(bodyPrefab, CannonSkillDef);
            Skills.AddThirdExtraSkill(bodyPrefab, StepSwordSkillDef);
            Skills.AddThirdExtraSkill(bodyPrefab, ShokWaveSkillDef);
            Skills.AddThirdExtraSkill(bodyPrefab, MiniBombSkillDef);
        }
        private void AddExtraFourthSkills()
        {
            Skills.AddFourthExtraSkill(bodyPrefab, BugFixSkillDef);
            Skills.AddFourthExtraSkill(bodyPrefab, InvisSkillDef);
            Skills.AddFourthExtraSkill(bodyPrefab, DrkRecovSkillDef);
            Skills.AddFourthExtraSkill(bodyPrefab, ReflectorSkillDef);
        }

        #endregion

        #region skins
        public override void InitializeSkins()
        {
            ModelSkinController skinController = prefabCharacterModel.gameObject.AddComponent<ModelSkinController>();
            ChildLocator childLocator = prefabCharacterModel.GetComponent<ChildLocator>();

            CharacterModel.RendererInfo[] defaultRendererinfos = prefabCharacterModel.baseRendererInfos;

            List<SkinDef> skins = new List<SkinDef>();

            #region DefaultSkin
            //this creates a SkinDef with all default fields
            SkinDef defaultSkin = Skins.CreateSkinDef(MMEXE_PREFIX + "MMEXE_SKIN_NAME",
                EXEAssets.IconEXE,
                defaultRendererinfos,
                prefabCharacterModel.gameObject);

            //these are your Mesh Replacements. The order here is based on your CustomRendererInfos from earlier
            //pass in meshes as they are named in your assetbundle
            //currently not needed as with only 1 skin they will simply take the default meshes
            //uncomment this when you have another skin
            defaultSkin.meshReplacements = Modules.Skins.getMeshReplacements(assetBundle, defaultRendererinfos,
                "EXEBodyMesh",
                "EXELHandMesh",
                "EXERHandMesh",
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null);

            //you can simply access the RendererInfos' materials and set them to the new materials for your skin.
            defaultSkin.rendererInfos[0].defaultMaterial = EXEAssets.EXEMat;
            defaultSkin.rendererInfos[1].defaultMaterial = EXEAssets.EXEMat;
            defaultSkin.rendererInfos[2].defaultMaterial = EXEAssets.EXEMat;
            defaultSkin.rendererInfos[4].defaultMaterial = EXEAssets.EXEMat;
            defaultSkin.rendererInfos[7].defaultMaterial = EXEAssets.EXESwordMat;

            //here's a barebones example of using gameobjectactivations that could probably be streamlined or rewritten entirely, truthfully, but it works
            defaultSkin.gameObjectActivations = new SkinDef.GameObjectActivation[]
            {
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEBodyMesh"),
                    shouldActivate = true,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEHandLMesh"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEHandRMesh"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEBuster"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("ProtoBuster"),
                    shouldActivate = true,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("RollBuster"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("BassBuster"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("CYSword"),
                    shouldActivate = true,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEMask"),
                    shouldActivate = true,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("GutsPunch"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("DiveEXESword"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("DiveEXEBuster"),
                    shouldActivate = false,
                }
            };

            //add new skindef to our list of skindefs. this is what we'll be passing to the SkinController
            skins.Add(defaultSkin);
            #endregion

            #region PROTO

            ////creating a new skindef as we did before
            SkinDef protoSkin = Modules.Skins.CreateSkinDef(MMEXE_PREFIX + "PROTO_SKIN_NAME",
                EXEAssets.IconProtoman,
                defaultRendererinfos,
                prefabCharacterModel.gameObject);

            //adding the mesh replacements as above. 
            //if you don't want to replace the mesh (for example, you only want to replace the material), pass in null so the order is preserved
            protoSkin.meshReplacements = Modules.Skins.getMeshReplacements(assetBundle, defaultRendererinfos,
                "ProtomanBodyMesh",
                "PHandLMesh",
                "PHandRMesh",
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null);

            //masterySkin has a new set of RendererInfos (based on default rendererinfos)
            //you can simply access the RendererInfos' materials and set them to the new materials for your skin.
            protoSkin.rendererInfos[0].defaultMaterial = EXEAssets.ProtoMat;
            protoSkin.rendererInfos[1].defaultMaterial = EXEAssets.ProtoMat;
            protoSkin.rendererInfos[2].defaultMaterial = EXEAssets.ProtoMat;
            protoSkin.rendererInfos[4].defaultMaterial = EXEAssets.ProtoBusterMat;
            protoSkin.rendererInfos[7].defaultMaterial = EXEAssets.ProtoSwordMat;

            //here's a barebones example of using gameobjectactivations that could probably be streamlined or rewritten entirely, truthfully, but it works
            protoSkin.gameObjectActivations = new SkinDef.GameObjectActivation[]
            {
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEBodyMesh"),
                    shouldActivate = true,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEHandLMesh"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEHandRMesh"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEBuster"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("ProtoBuster"),
                    shouldActivate = true,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("RollBuster"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("BassBuster"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("CYSword"),
                    shouldActivate = true,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEMask"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("GutsPunch"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("DiveEXESword"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("DiveEXEBuster"),
                    shouldActivate = false,
                }
            };
            //simply find an object on your child locator you want to activate/deactivate and set if you want to activate/deacitvate it with this skin

            skins.Add(protoSkin);

            #endregion

            #region ROLL

            ////creating a new skindef as we did before
            SkinDef rollSkin = Modules.Skins.CreateSkinDef(MMEXE_PREFIX + "ROLL_SKIN_NAME",
                EXEAssets.IconRoll,
                defaultRendererinfos,
                prefabCharacterModel.gameObject);

            //adding the mesh replacements as above. 
            //if you don't want to replace the mesh (for example, you only want to replace the material), pass in null so the order is preserved
            rollSkin.meshReplacements = Modules.Skins.getMeshReplacements(assetBundle, defaultRendererinfos,
                "RollBodyMesh",
                "RollHandLMesh",
                "RollHandRMesh",
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null);

            //masterySkin has a new set of RendererInfos (based on default rendererinfos)
            //you can simply access the RendererInfos' materials and set them to the new materials for your skin.
            rollSkin.rendererInfos[0].defaultMaterial = EXEAssets.RollMat;
            rollSkin.rendererInfos[1].defaultMaterial = EXEAssets.RollMat;
            rollSkin.rendererInfos[2].defaultMaterial = EXEAssets.RollMat;
            rollSkin.rendererInfos[7].defaultMaterial = EXEAssets.RollSwordMat;

            //here's a barebones example of using gameobjectactivations that could probably be streamlined or rewritten entirely, truthfully, but it works
            rollSkin.gameObjectActivations = new SkinDef.GameObjectActivation[]
            {
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEBodyMesh"),
                    shouldActivate = true,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEHandLMesh"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEHandRMesh"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEBuster"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("ProtoBuster"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("RollBuster"),
                    shouldActivate = true,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("BassBuster"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("CYSword"),
                    shouldActivate = true,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEMask"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("GutsPunch"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("DiveEXESword"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("DiveEXEBuster"),
                    shouldActivate = false,
                }
            };
            //simply find an object on your child locator you want to activate/deactivate and set if you want to activate/deacitvate it with this skin

            skins.Add(rollSkin);

            #endregion

            #region BASS

            ////creating a new skindef as we did before
            SkinDef bassSkin = Modules.Skins.CreateSkinDef(MMEXE_PREFIX + "BASS_SKIN_NAME",
                EXEAssets.IconBass,
                defaultRendererinfos,
                prefabCharacterModel.gameObject);

            //adding the mesh replacements as above. 
            //if you don't want to replace the mesh (for example, you only want to replace the material), pass in null so the order is preserved
            bassSkin.meshReplacements = Modules.Skins.getMeshReplacements(assetBundle, defaultRendererinfos,
                "BEBodyMesh",
                "BEHandLMesh",
                "BEHandRMesh",
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null);

            //masterySkin has a new set of RendererInfos (based on default rendererinfos)
            //you can simply access the RendererInfos' materials and set them to the new materials for your skin.
            bassSkin.rendererInfos[0].defaultMaterial = EXEAssets.BassMat;
            bassSkin.rendererInfos[1].defaultMaterial = EXEAssets.BassMat;
            bassSkin.rendererInfos[2].defaultMaterial = EXEAssets.BassMat;
            bassSkin.rendererInfos[7].defaultMaterial = EXEAssets.BassSwordMat;

            //here's a barebones example of using gameobjectactivations that could probably be streamlined or rewritten entirely, truthfully, but it works
            bassSkin.gameObjectActivations = new SkinDef.GameObjectActivation[]
            {
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEBodyMesh"),
                    shouldActivate = true,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEHandLMesh"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEHandRMesh"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEBuster"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("ProtoBuster"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("RollBuster"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("BassBuster"),
                    shouldActivate = true,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("CYSword"),
                    shouldActivate = true,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEMask"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("GutsPunch"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("DiveEXESword"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("DiveEXEBuster"),
                    shouldActivate = false,
                }
            };
            //simply find an object on your child locator you want to activate/deactivate and set if you want to activate/deacitvate it with this skin

            skins.Add(bassSkin);

            #endregion

            #region DIVE

            ////creating a new skindef as we did before
            SkinDef diveSkin = Modules.Skins.CreateSkinDef(MMEXE_PREFIX + "DIVEEXE_SKIN_NAME",
                EXEAssets.IconEXEDive,
                defaultRendererinfos,
                prefabCharacterModel.gameObject);

            //adding the mesh replacements as above. 
            //if you don't want to replace the mesh (for example, you only want to replace the material), pass in null so the order is preserved
            diveSkin.meshReplacements = Modules.Skins.getMeshReplacements(assetBundle, defaultRendererinfos,
                "DiveEXEBodyMesh",
                "DiveEXEHandLMesh",
                "DiveEXEHandRMesh",
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null);

            //masterySkin has a new set of RendererInfos (based on default rendererinfos)
            //you can simply access the RendererInfos' materials and set them to the new materials for your skin.
            diveSkin.rendererInfos[0].defaultMaterial = EXEAssets.DiveMat;
            diveSkin.rendererInfos[1].defaultMaterial = EXEAssets.DiveMat;
            diveSkin.rendererInfos[2].defaultMaterial = EXEAssets.DiveMat;

            //here's a barebones example of using gameobjectactivations that could probably be streamlined or rewritten entirely, truthfully, but it works
            diveSkin.gameObjectActivations = new SkinDef.GameObjectActivation[]
            {
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEBodyMesh"),
                    shouldActivate = true,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEHandLMesh"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEHandRMesh"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEBuster"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("ProtoBuster"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("RollBuster"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("BassBuster"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("CYSword"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("EXEMask"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("GutsPunch"),
                    shouldActivate = false,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("DiveEXESword"),
                    shouldActivate = true,
                },
                new SkinDef.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("DiveEXEBuster"),
                    shouldActivate = true,
                }
            };
            //simply find an object on your child locator you want to activate/deactivate and set if you want to activate/deacitvate it with this skin

            skins.Add(diveSkin);

            #endregion

            //uncomment this when you have a mastery skin
            #region MasterySkin

            ////creating a new skindef as we did before
            //SkinDef masterySkin = Modules.Skins.CreateSkinDef(MMEXE_PREFIX + "MASTERY_SKIN_NAME",
            //    assetBundle.LoadAsset<Sprite>("texMasteryAchievement"),
            //    defaultRendererinfos,
            //    prefabCharacterModel.gameObject,
            //    HenryUnlockables.masterySkinUnlockableDef);

            ////adding the mesh replacements as above. 
            ////if you don't want to replace the mesh (for example, you only want to replace the material), pass in null so the order is preserved
            //masterySkin.meshReplacements = Modules.Skins.getMeshReplacements(assetBundle, defaultRendererinfos,
            //    "meshHenrySwordAlt",
            //    null,//no gun mesh replacement. use same gun mesh
            //    "meshHenryAlt");

            ////masterySkin has a new set of RendererInfos (based on default rendererinfos)
            ////you can simply access the RendererInfos' materials and set them to the new materials for your skin.
            //masterySkin.rendererInfos[0].defaultMaterial = assetBundle.LoadMaterial("matHenryAlt");
            //masterySkin.rendererInfos[1].defaultMaterial = assetBundle.LoadMaterial("matHenryAlt");
            //masterySkin.rendererInfos[2].defaultMaterial = assetBundle.LoadMaterial("matHenryAlt");

            ////here's a barebones example of using gameobjectactivations that could probably be streamlined or rewritten entirely, truthfully, but it works
            //masterySkin.gameObjectActivations = new SkinDef.GameObjectActivation[]
            //{
            //    new SkinDef.GameObjectActivation
            //    {
            //        gameObject = childLocator.FindChildGameObject("GunModel"),
            //        shouldActivate = false,
            //    }
            //};
            ////simply find an object on your child locator you want to activate/deactivate and set if you want to activate/deacitvate it with this skin

            //skins.Add(masterySkin);

            #endregion

            skinController.skins = skins.ToArray();
        }
        #endregion skins

        //Character Master is what governs the AI of your character when it is not controlled by a player (artifact of vengeance, goobo)
        public override void InitializeCharacterMaster()
        {
            //you must only do one of these. adding duplicate masters breaks the game.

            //if you're lazy or prototyping you can simply copy the AI of a different character to be used
            //Modules.Prefabs.CloneDopplegangerMaster(bodyPrefab, masterName, "Merc");

            //how to set up AI in code
            EXEAI.Init(bodyPrefab, masterName);

            //how to load a master set up in unity, can be an empty gameobject with just AISkillDriver components
            //assetBundle.LoadMaster(bodyPrefab, masterName);
        }

        private void AddHooks()
        {
            R2API.RecalculateStatsAPI.GetStatCoefficients += RecalculateStatsAPI_GetStatCoefficients;
            On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;
            On.RoR2.SurvivorCatalog.Init += SurvivorCatalog_Init;
            CustomEmotesAPI.animChanged += CustomEmotesAPI_animChanged;
            On.RoR2.CharacterMaster.OnBodyStart += RestoreHPAfterRespawn;
        }

        private void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        {
            orig(self, damageInfo);

            Debug.Log("Enter Hook");

            if (self == null || damageInfo == null)
                return;

            if (damageInfo.inflictor == null || damageInfo.attacker == null)
                return;

            if (self.GetComponent<CharacterBody>() == null)
                return;

            //Debug.Log("self " + self);
            //Debug.Log("self name " + self.name);
            //Debug.Log("damageInfo " + damageInfo);
            //Debug.Log("damageInfo.attacker " + damageInfo.attacker);

            //Se nao tiver o BUFF do REFLECTOR sofre o dano normalmente e tem a penalidade emocional.
            if (!damageInfo.attacker.name.Contains("MegamanEXE") && self.name.Contains("MegamanEXE") && !self.GetComponent<CharacterBody>().HasBuff(EXEBuffs.ReflectorBuff))
                self.GetComponent<CharacterBody>().GetComponent<EXEBaseComponent>().UpdateEmotionalValue(-1, 0, damageInfo.damage);

            //Se tiver, realiza o contra ataque e nao sofre penalidade, ganhando pontos na verdade.
            if (!damageInfo.attacker.name.Contains("MegamanEXE") && self.name.Contains("MegamanEXE") && self.GetComponent<CharacterBody>().HasBuff(EXEBuffs.ReflectorBuff))
            {

                Vector3 direction = (damageInfo.attacker.transform.position - self.transform.position).normalized;

                FireProjectileInfo ReflectorProjectille = new FireProjectileInfo();
                ReflectorProjectille.projectilePrefab = EXEAssets.shockwaveProjectilePrefab;
                ReflectorProjectille.position = self.transform.position;
                ReflectorProjectille.rotation = Util.QuaternionSafeLookRotation(direction);
                ReflectorProjectille.owner = self.GetComponent<CharacterBody>().gameObject;
                ReflectorProjectille.damage = EXEStaticValues.swordDamageCoefficient * self.GetComponent<CharacterBody>().damage;
                ReflectorProjectille.force = 1000;
                ReflectorProjectille.crit = self.GetComponent<CharacterBody>().RollCrit();
                ReflectorProjectille.damageColorIndex = DamageColorIndex.Default;
                ReflectorProjectille.damageTypeOverride = DamageTypeCombo.Generic;

                ProjectileManager.instance.FireProjectile(ReflectorProjectille);

                self.GetComponent<CharacterBody>().GetComponent<EXEBaseComponent>().UpdateEmotionalValue(2, 0, 0);

            }






        }

        private void RecalculateStatsAPI_GetStatCoefficients(CharacterBody sender, R2API.RecalculateStatsAPI.StatHookEventArgs args)
        {

            if (sender.HasBuff(EXEBuffs.armorBuff))
            {
                args.armorAdd += 300;
            }

            if (sender.HasBuff(EXEBuffs.Attack10Buff))
            {
                args.damageMultAdd += 0.1f;

            }

            if (sender.HasBuff(EXEBuffs.Attack20Buff))
            {
                args.damageMultAdd += 0.2f;

            }

            if (sender.HasBuff(EXEBuffs.Attack30Buff))
            {
                args.damageMultAdd += 0.3f;

            }

            if (sender.HasBuff(EXEBuffs.FullSyncBuff))
            {
                args.damageMultAdd += 2f;
                args.critDamageMultAdd += 2f;
                args.critAdd += 45f;
                args.moveSpeedMultAdd += 0.4f;
                args.regenMultAdd += 0.5f;

            }

            if (sender.HasBuff(EXEBuffs.RageBuff))
            {
                args.damageMultAdd += 3f;
                args.moveSpeedMultAdd += 0.3f;

            }


            if (sender.HasBuff(EXEBuffs.AnxiousBuff))
            {
                args.damageMultAdd -= 0.2f;
                args.armorAdd -= sender.baseArmor * 0.8f;
                args.moveSpeedMultAdd += 0.3f;

            }

            if (sender.HasBuff(EXEBuffs.DarkDebuff1))
            {
                args.jumpPowerMultAdd = 0.1f;
                args.baseJumpPowerAdd = 0.1f;
                args.moveSpeedMultAdd = 0.25f;
                args.baseMoveSpeedAdd = 0.25f;
                args.moveSpeedReductionMultAdd += 2f;
            }

            if (sender.HasBuff(EXEBuffs.DarkDebuff2))
            {
                args.jumpPowerMultAdd += 3f;
                args.baseJumpPowerAdd += 3f;
                args.moveSpeedMultAdd += 3f;
                args.baseMoveSpeedAdd += 3f;
            }

            if (sender.HasBuff(EXEBuffs.DarkDebuff3))
            {
                args.damageMultAdd = 0.1f;
                args.baseDamageAdd -= sender.baseDamage/2;
                args.primaryCooldownMultAdd += 3f;
            }

            if (sender.HasBuff(EXEBuffs.DarkDebuff4))
            {
                if (NetworkServer.active)
                {
                    sender.AddHelfireDuration(2f);
                    sender.AddTimedBuff(RoR2Content.Buffs.Weak, 5f);
                }
                
            }

            if (sender.HasBuff(EXEBuffs.DarkDebuff5))
            {
                sender.hideCrosshair = true;
            }
            else
            {
                sender.hideCrosshair = false;
            }

            if (sender.HasBuff(EXEBuffs.DarkDebuff6))
            {
                if (NetworkServer.active)
                {
                    sender.AddTimedBuff(RoR2Content.Buffs.Slow50, 5f);
                    sender.AddTimedBuff(DLC1Content.Buffs.Blinded, 8f);
                }

            }

            if (sender.HasBuff(EXEBuffs.DarkDebuff7))
            {
                args.armorAdd -= sender.baseArmor * 0.4f;
            }

            if (sender.HasBuff(EXEBuffs.DarkDebuff8))
            {
                sender.AddTimedBuff(DLC1Content.Buffs.Blinded, 15f);
            }

            if (sender.HasBuff(EXEBuffs.DarkDebuff9))
            {
                sender.skillLocator.primary.temporaryCooldownPenalty = 3f;
                sender.skillLocator.secondary.temporaryCooldownPenalty = 10f;
                sender.skillLocator.utility.temporaryCooldownPenalty = 20f;
            }

        }

        private void RestoreHPAfterRespawn(On.RoR2.CharacterMaster.orig_OnBodyStart orig, CharacterMaster self, CharacterBody newBody)
        {
            orig(self, newBody);

            //Debug.Log("xTakeDamageValue: " + xTakeDamageValue);
            //Debug.Log("xMaster: " + xMaster);
            //Debug.Log("self: " + self);

            if (self == EXEMaster) // Certifica-se de que estamos restaurando o HP do personagem correto
            {
                float restoredHP = EXETakeDamageValue;

                if (newBody && newBody.healthComponent)
                {
                    newBody.healthComponent.health = Mathf.Clamp(restoredHP, 1f, newBody.healthComponent.fullHealth);
                    //Debug.Log($"HP restaurado para {newBody.healthComponent.health}");
                }
            }
        }

        private void CustomEmotesAPI_animChanged(string newAnimation, BoneMapper mapper)
        {
            //Debug.Log("newAnimation: " + newAnimation);
            //Debug.Log("mapper: " + mapper);
            //Debug.Log("mapper.bodyPrefab.name: " + mapper.bodyPrefab.name);

            if (mapper.bodyPrefab.name.Contains("MegamanEXEBody"))
            {
                if (newAnimation == "none")
                {
                    if (mapper.bodyPrefab.GetComponent<CharacterBody>())
                    {

                        //Debug.Log("ANim changed to NONE");

                        //NA MORAL VOU DEIXAR ISSO TUDO COMENTADO PELO ÓDIO QUE EU SENTI!

                        float savedHP = mapper.bodyPrefab.GetComponent<CharacterBody>().healthComponent.health;

                        EXETakeDamageValue = savedHP;
                        EXEMaster = mapper.bodyPrefab.GetComponent<CharacterBody>().master;


                        //Debug.Log("xTakeDamageValue: " + xTakeDamageValue);

                        // Mata o personagem atual (sem contar como "morte real")
                        GameObject.Destroy(mapper.bodyPrefab.GetComponent<CharacterBody>().gameObject);

                        // Força o CharacterMaster a reaparecer o personagem
                        mapper.bodyPrefab.GetComponent<CharacterBody>().master.Respawn(mapper.bodyPrefab.GetComponent<CharacterBody>().footPosition, Quaternion.identity);


                    }
                }
            }
        }

        private void SurvivorCatalog_Init(On.RoR2.SurvivorCatalog.orig_Init orig)
        {
            orig();
            if (!setupEmoteSkeleton)
            {
                setupEmoteSkeleton = true;
                foreach (var item in SurvivorCatalog.allSurvivorDefs)
                {
                    if (item.bodyPrefab.name == "MegamanEXEBody")
                    {
                        var skele = EXEAssets.EXEEmotePrefab;
                        //Debug.Log("Before Emote: " + item.bodyPrefab.transform.position);
                        CustomEmotesAPI.ImportArmature(item.bodyPrefab, skele, true);
                        CustomEmotesAPI.CreateNameTokenSpritePair(MMEXE_PREFIX + "NAME", EXEAssets.IconEXEEmote);
                        //skele.GetComponentInChildren<BoneMapper>().scale = 1.05f;
                        //item.bodyPrefab.GetComponentInChildren<BoneMapper>().scale = 0.5f;
                        //skele.GetComponentInChildren<BoneMapper>().scale = 0.5f;
                        //Debug.Log("after Emote: " + item.bodyPrefab.transform.position);
                        //Debug.Log("skele pos: " + skele.transform.position);
                    }
                }
            }
        }

    }
}