using EntityStates;
using MegamanEXEMod.Modules;
using RoR2;
using RoR2.Skills;
using System;
using UnityEngine;
using UnityEngine.Networking;


namespace MegamanEXEMod.SkillStates.BaseStates
{
    public class DeathState : GenericCharacterDeath
    {
        private float duration;
        public float baseDuration = 0.5f;
        private Animator animator;

        private Transform modelTransform;
        private CharacterModel characterModel;
        private HurtBoxGroup hurtboxGroup;


        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;

            SyncNetworkExe.MemoryCode = "";

            SyncNetworkExe.DamageReceived = 0;

            SyncNetworkExe.EvilEmotionValue = 0;

            SyncNetworkExe.EmotionValue = 25;


            ArmHelper.ArmChanger(0);


            base.PlayAnimation("FullBody, Override", "Deleted", "attackSpeed", this.duration);
            Util.PlaySound(Sounds.SFXDeleted, this.gameObject);


            EffectManager.SimpleMuzzleFlash(Modules.Assets.VfxDeleted, base.gameObject, "CenterMZR", true);

            modelTransform = GetModelTransform();
            if ((bool)modelTransform)
            {
                animator = modelTransform.GetComponent<Animator>();
                characterModel = modelTransform.GetComponent<CharacterModel>();
                hurtboxGroup = modelTransform.GetComponent<HurtBoxGroup>();
            }

            if ((bool)characterModel)
            {
                characterModel.invisibilityCount++;
            }





        }

        public override void OnExit()
        {

            if ((bool)characterModel)
            {
                characterModel.invisibilityCount--;
            }

            base.PlayAnimation("FullBody, Override", "BufferEmpty", "attackSpeed", this.duration);

            base.OnExit();
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();

        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Death;
        }

    }
}
