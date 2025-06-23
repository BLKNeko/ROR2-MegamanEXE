using RoR2;
using UnityEngine;
using MegamanEXEMod.Modules;
using System;
using RoR2.Projectile;
using R2API;
using MegamanEXEMod.Survivors.MegamanEXE.Components;

namespace MegamanEXEMod.Survivors.MegamanEXE
{
    public static class EXEAssets
    {
        // particle effects
        public static GameObject swordSwingEffect;
        public static GameObject swordHitImpactEffect;

        public static GameObject bombExplosionEffect;

        // networked hit sounds
        public static NetworkSoundEventDef swordHitSoundEvent;

        //projectiles
        public static GameObject bombProjectilePrefab;
        public static GameObject miniBombProjectilePrefab;
        public static GameObject thunderProjectilePrefab;
        public static GameObject yoyoProjectilePrefab;
        public static GameObject shockwaveProjectilePrefab;
        public static GameObject shotgunProjectilePrefab;
        public static GameObject gutsPnchProjectilePrefab;
        public static GameObject exeTurretProjectilePrefab;


        public static Material EXEMat;
        public static Material ProtoMat;
        public static Material RollMat;
        public static Material BassMat;
        public static Material DiveMat;

        public static Material DarkEXEMat;
        public static Material DarkProtoMat;
        public static Material DarkRollMat;
        public static Material DarkBassMat;
        public static Material DarkDiveMat;


        public static Material EXESwordMat;
        public static Material DarkEXESwordMat;
        public static Material ProtoSwordMat;
        public static Material RollSwordMat;
        public static Material BassSwordMat;

        public static Material ProtoBusterMat;
        public static Material RollBusterMat;

        public static Texture IconEXETex;

        public static Sprite IconEXE;
        public static Sprite IconProtoman;
        public static Sprite IconRoll;
        public static Sprite IconBass;
        public static Sprite IconEXEDive;

        public static Sprite IconAirShot;
        public static Sprite IconAquaSwrd;
        public static Sprite IconBarr100;
        public static Sprite IconBusterEXE;
        public static Sprite IconCannon;
        public static Sprite IconCyberSword;
        public static Sprite IconElecSwrd;
        public static Sprite IconFireSwrd;
        public static Sprite IconHiCannon;
        public static Sprite IconMCannon;
        public static Sprite IconMiniBomb;
        public static Sprite IconMuramasa;
        public static Sprite IconRecov50;
        public static Sprite IconSuprVulc;
        public static Sprite IconThunder;
        public static Sprite IconVulcan;
        public static Sprite IconYoyo;

        public static Sprite IconAnxious;
        public static Sprite IconAtk10;
        public static Sprite IconAtk20;
        public static Sprite IconAtk30;
        public static Sprite IconBarr200;
        public static Sprite IconDrkBomb;
        public static Sprite IconDrkCannon;
        public static Sprite IconDrkRecov;
        public static Sprite IconDrkSword;
        public static Sprite IconDrkVulcan;
        public static Sprite IconEvil;
        public static Sprite IconFullSync;
        public static Sprite IconGutPunch;
        public static Sprite IconGutPunchShot;
        public static Sprite IconNormal;
        public static Sprite IconRage;
        public static Sprite IconRecov300;
        public static Sprite IconShockWave;
        public static Sprite IconShotgun;

        public static Sprite IconInvis;

        public static Sprite IconBarr500;
        public static Sprite IconBugFix;
        public static Sprite IconDrkDebuff;
        public static Sprite IconFstGauge;
        public static Sprite IconGigaCannon;
        public static Sprite IconGreatYoyo;
        public static Sprite IconInfiniteVulcan;
        public static Sprite IconLifeSword;
        public static Sprite IconNoData;
        public static Sprite IconPassive;
        public static Sprite IconSendChip;

        public static Sprite IconReflector;

        public static Sprite IconStepSword;

        public static Sprite IconAdvAirShot;

        public static Sprite IconSpreader;

        internal static GameObject VfxChargeeffect1C;
        internal static GameObject VfxChargeeffect2C;
        internal static GameObject VfxSwordFire;
        internal static GameObject VfxSwordIce;
        internal static GameObject VfxSwordEletric;
        internal static GameObject VfxRecov;
        internal static GameObject VfxFullSync;
        internal static GameObject VfxRage;
        internal static GameObject VfxEvil;
        internal static GameObject VfxDeleted;
        internal static GameObject VfxSpreaderExplosion1;
        internal static GameObject VfxSpreaderExplosion2;

