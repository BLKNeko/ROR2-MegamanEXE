using EntityStates;
using MegamanEXEMod.Modules.BaseStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace MegamanEXEMod.Survivors.MegamanEXE.SkillStates
{
    public class Recov300 : BaseSkillState
    {

        public static float BaseDuration = 1f;
        private bool Healed = false;

        private Animator animator;

        private EXEBaseComponent execomponent;

        public override void OnEnter()
        {
            base.OnEnter();
            this.animator = base.GetModelAnimator();

            execomponent = GetComponent<EXEBaseComponent>();

            AkSoundEngine.PostEvent(EXEStaticValues.SFXRecov, this.gameObject);
            EffectManager.SimpleMuzzleFlash(EXEAssets.VfxRecov, base.gameObject, "BaseMZ", true);

        }

        public void ApplyHeal()
        {

            //base.healthComponent.health += 300f;
            base.healthComponent.HealFraction(300f, default(ProcChainMask));

            Healed = true;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();


            if (!Healed && isAuthority)
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

            if (isAuthority)
            {
                execomponent.UpdateEmotionalValue(1, -1, 0);

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