using EntityStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using RoR2;
using RoR2.Audio;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace MegamanEXEMod.Modules.BaseStates
{
    internal class EXEHeart : GenericCharacterMain
    {

        private Transform modelTransform;
        private CharacterModel characterModel;

        public override void OnEnter()
        {
            base.OnEnter();

            modelTransform = GetModelTransform();
            if ((bool)modelTransform)
            {
                characterModel = modelTransform.GetComponent<CharacterModel>();
            }


        }
        public override void OnExit()
        {
            base.OnExit();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public void IntoDarkMode()
        {

            if(characterBody.hasAuthority)
            {
                if(characterBody.skinIndex == 0)
                {
                    characterModel.baseRendererInfos[0].defaultMaterial = EXEAssets.DarkEXEMat;
                    characterModel.baseRendererInfos[1].defaultMaterial = EXEAssets.DarkEXEMat;
                    characterModel.baseRendererInfos[2].defaultMaterial = EXEAssets.DarkEXEMat;
                    characterModel.GetComponent<ChildLocator>().FindChildGameObject("EXEBodyMesh").GetComponent<SkinnedMeshRenderer>().sharedMaterial = EXEAssets.DarkEXEMat;
                }
            }

            


        }
        

    }
}