        internal static GameObject BlueSwordSwingVFX;
        internal static GameObject CyanSwordSwingVFX;
        internal static GameObject RedSwordSwingVFX;
        internal static GameObject PinkSwordSwingVFX;
        internal static GameObject PurpleSwordSwingVFX;
        internal static GameObject YellowSwordSwingVFX;


        internal static GameObject AllyBodyPrefab;
        internal static GameObject AllyMasterPrefab;


        //EMOTE API
        public static GameObject EXEEmotePrefab;
        public static Sprite IconEXEEmote;

        private static AssetBundle _assetBundle;

        public static void Init(AssetBundle assetBundle)
        {

            _assetBundle = assetBundle;

            swordHitSoundEvent = Content.CreateAndAddNetworkSoundEventDef("HenrySwordHit");

            //EMOTE API
            EXEEmotePrefab = _assetBundle.LoadAsset<GameObject>("emoteskele");
            IconEXEEmote = _assetBundle.LoadAsset<Sprite>("EXEEmoteIcon");


            BlueSwordSwingVFX = _assetBundle.LoadEffect("BlueSwordSwingEffect", true);
            CyanSwordSwingVFX = _assetBundle.LoadEffect("CyanSwordSwingEffect", true);
            PinkSwordSwingVFX = _assetBundle.LoadEffect("PinkSwordSwingEffect", true);
            PurpleSwordSwingVFX = _assetBundle.LoadEffect("PurpleSwordSwingEffect", true);
            RedSwordSwingVFX = _assetBundle.LoadEffect("RedSwordSwingEffect", true);
            YellowSwordSwingVFX = _assetBundle.LoadEffect("YellowSwordSwingEffect", true);

            VfxChargeeffect1C = _assetBundle.LoadEffect("Charge1VFX", true);
            VfxChargeeffect2C = _assetBundle.LoadEffect("Charge2VFX", true);
            VfxSwordFire = _assetBundle.LoadEffect("VFXFire1", true);
            VfxSwordIce = _assetBundle.LoadEffect("VFXIce1", true);
            VfxSwordEletric = _assetBundle.LoadEffect("VFXEletric", true);
            VfxRecov = _assetBundle.LoadEffect("VFXRecov", true);
            VfxFullSync = _assetBundle.LoadEffect("VFXFullSync", true);
            VfxRage = _assetBundle.LoadEffect("VFXRage", true);
            VfxEvil = _assetBundle.LoadEffect("VFXEnterEvil", true);

            VfxDeleted = _assetBundle.LoadEffect("DeathEffect", true);

            VfxSpreaderExplosion1 = _assetBundle.LoadEffect("Explosion1VFX", false);
            VfxSpreaderExplosion2 = _assetBundle.LoadEffect("Explosion2VFX", false);


            EXEMat = _assetBundle.LoadAsset<Material>("matMMEXE");
            ProtoMat = _assetBundle.LoadAsset<Material>("matProtoEXE");
            RollMat = _assetBundle.LoadAsset<Material>("matROLLEXE");
            BassMat = _assetBundle.LoadAsset<Material>("matBassEXE");
            DiveMat = _assetBundle.LoadAsset<Material>("matDVEXE");

            DarkEXEMat = _assetBundle.LoadAsset<Material>("matDARKMMEXE");
            DarkProtoMat = _assetBundle.LoadAsset<Material>("matDRKProtoEXE");
            DarkRollMat = _assetBundle.LoadAsset<Material>("matDRKROLLEXE");
            DarkBassMat = _assetBundle.LoadAsset<Material>("matDRKBassEXE");
            DarkDiveMat = _assetBundle.LoadAsset<Material>("matDRKDVEXE");


            EXESwordMat = _assetBundle.LoadAsset<Material>("matEXESword");
            DarkEXESwordMat = _assetBundle.LoadAsset<Material>("matDarkSword");
            ProtoSwordMat = _assetBundle.LoadAsset<Material>("matProtoSword");
            RollSwordMat = _assetBundle.LoadAsset<Material>("matROLLSword");
            BassSwordMat = _assetBundle.LoadAsset<Material>("matBASSSword");
            ProtoBusterMat = _assetBundle.LoadAsset<Material>("matProtoBuster");
            RollBusterMat = _assetBundle.LoadAsset<Material>("matRBuster");

            IconEXETex = _assetBundle.LoadAsset<Texture>("TexEXE");

            IconEXE = _assetBundle.LoadAsset<Sprite>("IconEXE");
            IconProtoman = _assetBundle.LoadAsset<Sprite>("IconProtoman");
            IconRoll = _assetBundle.LoadAsset<Sprite>("IconRoll");
            IconBass = _assetBundle.LoadAsset<Sprite>("IconBass");
            IconEXEDive = _assetBundle.LoadAsset<Sprite>("IconEXEDive");


            IconAdvAirShot = _assetBundle.LoadAsset<Sprite>("IconAdvAirShot");
            IconAirShot = _assetBundle.LoadAsset<Sprite>("IconAirShot");
            IconAquaSwrd = _assetBundle.LoadAsset<Sprite>("IconAquaSwrd");
            IconBarr100 = _assetBundle.LoadAsset<Sprite>("IconBarr100");
            IconBusterEXE = _assetBundle.LoadAsset<Sprite>("IconBusterEXE");
            IconCannon = _assetBundle.LoadAsset<Sprite>("IconCannon");
            IconCyberSword = _assetBundle.LoadAsset<Sprite>("IconCyberSword");
            IconElecSwrd = _assetBundle.LoadAsset<Sprite>("IconElecSwrd");
            IconFireSwrd = _assetBundle.LoadAsset<Sprite>("IconFireSwrd");
            IconHiCannon = _assetBundle.LoadAsset<Sprite>("IconHiCannon");
            IconMCannon = _assetBundle.LoadAsset<Sprite>("IconMCannon");
            IconMiniBomb = _assetBundle.LoadAsset<Sprite>("IconMiniBomb");
            IconMuramasa = _assetBundle.LoadAsset<Sprite>("IconMuramasa");
            IconRecov50 = _assetBundle.LoadAsset<Sprite>("IconRecov50");
            IconSuprVulc = _assetBundle.LoadAsset<Sprite>("IconSuprVulc");
            IconThunder = _assetBundle.LoadAsset<Sprite>("IconThunder");
            IconVulcan = _assetBundle.LoadAsset<Sprite>("IconVulcan");
            IconYoyo = _assetBundle.LoadAsset<Sprite>("IconYoyo");



            IconAnxious = _assetBundle.LoadAsset<Sprite>("IconAnxious");
            IconAtk10 = _assetBundle.LoadAsset<Sprite>("IconAtk10");
            IconAtk20 = _assetBundle.LoadAsset<Sprite>("IconAtk20");
            IconAtk30 = _assetBundle.LoadAsset<Sprite>("IconAtk30");
            IconBarr200 = _assetBundle.LoadAsset<Sprite>("IconBarr200");
            IconDrkBomb = _assetBundle.LoadAsset<Sprite>("IconDrkBomb");
            IconDrkCannon = _assetBundle.LoadAsset<Sprite>("IconDrkCannon");
            IconDrkRecov = _assetBundle.LoadAsset<Sprite>("IconDrkRecov");
            IconDrkSword = _assetBundle.LoadAsset<Sprite>("IconDrkSword");
            IconDrkVulcan = _assetBundle.LoadAsset<Sprite>("IconDrkVulcan");
            IconEvil = _assetBundle.LoadAsset<Sprite>("IconEvil");
            IconFullSync = _assetBundle.LoadAsset<Sprite>("IconFullSync");
            IconGutPunch = _assetBundle.LoadAsset<Sprite>("IconGutPunch");
            IconGutPunchShot = _assetBundle.LoadAsset<Sprite>("IconGutPunchShot");
            IconNormal = _assetBundle.LoadAsset<Sprite>("IconNormal");
            IconRage = _assetBundle.LoadAsset<Sprite>("IconRage");
            IconRecov300 = _assetBundle.LoadAsset<Sprite>("IconRecov300");
            IconShockWave = _assetBundle.LoadAsset<Sprite>("IconShockWave");
            IconShotgun = _assetBundle.LoadAsset<Sprite>("IconShotgun");

            IconInvis = _assetBundle.LoadAsset<Sprite>("IconInvis");


            IconBarr500 = _assetBundle.LoadAsset<Sprite>("IconBarr500");
            IconBugFix = _assetBundle.LoadAsset<Sprite>("IconBugFix");
            IconDrkDebuff = _assetBundle.LoadAsset<Sprite>("IconDrkDebuff");
            IconFstGauge = _assetBundle.LoadAsset<Sprite>("IconFstGauge");
            IconGigaCannon = _assetBundle.LoadAsset<Sprite>("IconGigaCannon");
            IconGreatYoyo = _assetBundle.LoadAsset<Sprite>("IconGreatYoyo");
            IconInfiniteVulcan = _assetBundle.LoadAsset<Sprite>("IconInfiniteVulcan");
            IconLifeSword = _assetBundle.LoadAsset<Sprite>("IconLifeSword");
            IconNoData = _assetBundle.LoadAsset<Sprite>("IconNoData");
            IconPassive = _assetBundle.LoadAsset<Sprite>("IconPassive");
            IconSendChip = _assetBundle.LoadAsset<Sprite>("IconSendChip");

            IconReflector = _assetBundle.LoadAsset<Sprite>("IconReflector");

            IconStepSword = _assetBundle.LoadAsset<Sprite>("IconStepSword");

            IconSpreader = _assetBundle.LoadAsset<Sprite>("IconSpreader");





            CreateEffects();

            CreateProjectiles();

            //CreateAllyPrefab();
        }

