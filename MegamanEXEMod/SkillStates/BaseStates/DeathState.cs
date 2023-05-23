using EntityStates;
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

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;

            SyncNetworkExe.MemoryCode = "";

            SyncNetworkExe.DamageReceived = 0;

            SyncNetworkExe.EvilEmotionValue = 0;

            SyncNetworkExe.EmotionValue = 25;


            base.PlayAnimation("FullBody, Override", "Death", "attackSpeed", this.duration);
            // Util.PlaySound(Sounds.HaseoDie, base.gameObject);


        }

        public override void OnExit()
        {
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
