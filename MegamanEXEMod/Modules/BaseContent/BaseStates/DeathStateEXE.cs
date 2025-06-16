using EntityStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using R2API;
using RoR2;
using RoR2.Audio;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MegamanEXEMod.Modules.BaseStates
{
    public class DeathStateEXE : GenericCharacterDeath
    {
        private float duration;
        public float baseDuration = 1f;
        private Animator animator;

        private Transform modelTransform;
        private CharacterModel characterModel;
        private HurtBoxGroup hurtboxGroup;

        private EXEBaseComponent execomponent;


        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;

            execomponent = GetComponent<EXEBaseComponent>();

            if(isAuthority)
            {
                execomponent.SetEmotionalValue(0,3);
                execomponent.SetMemoryCode("");
            }

            AkSoundEngine.PostEvent(EXEStaticValues.SFXDeleted, this.gameObject);

            EffectManager.SimpleMuzzleFlash(EXEAssets.VfxDeleted, base.gameObject, "CorePosition", true);

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

