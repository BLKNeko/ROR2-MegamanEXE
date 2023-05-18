using EntityStates;
using MegamanEXEMod.Modules;
using MegamanEXEMod.Modules.Survivors;
using RoR2;
using RoR2.Audio;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace MegamanEXEMod.SkillStates.BaseStates
{
    public class SyncNetworkExe : GenericCharacterMain
    {

        private Animator animator;

        public static string MemoryCode = "";
        public static string MemoryCodeCheck = "";

        public static int EmotionValue = 25;
        public static float EvilEmotionValue = 0f;

        public static bool CanDrkDrain = true;
        private float DrkDrainTimer = 0f;

        private float EvilTimer = 0f;



        public override void OnEnter()
        {
            base.OnEnter();

        }
        public override void OnExit()
        {
            base.OnExit();
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            AdvanceProgram();

            Debug.Log("EvilMotionValue:" + EvilEmotionValue);

            if (EvilTimer >= 100f)
            {

                if (EvilEmotionValue > 0)
                    EvilEmotionValue--;

                EvilTimer = 0;

            }
            else
                EvilTimer += Time.fixedDeltaTime;

            //EMOTION BUFFS
            if (EvilEmotionValue >= 10)
            {

                if (NetworkServer.active)
                {
                    base.characterBody.RemoveBuff(Modules.Buffs.FullSyncBuff);
                    base.characterBody.RemoveBuff(Modules.Buffs.NormalBuff);
                    base.characterBody.RemoveBuff(Modules.Buffs.AnxiousBuff);

                    base.characterBody.AddBuff(Modules.Buffs.EvilBuff);
                }

            }
            else
            {


                if (EmotionValue >= 40)
                {

                    if (NetworkServer.active)
                    {
                        base.characterBody.RemoveBuff(Modules.Buffs.NormalBuff);
                        base.characterBody.RemoveBuff(Modules.Buffs.AnxiousBuff);

                        base.characterBody.AddBuff(Modules.Buffs.FullSyncBuff);
                    }

                }
                else if (EmotionValue <= 39 && EmotionValue >= 15)
                {

                    if (NetworkServer.active)
                    {
                        base.characterBody.RemoveBuff(Modules.Buffs.FullSyncBuff);
                        base.characterBody.RemoveBuff(Modules.Buffs.AnxiousBuff);

                        base.characterBody.AddBuff(Modules.Buffs.NormalBuff);
                    }

                }
                else if (EmotionValue <= 14)
                {

                    if (NetworkServer.active)
                    {
                        base.characterBody.RemoveBuff(Modules.Buffs.FullSyncBuff);
                        base.characterBody.RemoveBuff(Modules.Buffs.NormalBuff);

                        base.characterBody.AddBuff(Modules.Buffs.AnxiousBuff);
                    }

                }




            }

            //EMOTION BUFFS END



            //DARK DRAIN

            if (!CanDrkDrain)
            {
                if (DrkDrainTimer >= 2.5f)
                {
                    CanDrkDrain = true;
                    DrkDrainTimer = 0f;
                }
                else
                    DrkDrainTimer += Time.fixedDeltaTime;
            }

            //DARK DRAIN END





        }



        public void AdvanceProgram()
        {
            Debug.Log(MemoryCode);

            RemoveAdvanceProgram();

            if (MemoryCode.Length >= 2000)
                MemoryCode = "";


            if (MemoryCode.Length >= 3)
            {
                MemoryCodeCheck = MemoryCode.Substring(MemoryCode.Length - 3);

                Debug.Log("inside IF:" + MemoryCodeCheck);
            }

            if (MemoryCodeCheck.Contains("SSS"))
            {

                
                skillLocator.special.SetSkillOverride(skillLocator.special, MegamanEXE.DrkSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
                
            }

            if (MemoryCodeCheck.Contains("CCC"))
            {

                skillLocator.special.SetSkillOverride(skillLocator.special, MegamanEXE.MCannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);

            }


        }


        private void RemoveAdvanceProgram()
        {
            skillLocator.special.UnsetSkillOverride(skillLocator.special, MegamanEXE.DrkSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
            skillLocator.special.UnsetSkillOverride(skillLocator.special, MegamanEXE.MCannonSkillDef, GenericSkill.SkillOverridePriority.Contextual);



            skillLocator.special.SetSkillOverride(skillLocator.special, MegamanEXE.CyberSwordSkillDef, GenericSkill.SkillOverridePriority.Contextual);
        }



        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }

    }
}
