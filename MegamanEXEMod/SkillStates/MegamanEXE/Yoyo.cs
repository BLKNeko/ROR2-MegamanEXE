using EntityStates;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using MegamanEXEMod.SkillStates.BaseStates;
using MegamanEXEMod.Modules;

namespace MegamanEXEMod.SkillStates
{
    public class Yoyo : BaseSkillState
    {
        public float damageCoefficient = 1.5f;
        public float baseDuration = 0.5f;
        public float recoil = 1f;
        public static GameObject tracerEffectPrefab = Resources.Load<GameObject>("Prefabs/Effects/Tracers/TracerToolbotRebar");

        private float duration;
        private float fireDuration;
        private bool hasFired;
        private Animator animator;
        private string muzzleString;
        private string muzzleString2;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration;
            this.fireDuration = 0.25f * this.duration;
            base.characterBody.SetAimTimer(2f);
            this.animator = base.GetModelAnimator();
            this.muzzleString = "BusterMZ";

            ArmHelper.ArmChanger(1);


            //Util.PlaySound(Modules.Sounds.vileFragDrop, base.gameObject);
            base.PlayAnimation("Gesture, Override", "ShootPose", "attackSpeed", this.duration);
        }

        public override void OnExit()
        {

            base.OnExit();
        }

        private void FireES()
        {
            if (!this.hasFired)
            {
                this.hasFired = true;

                base.characterBody.AddSpreadBloom(0.15f);
                Ray aimRay = base.GetAimRay();
                EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FireBarrage.effectPrefab, base.gameObject, this.muzzleString, false);
                //EffectManager.SimpleMuzzleFlash(EntityStates.Mage.Weapon.FireLaserbolt.impactEffectPrefab, base.gameObject, this.muzzleString, false);

                if (base.isAuthority)
                {
                    base.PlayAnimation("Gesture, Override", "ShootBurst", "attackSpeed", this.duration);
                    ProjectileManager.instance.FireProjectile(Modules.Projectiles.YoyoProjectile, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageCoefficient * this.damageStat, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
                }
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.fixedAge >= this.fireDuration)
            {
                FireES();
            }

            if (base.fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }
    }
}
