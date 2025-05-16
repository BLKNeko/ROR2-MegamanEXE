using MegamanEXEMod.Modules;
using RoR2;
using UnityEngine;

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
        private static int EvilEmotionValue = 0;
        private static float RandBugDebuf = 0;
        private static float DamageReceived = 0f;


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

            IsEXEWeak();
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

            //logs
            Debug.Log("Emotion: " + EmotionValue);
            Debug.Log("Dark: " + EvilEmotionValue);
            Debug.Log("DmgR: " + DamageReceived);

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

        public int GetDarkEmotionValue()
        {
            return EvilEmotionValue;
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