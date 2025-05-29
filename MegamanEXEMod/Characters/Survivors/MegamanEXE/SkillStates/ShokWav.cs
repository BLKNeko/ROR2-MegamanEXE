using EntityStates;
using ExtraSkillSlots;
using MegamanEXEMod.Modules.BaseStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using RoR2;
using RoR2.Projectile;
using RoR2.Skills;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.ParticleSystem.PlaybackState;

namespace MegamanEXEMod.Survivors.MegamanEXE.SkillStates
{
    public class ShokWav : BaseSkillState
    {
        public float damageCoefficient = 1.8f;
        public float baseDuration = 0.5f;
        public float recoil = 1f;
        public static GameObject tracerEffectPrefab = Resources.Load<GameObject>("Prefabs/Effects/Tracers/TracerToolbotRebar");

        private float duration;
        private float fireDuration;
        private bool hasFired;
        private Animator animator;
        private string muzzleString;
        private string muzzleString2;

        public static float force = 2500f;

        private EXEBaseComponent execomponent;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration;
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


            //Util.PlaySound(Modules.Sounds.vileFragDrop, base.gameObject);
        }

        public override void OnExit()
        {

            if (isAuthority)
            {
                execomponent.UpdateEmotionalValue(1, 0, 0);

                execomponent.UpdateMemoryCode('X');
            }

            base.OnExit();
        }

        private void FireES()
        {
            if (!this.hasFired)
            {
                this.hasFired = true;

                if (base.isAuthority)
                {

                    base.characterBody.AddSpreadBloom(0.15f);
                    Ray aimRay = base.GetAimRay();
                    EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FireBarrage.effectPrefab, base.gameObject, this.muzzleString, false);

                    base.PlayAnimation("Gesture, Override", "EXEBusterAttack", "attackSpeed", this.duration);
                    //ProjectileManager.instance.FireProjectile(Modules.Projectiles.ShokWaveProjectile, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageCoefficient * this.damageStat, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);

                    FireProjectileInfo ShockwaveProjectille = new FireProjectileInfo();
                    ShockwaveProjectille.projectilePrefab = EXEAssets.shockwaveProjectilePrefab;
                    ShockwaveProjectille.position = aimRay.origin;
                    ShockwaveProjectille.rotation = Util.QuaternionSafeLookRotation(aimRay.direction);
                    ShockwaveProjectille.owner = gameObject;
                    ShockwaveProjectille.damage = damageCoefficient;
                    ShockwaveProjectille.force = force;
                    ShockwaveProjectille.crit = RollCrit();
                    ShockwaveProjectille.damageColorIndex = DamageColorIndex.Default;

                    ProjectileManager.instance.FireProjectile(ShockwaveProjectille);

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
