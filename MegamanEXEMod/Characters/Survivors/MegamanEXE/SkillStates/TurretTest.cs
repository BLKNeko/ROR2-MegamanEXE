using EntityStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using On.EntityStates.Engi.EngiWeapon;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace MegamanEXEMod.Survivors.MegamanEXE.SkillStates
{
    public class TurretTest : BaseState
    {

        public static float BaseDuration = 1f;
        private bool Spawn = false;

        private Animator animator;

        private EXEBaseComponent execomponent;


        [SerializeField]
        public GameObject wristDisplayPrefab;

        // Token: 0x04001DF1 RID: 7665
        [SerializeField]
        public string placeSoundString;

        // Token: 0x04001DF2 RID: 7666
        [SerializeField]
        public GameObject blueprintPrefab;

        // Token: 0x04001DF3 RID: 7667
        [SerializeField]
        public GameObject turretMasterPrefab;

        // Token: 0x04001DF4 RID: 7668
        private const float placementMaxUp = 1f;

        // Token: 0x04001DF5 RID: 7669
        private const float placementMaxDown = 3f;

        // Token: 0x04001DF6 RID: 7670
        private const float placementForwardDistance = 2f;

        // Token: 0x04001DF7 RID: 7671
        private const float entryDelay = 0.1f;

        // Token: 0x04001DF8 RID: 7672
        private const float exitDelay = 0.25f;

        // Token: 0x04001DF9 RID: 7673
        private const float turretRadius = 0.5f;

        // Token: 0x04001DFA RID: 7674
        private const float turretHeight = 1.82f;

        // Token: 0x04001DFB RID: 7675
        private const float turretCenter = 0f;

        // Token: 0x04001DFC RID: 7676
        private const float turretModelYOffset = -0.75f;

        // Token: 0x04001DFD RID: 7677
        private GameObject wristDisplayObject;

        // Token: 0x04001DFE RID: 7678
        private BlueprintController blueprints;

        // Token: 0x04001DFF RID: 7679
        private float exitCountdown;

        // Token: 0x04001E00 RID: 7680
        private bool exitPending;

        // Token: 0x04001E01 RID: 7681
        private float entryCountdown;

        // Token: 0x04001E02 RID: 7682
        private static int PrepTurretStateHash = Animator.StringToHash("PrepTurret");

        // Token: 0x04001E03 RID: 7683
        private static int PlaceTurretStateHash = Animator.StringToHash("PlaceTurret");

        // Token: 0x04001E04 RID: 7684
        private TurretTest.PlacementInfo currentPlacementInfo;

        // Token: 0x020004A2 RID: 1186
        private struct PlacementInfo
        {
            // Token: 0x04001E05 RID: 7685
            public bool ok;

            // Token: 0x04001E06 RID: 7686
            public Vector3 position;

            // Token: 0x04001E07 RID: 7687
            public Quaternion rotation;
        }


        public override void OnEnter()
        {
            base.OnEnter();
            this.animator = base.GetModelAnimator();

            //Util.PlaySound(Sounds.SFXBarrier, base.gameObject);

            execomponent = GetComponent<EXEBaseComponent>();

            blueprintPrefab = EXEAssets.AllyBodyPrefab;
            turretMasterPrefab = EXEAssets.AllyMasterPrefab;

            if (base.isAuthority)
            {
                this.currentPlacementInfo = this.GetPlacementInfo();
                this.blueprints = UnityEngine.Object.Instantiate<GameObject>(this.blueprintPrefab, this.currentPlacementInfo.position, this.currentPlacementInfo.rotation).GetComponent<BlueprintController>();
            }
            //this.PlayAnimation("Gesture", TurretTest.PrepTurretStateHash);
            this.entryCountdown = 0.1f;
            this.exitCountdown = 0.25f;
            this.exitPending = false;
            //if (base.modelLocator)
            //{
            //    ChildLocator component = base.modelLocator.modelTransform.GetComponent<ChildLocator>();
            //    if (component)
            //    {
            //        Transform transform = component.FindChild("WristDisplay");
            //        if (transform)
            //        {
            //            this.wristDisplayObject = UnityEngine.Object.Instantiate<GameObject>(this.wristDisplayPrefab, transform);
            //        }
            //    }
            //}


        }

        public void SpawnAlly()
        {
            // Posição na frente do player
            Vector3 spawnPosition = characterBody.corePosition + characterBody.inputBank.aimDirection * 5f;

            // Instancia o master
            GameObject allyMasterObj = UnityEngine.Object.Instantiate(EXEAssets.AllyMasterPrefab, spawnPosition, Quaternion.identity);

            CharacterMaster allyMaster = allyMasterObj.GetComponent<CharacterMaster>();
            allyMaster.teamIndex = TeamIndex.Player;
            allyMaster.inventory.CopyEquipmentFrom(characterBody.inventory); // opcional, pode herdar equipamentos
            allyMaster.inventory.GiveItem(RoR2Content.Items.UseAmbientLevel, 1); // faz escalar com nível do jogador

            allyMaster.SpawnBody(spawnPosition, Quaternion.identity);

            // Destruir depois de 15 segundos
            UnityEngine.Object.Destroy(allyMasterObj, 15f);

            Spawn = true;
        }

        private TurretTest.PlacementInfo GetPlacementInfo()
        {
            Ray aimRay = base.GetAimRay();
            Vector3 direction = aimRay.direction;
            direction.y = 0f;
            direction.Normalize();
            aimRay.direction = direction;
            TurretTest.PlacementInfo placementInfo = default(TurretTest.PlacementInfo);
            placementInfo.ok = false;
            placementInfo.rotation = Util.QuaternionSafeLookRotation(-direction);
            Ray ray = new Ray(aimRay.GetPoint(2f) + Vector3.up * 1f, Vector3.down);
            float num = 4f;
            float num2 = num;
            RaycastHit raycastHit;
            if (Physics.SphereCast(ray, 0.5f, out raycastHit, num, LayerIndex.world.mask) && raycastHit.normal.y > 0.5f)
            {
                num2 = raycastHit.distance;
                placementInfo.ok = true;
            }
            Vector3 point = ray.GetPoint(num2 + 0.5f);
            placementInfo.position = point;
            if (placementInfo.ok)
            {
                float num3 = Mathf.Max(1.82f, 0f);
                if (Physics.CheckCapsule(placementInfo.position + Vector3.up * (num3 - 0.5f), placementInfo.position + Vector3.up * 0.5f, 0.45f, LayerIndex.world.mask | LayerIndex.CommonMasks.characterBodiesOrDefault))
                {
                    placementInfo.ok = false;
                }
            }
            return placementInfo;
        }

        // Token: 0x06001708 RID: 5896 RVA: 0x0006A082 File Offset: 0x00068282
        private void DestroyBlueprints()
        {
            if (this.blueprints)
            {
                EntityState.Destroy(this.blueprints.gameObject);
                this.blueprints = null;
            }
        }

        public override void Update()
        {
            base.Update();
            this.currentPlacementInfo = this.GetPlacementInfo();
            if (this.blueprints)
            {
                this.blueprints.PushState(this.currentPlacementInfo.position, this.currentPlacementInfo.rotation, this.currentPlacementInfo.ok);
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();


            if (base.isAuthority)
            {
                this.entryCountdown -= base.GetDeltaTime();
                if (this.exitPending)
                {
                    this.exitCountdown -= base.GetDeltaTime();
                    if (this.exitCountdown <= 0f)
                    {
                        this.outer.SetNextStateToMain();
                        return;
                    }
                }
                else if (base.inputBank && this.entryCountdown <= 0f)
                {
                    if ((base.inputBank.skill1.down || base.inputBank.skill4.justPressed) && this.currentPlacementInfo.ok)
                    {
                        if (base.characterBody)
                        {
                            base.characterBody.SendConstructTurret(base.characterBody, this.currentPlacementInfo.position, this.currentPlacementInfo.rotation, MasterCatalog.FindMasterIndex(this.turretMasterPrefab));
                            //if (base.skillLocator)
                            //{
                            //    GenericSkill skill = base.skillLocator.GetSkill(SkillSlot.Special);
                            //    if (skill)
                            //    {
                            //        skill.DeductStock(1);
                            //    }
                            //}
                        }
                        //Util.PlaySound(this.placeSoundString, base.gameObject);
                        this.DestroyBlueprints();
                        this.exitPending = true;
                    }
                    if (base.inputBank.skill2.justPressed)
                    {
                        this.DestroyBlueprints();
                        this.exitPending = true;
                    }
                }
            }


            //if (!Spawn && isAuthority)
            //{
            //    SpawnAlly();
            //}
            //else
            //{
            //    Spawn = false;
            //    this.outer.SetNextStateToMain();
            //}




        }


        public override void OnExit()
        {

            ////SyncNetworkExe.MemoryCode = ////SyncNetworkExe.MemoryCode + "B";

            //if (//SyncNetworkExe.EvilEmotionValue > 0)
            //SyncNetworkExe.EvilEmotionValue--;

            if(isAuthority)
            {
                execomponent.UpdateEmotionalValue(1, -1, 0);

                execomponent.UpdateMemoryCode('B');
            }

            //this.PlayAnimation("Gesture", TurretTest.PlaceTurretStateHash);
            //if (this.wristDisplayObject)
            //{
            //    EntityState.Destroy(this.wristDisplayObject);
            //}
            this.DestroyBlueprints();

            base.OnExit();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
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