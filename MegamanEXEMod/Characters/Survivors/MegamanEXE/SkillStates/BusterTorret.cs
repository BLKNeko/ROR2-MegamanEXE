using EntityStates;
using MegamanEXEMod.Modules.BaseStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using RoR2;
using System;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.SendMouseEvents;

namespace MegamanEXEMod.Survivors.MegamanEXE.SkillStates
{

    public class BusterTorret : BaseState
    {
        // Token: 0x0600165B RID: 5723 RVA: 0x0006736C File Offset: 0x0006556C
        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = BusterTorret.baseDuration / this.attackSpeedStat;
            Util.PlaySound(BusterTorret.attackSoundString, base.gameObject);
            Ray aimRay = base.GetAimRay();
            base.StartAimMode(aimRay, 2f, false);
            PlayAnimation("Gesture, Override", "EXEBusterAttack", "attackSpeed", this.duration);
            string muzzleName = "Muzzle";
            if (BusterTorret.effectPrefab)
            {
                EffectManager.SimpleMuzzleFlash(BusterTorret.effectPrefab, base.gameObject, muzzleName, false);
            }
            if (base.isAuthority)
            {
                BulletAttack bulletAttack = new BulletAttack();
                bulletAttack.owner = base.gameObject;
                bulletAttack.weapon = base.gameObject;
                bulletAttack.origin = aimRay.origin;
                bulletAttack.aimVector = aimRay.direction;
                bulletAttack.minSpread = BusterTorret.minSpread;
                bulletAttack.maxSpread = BusterTorret.maxSpread;
                bulletAttack.bulletCount = 1U;
                bulletAttack.damage = BusterTorret.damageCoefficient * this.damageStat;
                bulletAttack.force = BusterTorret.force;
                bulletAttack.tracerEffectPrefab = BusterTorret.tracerEffectPrefab;
                bulletAttack.muzzleName = muzzleName;
                bulletAttack.hitEffectPrefab = BusterTorret.hitEffectPrefab;
                bulletAttack.isCrit = Util.CheckRoll(this.critStat, base.characterBody.master);
                bulletAttack.HitEffectNormal = false;
                bulletAttack.radius = 0.15f;
                bulletAttack.damageType.damageSource = DamageSource.Primary;
                bulletAttack.Fire();
            }
        }

        // Token: 0x0600165C RID: 5724 RVA: 0x00018E85 File Offset: 0x00017085
        public override void OnExit()
        {
            base.OnExit();
        }

        // Token: 0x0600165D RID: 5725 RVA: 0x000674CD File Offset: 0x000656CD
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }

        // Token: 0x0600165E RID: 5726 RVA: 0x0000E0C3 File Offset: 0x0000C2C3
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }

        // Token: 0x04001D4C RID: 7500
        public static GameObject effectPrefab;

        // Token: 0x04001D4D RID: 7501
        public static GameObject hitEffectPrefab;

        // Token: 0x04001D4E RID: 7502
        public static GameObject tracerEffectPrefab;

        // Token: 0x04001D4F RID: 7503
        public static string attackSoundString;

        // Token: 0x04001D50 RID: 7504
        public static float damageCoefficient;

        // Token: 0x04001D51 RID: 7505
        public static float force;

        // Token: 0x04001D52 RID: 7506
        public static float minSpread;

        // Token: 0x04001D53 RID: 7507
        public static float maxSpread;

        // Token: 0x04001D54 RID: 7508
        public static int bulletCount;

        // Token: 0x04001D55 RID: 7509
        public static float baseDuration = 2f;

        // Token: 0x04001D56 RID: 7510
        public int bulletCountCurrent = 1;

        // Token: 0x04001D57 RID: 7511
        private float duration;

        // Token: 0x04001D58 RID: 7512
        private static int FireGaussStateHash = Animator.StringToHash("FireGauss");

        // Token: 0x04001D59 RID: 7513
        private static int FireGaussParamHash = Animator.StringToHash("FireGauss.playbackRate");
    }
}