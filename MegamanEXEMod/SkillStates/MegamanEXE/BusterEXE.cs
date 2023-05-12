using EntityStates;
using MegamanEXEMod.Modules;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace MegamanEXEMod.SkillStates
{
    public class BusterEXE : BaseSkillState
    {
        public static float damageCoefficient = 0.8f;
        public static float procCoefficient = 1f;
        public static float baseDuration = 0.7f;
        public static float force = 800f;
        public static float recoil = 3f;
        public static float range = 256f;
        public static GameObject tracerEffectPrefab = Resources.Load<GameObject>("prefabs/effects/tracers/TracerBanditPistol");
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
            this.duration = BusterEXE.baseDuration / this.attackSpeedStat;
            this.fireDuration = 0.25f * this.duration;
            base.characterBody.SetAimTimer(2f);
            this.animator = base.GetModelAnimator();
            this.muzzleString = "BusterMZ";
            base.PlayAnimation("Gesture, Override", "ShootPose", "attackSpeed", this.duration);


            ArmHelper.ArmChanger(1);

            /*
            GameObject.Find("EXEBuster").transform.localScale = new Vector3(1, 1, 1);
            GameObject.Find("EXEBuster").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("EXESword").transform.localScale = new Vector3(0, 0, 0);
            GameObject.Find("EXESword").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("EXESwordDark").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("EXESwordRed").GetComponent<MeshRenderer>().enabled = false;
            */
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        private void FireArrow()
        {
            if (!this.hasFired)
            {
                this.hasFired = true;

                base.characterBody.AddSpreadBloom(1.5f);
                EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FirePistol2.muzzleEffectPrefab, base.gameObject, this.muzzleString, false);
                Util.PlaySound("HenryShootPistol", base.gameObject);
                base.PlayAnimation("Gesture, Override", "ShootBurst", "attackSpeed", this.duration);

                if (base.isAuthority)
                {
                    Ray aimRay = base.GetAimRay();
                    base.AddRecoil(-1f * BusterEXE.recoil, -2f * BusterEXE.recoil, -0.5f * BusterEXE.recoil, 0.5f * BusterEXE.recoil);

                    new BulletAttack
                    {
                        bulletCount = 1,
                        aimVector = aimRay.direction,
                        origin = aimRay.origin,
                        damage = BusterEXE.damageCoefficient * this.damageStat,
                        damageColorIndex = DamageColorIndex.Default,
                        damageType = DamageType.Generic,
                        falloffModel = BulletAttack.FalloffModel.DefaultBullet,
                        maxDistance = BusterEXE.range,
                        force = BusterEXE.force,
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
                        tracerEffectPrefab = BusterEXE.tracerEffectPrefab,
                        spreadPitchScale = 0f,
                        spreadYawScale = 0f,
                        queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                        hitEffectPrefab = BusterEXE.hitEffectPrefab,
                    }.Fire();
                }
            }
        }

        private void FireArrowC()
        {
            if (!this.hasFired)
            {
                this.hasFired = true;

                base.characterBody.AddSpreadBloom(1.5f);
                EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FirePistol2.muzzleEffectPrefab, base.gameObject, this.muzzleString, false);
                Util.PlaySound("HenryShootPistol", base.gameObject);
                base.PlayAnimation("Gesture, Override", "ShootBurst", "attackSpeed", this.duration);

                if (base.isAuthority)
                {
                    Ray aimRay = base.GetAimRay();
                    base.AddRecoil(-1f * BusterEXE.recoil, -2f * BusterEXE.recoil, -0.5f * BusterEXE.recoil, 0.5f * BusterEXE.recoil);

                    new BulletAttack
                    {
                        bulletCount = 1,
                        aimVector = aimRay.direction,
                        origin = aimRay.origin,
                        damage = 1.25f * this.damageStat,
                        damageColorIndex = DamageColorIndex.Default,
                        damageType = DamageType.Generic,
                        falloffModel = BulletAttack.FalloffModel.DefaultBullet,
                        maxDistance = BusterEXE.range,
                        force = BusterEXE.force,
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
                        tracerEffectPrefab = BusterEXE.tracerEffectPrefab,
                        spreadPitchScale = 0f,
                        spreadYawScale = 0f,
                        queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                        hitEffectPrefab = BusterEXE.hitEffectPrefab,
                    }.Fire();
                }
            }
        }

        public override void Update()
        {
            base.Update();
            if (base.inputBank.skill1.down)
            {
                chargeTime += Time.deltaTime;
                base.characterBody.SetAimTimer(2f);

                if (chargeTime > 0.5f && chargeTime <= 1.8f && chargingSFX == false)
                {
                    //Util.PlaySound(Sounds.charging, base.gameObject);
                    EffectManager.SimpleMuzzleFlash(Modules.Assets.VfxChargeeffect1C, base.gameObject, "CenterMZ", true);
                    EffectManager.SimpleMuzzleFlash(Modules.Assets.VfxChargeeffect1W, base.gameObject, "CenterMZ", true);
                    chargingSFX = true;
                }

                if (chargeTime >= 1.8f && chargeFullSFX == false)
                {
                    //Util.PlaySound(Sounds.fullCharge, base.gameObject);
                    EffectManager.SimpleMuzzleFlash(Modules.Assets.VfxChargeeffect2C, base.gameObject, "CenterMZ", true);
                    chargeFullSFX = true;
                    LastChargeTime = chargeTime;
                }

                if ((chargeTime - LastChargeTime) >= 0.68f && chargeFullSFX == true)
                {
                    //Util.PlaySound(Sounds.fullCharge, base.gameObject);
                    EffectManager.SimpleMuzzleFlash(Modules.Assets.VfxChargeeffect2C, base.gameObject, "CenterMZ", true);
                    LastChargeTime = chargeTime;
                }
            }

            if (!base.inputBank.skill1.down)
            {
                if (chargeTime >= 1.8f)
                    hasCharged = true;
                chargingSFX = false;
                hasTime = true;
            }

        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (((base.fixedAge >= this.fireDuration || !base.inputBank || !base.inputBank.skill1.down) && hasCharged == true && hasTime == true))
            {
                FireArrowC();
            }

            if ((base.fixedAge >= this.fireDuration || !base.inputBank || !base.inputBank.skill1.down) && hasCharged == false && hasTime == true)
            {
                FireArrow();
            }

            if (base.fixedAge >= this.duration && base.isAuthority && hasTime == true)
            {
                hasTime = false;
                chargeTime = 0f;
                this.outer.SetNextStateToMain();
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }

    }
}
