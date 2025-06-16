using EntityStates;
using MegamanEXEMod.Modules.BaseStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace MegamanEXEMod.Survivors.MegamanEXE.SkillStates
{
    public class AquaSwrd : BaseMeleeAttack2
    {
        public override void OnEnter()
        {
            hitboxGroupName = "EXESwordGroup";

            damageType |= DamageTypeCombo.Generic;
            damageType |= DamageType.Freeze2s;
            damageCoefficient = 1f;
            procCoefficient = 1f;
            pushForce = 500f;
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
            muzzleString = "SwingLeft";
            playbackRateParam = "attackSpeed";
            hitEffectPrefab = Resources.Load<GameObject>("prefabs/effects/impacteffects/ImpactMercSwing");

            impactSound = EXEAssets.swordHitSoundEvent.index;

            swingEffectPrefab = EXEAssets.BlueSwordSwingVFX;

            SetHitReset(true, 2);

            EMValue = 2;
            EVValue = 0;
            DMGValue = 0;

            chipMemoryCode = 'S';

            SwordModelID = 0;

            //SetHitReset(true, 3);

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

        }

        public override void OnExit()
        {

            base.PlayAnimation("FullBody, Override", "BufferEmpty", "attackSpeed", this.duration);
            base.PlayAnimation("Gesture, Override", "BufferEmpty", "attackSpeed", this.duration);

            base.OnExit();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }
    }
}