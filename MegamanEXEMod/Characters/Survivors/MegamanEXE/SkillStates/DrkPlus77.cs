using EntityStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace MegamanEXEMod.Survivors.MegamanEXE.SkillStates
{
    public class DrkPlus77 : BaseSkillState
    {

        public static float BaseDuration = 1f;
        private bool Attack = false;

        private Animator animator;

        private EXEBaseComponent execomponent;


        public override void OnEnter()
        {
            base.OnEnter();
            this.animator = base.GetModelAnimator();

            execomponent = GetComponent<EXEBaseComponent>();

            AkSoundEngine.PostEvent(EXEStaticValues.SFXRecov, this.gameObject);

        }

        public void ApplyAttack()
        {

            if (NetworkServer.active)
            {
                //base.characterBody.AddTimedBuff(EXEBuffs.Attack77Buff, 15f);
            }

            Attack = true;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();


            if (!Attack)
            {
                ApplyAttack();
            }
            else
            {
                Attack = false;
                this.outer.SetNextStateToMain();
            }


        }


        public override void OnExit()
        {

            if (isAuthority)
            {
                execomponent.UpdateEmotionalValue(-1, 3, 0);

                execomponent.UpdateMemoryCode('X');

                if (NetworkServer.active)
                {
                    var rand = UnityEngine.Random.Range(0, 9);
                    characterBody.AddTimedBuff(execomponent.GetDebuffByIndex(rand), 7f);

                }

            }

            base.OnExit();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }

        public override void OnSerialize(NetworkWriter writer)
        {
            base.OnSerialize(writer);

        }

        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);

        }

        
    }
}