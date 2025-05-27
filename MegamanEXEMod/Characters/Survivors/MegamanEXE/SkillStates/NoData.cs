using EntityStates;
using MegamanEXEMod.Modules.BaseStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace MegamanEXEMod.Survivors.MegamanEXE.SkillStates
{
    public class NoData : BaseSkillState
    {

        public static float BaseDuration = 0.1f;
        private bool Attack = false;

        private Animator animator;



        public override void OnEnter()
        {
            base.OnEnter();
            this.animator = base.GetModelAnimator();

            //Util.PlaySound(Sounds.SFXRecov, base.gameObject);

        }

        public void ApplyAttack()
        {

            

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
            return InterruptPriority.Skill;
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