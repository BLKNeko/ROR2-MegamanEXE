using EntityStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using static RoR2.BulletAttack;

namespace MegamanEXEMod.Survivors.MegamanEXE.SkillStates
{
    public class Spreader : BaseSkillState
    {
        public static float damageCoefficient = EXEStaticValues.SpreaderSkillDefDamageCoefficient;
        public static float procCoefficient = 1f;
        public static float baseDuration = 0.5f;
        public static float force = 1000f;
        public static float recoil = 3f;
        public static float range = 256f;
        public static GameObject tracerEffectPrefab = Resources.Load<GameObject>("prefabs/effects/tracers/TracerBanditPistol");
        public static GameObject hitEffectPrefab = Resources.Load<GameObject>("Prefabs/Effects/ImpactEffects/ImpactPotMobileCannon");



        private float duration;
        private float fireDuration;
        private bool hasFired;
        private Animator animator;
        private string muzzleString;

        private EXEBaseComponent execomponent;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = Cannon.baseDuration / this.attackSpeedStat;
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
                if (isAuthority)
                {

                    execomponent.UpdateMemoryCode('X');
                }

            }

            base.OnExit();
        }

        private void Fire()
        {
            if (!this.hasFired)
            {
                this.hasFired = true;

                

                if (base.isAuthority)
                {
                    Ray aimRay = base.GetAimRay();
                    //base.AddRecoil(-1f * Cannon.recoil, -2f * Cannon.recoil, -0.5f * Cannon.recoil, 0.5f * Cannon.recoil);

                    base.characterBody.AddSpreadBloom(1.5f);
                    EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FirePistol2.muzzleEffectPrefab, base.gameObject, this.muzzleString, false);
                    AkSoundEngine.PostEvent(EXEStaticValues.SFXGun, this.gameObject);
                    base.PlayAnimation("Gesture, Override", "EXEBusterAttack", "attackSpeed", this.duration);

                    new BulletAttack
                    {
                        bulletCount = 1,
                        aimVector = aimRay.direction,
                        origin = aimRay.origin,
                        damage = Cannon.damageCoefficient * this.damageStat,
                        damageColorIndex = DamageColorIndex.Default,
                        damageType = DamageType.Generic,
                        falloffModel = BulletAttack.FalloffModel.DefaultBullet,
                        maxDistance = Cannon.range,
                        force = Cannon.force,
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
                        tracerEffectPrefab = Cannon.tracerEffectPrefab,
                        spreadPitchScale = 0f,
                        spreadYawScale = 0f,
                        queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                        hitEffectPrefab = Cannon.hitEffectPrefab,
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

            BlastAttack spreaderBlast= new BlastAttack();
            spreaderBlast.attacker = base.gameObject;
            spreaderBlast.inflictor = base.gameObject;
            spreaderBlast.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
            spreaderBlast.baseDamage = damageCoefficient * damageStat;
            spreaderBlast.baseForce = 800f;
            spreaderBlast.position = gameObject.transform.position;
            spreaderBlast.radius = 20f;
            spreaderBlast.bonusForce = new Vector3(1f, 1f, 1f);
            spreaderBlast.damageType |= DamageType.Generic;
            spreaderBlast.damageColorIndex = DamageColorIndex.Default;

            spreaderBlast.Fire();

            EffectData effectData = new EffectData
            {
                origin = hitlnfo.point,
                scale = 1f,
            };
            EffectManager.SpawnEffect(EXEAssets.VfxSpreaderExplosion1, effectData, false);

            return result;
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if ((base.fixedAge >= this.fireDuration))
            {
                Fire();
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
