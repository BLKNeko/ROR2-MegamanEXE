using EntityStates;
using MegamanEXEMod.Modules.BaseStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace MegamanEXEMod.Survivors.MegamanEXE.SkillStates
{
    public class DrkCannon : BaseSkillState
    {
        public static float damageCoefficient = 30f;
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
            this.duration = DrkCannon.baseDuration / this.attackSpeedStat;
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
                execomponent.UpdateEmotionalValue(-1, 1, 0);

                execomponent.UpdateMemoryCode('X');

                if (NetworkServer.active)
                {
                    var rand = UnityEngine.Random.Range(0, 9);
                    characterBody.AddTimedBuff(execomponent.GetDebuffByIndex(rand), 5f);

                }

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
                    base.AddRecoil(-1f * DrkCannon.recoil, -2f * DrkCannon.recoil, -0.5f * DrkCannon.recoil, 0.5f * DrkCannon.recoil);

                    base.characterBody.AddSpreadBloom(1.5f);
                    EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FirePistol2.muzzleEffectPrefab, base.gameObject, this.muzzleString, false);
                    //Util.PlaySound(Sounds.SFXCanon, base.gameObject);
                    base.PlayAnimation("Gesture, Override", "EXEBusterAttack", "attackSpeed", this.duration);

                    new BulletAttack
                    {
                        bulletCount = 1,
                        aimVector = aimRay.direction,
                        origin = aimRay.origin,
                        damage = (DrkCannon.damageCoefficient * this.damageStat + ((base.characterBody.maxHealth - base.characterBody.healthComponent.health) / 2)),
                        damageColorIndex = DamageColorIndex.Default,
                        damageType = DamageType.Generic,
                        falloffModel = BulletAttack.FalloffModel.DefaultBullet,
                        maxDistance = DrkCannon.range,
                        force = DrkCannon.force,
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
                        tracerEffectPrefab = DrkCannon.tracerEffectPrefab,
                        spreadPitchScale = 0f,
                        spreadYawScale = 0f,
                        queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                        hitEffectPrefab = DrkCannon.hitEffectPrefab,
                    }.Fire();
                }
            }
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