        //public static void CreateAllyPrefab()
        //{
        //    // Clonar o corpo da torreta (ou Commando se quiser que pareça mais um personagem)
        //    GameObject baseBody = LegacyResourcesAPI.Load<GameObject>("prefabs/characterbodies/EngiTurretBody");
        //    AllyBodyPrefab = PrefabAPI.InstantiateClone(baseBody, "MyAllyBody", true);
        //    //AllyBodyPrefab = _assetBundle.LoadAsset<GameObject>("EXETurret");

        //    // Pega o ModelLocator do prefab
        //    ModelLocator modelLocator = AllyBodyPrefab.GetComponent<ModelLocator>();

        //    // Destrói o modelo antigo
        //    GameObject oldModel = modelLocator.modelTransform.gameObject;
        //    UnityEngine.Object.DestroyImmediate(oldModel);

        //    // Instancia seu novo modelo (deve ser um prefab seu carregado nos assets)
        //    GameObject newModel = UnityEngine.Object.Instantiate(_assetBundle.LoadAsset<GameObject>("EXETurret"), AllyBodyPrefab.transform);

        //    // Atualiza o modelLocator
        //    modelLocator.modelTransform = newModel.transform;
        //    modelLocator.modelBaseTransform = newModel.transform; // ou algum filho específico, se quiser

