using EntityStates;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using ExtraSkillSlots;
using MegamanEXEMod.Modules.Survivors;
using UnityEngine.Networking;
using MegamanEXEMod.Modules;

namespace MegamanEXEMod.SkillStates
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


        }

        public void ApplyAttack()
        {

            if (NetworkServer.active)
            {
                base.characterBody.AddTimedBuff(Modules.Buffs.Attack30Buff, 20f);
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