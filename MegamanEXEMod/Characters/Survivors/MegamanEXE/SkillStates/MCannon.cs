using EntityStates;
using MegamanEXEMod.Modules.BaseStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using static RoR2.BulletAttack;

namespace MegamanEXEMod.Survivors.MegamanEXE.SkillStates
{
    public class MCannon : BaseSkillState
    {
        public static float damageCoefficient = 2.25f;
        public static float procCoefficient = 1f;
        public static float baseDuration = 0.5f;
        public static float force = 1250f;
        public static float recoil = 3f;
        public static float range = 256f;
        public static GameObject tracerEffectPrefab = Resources.Load<GameObject>("prefabs/effects/tracers/TracerBanditPistol");
        public static GameObject hitEffectPrefab = Resources.Load<GameObject>("Prefabs/Effects/ImpactEffects/MagmaOrbExplosion");



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

        private EXEBaseComponent execomponent;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = HiCannon.baseDuration / this.attackSpeedStat;
            this.fireDuration = 0.25f * this.duration;
            base.characterBody.SetAimTimer(2f);
            this.animator = base.GetModelAnimator();
            this.muzzleString = "BusterMZ";

            execomponent = GetComponent<EXEBaseComponent>();

            execomponent.ChangeBusterArm(
                GetModelTransform(),
                GetModelTransform().GetComponent<CharacterModel>(),
                GetModelTransform().GetComponent<CharacterModel>().GetComponent<ChildLocator>(),
                ((int)characterBody.skinIndex)
                );

        }

        public override void OnExit()
        {

            if (isAuthority)
            {
                execomponent.UpdateMemoryCode('C');

            }

            base.OnExit();
        }

        private void FireArrow()
        {
            if (!this.hasFired)
            {
                this.hasFired = true;

                if (base.isAuthority)
                {
                    Ray aimRay = base.GetAimRay();
                    base.AddRecoil(-1f * HiCannon.recoil, -2f * HiCannon.recoil, -0.5f * HiCannon.recoil, 0.5f * HiCannon.recoil);

                    base.characterBody.AddSpreadBloom(1.5f);
                    EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FirePistol2.muzzleEffectPrefab, base.gameObject, this.muzzleString, false);
                    //Util.PlaySound(Sounds.SFXCanon, base.gameObject);
                    base.PlayAnimation("Gesture, Override", "EXEBusterAttack", "attackSpeed", this.duration);

                    new BulletAttack
                    {
                        bulletCount = 1,
                        aimVector = aimRay.direction,
                        origin = aimRay.origin,
                        damage = HiCannon.damageCoefficient * this.damageStat,
                        damageColorIndex = DamageColorIndex.Default,
                        damageType = DamageType.Generic,
                        falloffModel = BulletAttack.FalloffModel.DefaultBullet,
                        maxDistance = HiCannon.range,
                        force = HiCannon.force,
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
                        tracerEffectPrefab = HiCannon.tracerEffectPrefab,
                        spreadPitchScale = 0f,
                        spreadYawScale = 0f,
                        queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                        hitEffectPrefab = HiCannon.hitEffectPrefab,
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


                if (isAuthority)
                    execomponent.UpdateEmotionalValue(1, 0, 0);


            }
            else
            {

                if (isAuthority)
                    execomponent.UpdateEmotionalValue(-1, 0, 0);


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
            return InterruptPriority.Skill;
        }

    }
}
