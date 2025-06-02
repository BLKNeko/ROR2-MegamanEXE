using RoR2;
using RoR2.Projectile;
using System;
using System.Linq;
using UnityEngine;
using static UnityEngine.ParticleSystem.PlaybackState;

namespace MegamanEXEMod.Survivors.MegamanEXE.Components
{
    public class EXETurretComponent : MonoBehaviour
    {
        public float lifetime = 10f;
        public float scanRange = 30f;
        public float fireInterval = 0.25f;
        public GameObject projectilePrefab;
        public Transform firePoint;

        private Vector3 direction;
        private Vector3 shootDir;
        private Vector3 originEXE;

        private float fireTimer;

        private Animator animator;

        private ProjectileController projectileController;

        void Awake()
        {
            // Caso o Animator esteja no próprio objeto
            //animator = GetComponent<Animator>();

            // Caso o Animator esteja em um filho, use:
            //animator = GetComponentInChildren<Animator>();

            projectileController = GetComponent<ProjectileController>();

            

            if (!firePoint)
            {
                GameObject fp = new GameObject("FirePoint");
                fp.transform.parent = transform;
                fp.transform.localPosition = Vector3.up * 1.5f + Vector3.forward * 0.5f; // Ajuste conforme sua turret
                firePoint = fp.transform;
            }

            //if (!GetComponent<TeamComponent>())
            //{
            //    var teamComponent = gameObject.AddComponent<TeamComponent>();
            //    teamComponent.teamIndex = TeamIndex.Player; // ou outro time se for necessário
            //}

        }

        void Start()
        {
            Destroy(gameObject, lifetime);
        }

        void FixedUpdate()
        {
            fireTimer -= Time.fixedDeltaTime;

            HurtBox target = FindTarget();

            if (target)
            {
                var characterBody = target.healthComponent?.body;
                Vector3 targetPosition;

                if (characterBody != null)
                {
                    targetPosition = characterBody.corePosition;
                }
                else
                {
                    // Fallback para o centro do collider da hitbox
                    targetPosition = target.transform.position + target.collider.bounds.center - target.collider.transform.position;
                }

                shootDir = (targetPosition - projectileController.transform.position).normalized;
                direction = new Vector3(shootDir.x, 0, shootDir.z); // Só gira no eixo Y

                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
                }

                if (fireTimer <= 0f)
                {
                    Fire(target);
                    fireTimer = fireInterval;
                }
            }
        }

        void Fire(HurtBox target)
        {
            if (target)
            {
                new BulletAttack
                {
                    bulletCount = 1,
                    aimVector = shootDir,
                    origin = firePoint.position,
                    damage = 5f,
                    damageColorIndex = DamageColorIndex.Default,
                    damageType = DamageType.Generic,
                    falloffModel = BulletAttack.FalloffModel.None,
                    maxDistance = 1000f,
                    force = 800f,
                    hitMask = LayerIndex.CommonMasks.bullet,
                    minSpread = 0f,
                    maxSpread = 0f,
                    isCrit = false,
                    owner = gameObject,
                    smartCollision = true,
                    procChainMask = default,
                    procCoefficient = 1f,
                    radius = 0.75f,
                    sniper = false,
                    stopperMask = LayerIndex.CommonMasks.bullet,
                    weapon = null,
                    tracerEffectPrefab = LegacyResourcesAPI.Load<GameObject>("prefabs/effects/tracers/TracerBanditShotgun"),
                    spreadPitchScale = 1f,
                    spreadYawScale = 1f,
                    queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                    hitEffectPrefab = EntityStates.Commando.CommandoWeapon.FireShotgun.hitEffectPrefab,
                }.Fire();
            }
        }

        HurtBox FindTarget()
        {
            BullseyeSearch search = new BullseyeSearch();
            search.teamMaskFilter = TeamMask.GetUnprotectedTeams(TeamIndex.Player);
            search.maxDistanceFilter = scanRange;
            search.searchOrigin = firePoint.position;
            search.sortMode = BullseyeSearch.SortMode.Distance;
            search.filterByLoS = true; // Agora só alvos com linha de visão
            search.RefreshCandidates();

            return search.GetResults().FirstOrDefault();
        }
    }

}