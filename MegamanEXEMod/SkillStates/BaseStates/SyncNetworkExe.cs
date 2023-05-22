using EntityStates;
using MegamanEXEMod.Modules;
using MegamanEXEMod.Modules.Survivors;
using RoR2;
using RoR2.Audio;
using RoR2.Projectile;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace MegamanEXEMod.SkillStates.BaseStates
{
    public class SyncNetworkExe : GenericCharacterMain
    {

        private Animator animator;

        public static string MemoryCode = "";
        public static string MemoryCodeCheck = "";

        public static int EmotionValue = 25;
        public static float EvilEmotionValue = 0f;

        public static bool CanDrkDrain = true;
        private float DrkDrainTimer = 0f;

        private float EvilTimer = 0f;

        public static float RandBugDebuf = 0;

        private bool CanSwitchEmotion = true;

        private float RedHpTimer = 0f;

        private float FullSyncTimer = 0f;

        private float RageTimer = 0f;

        public static float DamageReceived = 0f;

        private string muzzleString;
        public float RDuration = 0.5f;

        public static bool Hurt = false;


        private Transform modelTransform;
        private CharacterModel characterModel;
        private HurtBoxGroup hurtboxGroup;



        public override void OnEnter()
        {
            base.OnEnter();

            this.muzzleString = "BusterMZ";

            modelTransform = GetModelTransform();
            if ((bool)modelTransform)
            {
                animator = modelTransform.GetComponent<Animator>();
                characterModel = modelTransform.GetComponent<CharacterModel>();
                hurtboxGroup = modelTransform.GetComponent<HurtBoxGroup>();
            }


        }
        public override void OnExit()
        {
            base.OnExit();
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            AdvanceProgram();

            ReflectorProjectile();

            //Debug.Log("EvilMotionValue:" + EvilEmotionValue);

            if (EvilTimer >= 18f && EvilEmotionValue > 0)
            {

                if (EvilEmotionValue > 0)
                    EvilEmotionValue--;

                Debug.Log("EvilEmotion Value:" + EvilEmotionValue);

                SyncNetworkExe.DrkBugChanger();

                EvilTimer = 0;

            }
            else
                EvilTimer += Time.fixedDeltaTime;

            //EMOTION BUFFS
            if (EvilEmotionValue >= 10 && CanSwitchEmotion)
            {

                if (NetworkServer.active)
                {
                    base.characterBody.RemoveBuff(Modules.Buffs.FullSyncBuff);
                    base.characterBody.RemoveBuff(Modules.Buffs.NormalBuff);
                    base.characterBody.RemoveBuff(Modules.Buffs.AnxiousBuff);

                    base.characterBody.AddBuff(Modules.Buffs.EvilBuff);
                }

            }
            else
            {


                if (EmotionValue >= 40 && CanSwitchEmotion && !base.characterBody.HasBuff(Modules.Buffs.FullSyncBuff))
                {

                    if (NetworkServer.active)
                    {
                        base.characterBody.RemoveBuff(Modules.Buffs.NormalBuff);
                        base.characterBody.RemoveBuff(Modules.Buffs.AnxiousBuff);

                        base.characterBody.AddBuff(Modules.Buffs.FullSyncBuff);
                    }

                }


                if (EmotionValue <= 39 && EmotionValue >= 15 && CanSwitchEmotion && !base.characterBody.HasBuff(Modules.Buffs.NormalBuff))
                {

                    if (NetworkServer.active)
                    {
                        base.characterBody.RemoveBuff(Modules.Buffs.FullSyncBuff);
                        base.characterBody.RemoveBuff(Modules.Buffs.AnxiousBuff);

                        base.characterBody.AddBuff(Modules.Buffs.NormalBuff);
                    }

                }


                if (EmotionValue <= 14 && CanSwitchEmotion && !base.characterBody.HasBuff(Modules.Buffs.AnxiousBuff))
                {

                    if (NetworkServer.active)
                    {
                        base.characterBody.RemoveBuff(Modules.Buffs.FullSyncBuff);
                        base.characterBody.RemoveBuff(Modules.Buffs.NormalBuff);

                        base.characterBody.AddBuff(Modules.Buffs.AnxiousBuff);
                    }

                }




            }


            if (base.characterBody.HasBuff(Modules.Buffs.EvilBuff))
            {

                if (CanSwitchEmotion)
                {

                    EffectManager.SimpleMuzzleFlash(Modules.Assets.VfxEvil, base.gameObject, "BaseMZ", true);

                    characterModel.baseRendererInfos[0].defaultMaterial = Modules.Materials.CreateHopooMaterial("matMMBNDRK");
                    characterModel.baseRendererInfos[1].defaultMaterial = Modules.Materials.CreateHopooMaterial("matMMBNDRK");
                    characterModel.baseRendererInfos[2].defaultMaterial = Modules.Materials.CreateHopooMaterial("matMMBNDRK");
                    characterModel.baseRendererInfos[3].defaultMaterial = Modules.Materials.CreateHopooMaterial("matMMBNDRK");
                    characterModel.baseRendererInfos[4].defaultMaterial = Modules.Materials.CreateHopooMaterial("matMMBNDRK");

                    CanSwitchEmotion = false;
                }
                else
                {

                    if(EvilEmotionValue <= 0)
                    {

                        characterModel.baseRendererInfos[0].defaultMaterial = Modules.Materials.CreateHopooMaterial("matMMBN");
                        characterModel.baseRendererInfos[1].defaultMaterial = Modules.Materials.CreateHopooMaterial("matMMBN");
                        characterModel.baseRendererInfos[2].defaultMaterial = Modules.Materials.CreateHopooMaterial("matMMBN");
                        characterModel.baseRendererInfos[3].defaultMaterial = Modules.Materials.CreateHopooMaterial("matMMBN");
                        characterModel.baseRendererInfos[4].defaultMaterial = Modules.Materials.CreateHopooMaterial("matMMBN");


                        EmotionValue = 1;

                        base.characterBody.RemoveBuff(Modules.Buffs.FullSyncBuff);
                        base.characterBody.RemoveBuff(Modules.Buffs.NormalBuff);
                        base.characterBody.RemoveBuff(Modules.Buffs.EvilBuff);

                        base.characterBody.AddBuff(Modules.Buffs.AnxiousBuff);

                        CanSwitchEmotion = true;
                    }

                    
                }


            }


            if (base.characterBody.HasBuff(Modules.Buffs.FullSyncBuff))
            {

                if (CanSwitchEmotion)
                {

                    EffectManager.SimpleMuzzleFlash(Modules.Assets.VfxFullSync, base.gameObject, "CenterMZR", true);



                    CanSwitchEmotion = false;
                }
                else
                {

                    FullSyncTimer += Time.fixedDeltaTime;

                    if (FullSyncTimer >= 15f)
                    {

                        FullSyncTimer = 0;

                        EmotionValue = 25;

                        base.characterBody.RemoveBuff(Modules.Buffs.FullSyncBuff);
                        base.characterBody.RemoveBuff(Modules.Buffs.AnxiousBuff);
                        base.characterBody.RemoveBuff(Modules.Buffs.EvilBuff);

                        base.characterBody.AddBuff(Modules.Buffs.NormalBuff);

                        CanSwitchEmotion = true;
                    }

                    


                }


            }


            if (DamageReceived >= (500f + (base.characterBody.level * 10)) && CanSwitchEmotion)
            {
                EffectManager.SimpleMuzzleFlash(Modules.Assets.VfxRage, base.gameObject, "BaseMZ", true);

                CanSwitchEmotion = false;

                if (NetworkServer.active)
                {
                    base.characterBody.RemoveBuff(Modules.Buffs.FullSyncBuff);
                    base.characterBody.RemoveBuff(Modules.Buffs.NormalBuff);
                    base.characterBody.RemoveBuff(Modules.Buffs.AnxiousBuff);

                    base.characterBody.AddBuff(Modules.Buffs.RageBuff);
                }



            }

            if(base.characterBody.HasBuff(Modules.Buffs.RageBuff))
            {


                    RageTimer += Time.fixedDeltaTime;

                    if (RageTimer >= 10f)
                    {

                        RageTimer = 0;

                        DamageReceived = 0f;

                        CanSwitchEmotion = true;

                        base.characterBody.RemoveBuff(Modules.Buffs.RageBuff);
                    }

            }


            //EMOTION BUFFS END



            //DARK DRAIN

            if (!CanDrkDrain)
            {
                if (DrkDrainTimer >= 2.5f)
                {
                    CanDrkDrain = true;
                    DrkDrainTimer = 0f;
                }
                else
                    DrkDrainTimer += Time.fixedDeltaTime;
            }

            //DARK DRAIN END


            //RED HP ALARM

            if (base.characterBody.healthComponent.combinedHealthFraction < 0.3f && RedHpTimer < 5f)
            {
                Util.PlaySound(Sounds.SFXRedHP, base.gameObject);

                RedHpTimer = 6.2f;

            }

            if(RedHpTimer >= 5f)
                RedHpTimer -= Time.fixedDeltaTime;


            //RED HP ALARM END


            //INVIS EFFECT


        }



        public void AdvanceProgram()
        {
            //Debug.Log(MemoryCode);

            RemoveAdvanceProgram();

            if (MemoryCode.Length >= 2000)
                MemoryCode = "";


            if (MemoryCode.Length >= 3)
            {
                MemoryCodeCheck = MemoryCode.Substring(MemoryCode.Length - 3);

                //Debug.Log("inside IF:" + MemoryCodeCheck);
            }

            if (MemoryCodeCheck.Contains("SSS"))
            {

                
                skillLocator.special.SetSkillOverride(skillLocator.special, MegamanEXE.DrkSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                
            }

            if (MemoryCodeCheck.Contains("CCC"))
            {

                skillLocator.special.SetSkillOverride(skillLocator.special, MegamanEXE.MCannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);

            }


        }

        private void ReflectorProjectile()
        {

            if (Hurt && base.characterBody.HasBuff(Modules.Buffs.ReflectorBuff))
            {

                base.characterBody.AddSpreadBloom(0.15f);
                Ray aimRay = base.GetAimRay();
                EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FireBarrage.effectPrefab, base.gameObject, this.muzzleString, false);

                base.PlayAnimation("Gesture, Override", "ShootBurst", "attackSpeed", this.RDuration);

                ProjectileManager.instance.FireProjectile(Modules.Projectiles.ShokWaveProjectile, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, 3f * this.damageStat, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);

                Hurt = false;

            }
            else
            {
                Hurt = false;
            }


        }


        private void RemoveAdvanceProgram()
        {
            skillLocator.special.UnsetSkillOverride(skillLocator.special, MegamanEXE.DrkSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            skillLocator.special.UnsetSkillOverride(skillLocator.special, MegamanEXE.MCannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);



            skillLocator.special.SetSkillOverride(skillLocator.special, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
        }

        public static void DrkBugChanger()
        {

            RandBugDebuf = UnityEngine.Random.Range(1, 10);

            Debug.Log("Randon Bug Index:" + RandBugDebuf);

        }



        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }

    }
}
