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
    public class Invis : BaseSkillState
    {

        public static float BaseDuration = 1f;
        private bool Invisble = false;

        private Transform modelTransform;
        private CharacterModel characterModel;
        private Animator animator;
        private HurtBoxGroup hurtboxGroup;


        public override void OnEnter()
        {
            base.OnEnter();
            this.animator = base.GetModelAnimator();

            modelTransform = GetModelTransform();
            if ((bool)modelTransform)
            {
                animator = modelTransform.GetComponent<Animator>();
                characterModel = modelTransform.GetComponent<CharacterModel>();
                hurtboxGroup = modelTransform.GetComponent<HurtBoxGroup>();
            }

            if ((bool)characterModel)
            {
                characterModel.invisibilityCount++;
            }

            if ((bool)hurtboxGroup)
            {
                hurtboxGroup.hurtBoxesDeactivatorCounter++;
            }

        }

        public void ApplyInvis()
        {

            if (NetworkServer.active)
            {
                base.characterBody.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, 10f);
                base.characterBody.AddTimedBuff(RoR2Content.Buffs.Intangible, 10f);
                base.characterBody.AddTimedBuff(RoR2Content.Buffs.Immune, 10f);
            }


            Invisble = true;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();


            if (!Invisble)
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

            if ((bool)characterModel)
            {
                characterModel.invisibilityCount--;
            }

            if ((bool)hurtboxGroup)
            {
                hurtboxGroup.hurtBoxesDeactivatorCounter--;
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