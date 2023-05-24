using EntityStates;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using ExtraSkillSlots;
using MegamanEXEMod.Modules.Survivors;
using UnityEngine.Networking;
using MegamanEXEMod.Modules;
using MegamanEXEMod.SkillStates.BaseStates;

namespace MegamanEXEMod.SkillStates
{
    public class BugFix : BaseSkillState
    {

        public static float BaseDuration = 0.1f;
        private bool Fix = false;

        private Animator animator;



        public override void OnEnter()
        {
            base.OnEnter();
            this.animator = base.GetModelAnimator();

            

        }

        public void ApplyFix()
        {

            SyncNetworkExe.EvilEmotionValue = 0;

            if (SyncNetworkExe.EmotionValue < 25)
                SyncNetworkExe.EmotionValue = 25;

            if (NetworkServer.active)
            {
                base.characterBody.RemoveBuff(RoR2Content.Buffs.OnFire);
                base.characterBody.RemoveBuff(RoR2Content.Buffs.Slow50);
                base.characterBody.RemoveBuff(RoR2Content.Buffs.Slow60);
                base.characterBody.RemoveBuff(RoR2Content.Buffs.Slow80);
                base.characterBody.RemoveBuff(RoR2Content.Buffs.Poisoned);
                base.characterBody.RemoveBuff(RoR2Content.Buffs.Weak);
                base.characterBody.helfireLifetime = 0f;


                //base.characterBody.RemoveBuff(Modules.Buffs.DarkDebuff);
                base.characterBody.RemoveOldestTimedBuff(Modules.Buffs.DarkDebuff);

            }


            Fix = true;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();


            if (!Fix)
            {
                ApplyFix();
            }
            else
            {
                Fix = false;
                this.outer.SetNextStateToMain();
            }


        }


        public override void OnExit()
        {

            SyncNetworkExe.MemoryCode = SyncNetworkExe.MemoryCode + "X";

            base.OnExit();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }

        public override void OnSerialize(NetworkWriter writer)
        {
            base.OnSerialize(writer);

        }

        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);

        }

        
    }
}