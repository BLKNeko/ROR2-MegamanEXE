using EntityStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

using static RoR2.BulletAttack;

namespace MegamanEXEMod.Survivors.MegamanEXE.SkillStates
{
    public class AdvGigaCannon : BaseSkillState
    {
        public static float damageCoefficient = EXEStaticValues.AdvGigaCannonSkillDefDamageCoefficient;
        public static float procCoefficient = 1f;
        public static float baseDuration = 0.5f;
        public static float force = 8250f;
        public static float recoil = 5f;
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
            this.duration = AdvGigaCannon.baseDuration / this.attackSpeedStat;
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

            //////SyncNetworkExe.MemoryCode = ////SyncNetworkExe.MemoryCode + "X";

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
                    //base.AddRecoil(-1f * AdvGigaCannon.recoil, -2f * AdvGigaCannon.recoil, -0.5f * AdvGigaCannon.recoil, 0.5f * AdvGigaCannon.recoil);

                    base.characterBody.AddSpreadBloom(1.5f);
                    EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FirePistol2.muzzleEffectPrefab, base.gameObject, this.muzzleString, false);
                    AkSoundEngine.PostEvent(EXEStaticValues.SFXCanon, this.gameObject);
                    PlayAnimation("Gesture, Override", "EXEBusterAttack", "attackSpeed", this.duration);

                    new BulletAttack
                    {
                        bulletCount = 1,
                        aimVector = aimRay.direction,
                        origin = aimRay.origin,
                        damage = AdvGigaCannon.damageCoefficient * this.damageStat,
                        damageColorIndex = DamageColorIndex.Default,
                        damageType = DamageType.Generic,
                        falloffModel = BulletAttack.FalloffModel.DefaultBullet,
                        maxDistance = AdvGigaCannon.range,
                        force = AdvGigaCannon.force,
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
                        tracerEffectPrefab = AdvGigaCannon.tracerEffectPrefab,
                        spreadPitchScale = 0f,
                        spreadYawScale = 0f,
                        queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                        hitEffectPrefab = AdvGigaCannon.hitEffectPrefab,
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

                ////SyncNetworkExe.EmotionValue++;

                //Debug.Log("Emotion value:" + //SyncNetworkExe.EmotionValue);

            }
            else
            {
                //Debug.Log("Miss the enemy");

                ////SyncNetworkExe.EmotionValue--;

                //Debug.Log("Emotion value:" + //SyncNetworkExe.EmotionValue);

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
