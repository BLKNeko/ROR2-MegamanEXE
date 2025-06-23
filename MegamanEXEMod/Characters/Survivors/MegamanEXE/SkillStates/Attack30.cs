using EntityStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace MegamanEXEMod.Survivors.MegamanEXE.SkillStates
{
    public class Attack30 : BaseSkillState
    {

        public static float BaseDuration = 1f;
        private bool Attack = false;

        private Animator animator;



        public override void OnEnter()
        {
            base.OnEnter();
            this.animator = base.GetModelAnimator();

            AkSoundEngine.PostEvent(EXEStaticValues.SFXRecov, this.gameObject);


        }

        public void ApplyAttack()
        {

            if (NetworkServer.active)
            {
                base.characterBody.AddTimedBuff(EXEBuffs.Attack30Buff, 20f);
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

            ////SyncNetworkExe.MemoryCode = ////SyncNetworkExe.MemoryCode + "X";

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