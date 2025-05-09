using EntityStates;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using ExtraSkillSlots;
using MegamanEXEMod.Modules.Survivors;
using UnityEngine.Networking;
using MegamanEXEMod.Modules;
using MegamanEXEMod.SkillStates.BaseStates;

namespace MegamanEXEMod.SkillStates
{
    public class Reflector : BaseSkillState
    {

        public static float BaseDuration = 0.2f;
        private bool Attack = false;

        private Animator animator;



        public override void OnEnter()
        {
            base.OnEnter();
            this.animator = base.GetModelAnimator();

            SyncNetworkExe.Hurt = false;

        }

        public void ApplyAttack()
        {

            if (NetworkServer.active)
            {
                base.characterBody.AddTimedBuff(Modules.Buffs.ReflectorBuff, 2f);
                base.characterBody.AddTimedBuff(RoR2Content.Buffs.Immune, 2f);
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

            SyncNetworkExe.MemoryCode = SyncNetworkExe.MemoryCode + "X";


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