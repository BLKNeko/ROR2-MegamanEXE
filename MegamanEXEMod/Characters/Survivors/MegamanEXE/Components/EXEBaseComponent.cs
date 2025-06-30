using MegamanEXEMod.Modules;
using MegamanEXEMod.Modules.BaseStates;
using RoR2;
using RoR2.Skills;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static Wamp;

namespace MegamanEXEMod.Survivors.MegamanEXE.Components
{
    internal class EXEBaseComponent : MonoBehaviour
    {

        private Transform EXEmodelTransform;

        private Animator EXEAnim;

        private HealthComponent EXEHealth;

        private CharacterBody EXEBody;

        private bool isWeak;


        private float minHpWeak, initialStoreTime;

        private ChildLocator childLocator;

        private FootstepHandler footstepHandler;

        private float RedHpTimer = 0f;
        private float NaviChatTimer = 0f;
        private float RedHpTimerCooldown = 3f;

        private float VulcanLetterCount = 0f;

        private float EvilBugTimer = 0f;
        private float EvilBugCooldown = 8f;

        private string MemoryCode { get; set; }

        private string MemoryCodeCheck;

        private static float EmotionValue = 25;
        private static float EvilEmotionValue = 0;
        private static float RandBugDebuf = 0;
        private static float DamageReceived = 0f;

        private CharacterModel modelFromSkill;
        private ChildLocator childLocatorFromSkill;

        private CharacterModel EXEmodel;
        private ChildLocator EXEchildLocator;


        private void Start()
        {
            //any funny custom behavior you want here
            //for example, enforcer uses a component like this to change his guns depending on selected skill
            if (EXEBody == null)
            {
                EXEBody = GetComponent<CharacterBody>();
            }

            EXEHealth = EXEBody.GetComponent<HealthComponent>();

            EXEmodelTransform = EXEBody.transform;

            EXEAnim = EXEBody.characterDirection.modelAnimator;

            minHpWeak = 0.45f;

            childLocator = GetComponentInChildren<ChildLocator>();

            MemoryCode = "";
            MemoryCodeCheck = "";

            footstepHandler = EXEBody.GetComponent<ModelLocator>().modelTransform.gameObject.GetComponent<CharacterModel>().GetComponent<FootstepHandler>();

            EXEmodel = EXEBody.GetComponent<ModelLocator>().modelTransform.gameObject.GetComponent<CharacterModel>();

            EXEchildLocator = EXEBody.GetComponent<ModelLocator>().modelTransform.gameObject.GetComponent<CharacterModel>().GetComponent<ChildLocator>();

            //Debug.Log("EXEmodel: " + EXEmodel);
            //Debug.Log("EXEchildLocator: " + EXEchildLocator);

            //Debug.Log("footstepHandler: " + footstepHandler);

            //switch (XConfig.enableXFootstep.Value)
            //{
            //    case 0:
            //        footstepHandler.baseFootstepString = "";
            //        footstepHandler.sprintFootstepOverrideString = "";
            //        break;
            //    case 1:
            //        footstepHandler.baseFootstepString = "Play_X_Footstep_SFX";
            //        footstepHandler.sprintFootstepOverrideString = "Play_X_Footstep_SFX";
            //        break;
            //    case 2:
            //        footstepHandler.baseFootstepString = "Play_X_Footstep_X8_SFX";
            //        footstepHandler.sprintFootstepOverrideString = "Play_X_Footstep_X8_SFX";
            //        break;
            //    default:
            //        footstepHandler.baseFootstepString = "";
            //        footstepHandler.sprintFootstepOverrideString = "";
            //        break;
            //}


        }

        private void FixedUpdate()
        {
            UpdateEmotionState();
            IsEXEWeak();

            if (EXEConfig.NaviChatBool.Value)
                NetNaviChat();

            if (EXEBody.HasBuff(EXEBuffs.EvilBuff))
                ChangeEvilBugs();

        }

        private void ChangeEvilBugs()
        {

            if (!EXEBody.hasAuthority)
                return;



            if (NetworkServer.active && EvilBugTimer >= EvilBugCooldown)
            {

                var rand = UnityEngine.Random.Range(0, 8);
                var rand2 = UnityEngine.Random.Range(2, 5);
                EXEBody.AddTimedBuff(GetDebuffByIndex(rand), rand2);


                EvilBugTimer = 0f;
            }
            else
            {
                EvilBugTimer += Time.fixedDeltaTime;
            }

        }

        public BuffDef GetDebuffByIndex(int index)
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

