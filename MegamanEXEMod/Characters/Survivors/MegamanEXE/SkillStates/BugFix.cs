using EntityStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace MegamanEXEMod.Survivors.MegamanEXE.SkillStates
{
    public class BugFix : BaseSkillState
    {

        public static float BaseDuration = 0.4f;
        private bool Fix = false;

        private Animator animator;

        private EXEBaseComponent execomponent;


        public override void OnEnter()
        {
            base.OnEnter();
            this.animator = base.GetModelAnimator();

            //Util.PlaySound(Sounds.SFXBugFix, this.gameObject);

            execomponent = GetComponent<EXEBaseComponent>();

        }

        public void ApplyFix()
        {

            if (isAuthority)
            {

                if (NetworkServer.active)
                {

                    if(characterBody.HasBuff(RoR2Content.Buffs.OnFire))
                        characterBody.RemoveBuff(RoR2Content.Buffs.OnFire);

                    if (characterBody.HasBuff(RoR2Content.Buffs.Slow50))
                        characterBody.RemoveBuff(RoR2Content.Buffs.Slow50);

                    if (characterBody.HasBuff(RoR2Content.Buffs.Slow60))
                        characterBody.RemoveBuff(RoR2Content.Buffs.Slow60);

                    if (characterBody.HasBuff(RoR2Content.Buffs.Slow80))
                        characterBody.RemoveBuff(RoR2Content.Buffs.Slow80);

                    if (characterBody.HasBuff(RoR2Content.Buffs.Poisoned))
                        characterBody.RemoveBuff(RoR2Content.Buffs.Poisoned);

                    if (characterBody.HasBuff(RoR2Content.Buffs.Weak))
                        characterBody.RemoveBuff(RoR2Content.Buffs.Weak);

                    if (characterBody.HasBuff(RoR2Content.Buffs.Bleeding))
                        characterBody.RemoveBuff(RoR2Content.Buffs.Bleeding);

                    if (characterBody.HasBuff(RoR2Content.Buffs.HealingDisabled))
                        characterBody.RemoveBuff(RoR2Content.Buffs.HealingDisabled);

                    if (characterBody.HasBuff(RoR2Content.Buffs.SuperBleed))
                        characterBody.RemoveBuff(RoR2Content.Buffs.SuperBleed);

                    if (characterBody.HasBuff(DLC1Content.Buffs.Blinded))
                        characterBody.RemoveBuff(DLC1Content.Buffs.Blinded);

                    if (characterBody.HasBuff(DLC1Content.Buffs.JailerSlow))
                        characterBody.RemoveBuff(DLC1Content.Buffs.JailerSlow);


                    characterBody.helfireLifetime = 0f;

                }

                EffectManager.SimpleMuzzleFlash(EXEAssets.VfxRecov, gameObject, "BaseMZ", true);

                execomponent.SetEmotionalValue(25, 0);
                execomponent.SetEmotionalValue(0, 1);

                execomponent.UpdateMemoryCode('X');
            }


            Fix = true;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();


            if (!Fix)
            {
                ApplyFix();
            }
            else
            {
                Fix = false;
                this.outer.SetNextStateToMain();
            }


        }


        public override void OnExit()
        {

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