using EntityStates;
using MegamanEXEMod.Modules.BaseStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.ParticleSystem.PlaybackState;

namespace MegamanEXEMod.Survivors.MegamanEXE.SkillStates
{
    public class Yoyo : BaseSkillState
    {
        public float damageCoefficient = 1.5f;
        public float baseDuration = 0.5f;
        public float recoil = 1f;
        public static GameObject tracerEffectPrefab = Resources.Load<GameObject>("Prefabs/Effects/Tracers/TracerToolbotRebar");

        public static float force = 1000f;

        private float duration;
        private float fireDuration;
        private bool hasFired;
        private Animator animator;
        private string muzzleString;
        private string muzzleString2;

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



            base.PlayAnimation("Gesture, Override", "ShootPose", "attackSpeed", this.duration);
        }

        public override void OnExit()
        {

            execomponent.UpdateMemoryCode('Y');

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
                    //EffectManager.SimpleMuzzleFlash(EntityStates.Mage.Weapon.FireLaserbolt.impactEffectPrefab, base.gameObject, this.muzzleString, false);
                    AkSoundEngine.PostEvent(EXEStaticValues.SFXGun, this.gameObject);

                    base.PlayAnimation("Gesture, Override", "EXEBusterAttack", "attackSpeed", this.duration);
                    //ProjectileManager.instance.FireProjectile(Modules.Projectiles.YoyoProjectile, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageCoefficient * this.damageStat, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);

                    FireProjectileInfo YoyoProjectille = new FireProjectileInfo();
                    YoyoProjectille.projectilePrefab = EXEAssets.yoyoProjectilePrefab;
                    YoyoProjectille.position = aimRay.origin;
                    YoyoProjectille.rotation = Util.QuaternionSafeLookRotation(aimRay.direction);
                    YoyoProjectille.owner = gameObject;
                    YoyoProjectille.damage = damageCoefficient;
                    YoyoProjectille.force = force;
                    YoyoProjectille.crit = RollCrit();
                    YoyoProjectille.damageColorIndex = DamageColorIndex.Luminous;

                    ProjectileManager.instance.FireProjectile(YoyoProjectille);

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
