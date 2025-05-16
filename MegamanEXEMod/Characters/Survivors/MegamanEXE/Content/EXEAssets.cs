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


        public static Material EXESwordMat;
        public static Material ProtoSwordMat;
        public static Material RollSwordMat;
        public static Material BassSwordMat;

        public static Material ProtoBusterMat;
        public static Material RollBusterMat;


        private static AssetBundle _assetBundle;

        public static void Init(AssetBundle assetBundle)
        {

            _assetBundle = assetBundle;

            swordHitSoundEvent = Content.CreateAndAddNetworkSoundEventDef("HenrySwordHit");

            EXEMat = _assetBundle.LoadAsset<Material>("matMMEXE");
            ProtoMat = _assetBundle.LoadAsset<Material>("matProtoEXE");
            RollMat = _assetBundle.LoadAsset<Material>("matROLLEXE");
            BassMat = _assetBundle.LoadAsset<Material>("matBassEXE");
            EXESwordMat = _assetBundle.LoadAsset<Material>("matEXESword");
            ProtoSwordMat = _assetBundle.LoadAsset<Material>("matProtoSword");
            RollSwordMat = _assetBundle.LoadAsset<Material>("matROLLSword");
            BassSwordMat = _assetBundle.LoadAsset<Material>("matBASSSword");
            ProtoBusterMat = _assetBundle.LoadAsset<Material>("matProtoBuster");
            RollBusterMat = _assetBundle.LoadAsset<Material>("matRBuster");

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