        //    // Acessa o SkillLocator da torreta
        //    SkillLocator skillLocator = AllyBodyPrefab.GetComponent<SkillLocator>();

        //    // Substitui a habilidade primária por uma que você já tem
        //    skillLocator.primary.skillFamily.variants[0].skillDef = MegamanEXESurvivor.BusterTurretSkillDef;


        //    // Ajustar stats (opcional)
        //    CharacterBody body = AllyBodyPrefab.GetComponent<CharacterBody>();
        //    body.baseMaxHealth = 200f;
        //    body.baseDamage = 15f;
        //    body.baseMoveSpeed = 0f; // parado como uma torreta
        //    body.baseAttackSpeed = 1.5f;
        //    body.isChampion = false;

        //    // Clonar o master da torreta
        //    GameObject baseMaster = LegacyResourcesAPI.Load<GameObject>("prefabs/charactermasters/EngiTurretMaster");
        //    AllyMasterPrefab = PrefabAPI.InstantiateClone(baseMaster, "MyAllyMaster", true);

        //    // Definir que o master usa nosso body
        //    CharacterMaster master = AllyMasterPrefab.GetComponent<CharacterMaster>();
        //    master.bodyPrefab = AllyBodyPrefab;

        //    // Registrar no catálogo
        //    BodyCatalog.getAdditionalEntries += list => list.Add(AllyBodyPrefab);
        //    MasterCatalog.getAdditionalEntries += list => list.Add(AllyMasterPrefab);
        //}

        #region effects
        private static void CreateEffects()
        {
            CreateBombExplosionEffect();

            swordSwingEffect = _assetBundle.LoadEffect("HenrySwordSwingEffect", true);
            swordHitImpactEffect = _assetBundle.LoadEffect("ImpactHenrySlash");
        }

