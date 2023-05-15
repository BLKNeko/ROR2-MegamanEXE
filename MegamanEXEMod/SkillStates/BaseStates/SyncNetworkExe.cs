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
                // Faça algo com os últimos caracteres (ultimosCaracteres)
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
