using MegamanEXEMod.Modules;
using EntityStates;
using RoR2;
using System;
using UnityEngine;
using UnityEngine.Networking;
using static RoR2.Chat;
using MegamanEXEMod.SkillStates.BaseStates;

namespace MegamanEXEMod.SkillStates
{
    public class FireSwrd : BaseSkillState
    {
        public static float damageCoefficient = 5.5f;
        public static float buffDamageCoefficient = 1f;
        public float baseDuration = 0.5f;
        public static float attackRecoil = 0.5f;
        public static float hitHopVelocity = 5f;
        public static float baseEarlyExit = 0.25f;
        public int swingIndex;

        public static GameObject hitEffectPrefab = Resources.Load<GameObject>("prefabs/effects/impacteffects/ImpactMercSwing");

        public GameObject tracerEffectPrefab = Resources.Load<GameObject>("prefabs/effects/omnieffect/OmniImpactVFXSlashMerc");

        private float earlyExitDuration;
        private float duration;
        private bool hasFired;
        private float hitPauseTimer;
        private OverlapAttack attack;
        private bool inHitPause;
        private bool hasHopped;
        private float stopwatch;
        private Animator animator;
        private BaseState.HitStopCachedState hitStopCachedState;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            this.earlyExitDuration = FireSwrd.baseEarlyExit / this.attackSpeedStat;
            this.hasFired = false;
            this.animator = base.GetModelAnimator();
            base.StartAimMode(0.5f + this.duration, false);


            ArmHelper.ArmChanger(3);

            EffectManager.SimpleMuzzleFlash(Modules.Assets.VfxSwordFire, base.gameObject, "SwordMZ", true);
            EffectManager.SimpleMuzzleFlash(Modules.Assets.VfxSwordFire, base.gameObject, "SwordMZ", true);
            EffectManager.SimpleMuzzleFlash(Modules.Assets.VfxSwordFire, base.gameObject, "SwordMZ", true);

            HitBoxGroup hitBoxGroup = null;
            Transform modelTransform = base.GetModelTransform();

            if (modelTransform)
            {
                hitBoxGroup = Array.Find<HitBoxGroup>(modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == "EXESwordHB");
            }


            base.PlayAnimation("Gesture, Override", "SlashV", "attackSpeed", this.duration);

            Util.PlaySound(Sounds.SwordSwing, base.gameObject);


            Ray aimRay = base.GetAimRay();
            Vector3 forwardDirection;
            forwardDirection = aimRay.direction;


            float dmg = FireSwrd.damageCoefficient;

            this.attack = new OverlapAttack();
            this.attack.damageType = DamageType.IgniteOnHit;
            this.attack.attacker = base.gameObject;
            this.attack.inflictor = base.gameObject;
            this.attack.teamIndex = base.GetTeam();
            this.attack.damage = dmg * this.damageStat;
            this.attack.procCoefficient = 1;
            this.attack.hitEffectPrefab = FireSwrd.hitEffectPrefab;
            this.attack.forceVector = forwardDirection;
            this.attack.pushAwayForce = 500f;
            this.attack.hitBoxGroup = hitBoxGroup;
            this.attack.isCrit = base.RollCrit();
            this.attack.impactSound = MegamanEXEMod.Modules.Assets.swordHitSoundEvent.index;



        }

        public override void OnExit()
        {
            base.PlayAnimation("Gesture, Override", "BufferEmpty", "attackSpeed", this.duration);

            SyncNetworkExe.MemoryCode = SyncNetworkExe.MemoryCode + "S";

            base.OnExit();
        }

        public void FireAttack()
        {
            if (!this.hasFired)
            {
                this.hasFired = true;

                EffectManager.SimpleMuzzleFlash(EntityStates.Merc.GroundLight.comboSwingEffectPrefab, base.gameObject, "SwingLeft", true);

                if (base.isAuthority)
                {
                    base.AddRecoil(-1f * FireSwrd.attackRecoil, -2f * FireSwrd.attackRecoil, -0.5f * FireSwrd.attackRecoil, 0.5f * FireSwrd.attackRecoil);

                    Ray aimRay = base.GetAimRay();


                    if (this.attack.Fire())
                    {

                        SyncNetworkExe.EmotionValue++;

                        if (!this.hasHopped)
                        {
                            if (base.characterMotor && !base.characterMotor.isGrounded)
                            {
                                base.SmallHop(base.characterMotor, FireSwrd.hitHopVelocity);
                            }

                            this.hasHopped = true;
                        }

                        if (!this.inHitPause)
                        {
                            this.hitStopCachedState = base.CreateHitStopCachedState(base.characterMotor, this.animator, "FireArrow.playbackRate");
                            this.hitPauseTimer = (0.6f * EntityStates.Merc.GroundLight.hitPauseDuration) / this.attackSpeedStat;
                            this.inHitPause = true;
                        }
                    }
                }
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            this.hitPauseTimer -= Time.fixedDeltaTime;

            if (this.hitPauseTimer <= 0f && this.inHitPause)
            {
                base.ConsumeHitStopCachedState(this.hitStopCachedState, base.characterMotor, this.animator);
                this.inHitPause = false;
            }

            if (!this.inHitPause)
            {
                this.stopwatch += Time.fixedDeltaTime;
            }
            else
            {
                if (base.characterMotor) base.characterMotor.velocity = Vector3.zero;
                if (this.animator) this.animator.SetFloat("FireArrow.playbackRate", 1f);
            }

            if (this.stopwatch >= this.duration * 0.2f)
            {
                this.FireAttack();
            }


            if (base.fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }

        public override void OnSerialize(NetworkWriter writer)
        {
            base.OnSerialize(writer);
            writer.Write(this.swingIndex);
        }

        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);
            this.swingIndex = reader.ReadInt32();
        }
    }
}