        private static void CreateBombExplosionEffect()
        {
            bombExplosionEffect = _assetBundle.LoadEffect("BombExplosionEffect", "HenryBombExplosion");

            if (!bombExplosionEffect)
                return;

            ShakeEmitter shakeEmitter = bombExplosionEffect.AddComponent<ShakeEmitter>();
            shakeEmitter.amplitudeTimeDecay = true;
            shakeEmitter.duration = 0.5f;
            shakeEmitter.radius = 200f;
            shakeEmitter.scaleShakeRadiusWithLocalScale = false;

            shakeEmitter.wave = new Wave
            {
                amplitude = 1f,
                frequency = 40f,
                cycleOffset = 0f
            };

        }
        #endregion effects

        #region projectiles
        private static void CreateProjectiles()
        {
            CreateBombProjectile();
            CreateMiniBombProjectile();
            CreateThunderProjectile();
            CreateYoyoProjectile();
            CreateShockwaveProjectile();
            CreateShotgunProjectile();
            CreateGutsPnchProjectile();
            CreateEXETurretProjectile();

            Content.AddProjectilePrefab(bombProjectilePrefab);
            Content.AddProjectilePrefab(miniBombProjectilePrefab);
            Content.AddProjectilePrefab(thunderProjectilePrefab);
            Content.AddProjectilePrefab(yoyoProjectilePrefab);
            Content.AddProjectilePrefab(shockwaveProjectilePrefab);
            Content.AddProjectilePrefab(shotgunProjectilePrefab);
            Content.AddProjectilePrefab(gutsPnchProjectilePrefab);
            Content.AddProjectilePrefab(exeTurretProjectilePrefab);
        }

        private static void CreateBombProjectile()
        {
            //highly recommend setting up projectiles in editor, but this is a quick and dirty way to prototype if you want
            bombProjectilePrefab = Asset.CloneProjectilePrefab("CommandoGrenadeProjectile", "HenryBombProjectile");

            //remove their ProjectileImpactExplosion component and start from default values
            UnityEngine.Object.Destroy(bombProjectilePrefab.GetComponent<ProjectileImpactExplosion>());
            ProjectileImpactExplosion bombImpactExplosion = bombProjectilePrefab.AddComponent<ProjectileImpactExplosion>();
            
            bombImpactExplosion.blastRadius = 16f;
            bombImpactExplosion.blastDamageCoefficient = 1f;
            bombImpactExplosion.falloffModel = BlastAttack.FalloffModel.None;
            bombImpactExplosion.destroyOnEnemy = true;
            bombImpactExplosion.lifetime = 12f;
            bombImpactExplosion.impactEffect = bombExplosionEffect;
            bombImpactExplosion.lifetimeExpiredSound = Content.CreateAndAddNetworkSoundEventDef("HenryBombExplosion");
            bombImpactExplosion.timerAfterImpact = true;
            bombImpactExplosion.lifetimeAfterImpact = 0.1f;

            ProjectileController bombController = bombProjectilePrefab.GetComponent<ProjectileController>();

            if (_assetBundle.LoadAsset<GameObject>("HenryBombGhost") != null)
                bombController.ghostPrefab = _assetBundle.CreateProjectileGhostPrefab("HenryBombGhost");
            
            bombController.startSound = "";
        }

        private static void CreateEXETurretProjectile()
        {
            //highly recommend setting up projectiles in editor, but this is a quick and dirty way to prototype if you want
            exeTurretProjectilePrefab = Asset.CloneProjectilePrefab("FMJ", "EXETurretProjectile");

            //remove their ProjectileImpactExplosion component and start from default values
            UnityEngine.Object.Destroy(exeTurretProjectilePrefab.GetComponent<ProjectileImpactExplosion>());
            //ProjectileImpactExplosion bombImpactExplosion = bombProjectilePrefab.AddComponent<ProjectileImpactExplosion>();

            //bombImpactExplosion.blastRadius = 16f;
            //bombImpactExplosion.blastDamageCoefficient = 1f;
            //bombImpactExplosion.falloffModel = BlastAttack.FalloffModel.None;
            //bombImpactExplosion.destroyOnEnemy = true;
            //bombImpactExplosion.lifetime = 12f;
            //bombImpactExplosion.impactEffect = bombExplosionEffect;
            //bombImpactExplosion.lifetimeExpiredSound = Content.CreateAndAddNetworkSoundEventDef("HenryBombExplosion");
            //bombImpactExplosion.timerAfterImpact = true;
            //bombImpactExplosion.lifetimeAfterImpact = 0.1f;

            exeTurretProjectilePrefab.GetComponent<ProjectileSimple>().lifetime = 10f;

            exeTurretProjectilePrefab.AddComponent<EXETurretComponent>();

            ProjectileController EXETurretController = exeTurretProjectilePrefab.GetComponent<ProjectileController>();

            if (_assetBundle.LoadAsset<GameObject>("EXETurret") != null)
                EXETurretController.ghostPrefab = _assetBundle.CreateProjectileGhostPrefab("EXETurret");

            EXETurretController.startSound = "";
        }

