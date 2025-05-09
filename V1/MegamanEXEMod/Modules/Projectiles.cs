using R2API;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace MegamanEXEMod.Modules
{

    //Prefabs/Projectiles/MinorConstructProjectile - talvez usar no thunder

    internal static class Projectiles
    {
        internal static GameObject bombPrefab;
        internal static GameObject MiniBombProjectile;
        internal static GameObject ThunderProjectile;
        internal static GameObject YoyoProjectile;
        internal static GameObject ShokWaveProjectile;
        internal static GameObject ShotGunProjectile;

        internal static void RegisterProjectiles()
        {
            CreateBomb();
            CreateMiniBombProjectile();
            CreateThunderProjectile();
            CreateYoyoProjectile();
            CreateShokWaveProjectile();
            CreateShotGunProjectile();

            AddProjectile(bombPrefab);
            AddProjectile(MiniBombProjectile);
            AddProjectile(ThunderProjectile);
            AddProjectile(YoyoProjectile);
            AddProjectile(ShokWaveProjectile);
            AddProjectile(ShotGunProjectile);
        }

        internal static void AddProjectile(GameObject projectileToAdd)
        {
            Modules.Content.AddProjectilePrefab(projectileToAdd);
        }

        private static void CreateBomb()
        {
            bombPrefab = CloneProjectilePrefab("CommandoGrenadeProjectile", "HenryBombProjectile");

            ProjectileImpactExplosion bombImpactExplosion = bombPrefab.GetComponent<ProjectileImpactExplosion>();
            InitializeImpactExplosion(bombImpactExplosion);

            bombImpactExplosion.blastRadius = 16f;
            bombImpactExplosion.destroyOnEnemy = true;
            bombImpactExplosion.lifetime = 12f;
            bombImpactExplosion.impactEffect = Modules.Assets.bombExplosionEffect;
            //bombImpactExplosion.lifetimeExpiredSound = Modules.Assets.CreateNetworkSoundEventDef("HenryBombExplosion");
            bombImpactExplosion.timerAfterImpact = true;
            bombImpactExplosion.lifetimeAfterImpact = 0.1f;

            ProjectileController bombController = bombPrefab.GetComponent<ProjectileController>();
            if (Modules.Assets.mainAssetBundle.LoadAsset<GameObject>("HenryBombGhost") != null) bombController.ghostPrefab = CreateGhostPrefab("HenryBombGhost");
            bombController.startSound = "";
        }

        private static void CreateMiniBombProjectile()
        {

            MiniBombProjectile = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("prefabs/projectiles/CommandoGrenadeProjectile"), "Prefabs/Projectiles/BombProjectile", true, "C:\\Users\\test\\Documents\\ror2mods\\MegamanXVile\\MegamanXVile\\MegamanXVile\\MegamanXVile.cs", "RegisterCharacter", 155);

            // just setting the numbers to 1 as the entitystate will take care of those
            MiniBombProjectile.GetComponent<ProjectileController>().procCoefficient = 1f;
            MiniBombProjectile.GetComponent<ProjectileDamage>().damage = 1f;
            MiniBombProjectile.GetComponent<ProjectileDamage>().damageType = DamageType.Generic;

            // register it for networking
            if (MiniBombProjectile) PrefabAPI.RegisterNetworkPrefab(MiniBombProjectile);

            ProjectileController MBController = MiniBombProjectile.GetComponent<ProjectileController>();
            if (Modules.Assets.mainAssetBundle.LoadAsset<GameObject>("MBGhost") != null) MBController.ghostPrefab = CreateGhostPrefab("MBGhost");
            MBController.startSound = "";
        }

        private static void CreateThunderProjectile()
        {

            ThunderProjectile = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/MageLightningboltBasic"), "Prefabs/Projectiles/ThunderProjectile", true, "C:\\Users\\test\\Documents\\ror2mods\\MegamanXVile\\MegamanXVile\\MegamanXVile\\MegamanXVile.cs", "RegisterCharacter", 155);

            // just setting the numbers to 1 as the entitystate will take care of those
            ThunderProjectile.GetComponent<ProjectileController>().procCoefficient = 1f;
            ThunderProjectile.GetComponent<ProjectileDamage>().damage = 1f;
            ThunderProjectile.GetComponent<ProjectileDamage>().damageType = DamageType.Shock5s;

            // register it for networking
            if (ThunderProjectile) PrefabAPI.RegisterNetworkPrefab(ThunderProjectile);

            ProjectileController ThController = ThunderProjectile.GetComponent<ProjectileController>();
            if (Modules.Assets.mainAssetBundle.LoadAsset<GameObject>("ThGhost") != null) ThController.ghostPrefab = CreateGhostPrefab("ThGhost");
            ThController.startSound = "";
        }

        private static void CreateYoyoProjectile()
        {

            YoyoProjectile = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/Sawmerang"), "Prefabs/Projectiles/YoyoProjectile", true, "C:\\Users\\test\\Documents\\ror2mods\\MegamanXVile\\MegamanXVile\\MegamanXVile\\MegamanXVile.cs", "RegisterCharacter", 155);

            // just setting the numbers to 1 as the entitystate will take care of those
            YoyoProjectile.GetComponent<ProjectileController>().procCoefficient = 1f;
            YoyoProjectile.GetComponent<ProjectileDamage>().damage = 1f;
            YoyoProjectile.GetComponent<ProjectileDamage>().damageType = DamageType.BypassArmor;

            // register it for networking
            if (YoyoProjectile) PrefabAPI.RegisterNetworkPrefab(YoyoProjectile);

            ProjectileController YoyoController = YoyoProjectile.GetComponent<ProjectileController>();
            if (Modules.Assets.mainAssetBundle.LoadAsset<GameObject>("YoyoGhost") != null) YoyoController.ghostPrefab = CreateGhostPrefab("YoyoGhost");
            YoyoController.startSound = "";
        }

        
        private static void CreateShokWaveProjectile()
        {

            ShokWaveProjectile = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/ArchWispGroundCannon"), "Prefabs/Projectiles/ShokWaveProjectile", true, "C:\\Users\\test\\Documents\\ror2mods\\MegamanXVile\\MegamanXVile\\MegamanXVile\\MegamanXVile.cs", "RegisterCharacter", 155);

            // just setting the numbers to 1 as the entitystate will take care of those
            ShokWaveProjectile.GetComponent<ProjectileController>().procCoefficient = 1f;
            ShokWaveProjectile.GetComponent<ProjectileDamage>().damage = 1f;
            ShokWaveProjectile.GetComponent<ProjectileDamage>().damageType = DamageType.Generic;

            // register it for networking
            if (ShokWaveProjectile) PrefabAPI.RegisterNetworkPrefab(ShokWaveProjectile);

            ProjectileController SWController = ShokWaveProjectile.GetComponent<ProjectileController>();
            if (Modules.Assets.mainAssetBundle.LoadAsset<GameObject>("SWGhost") != null) SWController.ghostPrefab = CreateGhostPrefab("SWGhost");
            SWController.startSound = "";
        }
        

        private static void CreateShotGunProjectile()
        {

            ShotGunProjectile = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/FMJ"), "Prefabs/Projectiles/ShotGunProjectile", true, "C:\\Users\\test\\Documents\\ror2mods\\MegamanXVile\\MegamanXVile\\MegamanXVile\\MegamanXVile.cs", "RegisterCharacter", 155);

            // just setting the numbers to 1 as the entitystate will take care of those
            ShotGunProjectile.GetComponent<ProjectileController>().procCoefficient = 1f;
            ShotGunProjectile.GetComponent<ProjectileDamage>().damage = 1f;
            ShotGunProjectile.GetComponent<ProjectileDamage>().damageType = DamageType.Generic;

            // register it for networking
            if (ShotGunProjectile) PrefabAPI.RegisterNetworkPrefab(ShotGunProjectile);

            ProjectileController SGController = ShotGunProjectile.GetComponent<ProjectileController>();
            if (Modules.Assets.mainAssetBundle.LoadAsset<GameObject>("SGGhost") != null) SGController.ghostPrefab = CreateGhostPrefab("SGGhost");
            SGController.startSound = "";
        }

        private static void InitializeImpactExplosion(ProjectileImpactExplosion projectileImpactExplosion)
        {
            projectileImpactExplosion.blastDamageCoefficient = 1f;
            projectileImpactExplosion.blastProcCoefficient = 1f;
            projectileImpactExplosion.blastRadius = 1f;
            projectileImpactExplosion.bonusBlastForce = Vector3.zero;
            projectileImpactExplosion.childrenCount = 0;
            projectileImpactExplosion.childrenDamageCoefficient = 0f;
            projectileImpactExplosion.childrenProjectilePrefab = null;
            projectileImpactExplosion.destroyOnEnemy = false;
            projectileImpactExplosion.destroyOnWorld = false;
            projectileImpactExplosion.falloffModel = RoR2.BlastAttack.FalloffModel.None;
            projectileImpactExplosion.fireChildren = false;
            projectileImpactExplosion.impactEffect = null;
            projectileImpactExplosion.lifetime = 0f;
            projectileImpactExplosion.lifetimeAfterImpact = 0f;
            projectileImpactExplosion.lifetimeRandomOffset = 0f;
            projectileImpactExplosion.offsetForLifetimeExpiredSound = 0f;
            projectileImpactExplosion.timerAfterImpact = false;

            projectileImpactExplosion.GetComponent<ProjectileDamage>().damageType = DamageType.Generic;
        }

        private static GameObject CreateGhostPrefab(string ghostName)
        {
            GameObject ghostPrefab = Modules.Assets.mainAssetBundle.LoadAsset<GameObject>(ghostName);
            if (!ghostPrefab.GetComponent<NetworkIdentity>()) ghostPrefab.AddComponent<NetworkIdentity>();
            if (!ghostPrefab.GetComponent<ProjectileGhostController>()) ghostPrefab.AddComponent<ProjectileGhostController>();

            Modules.Assets.ConvertAllRenderersToHopooShader(ghostPrefab);

            return ghostPrefab;
        }

        private static GameObject CloneProjectilePrefab(string prefabName, string newPrefabName)
        {
            GameObject newPrefab = PrefabAPI.InstantiateClone(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/" + prefabName), newPrefabName);
            return newPrefab;
        }
    }
}