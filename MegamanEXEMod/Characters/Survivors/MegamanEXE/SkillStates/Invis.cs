using EntityStates;
using MegamanEXEMod.Modules.BaseStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace MegamanEXEMod.Survivors.MegamanEXE.SkillStates
{
    public class Invis : BaseSkillState
    {

        public static float BaseDuration = 1f;
        private bool Invisble = false;

        private EXEBaseComponent execomponent;


        public override void OnEnter()
        {
            base.OnEnter();

            //Util.PlaySound(Sounds.SFXInvis, base.gameObject);

            execomponent = GetComponent<EXEBaseComponent>();

        }

        public void ApplyInvis()
        {

            if (NetworkServer.active)
            {
                base.characterBody.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, 10f);
                base.characterBody.AddTimedBuff(RoR2Content.Buffs.Intangible, 10f);
                base.characterBody.AddTimedBuff(RoR2Content.Buffs.Cloak, 10f);
                base.characterBody.AddTimedBuff(RoR2Content.Buffs.CloakSpeed, 10f);
            }


            Invisble = true;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();


            if (!Invisble && isAuthority)
            {
                ApplyInvis();
            }
            else
            {
                Invisble = false;
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