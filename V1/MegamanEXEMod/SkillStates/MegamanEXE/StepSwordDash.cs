using EntityStates;
using MegamanEXEMod.Modules;
using MegamanEXEMod.SkillStates.BaseStates;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using static RoR2.Chat;

namespace MegamanEXEMod.SkillStates
{
    public class StepSwordDash : BaseSkillState
    {
        public static float damageCoefficient = 1f;
        public static float procCoefficient = 1f;
        public static float baseDuration = 0.95f;
        public static float force = 0f;
        public static float recoil = 0f;
        public static float range = 356f;
        public static GameObject tracerEffectPrefab = Resources.Load<GameObject>("Prefabs/Effects/Tracers/TracerGoldGat");

        private float duration;
        private float fireTime;
        private bool hasFired;
        private string muzzleString;

        private Vector3 EnemyPos;
        private bool canTP = false;
        private float TPTimer = 0f;



        private Transform modelTransform;

        public static GameObject blinkPrefab;

        private float stopwatch;

        private Vector3 dashVector = Vector3.zero;

        public static float smallHopVelocity;

        public static float dashPrepDuration;

        public static float dashDuration = 0.50f;

        public static float speedCoefficient = 18f;

        public static string beginSoundString;

        public static string endSoundString;

        public static float overlapSphereRadius = 1f;

        public static float lollypopFactor;

        private Animator animator;

        private CharacterModel characterModel;

        private HurtBoxGroup hurtboxGroup;

        private bool isDashing;

        private CameraTargetParams.AimRequest aimRequest;






        public override void OnEnter()
        {

            base.OnEnter();
            this.duration = baseDuration / this.attackSpeedStat;
            this.fireTime = 0.2f * this.duration;
            base.characterBody.SetAimTimer(2f);
            this.muzzleString = "Muzzle";

            //base.PlayAnimation("AttackL", "MagnumShoot", "attackspeed", this.duration);
            //Util.PlaySound(Sounds.shoot, base.gameObject);
            //Util.PlaySound(Sounds.TBCharge, base.gameObject);

            ArmHelper.ArmChanger(2);


            PlayAnimation("FullBody, Override", "StepSwordDash", "attackSpeed", this.duration);



            // Util.PlaySound(beginSoundString, base.gameObject);
            modelTransform = GetModelTransform();
            if ((bool)base.cameraTargetParams)
            {
                aimRequest = base.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Aura);
            }
            if ((bool)modelTransform)
            {
                animator = modelTransform.GetComponent<Animator>();
                characterModel = modelTransform.GetComponent<CharacterModel>();
            }
            if (base.isAuthority)
            {
                SmallHop(base.characterMotor, smallHopVelocity);
            }
            if (NetworkServer.active)
            {
                base.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
            }
            //PlayAnimation("FullBody, Override", "EvisPrep", "EvisPrep.playbackRate", dashPrepDuration);
            dashVector = base.inputBank.aimDirection;
            base.characterDirection.forward = dashVector;

        }

        public override void OnExit()
        {
            base.PlayAnimation("Gesture, Override", "BufferEmpty", "attackSpeed", this.duration);
            base.PlayAnimation("FullBody, Override", "BufferEmpty", "attackSpeed", this.duration);
            //Util.PlaySound(endSoundString, base.gameObject);
            base.characterMotor.velocity *= 0.1f;
            SmallHop(base.characterMotor, smallHopVelocity);
            aimRequest?.Dispose();
            //PlayAnimation("FullBody, Override", "EvisLoopExit");
            if (NetworkServer.active)
            {
                base.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
            }

            base.OnExit();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            stopwatch += Time.fixedDeltaTime;
            if (stopwatch > dashPrepDuration && !isDashing)
            {
                isDashing = true;
                dashVector = base.inputBank.aimDirection;
                //CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
                /*
                PlayCrossfade("FullBody, Override", "EvisLoop", 0.1f);
                if ((bool)modelTransform)
                {
                    TemporaryOverlay temporaryOverlay = modelTransform.gameObject.AddComponent<TemporaryOverlay>();
                    temporaryOverlay.duration = 0.6f;
                    temporaryOverlay.animateShaderAlpha = true;
                    temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                    temporaryOverlay.destroyComponentOnEnd = true;
                    temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matHuntressFlashBright");
                    temporaryOverlay.AddToCharacerModel(modelTransform.GetComponent<CharacterModel>());
                    TemporaryOverlay temporaryOverlay2 = modelTransform.gameObject.AddComponent<TemporaryOverlay>();
                    temporaryOverlay2.duration = 0.7f;
                    temporaryOverlay2.animateShaderAlpha = true;
                    temporaryOverlay2.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                    temporaryOverlay2.destroyComponentOnEnd = true;
                    temporaryOverlay2.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matHuntressFlashExpanded");
                    temporaryOverlay2.AddToCharacerModel(modelTransform.GetComponent<CharacterModel>());
                }
                */
            }
            bool flag = stopwatch >= dashDuration + dashPrepDuration;
            if (isDashing)
            {
                if ((bool)base.characterMotor && (bool)base.characterDirection)
                {
                    base.characterMotor.rootMotion += dashVector * (((moveSpeedStat * speedCoefficient) / 2) * Time.fixedDeltaTime);
                }
                if (base.isAuthority)
                {
                    Collider[] array = Physics.OverlapSphere(base.transform.position, base.characterBody.radius + overlapSphereRadius * (flag ? lollypopFactor : 1f), LayerIndex.entityPrecise.mask);
                    for (int i = 0; i < array.Length; i++)
                    {
                        HurtBox component = array[i].GetComponent<HurtBox>();
                        if ((bool)component && component.healthComponent != base.healthComponent)
                        {



                            StepSwordAttack SSA = new StepSwordAttack();
                            this.outer.SetNextState(SSA);
                            return;



                            //TBSkillDMG nextState = new TBSkillDMG();
                            //outer.SetNextState(nextState);
                            //return;
                        }
                    }
                }
            }
            if (flag && base.isAuthority)
            {
                outer.SetNextStateToMain();
            }

        }



        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
    }
}