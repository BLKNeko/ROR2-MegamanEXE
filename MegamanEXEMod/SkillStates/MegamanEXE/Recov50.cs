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
    public class Recov50 : BaseSkillState
    {

        public static float BaseDuration = 1f;
        private bool Healed = false;

        private Animator animator;



        public override void OnEnter()
        {
            base.OnEnter();
            this.animator = base.GetModelAnimator();

            Util.PlaySound(Sounds.SFXRecov, base.gameObject);
            EffectManager.SimpleMuzzleFlash(Modules.Assets.VfxRecov, base.gameObject, "CenterMZR", true);


        }

        public void ApplyHeal()
        {

            base.healthComponent.health += 50f;

            Healed = true;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();


            if (!Healed)
            {
                ApplyHeal();
            }
            else
            {
                Healed = false;
                this.outer.SetNextStateToMain();
            }


        }


        public override void OnExit()
        {
            SyncNetworkExe.MemoryCode = SyncNetworkExe.MemoryCode + "X";

            SyncNetworkExe.EmotionValue++;

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