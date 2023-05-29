using System.Reflection;
using R2API;
using UnityEngine;
using UnityEngine.Networking;
using RoR2;
using System.IO;
using System.Collections.Generic;
using RoR2.UI;
using System;

namespace MegamanEXEMod.Modules
{
    internal static class Assets
    {
        #region henry's stuff
        // particle effects
        internal static GameObject swordSwingEffect;
        internal static GameObject swordHitImpactEffect;

        internal static GameObject bombExplosionEffect;

        // networked hit sounds
        internal static NetworkSoundEventDef swordHitSoundEvent;
        #endregion

        public static Texture TexMegamanExe;

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
        internal static GameObject VfxChargeeffect1W;
        internal static GameObject VfxChargeeffect2C;
        internal static GameObject VfxSwordFire;
        internal static GameObject VfxSwordIce;
        internal static GameObject VfxSwordEletric;
        internal static GameObject VfxRecov;
        internal static GameObject VfxFullSync;
        internal static GameObject VfxRage;
        internal static GameObject VfxEvil;
        internal static GameObject VfxDeleted;

        public static Material MatCyberSwordDefault;
        public static Material MatCyberSwordRed;
        public static Material MatCyberSwordDark;
        public static Material MatInvis;


        // the assetbundle to load assets from
        internal static AssetBundle mainAssetBundle;

        // CHANGE THIS
        private const string assetbundleName = "megamanexebundle";
        //change this to your project's name if/when you've renamed it
        private const string csProjName = "MegamanEXEMod";
        
        internal static void Initialize()
        {
            if (assetbundleName == "myassetbundle")
            {
                Log.Error("AssetBundle name hasn't been changed. not loading any assets to avoid conflicts");
                return;
            }

            LoadAssetBundle();
            LoadSoundbank();
            PopulateAssets();
        }

