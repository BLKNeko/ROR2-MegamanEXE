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
    public class SpawnStateEXE : GenericCharacterSpawnState
    {
        private float duration;
        public float baseDuration = 1f;
        private Animator animator;

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

        }
        public override void OnExit()
        {

            //AkSoundEngine.PostEvent(XStaticValues.X_Ready, this.gameObject);

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