        private static void CreateMiniBombProjectile()
        {
            //highly recommend setting up projectiles in editor, but this is a quick and dirty way to prototype if you want
            miniBombProjectilePrefab = Asset.CloneProjectilePrefab("CommandoGrenadeProjectile", "MiniBombProjectile");

            //remove their ProjectileImpactExplosion component and start from default values
            //UnityEngine.Object.Destroy(miniBombProjectilePrefab.GetComponent<ProjectileImpactExplosion>());
            //ProjectileImpactExplosion MiniBombImpactExplosion = miniBombProjectilePrefab.AddComponent<ProjectileImpactExplosion>();

            //MiniBombImpactExplosion.blastRadius = 10f;
            //MiniBombImpactExplosion.blastDamageCoefficient = 1f;
            //MiniBombImpactExplosion.falloffModel = BlastAttack.FalloffModel.None;
            //MiniBombImpactExplosion.destroyOnEnemy = true;
            //MiniBombImpactExplosion.lifetime = 12f;
            ////MiniBombImpactExplosion.impactEffect = bombExplosionEffect;
            ////MiniBombImpactExplosion.lifetimeExpiredSound = Content.CreateAndAddNetworkSoundEventDef("HenryBombExplosion");
            //MiniBombImpactExplosion.timerAfterImpact = true;
            //MiniBombImpactExplosion.lifetimeAfterImpact = 0.1f;

            ProjectileController miniBombController = bombProjectilePrefab.GetComponent<ProjectileController>();

            //if (_assetBundle.LoadAsset<GameObject>("HenryBombGhost") != null)
            //    bombController.ghostPrefab = _assetBundle.CreateProjectileGhostPrefab("HenryBombGhost");

            miniBombController.startSound = "";
        }

        private static void CreateThunderProjectile()
        {
            //highly recommend setting up projectiles in editor, but this is a quick and dirty way to prototype if you want
            thunderProjectilePrefab = Asset.CloneProjectilePrefab("MageLightningboltBasic", "ThunderProjectile");

            //remove their ProjectileImpactExplosion component and start from default values
            //UnityEngine.Object.Destroy(thunderProjectilePrefab.GetComponent<ProjectileImpactExplosion>());
            //ProjectileImpactExplosion ThunderImpactExplosion = thunderProjectilePrefab.AddComponent<ProjectileImpactExplosion>();

            //ThunderImpactExplosion.blastRadius = 1f;
            //ThunderImpactExplosion.blastDamageCoefficient = 1f;
            //ThunderImpactExplosion.falloffModel = BlastAttack.FalloffModel.None;
            //ThunderImpactExplosion.destroyOnEnemy = true;
            //ThunderImpactExplosion.lifetime = 12f;
            //ThunderImpactExplosion.timerAfterImpact = true;
            //ThunderImpactExplosion.lifetimeAfterImpact = 0.1f;

            thunderProjectilePrefab.GetComponent<ProjectileController>().procCoefficient = 1f;
            thunderProjectilePrefab.GetComponent<ProjectileDamage>().damage = 1f;
            thunderProjectilePrefab.GetComponent<ProjectileDamage>().damageType |= DamageType.Shock5s;
            thunderProjectilePrefab.GetComponent<ProjectileDamage>().damageType |= DamageTypeCombo.Generic;

            ProjectileController ThunderController = thunderProjectilePrefab.GetComponent<ProjectileController>();

            //if (_assetBundle.LoadAsset<GameObject>("HenryBombGhost") != null)
            //    bombController.ghostPrefab = _assetBundle.CreateProjectileGhostPrefab("HenryBombGhost");

            ThunderController.startSound = "";
        }