        internal static void LoadAssetBundle()
        {
            try
            {
                if (mainAssetBundle == null)
                {
                    using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{csProjName}.{assetbundleName}"))
                    {
                        mainAssetBundle = AssetBundle.LoadFromStream(assetStream);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Failed to load assetbundle. Make sure your assetbundle name is setup correctly\n" + e);
                return;
            }
        }

        internal static void LoadSoundbank()
        {
            using (Stream manifestResourceStream2 = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{csProjName}.MegamanEXESB.bnk"))
            {
                byte[] array = new byte[manifestResourceStream2.Length];
                manifestResourceStream2.Read(array, 0, array.Length);
                SoundAPI.SoundBanks.Add(array);
            }
        }

        internal static void PopulateAssets()
        {
            if (!mainAssetBundle)
            {
                Log.Error("There is no AssetBundle to load assets from.");
                return;
            }

            // feel free to delete everything in here and load in your own assets instead
            // it should work fine even if left as is- even if the assets aren't in the bundle
            
            swordHitSoundEvent = CreateNetworkSoundEventDef("HenrySwordHit");

            bombExplosionEffect = LoadEffect("BombExplosionEffect", "HenryBombExplosion");

            if (bombExplosionEffect)
            {
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

            swordSwingEffect = Assets.LoadEffect("HenrySwordSwingEffect", true);
            swordHitImpactEffect = Assets.LoadEffect("ImpactHenrySlash");


            VfxChargeeffect1C = Assets.LoadEffect("V2Charge1C", true);
            VfxChargeeffect2C = Assets.LoadEffect("V2Charge2C", true);
            VfxChargeeffect1W = Assets.LoadEffect("V2Charge1W", true);
            VfxSwordFire = Assets.LoadEffect("VFXFire1", true);
            VfxSwordIce = Assets.LoadEffect("VFXIce1", true);
            VfxSwordEletric = Assets.LoadEffect("VFXEletric", true);
            VfxRecov = Assets.LoadEffect("VFXRecov", true);
            VfxFullSync = Assets.LoadEffect("VFXFullSync", true);
            VfxRage = Assets.LoadEffect("VFXRage", true);
            VfxEvil = Assets.LoadEffect("VFXEnterEvil", true);

            VfxDeleted = Assets.LoadEffect("DeathEffect", true);

            MatCyberSwordDefault = mainAssetBundle.LoadAsset<Material>("matMMBNSword");
            MatCyberSwordRed = mainAssetBundle.LoadAsset<Material>("matMMBNSwordRed");
            MatCyberSwordDark = mainAssetBundle.LoadAsset<Material>("matMMBNSwordDark");
            MatInvis = mainAssetBundle.LoadAsset<Material>("matMMBNInv");



            TexMegamanExe = mainAssetBundle.LoadAsset<Texture>("MegmanExeIcon");

            IconAirShot = mainAssetBundle.LoadAsset<Sprite>("IconAirShot");
            IconAquaSwrd = mainAssetBundle.LoadAsset<Sprite>("IconAquaSwrd");
            IconBarr100 = mainAssetBundle.LoadAsset<Sprite>("IconBarr100");
            IconBusterEXE = mainAssetBundle.LoadAsset<Sprite>("IconBusterEXE");
            IconCannon = mainAssetBundle.LoadAsset<Sprite>("IconCannon");
            IconCyberSword = mainAssetBundle.LoadAsset<Sprite>("IconCyberSword");
            IconElecSwrd = mainAssetBundle.LoadAsset<Sprite>("IconElecSwrd");
            IconFireSwrd = mainAssetBundle.LoadAsset<Sprite>("IconFireSwrd");
            IconHiCannon = mainAssetBundle.LoadAsset<Sprite>("IconHiCannon");
            IconMCannon = mainAssetBundle.LoadAsset<Sprite>("IconMCannon");
            IconMiniBomb = mainAssetBundle.LoadAsset<Sprite>("IconMiniBomb");
            IconMuramasa = mainAssetBundle.LoadAsset<Sprite>("IconMuramasa");
            IconRecov50 = mainAssetBundle.LoadAsset<Sprite>("IconRecov50");
            IconSuprVulc = mainAssetBundle.LoadAsset<Sprite>("IconSuprVulc");
            IconThunder = mainAssetBundle.LoadAsset<Sprite>("IconThunder");
            IconVulcan = mainAssetBundle.LoadAsset<Sprite>("IconVulcan");
            IconYoyo = mainAssetBundle.LoadAsset<Sprite>("IconYoyo");



            IconAnxious = mainAssetBundle.LoadAsset<Sprite>("IconAnxious");
            IconAtk10 = mainAssetBundle.LoadAsset<Sprite>("IconAtk10");
            IconAtk30 = mainAssetBundle.LoadAsset<Sprite>("IconAtk30");
            IconBarr200 = mainAssetBundle.LoadAsset<Sprite>("IconBarr200");
            IconDrkBomb = mainAssetBundle.LoadAsset<Sprite>("IconDrkBomb");
            IconDrkCannon = mainAssetBundle.LoadAsset<Sprite>("IconDrkCannon");
            IconDrkRecov = mainAssetBundle.LoadAsset<Sprite>("IconDrkRecov");
            IconDrkSword = mainAssetBundle.LoadAsset<Sprite>("IconDrkSword");
            IconDrkVulcan = mainAssetBundle.LoadAsset<Sprite>("IconDrkVulcan");
            IconEvil = mainAssetBundle.LoadAsset<Sprite>("IconEvil");
            IconFullSync = mainAssetBundle.LoadAsset<Sprite>("IconFullSync");
            IconGutPunch = mainAssetBundle.LoadAsset<Sprite>("IconGutPunch");
            IconNormal = mainAssetBundle.LoadAsset<Sprite>("IconNormal");
            IconRage = mainAssetBundle.LoadAsset<Sprite>("IconRage");
            IconRecov300 = mainAssetBundle.LoadAsset<Sprite>("IconRecov300");
            IconShockWave = mainAssetBundle.LoadAsset<Sprite>("IconShockWave");
            IconShotgun = mainAssetBundle.LoadAsset<Sprite>("IconShotgun");

            IconInvis = mainAssetBundle.LoadAsset<Sprite>("IconInvis");


            IconBarr500 = mainAssetBundle.LoadAsset<Sprite>("IconBarr500");
            IconBugFix = mainAssetBundle.LoadAsset<Sprite>("IconBugFix");
            IconDrkDebuff = mainAssetBundle.LoadAsset<Sprite>("IconDrkDebuff");
            IconFstGauge = mainAssetBundle.LoadAsset<Sprite>("IconFstGauge");
            IconGigaCannon = mainAssetBundle.LoadAsset<Sprite>("IconGigaCannon");
            IconGreatYoyo = mainAssetBundle.LoadAsset<Sprite>("IconGreatYoyo");
            IconInfiniteVulcan = mainAssetBundle.LoadAsset<Sprite>("IconInfiniteVulcan");
            IconLifeSword = mainAssetBundle.LoadAsset<Sprite>("IconLifeSword");
            IconNoData = mainAssetBundle.LoadAsset<Sprite>("IconNoData");
            IconPassive = mainAssetBundle.LoadAsset<Sprite>("IconPassive");
            IconSendChip = mainAssetBundle.LoadAsset<Sprite>("IconSendChip");

            IconReflector = mainAssetBundle.LoadAsset<Sprite>("IconReflector");

            IconStepSword = mainAssetBundle.LoadAsset<Sprite>("IconStepSword");
        }

        private static GameObject CreateTracer(string originalTracerName, string newTracerName)
        {
            if (RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/Tracers/" + originalTracerName) == null) return null;

            GameObject newTracer = PrefabAPI.InstantiateClone(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/Tracers/" + originalTracerName), newTracerName, true);

            if (!newTracer.GetComponent<EffectComponent>()) newTracer.AddComponent<EffectComponent>();
            if (!newTracer.GetComponent<VFXAttributes>()) newTracer.AddComponent<VFXAttributes>();
            if (!newTracer.GetComponent<NetworkIdentity>()) newTracer.AddComponent<NetworkIdentity>();

            newTracer.GetComponent<Tracer>().speed = 250f;
            newTracer.GetComponent<Tracer>().length = 50f;

            AddNewEffectDef(newTracer);

            return newTracer;
        }

        internal static NetworkSoundEventDef CreateNetworkSoundEventDef(string eventName)
        {
            NetworkSoundEventDef networkSoundEventDef = ScriptableObject.CreateInstance<NetworkSoundEventDef>();
            networkSoundEventDef.akId = AkSoundEngine.GetIDFromString(eventName);
            networkSoundEventDef.eventName = eventName;

            Modules.Content.AddNetworkSoundEventDef(networkSoundEventDef);

            return networkSoundEventDef;
        }

        internal static void ConvertAllRenderersToHopooShader(GameObject objectToConvert)
        {
            if (!objectToConvert) return;

            foreach (Renderer i in objectToConvert.GetComponentsInChildren<Renderer>())
            {
                i?.material?.SetHopooMaterial();
            }
        }

        internal static CharacterModel.RendererInfo[] SetupRendererInfos(GameObject obj)
        {
            MeshRenderer[] meshes = obj.GetComponentsInChildren<MeshRenderer>();
            CharacterModel.RendererInfo[] rendererInfos = new CharacterModel.RendererInfo[meshes.Length];

            for (int i = 0; i < meshes.Length; i++)
            {
                rendererInfos[i] = new CharacterModel.RendererInfo
                {
                    defaultMaterial = meshes[i].material,
                    renderer = meshes[i],
                    defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                    ignoreOverlays = false
                };
            }

            return rendererInfos;
        }


        public static GameObject LoadSurvivorModel(string modelName) {
            GameObject model = mainAssetBundle.LoadAsset<GameObject>(modelName);
            if (model == null) {
                Log.Error("Trying to load a null model- check to see if the BodyName in your code matches the prefab name of the object in Unity\nFor Example, if your prefab in unity is 'mdlHenry', then your BodyName must be 'Henry'");
                return null;
            }

            return PrefabAPI.InstantiateClone(model, model.name, false);
        }

        internal static GameObject LoadCrosshair(string crosshairName)
        {
            if (RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Crosshair/" + crosshairName + "Crosshair") == null) return RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Crosshair/StandardCrosshair");
            return RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Crosshair/" + crosshairName + "Crosshair");
        }

        private static GameObject LoadEffect(string resourceName)
        {
            return LoadEffect(resourceName, "", false);
        }

        private static GameObject LoadEffect(string resourceName, string soundName)
        {
            return LoadEffect(resourceName, soundName, false);
        }

        private static GameObject LoadEffect(string resourceName, bool parentToTransform)
        {
            return LoadEffect(resourceName, "", parentToTransform);
        }

        private static GameObject LoadEffect(string resourceName, string soundName, bool parentToTransform)
        {
            GameObject newEffect = mainAssetBundle.LoadAsset<GameObject>(resourceName);

            if (!newEffect)
            {
                Log.Error("Failed to load effect: " + resourceName + " because it does not exist in the AssetBundle");
                return null;
            }

            newEffect.AddComponent<DestroyOnTimer>().duration = 12;
            newEffect.AddComponent<NetworkIdentity>();
            newEffect.AddComponent<VFXAttributes>().vfxPriority = VFXAttributes.VFXPriority.Always;
            var effect = newEffect.AddComponent<EffectComponent>();
            effect.applyScale = false;
            effect.effectIndex = EffectIndex.Invalid;
            effect.parentToReferencedTransform = parentToTransform;
            effect.positionAtReferencedTransform = true;
            effect.soundName = soundName;

            AddNewEffectDef(newEffect, soundName);

            return newEffect;
        }

        private static void AddNewEffectDef(GameObject effectPrefab)
        {
            AddNewEffectDef(effectPrefab, "");
        }

        private static void AddNewEffectDef(GameObject effectPrefab, string soundName)
        {
            EffectDef newEffectDef = new EffectDef();
            newEffectDef.prefab = effectPrefab;
            newEffectDef.prefabEffectComponent = effectPrefab.GetComponent<EffectComponent>();
            newEffectDef.prefabName = effectPrefab.name;
            newEffectDef.prefabVfxAttributes = effectPrefab.GetComponent<VFXAttributes>();
            newEffectDef.spawnSoundEventName = soundName;

            Modules.Content.AddEffectDef(newEffectDef);
        }
    }
}