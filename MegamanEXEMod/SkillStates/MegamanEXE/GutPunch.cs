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
    public class GutPunch : BaseSkillState
    {
        public static float damageCoefficient = 3.5f;
        public static float buffDamageCoefficient = 1f;
        public float baseDuration = 0.5f;
        public static float attackRecoil = 0.5f;
        public static float hitHopVelocity = 5f;
        public static float baseEarlyExit = 0.25f;
        public int swingIndex;

        public static GameObject hitEffectPrefab = Resources.Load<GameObject>("prefabs/effects/impacteffects/ImpactMercSwing");
        //public static GameObject hitEffectPrefab = Resources.Load<GameObject>("prefabs/effects/impacteffects/lunarneedledamageeffect");

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
        //private PaladinSwordController swordController;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            this.earlyExitDuration = GutPunch.baseEarlyExit / this.attackSpeedStat;
            this.hasFired = false;
            this.animator = base.GetModelAnimator();
            //this.swordController = base.GetComponent<PaladinSwordController>();
            base.StartAimMode(0.5f + this.duration, false);
            //base.characterBody.isSprinting = false;

            //base.characterBody.healthComponent.AddBarrier(base.characterBody.damage);

            //Chat.SendBroadcastChat(new SimpleChatMessage { baseToken = "<color=#e5eefc>{0}</color>", paramTokens = new[] { "HitCombo2" } });

            ArmHelper.ArmChanger(2);

            /*
            GameObject.Find("EXEBuster").transform.localScale = new Vector3(0, 0, 0);
            GameObject.Find("EXEBuster").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("EXESword").transform.localScale = new Vector3(1, 1, 1);
            GameObject.Find("EXESword").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("EXESword").GetComponent<MeshRenderer>().material = Assets.MatCyberSwordDefault;
            */

            HitBoxGroup hitBoxGroup = null;
            Transform modelTransform = base.GetModelTransform();

            if (modelTransform)
            {
                hitBoxGroup = Array.Find<HitBoxGroup>(modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == "EXESwordHB");
            }

            //if (this.swingIndex == 0) base.PlayAnimation("Gesture, Override", "ZSlash1", "FireArrow.playbackRate", this.duration);
            //else base.PlayAnimation("Gesture, Override", "ZSlash1", "FireArrow.playbackRate", this.duration);
            base.PlayAnimation("Gesture, Override", "GutsPunch", "attackSpeed", this.duration);

            //Util.PlaySound(Sounds.Second, base.gameObject);


            Ray aimRay = base.GetAimRay();
            Vector3 forwardDirection;
            forwardDirection = aimRay.direction;


            float dmg = GutPunch.damageCoefficient;
            //if (this.swordController && this.swordController.swordActive) dmg = Slash.buffDamageCoefficient;

            this.attack = new OverlapAttack();
            //this.attack.damageType = (Util.CheckRoll(84f, base.characterBody.master) ? DamageType.Stun1s : DamageType.SlowOnHit);
            this.attack.damageType = DamageType.Stun1s;
            this.attack.attacker = base.gameObject;
            this.attack.inflictor = base.gameObject;
            this.attack.teamIndex = base.GetTeam();
            this.attack.damage = dmg * this.damageStat;
            this.attack.procCoefficient = 1;
            this.attack.hitEffectPrefab = GutPunch.hitEffectPrefab;
            this.attack.forceVector = forwardDirection;
            this.attack.pushAwayForce = 8000f;
            this.attack.hitBoxGroup = hitBoxGroup;
            this.attack.isCrit = base.RollCrit();
            this.attack.impactSound = MegamanEXEMod.Modules.Assets.swordHitSoundEvent.index;



        }

        public override void OnExit()
        {
            base.PlayAnimation("Gesture, Override", "BufferEmpty", "attackSpeed", this.duration);

            SyncNetworkExe.MemoryCode = SyncNetworkExe.MemoryCode + "X";

            base.OnExit();
        }

        public void FireAttack()
        {
            if (!this.hasFired)
            {
                this.hasFired = true;
                //Util.PlayScaledSound(EntityStates.Merc.GroundLight.comboAttackSoundString, base.gameObject, 0.5f);
                //Util.PlaySound(Sounds.zSlash1Voice, base.gameObject);
                //Util.PlaySound(Sounds.zSlash1SFX, base.gameObject);
                //Util.PlaySound(Sounds.hit2, base.gameObject);
                
                //Util.PlaySound(Sounds.GSAttackSFX, base.gameObject);

                //string muzzleString = null;
                // if (this.swingIndex == 0) muzzleString = "SwingLeft";
                //else muzzleString = "SwingRight";


                // EffectManager.SimpleMuzzleFlash(Modules.Assets.swordSwing, base.gameObject, muzzleString, true);
                EffectManager.SimpleMuzzleFlash(EntityStates.Merc.GroundLight.comboSwingEffectPrefab, base.gameObject, "SwingLeft", true);

                if (base.isAuthority)
                {
                    base.AddRecoil(-1f * GutPunch.attackRecoil, -2f * GutPunch.attackRecoil, -0.5f * GutPunch.attackRecoil, 0.5f * GutPunch.attackRecoil);

                    Ray aimRay = base.GetAimRay();

                    //if (this.swordController && this.swordController.swordActive)
                    //{
                    //    ProjectileManager.instance.FireProjectile(Modules.Projectiles.swordBeam, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, StaticValues.beamDamageCoefficient * this.damageStat, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.WeakPoint, null, StaticValues.beamSpeed);
                    // }

                    if (this.attack.Fire())
                    {
                        //Util.PlaySound(EntityStates.Merc.GroundLight.hitSoundString, base.gameObject);
                        //Util.PlaySound(MinerPlugin.Sounds.Hit, base.gameObject);


                        //Debug.Log("MoraleGauge Value:");
                        //Debug.Log(AdeptRough.MoraleGauge);


                        if (!this.hasHopped)
                        {
                            if (base.characterMotor && !base.characterMotor.isGrounded)
                            {
                                base.SmallHop(base.characterMotor, GutPunch.hitHopVelocity);
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
            return InterruptPriority.Frozen;
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