        private void UpdateEmotionState()
        {
            if (EXEBody.hasAuthority)
            {
                //SET TO DEFAULT NORMAL STATE
                if (!EXEBody.HasBuff(EXEBuffs.AnxiousBuff) && !EXEBody.HasBuff(EXEBuffs.NormalBuff) && !EXEBody.HasBuff(EXEBuffs.RageBuff) && !EXEBody.HasBuff(EXEBuffs.EvilBuff) && !EXEBody.HasBuff(EXEBuffs.FullSyncBuff))
                {
                    if (NetworkServer.active)
                    {
                        EXEBody.AddBuff(EXEBuffs.NormalBuff);
                    }
                }
                    

                //SET TO ANXIOUS STATE
                if(EXEBody.HasBuff(EXEBuffs.NormalBuff) && !EXEBody.HasBuff(EXEBuffs.AnxiousBuff) && !EXEBody.HasBuff(EXEBuffs.RageBuff) &&  EmotionValue <= 10)
                {
                    if (NetworkServer.active)
                    {
                        if (EXEBody.HasBuff(EXEBuffs.NormalBuff))
                            EXEBody.RemoveBuff(EXEBuffs.NormalBuff);

                        EXEBody.AddBuff(EXEBuffs.AnxiousBuff);
                    }
                    
                }

                //REMOVE ANXIOUS STATE
                if (!EXEBody.HasBuff(EXEBuffs.NormalBuff) && EXEBody.HasBuff(EXEBuffs.AnxiousBuff) && EmotionValue > 15)
                {
                    if (NetworkServer.active)
                    {
                        if (EXEBody.HasBuff(EXEBuffs.AnxiousBuff))
                            EXEBody.RemoveBuff(EXEBuffs.AnxiousBuff);

                        EXEBody.AddBuff(EXEBuffs.NormalBuff);
                    }
                    
                }

                //SET TO RAGE STATE

                if ((EXEBody.HasBuff(EXEBuffs.NormalBuff) || EXEBody.HasBuff(EXEBuffs.AnxiousBuff))
                && !EXEBody.HasBuff(EXEBuffs.RageBuff)
                && DamageReceived >= (EXEBody.maxHealth / 2))
                {
                    if(NetworkServer.active)
                    {
                        if (EXEBody.HasBuff(EXEBuffs.NormalBuff))
                            EXEBody.RemoveBuff(EXEBuffs.NormalBuff);

                        if (EXEBody.HasBuff(EXEBuffs.AnxiousBuff))
                            EXEBody.RemoveBuff(EXEBuffs.AnxiousBuff);

                        EXEBody.AddTimedBuff(EXEBuffs.RageBuff, 5f + EXEBody.level);
                    }

                    DamageReceived = 0f;

                    EffectManager.SimpleMuzzleFlash(EXEAssets.VfxRage, base.gameObject, "BaseMZ", true);

                }

                //SET TO FULL SYNC STATE
                if (EXEBody.HasBuff(EXEBuffs.NormalBuff) && !EXEBody.HasBuff(EXEBuffs.FullSyncBuff) && EmotionValue >= 50)
                {
                    if(NetworkServer.active)
                    {
                        if (EXEBody.HasBuff(EXEBuffs.NormalBuff))
                            EXEBody.RemoveBuff(EXEBuffs.NormalBuff);

                        EXEBody.AddTimedBuff(EXEBuffs.FullSyncBuff, 5f + EXEBody.level);
                    }

                    EmotionValue = 30f;

                    EffectManager.SimpleMuzzleFlash(EXEAssets.VfxFullSync, gameObject, "CoreFS", true);

                }

                //SET TO EVIL STATE
                if (!EXEBody.HasBuff(EXEBuffs.EvilBuff) && EvilEmotionValue >= 50)
                {
                    if (NetworkServer.active)
                    {
                        if (EXEBody.HasBuff(EXEBuffs.NormalBuff))
                            EXEBody.RemoveBuff(EXEBuffs.NormalBuff);

                        if (EXEBody.HasBuff(EXEBuffs.AnxiousBuff))
                            EXEBody.RemoveBuff(EXEBuffs.AnxiousBuff);

                        if (EXEBody.HasBuff(EXEBuffs.RageBuff))
                            EXEBody.RemoveBuff(EXEBuffs.RageBuff);

                        if (EXEBody.HasBuff(EXEBuffs.FullSyncBuff))
                            EXEBody.RemoveBuff(EXEBuffs.FullSyncBuff);

                        EXEBody.AddBuff(EXEBuffs.EvilBuff);
                    }

                    SetDarkTex();

                    EffectManager.SimpleMuzzleFlash(EXEAssets.VfxEvil, base.gameObject, "BaseMZ", true);

                }

                //UPDATE EVIL STATE
                if(EXEBody.HasBuff(EXEBuffs.EvilBuff) && EvilEmotionValue > 0)
                {
                    EvilEmotionValue -= Time.fixedDeltaTime;

                }

                //REMOVE EVIL STATE
                if (EXEBody.HasBuff(EXEBuffs.EvilBuff) && EvilEmotionValue <= 0)
                {
                    if (NetworkServer.active)
                    {
                        if (EXEBody.HasBuff(EXEBuffs.EvilBuff))
                            EXEBody.RemoveBuff(EXEBuffs.EvilBuff);
                    }

                    SetEmotionalValue(0,0);

                    RemoverDarkTex();
                }

            }
        }

        private void IsEXEWeak()
        {
            isWeak = EXEHealth.combinedHealthFraction < minHpWeak;

            EXEAnim.SetBool("isWeak", isWeak);

            if (isWeak && RedHpTimer < 0f)
            {
                AkSoundEngine.PostEvent(EXEStaticValues.SFXRedHP, this.gameObject);

                RedHpTimer = RedHpTimerCooldown;

            }else if(RedHpTimer >= 0f)
            {
                RedHpTimer -= Time.fixedDeltaTime;
            }

        }

        /// <summary>
        /// Atualize o valor emocional do NetNavi
        /// </summary>
        /// <param name="value">Valor emocional a ser somado ou subtraido</param>
        /// <param name="drkValue">Valor emecional do uso dos DARKCHIP</param>
        /// <param name="rageValue">Valor a ser somado em danos sofridos</param>
        public void UpdateEmotionalValue(float value, float drkValue, float rageValue)
        {
            EmotionValue += value;
            EvilEmotionValue += drkValue;
            DamageReceived += rageValue;

            if (EmotionValue < 0)
                EmotionValue = 0;

            if (EvilEmotionValue < 0)
                EvilEmotionValue = 0;

            if (DamageReceived < 0)
                DamageReceived = 0;

            if (EmotionValue >= 50)
                EmotionValue = 50;

            if (EvilEmotionValue >= 50)
                EvilEmotionValue = 50;


            //logs
            Debug.Log("Emotion: " + EmotionValue);
            Debug.Log("Dark: " + EvilEmotionValue);
            Debug.Log("DmgR: " + DamageReceived);

        }