        private static void CreateYoyoProjectile()
        {
            //highly recommend setting up projectiles in editor, but this is a quick and dirty way to prototype if you want
            yoyoProjectilePrefab = Asset.CloneProjectilePrefab("Sawmerang", "YoyoProjectile");

            //remove their ProjectileImpactExplosion component and start from default values
            UnityEngine.Object.Destroy(yoyoProjectilePrefab.GetComponent<ProjectileImpactExplosion>());
            ProjectileImpactExplosion YoyoImpactExplosion = yoyoProjectilePrefab.AddComponent<ProjectileImpactExplosion>();

            YoyoImpactExplosion.blastRadius = 1f;
            YoyoImpactExplosion.blastDamageCoefficient = 1f;
            YoyoImpactExplosion.falloffModel = BlastAttack.FalloffModel.None;
            YoyoImpactExplosion.destroyOnEnemy = true;
            YoyoImpactExplosion.lifetime = 12f;
            YoyoImpactExplosion.timerAfterImpact = true;
            YoyoImpactExplosion.lifetimeAfterImpact = 0.1f;

            yoyoProjectilePrefab.GetComponent<ProjectileController>().procCoefficient = 1f;
            yoyoProjectilePrefab.GetComponent<ProjectileDamage>().damage = 1f;
            yoyoProjectilePrefab.GetComponent<ProjectileDamage>().damageType |= DamageType.BypassArmor;
            yoyoProjectilePrefab.GetComponent<ProjectileDamage>().damageType |= DamageType.BypassBlock;
            yoyoProjectilePrefab.GetComponent<ProjectileDamage>().damageType |= DamageTypeCombo.Generic;

            ProjectileController YoyoController = yoyoProjectilePrefab.GetComponent<ProjectileController>();

            //if (_assetBundle.LoadAsset<GameObject>("HenryBombGhost") != null)
            //    bombController.ghostPrefab = _assetBundle.CreateProjectileGhostPrefab("HenryBombGhost");

            YoyoController.startSound = "";
        }

        private static void CreateShockwaveProjectile()
        {
            //highly recommend setting up projectiles in editor, but this is a quick and dirty way to prototype if you want
            shockwaveProjectilePrefab = Asset.CloneProjectilePrefab("ArchWispGroundCannon", "ShockwaveProjectile");

            //remove their ProjectileImpactExplosion component and start from default values
            UnityEngine.Object.Destroy(shockwaveProjectilePrefab.GetComponent<ProjectileImpactExplosion>());
            ProjectileImpactExplosion ShockwaveImpactExplosion = shockwaveProjectilePrefab.AddComponent<ProjectileImpactExplosion>();

            ShockwaveImpactExplosion.blastRadius = 10f;
            ShockwaveImpactExplosion.blastDamageCoefficient = 1f;
            ShockwaveImpactExplosion.falloffModel = BlastAttack.FalloffModel.None;
            ShockwaveImpactExplosion.destroyOnEnemy = true;
            ShockwaveImpactExplosion.lifetime = 12f;
            ShockwaveImpactExplosion.timerAfterImpact = true;
            ShockwaveImpactExplosion.lifetimeAfterImpact = 0.1f;

            shockwaveProjectilePrefab.GetComponent<ProjectileController>().procCoefficient = 1f;
            shockwaveProjectilePrefab.GetComponent<ProjectileDamage>().damage = 1f;
            shockwaveProjectilePrefab.GetComponent<ProjectileDamage>().damageType |= DamageType.BypassArmor;
            shockwaveProjectilePrefab.GetComponent<ProjectileDamage>().damageType |= DamageType.BypassBlock;
            shockwaveProjectilePrefab.GetComponent<ProjectileDamage>().damageType |= DamageType.Stun1s;
            shockwaveProjectilePrefab.GetComponent<ProjectileDamage>().damageType |= DamageTypeCombo.Generic;

            ProjectileController ShockwaveController = shockwaveProjectilePrefab.GetComponent<ProjectileController>();

            //if (_assetBundle.LoadAsset<GameObject>("HenryBombGhost") != null)
            //    bombController.ghostPrefab = _assetBundle.CreateProjectileGhostPrefab("HenryBombGhost");

            ShockwaveController.startSound = "";
        }

