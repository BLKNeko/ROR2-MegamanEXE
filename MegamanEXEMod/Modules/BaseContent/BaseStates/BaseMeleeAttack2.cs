using EntityStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using R2API;
using RoR2;
using RoR2.Audio;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MegamanEXEMod.Modules.BaseStates
{
    public abstract class BaseMeleeAttack2 : BaseSkillState, SteppedSkillDef.IStepSetter
    {
        public int swingIndex;

        protected string hitboxGroupName = "SwordGroup";

        protected DamageType damageType = DamageType.Generic;
        protected float damageCoefficient = 3.5f;
        protected float procCoefficient = 1f;
        protected float pushForce = 300f;
        protected Vector3 bonusForce = Vector3.zero;
        protected float baseDuration = 1f;
        

        protected float attackStartPercentTime = 0.2f;
        protected float attackEndPercentTime = 0.4f;

        protected float earlyExitPercentTime = 0.4f;

        protected float hitStopDuration = 0.012f;
        protected float attackRecoil = 0.75f;
        protected float hitHopVelocity = 4f;

        protected string swingSoundString = "";
        protected string hitSoundString = "";
        protected string muzzleString = "SwingCenter";
        protected string playbackRateParam = "Slash.playbackRate";
        protected GameObject swingEffectPrefab;
        protected GameObject hitEffectPrefab;
        protected NetworkSoundEventIndex impactSound = NetworkSoundEventIndex.Invalid;

        public float duration;
        private bool hasFired;
        private float hitPauseTimer;
        private OverlapAttack attack;
        protected bool inHitPause;
        private bool hasHopped;
        protected float stopwatch;
        protected Animator animator;
        private HitStopCachedState hitStopCachedState;
        private Vector3 storedVelocity;

        private EntityState NextState;
        private float hitResetTime, resetTimer;
        private int amountOfHits = 1;
        private bool shouldResetHit = false;

        private EXEBaseComponent execomponent;
        protected int EMValue = 0;
        protected int EVValue = 0;
        protected float DMGValue = 0f;

        protected bool RollDebuff = false;

        protected string chatMessage = "";
        protected string netNaviName = "";
        protected char chipMemoryCode = ' ';

        public override void OnEnter()
        {
            base.OnEnter();
            duration = baseDuration / attackSpeedStat;
            animator = GetModelAnimator();
            StartAimMode(0.5f + duration, false);
            hitResetTime = duration / amountOfHits;

            PlayAttackAnimation();

            execomponent = GetComponent<EXEBaseComponent>();

            attack = new OverlapAttack();
            attack.damageType = damageType;
            attack.attacker = gameObject;
            attack.inflictor = gameObject;
            attack.teamIndex = GetTeam();
            attack.damage = damageCoefficient * damageStat;
            attack.procCoefficient = procCoefficient;
            attack.hitEffectPrefab = hitEffectPrefab;
            attack.forceVector = bonusForce;
            attack.pushAwayForce = pushForce;
            attack.hitBoxGroup = FindHitBoxGroup(hitboxGroupName);
            attack.isCrit = RollCrit();
            attack.impactSound = impactSound;
        }

        protected virtual void PlayAttackAnimation()
        {
            PlayCrossfade("Gesture, Override", "Slash" + (1 + swingIndex), playbackRateParam, duration, 0.05f);
        }

        public override void OnExit()
        {
            if (inHitPause)
            {
                RemoveHitstop();
            }
            shouldResetHit = false;

            if (RollDebuff && NetworkServer.active)
            {

                var rand = UnityEngine.Random.Range(0, 9);
                characterBody.AddTimedBuff(GetDebuffByIndex(rand), 3f);

                RollDebuff = false;

            }

            if(chipMemoryCode != ' ')
            {
                execomponent.UpdateMemoryCode(chipMemoryCode);

                chipMemoryCode = ' ';

            }

            //execomponent.UpdateModel(base.GetModelTransform().GetComponent<CharacterModel>(), base.GetModelTransform().GetComponent<CharacterModel>().GetComponent<ChildLocator>());

            base.OnExit();
        }

        protected virtual void PlaySwingEffect()
        {
            EffectManager.SimpleMuzzleFlash(swingEffectPrefab, gameObject, muzzleString, false);
        }

        protected virtual void OnHitEnemyAuthority()
        {
            Util.PlaySound(hitSoundString, gameObject);

            if (!hasHopped)
            {
                if (characterMotor && !characterMotor.isGrounded && hitHopVelocity > 0f)
                {
                    SmallHop(characterMotor, hitHopVelocity);
                }

                hasHopped = true;
            }

            if (isAuthority)
                execomponent.UpdateEmotionalValue(EMValue, EVValue, DMGValue);

            ApplyHitstop();
        }

        protected void ApplyHitstop()
        {
            if (!inHitPause && hitStopDuration > 0f)
            {
                storedVelocity = characterMotor.velocity;
                hitStopCachedState = CreateHitStopCachedState(characterMotor, animator, playbackRateParam);
                hitPauseTimer = hitStopDuration / attackSpeedStat;
                inHitPause = true;
            }
        }

        private void FireAttack()
        {
            if (isAuthority)
            {
                if (attack.Fire())
                {
                    OnHitEnemyAuthority();
                }
            }
        }

        private void EnterAttack()
        {
            hasFired = true;
            Util.PlayAttackSpeedSound(swingSoundString, gameObject, attackSpeedStat);

            PlaySwingEffect();

            if (isAuthority)
            {
                AddRecoil(-1f * attackRecoil, -2f * attackRecoil, -0.5f * attackRecoil, 0.5f * attackRecoil);
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            hitPauseTimer -= Time.deltaTime;

            if (shouldResetHit)
            {
                resetTimer += Time.fixedDeltaTime;

                if (resetTimer > hitResetTime)
                {
                    //Debug.Log("Reset!");
                    attack.ResetIgnoredHealthComponents();
                    resetTimer = 0f;
                }

            }

            if (hitPauseTimer <= 0f && inHitPause)
            {
                RemoveHitstop();
            }

            if (!inHitPause)
            {
                stopwatch += Time.deltaTime;
            }
            else
            {
                if (characterMotor) characterMotor.velocity = Vector3.zero;
                if (animator) animator.SetFloat(playbackRateParam, 0f);
            }

            bool fireStarted = stopwatch >= duration * attackStartPercentTime;
            bool fireEnded = stopwatch >= duration * attackEndPercentTime;

            //to guarantee attack comes out if at high attack speed the stopwatch skips past the firing duration between frames
            if (fireStarted && !fireEnded || fireStarted && fireEnded && !hasFired)
            {
                if (!hasFired)
                {
                    EnterAttack();
                }
                FireAttack();
            }

            if (stopwatch >= duration && isAuthority && !base.inputBank.skill4.down)
            {
                //Debug.Log("Back to main");
                //Debug.Log("Nextstate: "+ NextState);
                outer.SetNextStateToMain();
                return;
            }
            else if (stopwatch >= duration && isAuthority && base.inputBank.skill2.down)
            {
                if (NextState != null)
                {
                    //Debug.Log("To Next State");
                    //Debug.Log("Nextstate: " + NextState);
                    outer.SetNextState(NextState);
                    NextState = null;
                    return;
                }
                else
                {
                    //Debug.Log("NextStae suposted null");
                    //Debug.Log("Nextstate: " + NextState);
                    outer.SetNextStateToMain();
                    return;
                }
                
                
            }
        }

        private void RemoveHitstop()
        {
            ConsumeHitStopCachedState(hitStopCachedState, characterMotor, animator);
            inHitPause = false;
            characterMotor.velocity = storedVelocity;
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            if (stopwatch >= duration * earlyExitPercentTime)
            {
                return InterruptPriority.Any;
            }
            return InterruptPriority.Skill;
        }

        public override void OnSerialize(NetworkWriter writer)
        {
            base.OnSerialize(writer);
            writer.Write(swingIndex);
        }

        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);
            swingIndex = reader.ReadInt32();
        }

        public void SetStep(int i)
        {
            swingIndex = i;
        }

        public void SetNextEntityState(EntityState state)
        {
            NextState = state;
        }

        public void SetHitReset(bool reset, int amount)
        {
            shouldResetHit = reset;
            amountOfHits = amount;
        }

        private static BuffDef GetDebuffByIndex(int index)
        {
            BuffDef[] debuffs =
            {
                EXEBuffs.DarkDebuff1,
                EXEBuffs.DarkDebuff2,
                EXEBuffs.DarkDebuff3,
                EXEBuffs.DarkDebuff4,
                EXEBuffs.DarkDebuff5,
                EXEBuffs.DarkDebuff6,
                EXEBuffs.DarkDebuff7,
                EXEBuffs.DarkDebuff8,
                EXEBuffs.DarkDebuff9
            };

            return debuffs[Mathf.Clamp(index, 0, debuffs.Length - 1)];
        }

        public void SendChatMessage(string message)
        {
            Chat.SendBroadcastChat(new Chat.SimpleChatMessage
            {
                baseToken = message
            });
        }

        public string GetNetNaviName(uint skinIndex)
        {
            //return characterBody.modelLocator.modelTransform.GetComponent<ModelSkinController>().skins[skinIndex].name;

            var skinController = characterBody.modelLocator.modelTransform.GetComponent<ModelSkinController>();
            if (skinController && skinIndex < skinController.skins.Length)
            {
                switch (skinIndex)
                {
                    case 0:
                        return "<color=#043db8>Megaman.EXE</color>";
                    break;

                    case 1:
                        return "<color=#cf1919>Protoman.EXE</color>";
                    break;

                    case 2:
                        return "<color=#ff7ade>Roll.EXE</color>";
                    break;

                    case 3:
                        return "<color=#8a4601>Bass.EXE</color>";
                    break;

                    case 4:
                        return "<color=#00d0fa>Megaman.EXE Dive</color>";
                    break;



                }
            }
            return "NetNavi";

        }

    }
}