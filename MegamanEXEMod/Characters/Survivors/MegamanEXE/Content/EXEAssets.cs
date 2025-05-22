using RoR2;
using UnityEngine;
using MegamanEXEMod.Modules;
using System;
using RoR2.Projectile;

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

        internal static GameObject BlueSwordSwingVFX;
        internal static GameObject CyanSwordSwingVFX;
        internal static GameObject RedSwordSwingVFX;
        internal static GameObject PinkSwordSwingVFX;
        internal static GameObject PurpleSwordSwingVFX;
        internal static GameObject YellowSwordSwingVFX;


        private static AssetBundle _assetBundle;

        public static void Init(AssetBundle assetBundle)
        {

            _assetBundle = assetBundle;

            swordHitSoundEvent = Content.CreateAndAddNetworkSoundEventDef("HenrySwordHit");

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





            CreateEffects();

            CreateProjectiles();
        }

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
            Content.AddProjectilePrefab(bombProjectilePrefab);
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
        #endregion projectiles
    }
}
