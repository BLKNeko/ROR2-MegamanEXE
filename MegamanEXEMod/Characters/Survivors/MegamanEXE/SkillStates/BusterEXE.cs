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

    public class BusterEXE : BaseChargePrimary
    {

        private EXEBaseComponent execomponent;

        public override void OnEnter()
        {
            base.OnEnter();
            damageCoefficient = EXEStaticValues.EXEBusterDamageCoefficient;
            procCoefficient = 1f;
            baseDuration = 0.4f;
            firePercentTime = 0.0f;
            force = 500f;
            recoil = 2f;
            range = 500f;
            muzzleString = "BusterMuzzPos";

            duration = baseDuration / attackSpeedStat;
            fireTime = firePercentTime * duration;
            characterBody.SetAimTimer(1f);
            hitEffectPrefab = Resources.Load<GameObject>("Prefabs/Effects/ImpactEffects/HitsparkCommandoFMJ");
            muzzleEffectPrefab = Resources.Load<GameObject>("Prefabs/Effects/MuzzleFlashes/MuzzleflashFMJ");

            execomponent = GetComponent<EXEBaseComponent>();

            execomponent.ChangeBusterArm(
                GetModelTransform(),
                GetModelTransform().GetComponent<CharacterModel>(),
                GetModelTransform().GetComponent<CharacterModel>().GetComponent<ChildLocator>(),
                ((int)characterBody.skinIndex),
                0);

        }

        public override void OnExit()
        {
            base.OnExit();

            //execomponent.UpdateModel(base.GetModelTransform().GetComponent<CharacterModel>(), base.GetModelTransform().GetComponent<CharacterModel>().GetComponent<ChildLocator>());

        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

        }

        protected override void FireSimpleBullet()
        {
            if (!hasFired)
            {
                hasFired = true;

                if (isAuthority)
                {

                    characterBody.AddSpreadBloom(0.4f);
                    EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FirePistol2.muzzleEffectPrefab, gameObject, muzzleString, false);
                    //Util.PlaySound(XStaticValues.X_Simple_Bullet, gameObject);
                    PlayAnimation("Gesture, Override", "EXEBusterAttack", "attackSpeed", this.duration);

                    Ray aimRay = GetAimRay();

                    new BulletAttack
                    {
                        bulletCount = 1,
                        aimVector = aimRay.direction,
                        origin = aimRay.origin,
                        damage = damageCoefficient * damageStat,
                        damageColorIndex = DamageColorIndex.Default,
                        damageType = DamageTypeCombo.GenericPrimary,
                        falloffModel = BulletAttack.FalloffModel.None,
                        maxDistance = range,
                        force = force,
                        hitMask = LayerIndex.CommonMasks.bullet,
                        minSpread = 0f,
                        maxSpread = 0f,
                        isCrit = RollCrit(),
                        owner = gameObject,
                        muzzleName = muzzleString,
                        smartCollision = true,
                        procChainMask = default,
                        procCoefficient = procCoefficient,
                        radius = 0.75f,
                        sniper = false,
                        stopperMask = LayerIndex.CommonMasks.bullet,
                        weapon = null,
                        tracerEffectPrefab = tracerEffectPrefab,
                        spreadPitchScale = 1f,
                        spreadYawScale = 1f,
                        queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                        hitEffectPrefab = EntityStates.Commando.CommandoWeapon.FireShotgun.hitEffectPrefab,
                        hitCallback = BulletHitCallback,
                    }.Fire();
                }
            }
        }

        

        protected override void FireMediumBullet()
        {
            if (!hasFired)
            {
                hasFired = true;


                if (isAuthority)
                {

                    characterBody.AddSpreadBloom(0.4f);
                    EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FirePistol2.muzzleEffectPrefab, gameObject, muzzleString, false);
                    //Util.PlaySound(XStaticValues.X_Simple_Bullet, gameObject);
                    PlayAnimation("Gesture, Override", "EXEBusterAttack", "attackSpeed", this.duration);

                    Ray aimRay = GetAimRay();

                    new BulletAttack
                    {
                        bulletCount = 1,
                        aimVector = aimRay.direction,
                        origin = aimRay.origin,
                        damage = (damageCoefficient * EXEStaticValues.XMidChargeDamageCoefficient) * damageStat,
                        damageColorIndex = DamageColorIndex.Default,
                        damageType = DamageTypeCombo.GenericPrimary,
                        falloffModel = BulletAttack.FalloffModel.None,
                        maxDistance = range,
                        force = force,
                        hitMask = LayerIndex.CommonMasks.bullet,
                        minSpread = 0f,
                        maxSpread = 0f,
                        isCrit = RollCrit(),
                        owner = gameObject,
                        muzzleName = muzzleString,
                        smartCollision = true,
                        procChainMask = default,
                        procCoefficient = procCoefficient,
                        radius = 0.75f,
                        sniper = false,
                        stopperMask = LayerIndex.CommonMasks.bullet,
                        weapon = null,
                        tracerEffectPrefab = tracerEffectPrefab,
                        spreadPitchScale = 1f,
                        spreadYawScale = 1f,
                        queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                        hitEffectPrefab = EntityStates.Commando.CommandoWeapon.FireShotgun.hitEffectPrefab,
                        hitCallback = BulletHitCallback,
                    }.Fire();
                }
            }
        }

        protected override void FireChargedBullet()
        {
            if (!hasFired)
            {
                hasFired = true;

                if (isAuthority)
                {

                    characterBody.AddSpreadBloom(0.4f);
                    EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FirePistol2.muzzleEffectPrefab, gameObject, muzzleString, false);
                    //Util.PlaySound(XStaticValues.X_Simple_Bullet, gameObject);
                    PlayAnimation("Gesture, Override", "EXEBusterAttack", "attackSpeed", this.duration);

                    Ray aimRay = GetAimRay();

                    new BulletAttack
                    {
                        bulletCount = 1,
                        aimVector = aimRay.direction,
                        origin = aimRay.origin,
                        damage = (damageCoefficient * EXEStaticValues.XFullChargeDamageCoefficient) * damageStat,
                        damageColorIndex = DamageColorIndex.Default,
                        damageType = DamageTypeCombo.GenericPrimary,
                        falloffModel = BulletAttack.FalloffModel.None,
                        maxDistance = range,
                        force = force,
                        hitMask = LayerIndex.CommonMasks.bullet,
                        minSpread = 0f,
                        maxSpread = 0f,
                        isCrit = RollCrit(),
                        owner = gameObject,
                        muzzleName = muzzleString,
                        smartCollision = true,
                        procChainMask = default,
                        procCoefficient = procCoefficient,
                        radius = 0.75f,
                        sniper = false,
                        stopperMask = LayerIndex.CommonMasks.bullet,
                        weapon = null,
                        tracerEffectPrefab = tracerEffectPrefab,
                        spreadPitchScale = 1f,
                        spreadYawScale = 1f,
                        queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                        hitEffectPrefab = EntityStates.Commando.CommandoWeapon.FireShotgun.hitEffectPrefab,
                        hitCallback = BulletHitCallback,
                    }.Fire();
                }
            }
        }

        private bool BulletHitCallback(BulletAttack bulletAttack, ref BulletAttack.BulletHit hitInfo)
        {
            var result = BulletAttack.defaultHitCallback(bulletAttack, ref hitInfo);
            var hurtbox = hitInfo.hitHurtBox;

            if (hurtbox)
            {
                //Debug.Log("Hit the enemy");

                ////SyncNetworkExe.EmotionValue++;

                if(isAuthority)
                    execomponent.UpdateEmotionalValue(1, 0, 0);

                //Debug.Log("Emotion value:" + //SyncNetworkExe.EmotionValue);

            }
            else
            {
                //Debug.Log("Miss the enemy");

                ////SyncNetworkExe.EmotionValue--;

                if (isAuthority)
                    execomponent.UpdateEmotionalValue(-1, 0, 0);

                //Debug.Log("Emotion value:" + //SyncNetworkExe.EmotionValue);

            }


            return result;
        }

    }
}