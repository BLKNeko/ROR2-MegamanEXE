using EntityStates;
using MegamanEXEMod.Modules;
using RoR2;
using RoR2.Audio;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace MegamanEXEMod.SkillStates.BaseStates
{
    public class SpawnState : GenericCharacterSpawnState
    {
        private float duration;
        public float baseDuration = 1f;
        private Animator animator;


        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;

            SyncNetworkExe.MemoryCode = "";

            SyncNetworkExe.DamageReceived = 0;

            SyncNetworkExe.EvilEmotionValue = 0;

            SyncNetworkExe.EmotionValue = 25;

        }
        public override void OnExit()
        {

            //Util.PlaySound(Sounds.XReady, base.gameObject);
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

