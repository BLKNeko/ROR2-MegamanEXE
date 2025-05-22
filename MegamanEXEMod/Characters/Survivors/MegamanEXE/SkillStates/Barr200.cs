using EntityStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace MegamanEXEMod.Survivors.MegamanEXE.SkillStates
{
    public class Barr200 : BaseSkillState
    {

        public static float BaseDuration = 1f;
        private bool Barrier = false;

        private Animator animator;

        private EXEBaseComponent execomponent;

        public override void OnEnter()
        {
            base.OnEnter();
            this.animator = base.GetModelAnimator();

            //Util.PlaySound(Sounds.SFXBarrier, base.gameObject);

            execomponent = GetComponent<EXEBaseComponent>();

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

            ////SyncNetworkExe.MemoryCode = ////SyncNetworkExe.MemoryCode + "B";

            //if (//SyncNetworkExe.EvilEmotionValue > 0)
            //SyncNetworkExe.EvilEmotionValue--;

            if (isAuthority)
            {
                execomponent.UpdateEmotionalValue(1, -1, 0);

                execomponent.UpdateMemoryCode('B');
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