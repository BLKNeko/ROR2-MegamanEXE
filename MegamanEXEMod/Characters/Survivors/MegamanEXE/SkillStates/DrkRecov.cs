using EntityStates;
using MegamanEXEMod.Modules.BaseStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace MegamanEXEMod.Survivors.MegamanEXE.SkillStates
{
    public class DrkRecov : BaseSkillState
    {

        public static float BaseDuration = 1f;
        private bool Healed = false;

        private EXEBaseComponent execomponent;

        public override void OnEnter()
        {
            base.OnEnter();

            AkSoundEngine.PostEvent(EXEStaticValues.SFXRecov, this.gameObject);
            EffectManager.SimpleMuzzleFlash(EXEAssets.VfxRecov, base.gameObject, "CorePosition", true);

            execomponent = GetComponent<EXEBaseComponent>();

        }

        public void ApplyHeal()
        {

            if((base.healthComponent.health + 1000f) > (base.characterBody.healthComponent.fullHealth * 2))
            {

                if (base.healthComponent.health < (base.characterBody.healthComponent.fullHealth * 2))
                    base.healthComponent.health = (base.characterBody.healthComponent.fullHealth * 2);

            }
            else
                base.healthComponent.health += 1000f;

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
            if (isAuthority)
            {
                execomponent.UpdateEmotionalValue(-1, 1, 0);

                execomponent.UpdateMemoryCode('X');

                if (NetworkServer.active)
                {
                    var rand = UnityEngine.Random.Range(0, 9);
                    characterBody.AddTimedBuff(execomponent.GetDebuffByIndex(rand), 5f);

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