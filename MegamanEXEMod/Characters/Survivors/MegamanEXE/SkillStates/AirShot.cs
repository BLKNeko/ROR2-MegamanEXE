using EntityStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using static RoR2.BulletAttack;

namespace MegamanEXEMod.Survivors.MegamanEXE.SkillStates
{
    public class AirShot : BaseSkillState
    {
        public static float damageCoefficient = 1.2f;
        public static float procCoefficient = 1f;
        public static float baseDuration = 0.2f;
        public static float force = 15000f;
        public static float recoil = 3f;
        public static float range = 256f;
        public static GameObject tracerEffectPrefab = Resources.Load<GameObject>("prefabs/effects/tracers/TracerBanditPistol");
        public static GameObject hitEffectPrefab = Resources.Load<GameObject>("Prefabs/Effects/ImpactEffects/AffixWhiteImpactEffect");



        private float duration;
        private float fireDuration;
        private bool hasFired;
        private Animator animator;
        private string muzzleString;

        private EXEBaseComponent execomponent;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = AirShot.baseDuration / this.attackSpeedStat;
            this.fireDuration = 0.25f * this.duration;
            base.characterBody.SetAimTimer(2f);
            this.animator = base.GetModelAnimator();
            this.muzzleString = "BusterMZ";

            execomponent = GetComponent<EXEBaseComponent>();

            ////ArmHelper.ArmChanger(1);

        }

        public override void OnExit()
        {

            ////SyncNetworkExe.MemoryCode = ////SyncNetworkExe.MemoryCode + "X";
            
            //Verificar de criar um AdvAirshot
            execomponent.UpdateMemoryCode('A');

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
                    base.AddRecoil(-1f * AirShot.recoil, -2f * AirShot.recoil, -0.5f * AirShot.recoil, 0.5f * AirShot.recoil);

                    base.characterBody.AddSpreadBloom(1.5f);
                    EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FirePistol2.muzzleEffectPrefab, base.gameObject, this.muzzleString, false);
                    Util.PlaySound("HenryShootPistol", base.gameObject);
                    PlayAnimation("Gesture, Override", "EXEBusterAttack", "attackSpeed", this.duration);

                    new BulletAttack
                    {
                        bulletCount = 1,
                        aimVector = aimRay.direction,
                        origin = aimRay.origin,
                        damage = AirShot.damageCoefficient * this.damageStat,
                        damageColorIndex = DamageColorIndex.Default,
                        damageType = DamageType.Generic,
                        falloffModel = BulletAttack.FalloffModel.DefaultBullet,
                        maxDistance = AirShot.range,
                        force = AirShot.force,
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
                        tracerEffectPrefab = AirShot.tracerEffectPrefab,
                        spreadPitchScale = 0f,
                        spreadYawScale = 0f,
                        queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                        hitEffectPrefab = AirShot.hitEffectPrefab,
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
