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
    public class Barr200 : BaseSkillState
    {

        public static float BaseDuration = 1f;
        private bool Barrier = false;

        private Animator animator;



        public override void OnEnter()
        {
            base.OnEnter();
            this.animator = base.GetModelAnimator();

            Util.PlaySound(Sounds.SFXBarrier, base.gameObject);


        }

        public void ApplyBarrier()
        {

            base.healthComponent.AddBarrierAuthority(200f);

            Barrier = true;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();


            if (!Barrier)
            {
                ApplyBarrier();
            }
            else
            {
                Barrier = false;
                this.outer.SetNextStateToMain();
            }


        }


        public override void OnExit()
        {

            SyncNetworkExe.MemoryCode = SyncNetworkExe.MemoryCode + "B";

            if (SyncNetworkExe.EvilEmotionValue > 0)
                SyncNetworkExe.EvilEmotionValue--;


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