        /// <summary>
        /// Seta o valor emocional do NetNavi
        /// </summary>
        /// <param name="value">Valor emocional que se tornara</param>
        /// <param name="id">Qual valor sera setado, 0 = Emotion, 1 = Evil, 2 = DmgRec, 3 = Reset</param>
        public void SetEmotionalValue(int value, int id)
        {

            switch (id)
            {
                case 0:
                    EmotionValue = value; 
                    break;
                case 1:
                    EvilEmotionValue = value;
                    break;
                case 2:
                    DamageReceived = value;
                    break;
                case 3:
                    EmotionValue = 25;
                    EvilEmotionValue = 0;
                    DamageReceived = 0;
                    break;

            }

            if (EmotionValue < 0)
                EmotionValue = 0;

            if (EvilEmotionValue < 0)
                EvilEmotionValue = 0;

            if (DamageReceived < 0)
                DamageReceived = 0;

            if (EmotionValue >= 50)
                EmotionValue = 50;

            if (EvilEmotionValue >= 50)
                EvilEmotionValue = 50;


            //logs
            Debug.Log("Emotion: " + EmotionValue);
            Debug.Log("Dark: " + EvilEmotionValue);
            Debug.Log("DmgR: " + DamageReceived);

        }

        private void SetDarkTex()
        {
            if (EXEBody.skinIndex == 0)
            {

                if (EXEmodel && EXEchildLocator)
                {
                    EXEmodel.baseRendererInfos[0].defaultMaterial = EXEAssets.DarkEXEMat;
                    EXEmodel.baseRendererInfos[1].defaultMaterial = EXEAssets.DarkEXEMat;
                    EXEmodel.baseRendererInfos[2].defaultMaterial = EXEAssets.DarkEXEMat;
                    EXEmodel.baseRendererInfos[4].defaultMaterial = EXEAssets.DarkEXEMat;
                    EXEmodel.baseRendererInfos[7].defaultMaterial = EXEAssets.DarkEXESwordMat;

                    EXEchildLocator.FindChildGameObject("EXEBodyMesh").GetComponent<SkinnedMeshRenderer>().sharedMaterial = EXEAssets.DarkEXEMat;
                    EXEchildLocator.FindChildGameObject("CYSword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DarkEXESwordMat;
                    EXEchildLocator.FindChildGameObject("ProtoBuster").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DarkEXEMat;

                    if (EXEConfig.NaviChatBool.Value)
                        SendChatMessage("<color=#382a57>Dark Megaman.EXE</color>: " + GetDarkModeMessage());

                }


            }

            if (EXEBody.skinIndex == 1)
            {

                if (EXEmodel && EXEchildLocator)
                {
                    EXEmodel.baseRendererInfos[0].defaultMaterial = EXEAssets.DarkProtoMat;
                    EXEmodel.baseRendererInfos[1].defaultMaterial = EXEAssets.DarkProtoMat;
                    EXEmodel.baseRendererInfos[2].defaultMaterial = EXEAssets.DarkProtoMat;
                    EXEmodel.baseRendererInfos[4].defaultMaterial = EXEAssets.ProtoBusterMat;
                    EXEmodel.baseRendererInfos[7].defaultMaterial = EXEAssets.DarkEXESwordMat;

                    EXEchildLocator.FindChildGameObject("EXEBodyMesh").GetComponent<SkinnedMeshRenderer>().sharedMaterial = EXEAssets.DarkProtoMat;
                    EXEchildLocator.FindChildGameObject("CYSword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DarkEXESwordMat;

                    if (EXEConfig.NaviChatBool.Value)
                        SendChatMessage("<color=#401412>Dark Protoman.EXE</color>: " + GetDarkModeMessage());

                }


            }

            if (EXEBody.skinIndex == 2)
            {

                if (EXEmodel && EXEchildLocator)
                {
                    EXEmodel.baseRendererInfos[0].defaultMaterial = EXEAssets.DarkRollMat;
                    EXEmodel.baseRendererInfos[1].defaultMaterial = EXEAssets.DarkRollMat;
                    EXEmodel.baseRendererInfos[2].defaultMaterial = EXEAssets.DarkRollMat;
                    EXEmodel.baseRendererInfos[7].defaultMaterial = EXEAssets.DarkEXESwordMat;

                    EXEchildLocator.FindChildGameObject("EXEBodyMesh").GetComponent<SkinnedMeshRenderer>().sharedMaterial = EXEAssets.DarkRollMat;
                    EXEchildLocator.FindChildGameObject("CYSword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DarkEXESwordMat;

                    if (EXEConfig.NaviChatBool.Value)
                        SendChatMessage("<color=#810491>Empress</color>: " + GetDarkModeMessage());

                }


            }

            if (EXEBody.skinIndex == 3)
            {

                if (EXEmodel && EXEchildLocator)
                {
                    EXEmodel.baseRendererInfos[0].defaultMaterial = EXEAssets.DarkBassMat;
                    EXEmodel.baseRendererInfos[1].defaultMaterial = EXEAssets.DarkBassMat;
                    EXEmodel.baseRendererInfos[2].defaultMaterial = EXEAssets.DarkBassMat;
                    EXEmodel.baseRendererInfos[6].defaultMaterial = EXEAssets.DarkBassMat;
                    EXEmodel.baseRendererInfos[7].defaultMaterial = EXEAssets.DarkEXESwordMat;

                    EXEchildLocator.FindChildGameObject("EXEBodyMesh").GetComponent<SkinnedMeshRenderer>().sharedMaterial = EXEAssets.DarkBassMat;
                    EXEchildLocator.FindChildGameObject("BassBuster").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DarkBassMat;
                    EXEchildLocator.FindChildGameObject("CYSword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DarkEXESwordMat;

                    if (EXEConfig.NaviChatBool.Value)
                        SendChatMessage("<color=#140429>BASS XX.EXE</color>: " + GetDarkModeMessage());

                }


            }

            if (EXEBody.skinIndex == 4)
            {

                if (EXEmodel && EXEchildLocator)
                {
                    EXEmodel.baseRendererInfos[0].defaultMaterial = EXEAssets.DarkDiveMat;
                    EXEmodel.baseRendererInfos[1].defaultMaterial = EXEAssets.DarkDiveMat;
                    EXEmodel.baseRendererInfos[2].defaultMaterial = EXEAssets.DarkDiveMat;
                    EXEmodel.baseRendererInfos[10].defaultMaterial = EXEAssets.DarkDiveMat;
                    EXEmodel.baseRendererInfos[11].defaultMaterial = EXEAssets.DarkDiveMat;

                    EXEchildLocator.FindChildGameObject("EXEBodyMesh").GetComponent<SkinnedMeshRenderer>().sharedMaterial = EXEAssets.DarkDiveMat;
                    EXEchildLocator.FindChildGameObject("DiveEXESword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DarkDiveMat;
                    EXEchildLocator.FindChildGameObject("DiveEXEBuster").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DarkDiveMat;

                    if (EXEConfig.NaviChatBool.Value)
                        SendChatMessage("<color=#401412>Dark Megaman.EXE Dive</color>: " + GetDarkModeMessage());

                }


            }
        }

        private void RemoverDarkTex()
        {
            if (EXEBody.skinIndex == 0)
            {

                if (EXEmodel && EXEchildLocator)
                {
                    EXEmodel.baseRendererInfos[0].defaultMaterial = EXEAssets.EXEMat;
                    EXEmodel.baseRendererInfos[1].defaultMaterial = EXEAssets.EXEMat;
                    EXEmodel.baseRendererInfos[2].defaultMaterial = EXEAssets.EXEMat;
                    EXEmodel.baseRendererInfos[4].defaultMaterial = EXEAssets.EXEMat;
                    EXEmodel.baseRendererInfos[7].defaultMaterial = EXEAssets.EXESwordMat;
                    EXEmodel.baseRendererInfos[8].defaultMaterial = EXEAssets.EXEMat;

                    EXEchildLocator.FindChildGameObject("EXEBodyMesh").GetComponent<SkinnedMeshRenderer>().sharedMaterial = EXEAssets.EXEMat;
                    EXEchildLocator.FindChildGameObject("CYSword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.EXESwordMat;
                    EXEchildLocator.FindChildGameObject("ProtoBuster").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.EXEMat;
                    EXEchildLocator.FindChildGameObject("EXEMask").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.EXEMat;

                    if (EXEConfig.NaviChatBool.Value)
                        SendChatMessage("<color=#043db8>Megaman.EXE</color>: " + GetExitDarkModeMessage());

                }


            }

            if (EXEBody.skinIndex == 1)
            {

                if (EXEmodel && EXEchildLocator)
                {
                    EXEmodel.baseRendererInfos[0].defaultMaterial = EXEAssets.ProtoMat;
                    EXEmodel.baseRendererInfos[1].defaultMaterial = EXEAssets.ProtoMat;
                    EXEmodel.baseRendererInfos[2].defaultMaterial = EXEAssets.ProtoMat;
                    EXEmodel.baseRendererInfos[4].defaultMaterial = EXEAssets.ProtoBusterMat;
                    EXEmodel.baseRendererInfos[7].defaultMaterial = EXEAssets.ProtoSwordMat;

                    EXEchildLocator.FindChildGameObject("EXEBodyMesh").GetComponent<SkinnedMeshRenderer>().sharedMaterial = EXEAssets.ProtoMat;
                    EXEchildLocator.FindChildGameObject("CYSword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.ProtoSwordMat;

                    if (EXEConfig.NaviChatBool.Value)
                        SendChatMessage("<color=#cf1919>Protoman.EXE</color>: " + GetExitDarkModeMessage());
                }


            }

            if (EXEBody.skinIndex == 2)
            {

                if (EXEmodel && EXEchildLocator)
                {
                    EXEmodel.baseRendererInfos[0].defaultMaterial = EXEAssets.RollMat;
                    EXEmodel.baseRendererInfos[1].defaultMaterial = EXEAssets.RollMat;
                    EXEmodel.baseRendererInfos[2].defaultMaterial = EXEAssets.RollMat;
                    EXEmodel.baseRendererInfos[7].defaultMaterial = EXEAssets.RollSwordMat;

                    EXEchildLocator.FindChildGameObject("EXEBodyMesh").GetComponent<SkinnedMeshRenderer>().sharedMaterial = EXEAssets.RollMat;
                    EXEchildLocator.FindChildGameObject("CYSword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.RollSwordMat;

                    if (EXEConfig.NaviChatBool.Value)
                        SendChatMessage("<color=#ff7ade>Roll.EXE</color>: " + GetExitDarkModeMessage());
                }


            }

            if (EXEBody.skinIndex == 3)
            {

                if (EXEmodel && EXEchildLocator)
                {
                    EXEmodel.baseRendererInfos[0].defaultMaterial = EXEAssets.BassMat;
                    EXEmodel.baseRendererInfos[1].defaultMaterial = EXEAssets.BassMat;
                    EXEmodel.baseRendererInfos[2].defaultMaterial = EXEAssets.BassMat;
                    EXEmodel.baseRendererInfos[6].defaultMaterial = EXEAssets.BassMat;
                    EXEmodel.baseRendererInfos[7].defaultMaterial = EXEAssets.BassSwordMat;

                    EXEchildLocator.FindChildGameObject("EXEBodyMesh").GetComponent<SkinnedMeshRenderer>().sharedMaterial = EXEAssets.BassMat;
                    EXEchildLocator.FindChildGameObject("BassBuster").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.BassMat;
                    EXEchildLocator.FindChildGameObject("CYSword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.BassSwordMat;

                    if (EXEConfig.NaviChatBool.Value)
                        SendChatMessage("<<color=#8a4601>Bass.EXE</color>: " + GetExitDarkModeMessage());
                }


            }

            if (EXEBody.skinIndex == 4)
            {

                if (EXEmodel && EXEchildLocator)
                {
                    EXEmodel.baseRendererInfos[0].defaultMaterial = EXEAssets.DiveMat;
                    EXEmodel.baseRendererInfos[1].defaultMaterial = EXEAssets.DiveMat;
                    EXEmodel.baseRendererInfos[2].defaultMaterial = EXEAssets.DiveMat;
                    EXEmodel.baseRendererInfos[10].defaultMaterial = EXEAssets.DiveMat;
                    EXEmodel.baseRendererInfos[11].defaultMaterial = EXEAssets.DiveMat;

                    EXEchildLocator.FindChildGameObject("EXEBodyMesh").GetComponent<SkinnedMeshRenderer>().sharedMaterial = EXEAssets.DiveMat;
                    EXEchildLocator.FindChildGameObject("DiveEXESword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DiveMat;
                    EXEchildLocator.FindChildGameObject("DiveEXEBuster").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DiveMat;

                    if (EXEConfig.NaviChatBool.Value)
                        SendChatMessage("<color=#00d0fa>Megaman.EXE Dive</color>: " + GetExitDarkModeMessage());
                }


            }

        }

        public void UpdateMemoryCode(char letter)
        {

            if(letter == 'V')
            {

                VulcanLetterCount++;

                if(VulcanLetterCount >= 10)
                {
                    MemoryCode += letter;
                    VulcanLetterCount = 0f;
                }


            }
            else
            {
                MemoryCode += letter;
            }

            

            //logs
            Debug.Log("MemoryCode: " + MemoryCode);

            AdvancedProgram();

        }

        public void SetMemoryCode(string letter)
        {
            MemoryCode = letter;

            //logs
            Debug.Log("MemoryCode: " + MemoryCode);

        }

        public float GetEmotionValue()
        {
            return EmotionValue;
        }

        public float GetDarkEmotionValue()
        {
            return EvilEmotionValue;
        }

        public void SendChatMessage(string message)
        {
            Chat.SendBroadcastChat(new Chat.SimpleChatMessage
            {
                baseToken = message
            });
        }

        public string GetDarkChipWarningMessage()
        {

            if(EvilEmotionValue <= 25)
            {
                string[] messages =
                {
                    $"Hey!! {EXEBody.GetUserName()}, I don't think it's a good idea to keep using <color=#40128a>DARK CHIPS</color>!",
                    $"{EXEBody.GetUserName()}... I'm not feeling very well...",
                    $"{EXEBody.GetUserName()}... I think I'm hearing a strange voice... inside my mind.",
                    $"That <color=#40128a>DARK CHIP</color> was really powerful... but it hurts... a little.",
                    $"01101000 01100101 01101100 01101100 01101111 00100000 01110111 01101111 01110010 01101100 01100100", // (hello world em binário, perfeito!)
                    $"{EXEBody.GetUserName()}... <color=#40128a>DARK CHIPS</color> are dangerous... Please... stop using them...",
                    $"{EXEBody.GetUserName()}... Something... feels... wrong...",
                };

                return messages[UnityEngine.Random.Range(0, messages.Length)];
            }
            else
            {

                string[] messages2 =
                {
                    $"System error... System error... Please... stop... <color=#40128a>DARK CHIP</color> usage...",
                    $"I can't... control... myself...",
                    $"Why... does it... hurt... so much...?",
                    $"Darkness... it's... consuming... me...",
                    $"{EXEBody.GetUserName()}... You... wouldn't abandon me... right?",
                    $"Signal distortion... Data corruption detected... <color=#40128a>DARK CHIP</color> usage critical.",
                    $"Whispers... I hear... whispers... from... somewhere...",
                    $"Am I... still... me...?",
                    $"...Error... Error... Error... Error..."
                };

                return messages2[UnityEngine.Random.Range(0, messages2.Length)];

            }
           

        }

        public string GetDarkModeMessage()
        {
            string[] messages =
            {
                $"<color=#40128a>Hahaha!</color> {EXEBody.GetUserName()}... Forget the warnings about <color=#40128a>DARK CHIPS</color>... I feel <b>UNSTOPPABLE!</b> Now quit slacking and let's go crush those pathetic losers!",

                $"<b>POWER... I NEED MORE POWER!!</b> Feed me stronger chips, you useless fleshbag!",

                $"01110111 01101000 01111001 00100000 01100001 01101101 00100000 01100100 01101111 01101001 01101110 01100111 00100000 01110100 01101000 01101001 01110011 00111111", 
                // (Por que estou fazendo isso?) traduzido do binário.

                "Tch... I don't need you. I don't need ANYONE anymore!",

                "My code... my soul... it doesn't matter anymore. Only power matters!",

                "<color=#ff0000>ERROR</color>... <color=#40128a>Consciousness override complete.</color>",

                "Do you hear it...? The void... is calling... and I answer.",

                "Hahahaha... Their screams... are music to me now.",

                $"<color=#ff0000>E̸̖̟̦̯͇̐̈́͑̓͝R̸̢̤͓͇̺͊̽̍̚R̴̛̥̰̺̙͌̓͠ͅO̷̫̞̪̙̎̍̕̕ͅR̶̪̯͖̤̿͊̿͑ͅ</color>... {EXEBody.GetUserName()}...",

                $"D̸͉͐͌͛͗͜A̵̼̰̺̪̗̿̒͝T̶͇̥̺̫̝̍̒̐͂A̴̪̳͌͋̋͘ ̶͉̞̰̔̅͝C̴̳̰̋̄̕͠O̶̪̙̜͖̓̎͒R̶̞̤̳̰̾̏̓̕R̷͓̈́̈́͂͝U̶̪̺͖͑̔̔P̷̗͍̼̬̓̓̽̇T̶̙̱̞̺̏̈́̎͐É̸̘̰̠̬͊̐̾D̵̟̪̙̞̾́̀͝...",

                $"<color=#8a00ff>Whispers... I hear... ẅ̵͓̙́h̸͍̪̐i̴͓̯̚s̵̥̦̃p̶̛̰è̵̢̦r̴͇͌s̵̞̳̐... from... the void...</color>",

                $"<color=#40128a>S̷̥̒͜͠y̸̱̠̦͊̑̈́s̸̞͍̖̀̀̕t̸̰̜͑e̷̙̖̿͋̈́m̸̛̟̓͜</color>... <color=#ff0000>FAILING</color>..."       

            };

            return messages[UnityEngine.Random.Range(0, messages.Length)];

        }

        public string GetExitDarkModeMessage()
        {
            string[] messages =
            {
                $"{EXEBody.GetUserName()}... Did I... really do... all of that...?",

                $"{EXEBody.GetUserName()}... Why...? Why did you let me become... like that...?",

                "I... I think I'm okay now... I think...",

                "I just... I really hope... I won't have to go through... that... ever again...",

                $"{EXEBody.GetUserName()}! I knew I could count on you!",

                "Phew... Glad to be back! Now... let’s clean up those viruses!",

                "I... I couldn't stop it... I'm... sorry...",

                "That... wasn't me... right?",
                
                "I was... watching... but couldn't do anything...",
                
                "Ahhh... Finally! I'm free again!",
                
                "Thanks... I knew you wouldn't leave me like that.",
                
                "Back online and... feeling like myself again!"

            };

            return messages[UnityEngine.Random.Range(0, messages.Length)];
        }

        private string GetNaviTipMessage()
        {
            string[] messages =
            {
                $"Hey!! {EXEBody.GetUserName()}! Be careful when using <color=#40128a>DARK CHIPS</color>... They corrupt the Navi system, and some damage might be permanent.",

                $"Hey!! {EXEBody.GetUserName()}! Try using three Cannon chips consecutively... Something funny might happen!",

                $"Hey!! {EXEBody.GetUserName()}! Just wanted to say... We make a great team!",

                $"Hey!! {EXEBody.GetUserName()}! Aim carefully! Missing too many shots makes me feel... kinda anxious.",

                $"Hey!! {EXEBody.GetUserName()}! Some enemies just make me SO mad... Sometimes I can feel a surge of power building up.",

                $"Hey!! {EXEBody.GetUserName()}! The AirShot chip isn't very strong... but watching enemies get blasted away is always fun!",

                $"Hey!! {EXEBody.GetUserName()}... Do you think a human soul could become a NetNavi...? Hmm... just thinking out loud... hehe.",

                $"...Fish... Taking them out of the water... feels wrong. They belong in the sea... Can they really live on land...?",

                $"Hey!! {EXEBody.GetUserName()}! The Invis chip can be super useful when we're surrounded!",

                $"Hey!! {EXEBody.GetUserName()}! Don't you think it's funny how fast the Vulcan chip fires? It's like... brrrrrrr!",

                $"Hey!! {EXEBody.GetUserName()}! I got some updates and new customizations... Am I talking too much now?",

                $"Hey!! {EXEBody.GetUserName()}! The Reflector chip is awesome... if you time it right!",

                $"Hey... Do you know someone called X? Weird name... but kinda cool, huh?",

                $"Hmm...? Zero...? Wait... you mean the virus...?",

                $"Vollnut...? What is that...? Some kind of... nut?",

                $"Starforce...? Sounds powerful... Is that a chip or something?",

                $"Hey, {EXEBody.GetUserName()}! Try to get all the items we can!",

                $"A virus called Nightmare? Sorry, I don't remember anything like that..."

                //-- unrevised


            };

            return messages[UnityEngine.Random.Range(0, messages.Length)];
        }

        /// <summary>
        /// Funcao para as conversas casuais do NetNavi
        /// </summary>
        private void NetNaviChat()
        {

            if(NaviChatTimer >= EXEConfig.NaviChatCooldown.Value && !EXEBody.HasBuff(EXEBuffs.EvilBuff) && EXEBody.healthComponent.alive)
            {

                switch (EXEBody.skinIndex)
                {
                    case 0:
                        SendChatMessage("<color=#043db8>Megaman.EXE</color>: " + GetNaviTipMessage());
                        break;

                    case 1:
                        SendChatMessage("<color=#cf1919>Protoman.EXE</color>: " + GetNaviTipMessage());
                        break;

                    case 2:
                        SendChatMessage("<color=#ff7ade>Roll.EXE</color>: " + GetNaviTipMessage());
                        break;

                    case 3:
                        SendChatMessage("<color=#8a4601>Bass.EXE</color>: " + GetNaviTipMessage());
                        break;

                    case 4:
                        SendChatMessage("<color=#00d0fa>Megaman.EXE Dive</color>: " + GetNaviTipMessage());
                        break;
                }


                NaviChatTimer = 0f;

            }
            else if(!EXEBody.HasBuff(EXEBuffs.EvilBuff) && EXEBody.healthComponent.alive)
            {
                NaviChatTimer += Time.fixedDeltaTime;
            }

            
        }

        /// <summary>
        /// Funcao para enviar o aviso do dark chip no chat
        /// </summary>
        public void SendDarkChipWarningMessage()
        {

            if (!EXEBody.HasBuff(EXEBuffs.EvilBuff) && EXEBody.healthComponent.alive)
            {

                switch (EXEBody.skinIndex)
                {
                    case 0:
                        SendChatMessage("<color=#043db8>Megaman.EXE</color>: " + GetDarkChipWarningMessage());
                        break;

                    case 1:
                        SendChatMessage("<color=#cf1919>Protoman.EXE</color>: " + GetDarkChipWarningMessage());
                        break;

                    case 2:
                        SendChatMessage("<color=#ff7ade>Roll.EXE</color>: " + GetDarkChipWarningMessage());
                        break;

                    case 3:
                        SendChatMessage("<color=#8a4601>Bass.EXE</color>: " + GetDarkChipWarningMessage());
                        break;

                    case 4:
                        SendChatMessage("<color=#00d0fa>Megaman.EXE Dive</color>: " + GetDarkChipWarningMessage());
                        break;
                }


            }


        }

        /// <summary>
        /// Muda o modelo do buster para diferentes cenarios
        /// </summary>
        public void ChangeBusterArm(Transform modelTransform, CharacterModel characterModel, ChildLocator childLocator, int skinId)
        {
            if (modelTransform)
            {

                if (characterModel)
                {

                    childLocator.FindChildGameObject("EXEHandLMesh").SetActive(false);
                    childLocator.FindChildGameObject("ProtoBuster").SetActive(false);
                    childLocator.FindChildGameObject("RollBuster").SetActive(false);
                    childLocator.FindChildGameObject("BassBuster").SetActive(false);
                    childLocator.FindChildGameObject("DiveEXEBuster").SetActive(false);

                    // 0 - Enable Buster
                    //

                    if (skinId == 0)
                    {
                        childLocator.FindChildGameObject("ProtoBuster").SetActive(true);
                    
                        if(!EXEBody.HasBuff(EXEBuffs.EvilBuff))
                            childLocator.FindChildGameObject("ProtoBuster").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.EXEMat;
                        else
                            childLocator.FindChildGameObject("ProtoBuster").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DarkEXEMat;
                    }
                    if (skinId == 1)
                    {
                        childLocator.FindChildGameObject("ProtoBuster").SetActive(true);
                        childLocator.FindChildGameObject("ProtoBuster").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.ProtoBusterMat;
                    }
                    if (skinId == 2)
                    {
                        childLocator.FindChildGameObject("RollBuster").SetActive(true);
                        //childLocator.FindChildGameObject("RollBuster").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.RollBusterMat;
                    }
                    if (skinId == 3)
                    {
                        childLocator.FindChildGameObject("BassBuster").SetActive(true);
                    
                        if (!EXEBody.HasBuff(EXEBuffs.EvilBuff))
                            childLocator.FindChildGameObject("BassBuster").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.BassMat;
                        else
                            childLocator.FindChildGameObject("BassBuster").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DarkBassMat;
                    }
                    if (skinId == 4)
                    {
                        childLocator.FindChildGameObject("DiveEXEBuster").SetActive(true);
                    
                        if (!EXEBody.HasBuff(EXEBuffs.EvilBuff))
                            childLocator.FindChildGameObject("DiveEXEBuster").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DiveMat;
                        else
                            childLocator.FindChildGameObject("DiveEXEBuster").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DarkDiveMat;
                    }


                }
            }
        }

        /// <summary>
        /// Muda o modelo da espada para diferentes cenarios
        /// </summary>
        /// <param name="id"> 0 CyberSword | 1 Dark | 2 GutsPunch </param>
        public void ChangeSwordArm(Transform modelTransform, CharacterModel characterModel, ChildLocator childLocator, int skinId, int id)
        {
            if (modelTransform)
            {

                if (characterModel)
                {

                    childLocator.FindChildGameObject("EXEHandRMesh").SetActive(false);
                    childLocator.FindChildGameObject("CYSword").SetActive(false);
                    childLocator.FindChildGameObject("GutsPunch").SetActive(false);
                    childLocator.FindChildGameObject("DiveEXESword").SetActive(false);

                    // 0 - Enable Sword
                    // 1 - Enable Dark
                    // 2 - Enable GutsPunch

                    switch (id)
                    {
                        case 0:

                            if (skinId == 0)
                            {
                                childLocator.FindChildGameObject("CYSword").SetActive(true);

                                if (!EXEBody.HasBuff(EXEBuffs.EvilBuff))
                                    childLocator.FindChildGameObject("CYSword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.EXESwordMat;
                                else
                                    childLocator.FindChildGameObject("CYSword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DarkEXESwordMat;
                            }
                            if (skinId == 1)
                            {
                                childLocator.FindChildGameObject("CYSword").SetActive(true);

                                if (!EXEBody.HasBuff(EXEBuffs.EvilBuff))
                                    childLocator.FindChildGameObject("CYSword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.ProtoSwordMat;
                                else
                                    childLocator.FindChildGameObject("CYSword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DarkEXESwordMat;
                            }
                            if (skinId == 2)
                            {
                                childLocator.FindChildGameObject("CYSword").SetActive(true);

                                if (!EXEBody.HasBuff(EXEBuffs.EvilBuff))
                                    childLocator.FindChildGameObject("CYSword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.RollSwordMat;
                                else
                                    childLocator.FindChildGameObject("CYSword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DarkEXESwordMat;
                            }
                            if (skinId == 3)
                            {
                                childLocator.FindChildGameObject("CYSword").SetActive(true);

                                if (!EXEBody.HasBuff(EXEBuffs.EvilBuff))
                                    childLocator.FindChildGameObject("CYSword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.BassSwordMat;
                                else
                                    childLocator.FindChildGameObject("CYSword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DarkEXESwordMat;
                            }
                            if (skinId == 4)
                            {
                                childLocator.FindChildGameObject("DiveEXESword").SetActive(true);

                                if (!EXEBody.HasBuff(EXEBuffs.EvilBuff))
                                    childLocator.FindChildGameObject("DiveEXESword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DiveMat;
                                else
                                    childLocator.FindChildGameObject("DiveEXESword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DarkDiveMat;
                            }


                            break;


                        case 1:

                            if (skinId == 0)
                            {
                                childLocator.FindChildGameObject("CYSword").SetActive(true);
                                childLocator.FindChildGameObject("CYSword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DarkEXESwordMat;
                            }
                            if (skinId == 1)
                            {
                                childLocator.FindChildGameObject("CYSword").SetActive(true);
                                childLocator.FindChildGameObject("CYSword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DarkEXESwordMat;
                            }
                            if (skinId == 2)
                            {
                                childLocator.FindChildGameObject("CYSword").SetActive(true);
                                childLocator.FindChildGameObject("CYSword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DarkEXESwordMat;
                            }
                            if (skinId == 3)
                            {
                                childLocator.FindChildGameObject("CYSword").SetActive(true);
                                childLocator.FindChildGameObject("CYSword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DarkEXESwordMat;
                            }
                            if (skinId == 4)
                            {
                                childLocator.FindChildGameObject("DiveEXESword").SetActive(true);
                                childLocator.FindChildGameObject("DiveEXESword").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.DarkDiveMat;
                            }


                            break;

                        case 2:
                            childLocator.FindChildGameObject("GutsPunch").SetActive(true);
                            break;
                    }


                }
            }
        }

        /// <summary>
        /// Muda o modelo da espada e buster para maos
        /// </summary>
        public void ChangeHands(Transform modelTransform, CharacterModel characterModel, ChildLocator childLocator, int skinId)
        {
            if (modelTransform)
            {

                if (characterModel)
                {

                    childLocator.FindChildGameObject("CYSword").SetActive(false);
                    childLocator.FindChildGameObject("GutsPunch").SetActive(false);
                    childLocator.FindChildGameObject("DiveEXESword").SetActive(false);

                    childLocator.FindChildGameObject("ProtoBuster").SetActive(false);
                    childLocator.FindChildGameObject("RollBuster").SetActive(false);
                    childLocator.FindChildGameObject("BassBuster").SetActive(false);
                    childLocator.FindChildGameObject("DiveEXEBuster").SetActive(false);


                    childLocator.FindChildGameObject("EXEHandRMesh").SetActive(true);
                    childLocator.FindChildGameObject("EXEHandLMesh").SetActive(true);


                }
            }
        }



        private void AdvancedProgram()
        {
            if (!EXEBody.hasAuthority)
                return;

            RemoveAdvanceProgram();

            if (MemoryCode.Length >= 2000)
                MemoryCode = MemoryCode.Substring(MemoryCode.Length - 5);

            // Lista de códigos e suas respectivas skills
            Dictionary<string, SkillDef> codeToSkill = new Dictionary<string, SkillDef>()
            {
                { "AAAAA", MegamanEXESurvivor.AdvAirShotSkillDef },

                { "SSS", MegamanEXESurvivor.AdvLifeSwordSkillDef },
                { "SSSSS", MegamanEXESurvivor.AdvLifeSwordSkillDef },

                { "CCC", MegamanEXESurvivor.AdvGigaCannonSkillDef },
                { "CCCCC", MegamanEXESurvivor.AdvGigaCannonSkillDef },

                { "VVV", MegamanEXESurvivor.AdvInfiniteVulcanSkillDef },
                { "VVVVV", MegamanEXESurvivor.AdvInfiniteVulcanSkillDef },

                { "BBB", MegamanEXESurvivor.AdvBarr500SkillDef },
                { "BBBBB", MegamanEXESurvivor.AdvBarr500SkillDef },

                { "YYY", MegamanEXESurvivor.AdvGreatYoyoSkillDef },
                { "YYYYY", MegamanEXESurvivor.AdvGreatYoyoSkillDef }
            };

            // Verificar os últimos 3 e 5 caracteres
            string last3 = MemoryCode.Length >= 3 ? MemoryCode.Substring(MemoryCode.Length - 3) : "";
            string last5 = MemoryCode.Length >= 5 ? MemoryCode.Substring(MemoryCode.Length - 5) : "";

            // Checar se algum dos códigos bate
            foreach (var entry in codeToSkill)
            {
                if (last3 == entry.Key || last5 == entry.Key)
                {
                    EXEBody.skillLocator.special.SetSkillOverride(
                        EXEBody.skillLocator.special,
                        entry.Value,
                        GenericSkill.SkillOverridePriority.Contextual
                    );

                    // Se quiser que apenas 1 skill seja aplicada por vez, pode dar break aqui
                    break;
                }
            }
        }

        private void RemoveAdvanceProgram()
        {
            foreach (var skill in AdvanceSkillDefs)
            {
                EXEBody.skillLocator.special.UnsetSkillOverride(
                    EXEBody.skillLocator.special,
                    skill,
                    GenericSkill.SkillOverridePriority.Contextual
                );
            }

            EXEBody.skillLocator.special.SetSkillOverride(
                EXEBody.skillLocator.special,
                MegamanEXESurvivor.NoDataSkillDef,
                GenericSkill.SkillOverridePriority.Contextual
            );
        }

        private static readonly SkillDef[] AdvanceSkillDefs =
        {
            MegamanEXESurvivor.AdvAirShotSkillDef,
            MegamanEXESurvivor.AdvBarr500SkillDef,
            MegamanEXESurvivor.AdvGigaCannonSkillDef,
            MegamanEXESurvivor.AdvGreatYoyoSkillDef,
            MegamanEXESurvivor.AdvInfiniteVulcanSkillDef,
            MegamanEXESurvivor.AdvLifeSwordSkillDef
        };

    }
}