using EntityStates;
using MegamanEXEMod.Modules;
using MegamanEXEMod.SkillStates.BaseStates;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using static RoR2.BulletAttack;

namespace MegamanEXEMod.SkillStates
{
    public class SuprVulc : BaseSkillState
    {
        public static float damageCoefficient = 1.5f;
        public static float procCoefficient = 1f;
        public static float baseDuration = 0.1f;
        public static float force = 1250f;
        public static float recoil = 3f;
        public static float range = 256f;
        public static GameObject tracerEffectPrefab = Resources.Load<GameObject>("Prefabs/Effects/Tracers/TracerCommandoDefault");
        public static GameObject hitEffectPrefab = Resources.Load<GameObject>("prefabs/effects/impacteffects/BulletImpactSoft");



        public float chargeTime = 0f;
        public float LastChargeTime = 0f;
        public bool chargeFullSFX = false;
        public bool hasTime = false;
        public bool hasCharged = false;
        public bool chargingSFX = false;



        private float duration;
        private float fireDuration;
        private bool hasFired;
        private Animator animator;
        private string muzzleString;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = SuprVulc.baseDuration / this.attackSpeedStat;
            this.fireDuration = 0.25f * this.duration;
            base.characterBody.SetAimTimer(2f);
            this.animator = base.GetModelAnimator();
            this.muzzleString = "Weapon";
            base.PlayAnimation("Gesture, Override", "ShootPose", "attackSpeed", this.duration);

            ArmHelper.ArmChanger(1);

        }

        public override void OnExit()
        {

            SyncNetworkExe.MemoryCode = SyncNetworkExe.MemoryCode + "V";

            base.OnExit();
        }

        private void FireArrow()
        {
            if (!this.hasFired)
            {
                this.hasFired = true;

                base.characterBody.AddSpreadBloom(1.5f);
                EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FirePistol2.muzzleEffectPrefab, base.gameObject, this.muzzleString, false);
                Util.PlaySound(Sounds.SFXVulcan, base.gameObject);
                base.PlayAnimation("Gesture, Override", "ShootBurst", "attackSpeed", this.duration);

                if (base.isAuthority)
                {
                    Ray aimRay = base.GetAimRay();
                    base.AddRecoil(-1f * SuprVulc.recoil, -2f * SuprVulc.recoil, -0.5f * SuprVulc.recoil, 0.5f * SuprVulc.recoil);

                    new BulletAttack
                    {
                        bulletCount = 1,
                        aimVector = aimRay.direction,
                        origin = aimRay.origin,
                        damage = SuprVulc.damageCoefficient * this.damageStat,
                        damageColorIndex = DamageColorIndex.Default,
                        damageType = DamageType.Generic,
                        falloffModel = BulletAttack.FalloffModel.DefaultBullet,
                        maxDistance = SuprVulc.range,
                        force = SuprVulc.force,
                        hitMask = LayerIndex.CommonMasks.bullet,
                        minSpread = 0f,
                        maxSpread = 0f,
                        isCrit = base.RollCrit(),
                        owner = base.gameObject,
                        muzzleName = muzzleString,
                        smartCollision = false,
                        procChainMask = default(ProcChainMask),
                        procCoefficient = procCoefficient,
                        radius = 0.75f,
                        sniper = false,
                        stopperMask = LayerIndex.CommonMasks.bullet,
                        weapon = null,
                        tracerEffectPrefab = SuprVulc.tracerEffectPrefab,
                        spreadPitchScale = 0f,
                        spreadYawScale = 0f,
                        queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                        hitEffectPrefab = SuprVulc.hitEffectPrefab,
                        hitCallback = BulletHitCallback,
                    }.Fire();
                }
            }
        }


        private bool BulletHitCallback(BulletAttack bulletAttack, ref BulletHit hitlnfo)
        {
            var result = BulletAttack.defaultHitCallback(bulletAttack, ref hitlnfo);
            var hurtbox = hitlnfo.hitHurtBox;


            if (hurtbox)
            {
                //Debug.Log("Hit the enemy");

                SyncNetworkExe.EmotionValue += 0.1f;

                //Debug.Log("Emotion value:" + SyncNetworkExe.EmotionValue);

            }
            else
            {
                //Debug.Log("Miss the enemy");

                SyncNetworkExe.EmotionValue -= 0.1f;

                //Debug.Log("Emotion value:" + SyncNetworkExe.EmotionValue);

            }


            return result;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if ((base.fixedAge >= this.fireDuration))
            {
                FireArrow();
            }

            if (base.fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }

    }
}
