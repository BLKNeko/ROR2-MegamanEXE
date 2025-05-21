using MegamanEXEMod.Modules;
using MegamanEXEMod.Modules.BaseStates;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

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
        private float RedHpTimerCooldown = 6.5f;

        private string MemoryCode { get; set; }

        private string MemoryCodeCheck;

        private static int EmotionValue = 25;
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

            Debug.Log("EXEmodel: " + EXEmodel);
            Debug.Log("EXEchildLocator: " + EXEchildLocator);

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
                    

                    if(EXEBody.skinIndex == 0)
                    {

                        if (EXEmodel && EXEchildLocator)
                        {
                            EXEmodel.baseRendererInfos[0].defaultMaterial = EXEAssets.DarkEXEMat;

                            EXEchildLocator.FindChildGameObject("EXEBodyMesh").GetComponent<SkinnedMeshRenderer>().sharedMaterial = EXEAssets.DarkEXEMat;


                        }

                        SendChatMessage("Dark Megaman.EXE: Hahahaha, nevermind about the DarkChips, i feel powerfull! Now stop slacking off and lets kill some losers.");
                    }

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

                    if (EXEBody.skinIndex == 0)
                    {
                        EXEmodelTransform.GetComponent<CharacterModel>().baseRendererInfos[0].defaultMaterial = EXEAssets.EXEMat;

                    }
                }

            }
        }

        private void IsEXEWeak()
        {
            isWeak = EXEHealth.combinedHealthFraction < minHpWeak;

            EXEAnim.SetBool("isWeak", isWeak);

            if (isWeak && RedHpTimer < 0f)
            {
                //Util.PlaySound(Sounds.SFXRedHP, base.gameObject);

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
        public void UpdateEmotionalValue(int value, int drkValue, float rageValue)
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

        public void UpdateModel(CharacterModel model, ChildLocator child)
        {
            modelFromSkill = model;
            childLocatorFromSkill = child;
        }

        public void UpdateMemoryCode(char letter)
        {
            MemoryCode += letter;

            //logs
            Debug.Log("MemoryCode: " + MemoryCode);

        }

        public int GetEmotionValue()
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


        /// <summary>
        /// Muda o modelo do buster para diferentes cenarios
        /// </summary>
        /// <param name="id"> 0 Buster </param>
        public void ChangeBusterArm(Transform modelTransform, CharacterModel characterModel, ChildLocator childLocator, int skinId, int id)
        {
            if (modelTransform)
            {

                if (characterModel)
                {

                    childLocator.FindChildGameObject("EXEHandLMesh").SetActive(false);
                    childLocator.FindChildGameObject("ProtoBuster").SetActive(false);
                    childLocator.FindChildGameObject("RollBuster").SetActive(false);
                    childLocator.FindChildGameObject("BassBuster").SetActive(false);

                    // 0 - Enable Buster
                    //

                    switch (id)
                    {
                        case 0:

                            if (skinId == 0)
                            {
                                childLocator.FindChildGameObject("ProtoBuster").SetActive(true);
                                childLocator.FindChildGameObject("ProtoBuster").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.EXEMat;
                            }
                            if (skinId == 1)
                            {
                                childLocator.FindChildGameObject("ProtoBuster").SetActive(true);
                                childLocator.FindChildGameObject("ProtoBuster").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.ProtoBusterMat;
                            }
                            if (skinId == 2)
                            {
                                childLocator.FindChildGameObject("RollBuster").SetActive(true);
                                childLocator.FindChildGameObject("RollBuster").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.RollBusterMat;
                            }
                            if (skinId == 3)
                            {
                                childLocator.FindChildGameObject("BassBuster").SetActive(true);
                                childLocator.FindChildGameObject("BassBuster").GetComponent<MeshRenderer>().sharedMaterial = EXEAssets.BassMat;
                            }


                            break;

                        case 1:
                            break;

                        case 2:

                            break;

                        case 3:
                            break;

                        case 4:
                            break;
                    }


                }
            }
        }


    }
}