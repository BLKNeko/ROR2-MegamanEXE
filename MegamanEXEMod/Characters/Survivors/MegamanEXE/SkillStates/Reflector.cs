using EntityStates;
using MegamanEXEMod.Modules.BaseStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace MegamanEXEMod.Survivors.MegamanEXE.SkillStates
{
    public class Reflector : BaseSkillState
    {

        public static float BaseDuration = 0.2f;
        private bool Attack = false;

        private Animator animator;

        private EXEBaseComponent execomponent;

        public override void OnEnter()
        {
            base.OnEnter();
            this.animator = base.GetModelAnimator();

            execomponent = GetComponent<EXEBaseComponent>();

        }

        public void ApplyAttack()
        {

            if (NetworkServer.active)
            {
                base.characterBody.AddTimedBuff(EXEBuffs.ReflectorBuff, 2f);
                base.characterBody.AddTimedBuff(RoR2Content.Buffs.Immune, 2f);
            }

            Attack = true;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();


            if (!Attack && isAuthority)
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

                execomponent.UpdateMemoryCode('X');
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