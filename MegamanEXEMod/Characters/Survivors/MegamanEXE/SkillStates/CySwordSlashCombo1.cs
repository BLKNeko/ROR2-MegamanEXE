using EntityStates;
using MegamanEXEMod.Modules.BaseStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace MegamanEXEMod.Survivors.MegamanEXE.SkillStates
{
    public class CySwordSlashCombo1 : BaseMeleeAttack
    {

        EXEBaseComponent execomponent;

        public override void OnEnter()
        {
            hitboxGroupName = "EXESwordGroup";

            damageType = DamageTypeCombo.GenericSecondary;
            damageCoefficient = 1f;
            procCoefficient = 1f;
            pushForce = 300f;
            bonusForce = Vector3.zero;
            baseDuration = 0.5f;

            //0-1 multiplier of baseduration, used to time when the hitbox is out (usually based on the run time of the animation)
            //for example, if attackStartPercentTime is 0.5, the attack will start hitting halfway through the ability. if baseduration is 3 seconds, the attack will start happening at 1.5 seconds
            attackStartPercentTime = 0.1f;
            attackEndPercentTime = 0.9f;

            //this is the point at which the attack can be interrupted by itself, continuing a combo
            earlyExitPercentTime = 0.8f;

            hitStopDuration = 0.012f;
            attackRecoil = 0.5f;
            hitHopVelocity = 5f;

            //swingSoundString = swingIndex % 2 == 0 ? XStaticValues.X_Slash3_SFX : XStaticValues.X_Slash2_SFX;

            hitSoundString = "";
            //muzzleString = "SwordMuzzPos";
            //muzzleString = "SwingLeft";
            muzzleString = swingIndex % 2 == 0 ? "SwingLeft" : "SwingRight";
            playbackRateParam = "attackSpeed";
            hitEffectPrefab = EXEAssets.swordHitImpactEffect;

            impactSound = EXEAssets.swordHitSoundEvent.index;

            execomponent = GetComponent<EXEBaseComponent>();

            if (base.characterBody.skinIndex == 0)
                swingEffectPrefab = EXEAssets.CyanSwordSwingVFX;
            if (base.characterBody.skinIndex == 1)
                swingEffectPrefab = EXEAssets.RedSwordSwingVFX;
            if (base.characterBody.skinIndex == 2)
                swingEffectPrefab = EXEAssets.PinkSwordSwingVFX;
            if (base.characterBody.skinIndex == 3)
                swingEffectPrefab = EXEAssets.PurpleSwordSwingVFX;
            if (base.characterBody.skinIndex == 4)
                swingEffectPrefab = EXEAssets.CyanSwordSwingVFX;

            execomponent.ChangeSwordArm(
                GetModelTransform(),
                GetModelTransform().GetComponent<CharacterModel>(),
                GetModelTransform().GetComponent<CharacterModel>().GetComponent<ChildLocator>(),
                ((int)characterBody.skinIndex),
                0);

            AkSoundEngine.PostEvent(EXEStaticValues.SwordSwing, this.gameObject);



            base.OnEnter();
        }

        protected override void PlayAttackAnimation()
        {
            //PlayCrossfade("Gesture, Override", "Slash" + (1 + swingIndex), playbackRateParam, duration, 0.1f * duration);
            base.PlayAnimation("Gesture, Override", "CYSlash" + (1 + swingIndex), "attackSpeed", this.duration);
        }

        protected virtual void PlaySwingEffect()
        {
            EffectManager.SimpleMuzzleFlash(swingEffectPrefab, gameObject, muzzleString, true);
        }

        protected override void OnHitEnemyAuthority()
        {
            base.OnHitEnemyAuthority();

            if (isAuthority)
                execomponent.UpdateEmotionalValue(2, 0, 0);

        }

        public override void OnExit()
        {

            base.PlayAnimation("Gesture, Override", "BufferEmpty", "attackSpeed", this.duration);

            base.OnExit();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }
    }
}