        private static void CreateShotgunProjectile()
        {
            //highly recommend setting up projectiles in editor, but this is a quick and dirty way to prototype if you want
            shotgunProjectilePrefab = Asset.CloneProjectilePrefab("FMJ", "ShotgunProjectile");

            //remove their ProjectileImpactExplosion component and start from default values
            UnityEngine.Object.Destroy(shotgunProjectilePrefab.GetComponent<ProjectileImpactExplosion>());
            ProjectileImpactExplosion ShotgunImpactExplosion = shotgunProjectilePrefab.AddComponent<ProjectileImpactExplosion>();

            ShotgunImpactExplosion.blastRadius = 10f;
            ShotgunImpactExplosion.blastDamageCoefficient = 1f;
            ShotgunImpactExplosion.falloffModel = BlastAttack.FalloffModel.None;
            ShotgunImpactExplosion.destroyOnEnemy = true;
            ShotgunImpactExplosion.lifetime = 12f;
            ShotgunImpactExplosion.timerAfterImpact = true;
            ShotgunImpactExplosion.lifetimeAfterImpact = 0.1f;

            shotgunProjectilePrefab.GetComponent<ProjectileController>().procCoefficient = 1f;
            shotgunProjectilePrefab.GetComponent<ProjectileDamage>().damage = 1f;
            shotgunProjectilePrefab.GetComponent<ProjectileDamage>().damageType |= DamageType.BypassArmor;
            shotgunProjectilePrefab.GetComponent<ProjectileDamage>().damageType |= DamageType.BypassBlock;
            shotgunProjectilePrefab.GetComponent<ProjectileDamage>().damageType |= DamageTypeCombo.Generic;

            ProjectileController ShotgunController = shotgunProjectilePrefab.GetComponent<ProjectileController>();

            //if (_assetBundle.LoadAsset<GameObject>("HenryBombGhost") != null)
            //    bombController.ghostPrefab = _assetBundle.CreateProjectileGhostPrefab("HenryBombGhost");

            ShotgunController.startSound = "";
        }

        private static void CreateGutsPnchProjectile()
        {
            //highly recommend setting up projectiles in editor, but this is a quick and dirty way to prototype if you want
            gutsPnchProjectilePrefab = Asset.CloneProjectilePrefab("FMJ", "GutsPnchProjectile");

            //remove their ProjectileImpactExplosion component and start from default values
            UnityEngine.Object.Destroy(gutsPnchProjectilePrefab.GetComponent<ProjectileImpactExplosion>());
            ProjectileImpactExplosion gutsPnchImpactExplosion = gutsPnchProjectilePrefab.AddComponent<ProjectileImpactExplosion>();

            gutsPnchImpactExplosion.blastRadius = 10f;
            gutsPnchImpactExplosion.blastDamageCoefficient = 1f;
            gutsPnchImpactExplosion.falloffModel = BlastAttack.FalloffModel.None;
            gutsPnchImpactExplosion.destroyOnEnemy = true;
            gutsPnchImpactExplosion.lifetime = 12f;
            gutsPnchImpactExplosion.timerAfterImpact = true;
            gutsPnchImpactExplosion.lifetimeAfterImpact = 0.1f;

            gutsPnchProjectilePrefab.GetComponent<ProjectileController>().procCoefficient = 1f;
            gutsPnchProjectilePrefab.GetComponent<ProjectileDamage>().damage = 1f;
            gutsPnchProjectilePrefab.GetComponent<ProjectileDamage>().damageType |= DamageType.BypassArmor;
            gutsPnchProjectilePrefab.GetComponent<ProjectileDamage>().damageType |= DamageType.BypassBlock;
            gutsPnchProjectilePrefab.GetComponent<ProjectileDamage>().damageType |= DamageType.Stun1s;
            gutsPnchProjectilePrefab.GetComponent<ProjectileDamage>().damageType |= DamageTypeCombo.Generic;

            ProjectileController GutsPnchController = gutsPnchProjectilePrefab.GetComponent<ProjectileController>();

            if (_assetBundle.LoadAsset<GameObject>("GutsShot") != null)
                GutsPnchController.ghostPrefab = _assetBundle.CreateProjectileGhostPrefab("GutsShot");

            GutsPnchController.startSound = "";
        }

        #endregion projectiles
    }
}
