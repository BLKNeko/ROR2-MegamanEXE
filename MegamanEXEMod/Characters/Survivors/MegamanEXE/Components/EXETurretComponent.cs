using RoR2;
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
        public float fireInterval = 0.5f;
        public GameObject projectilePrefab;
        public Transform firePoint;

        private Vector3 direction;
        private Vector3 originEXE;

        private float fireTimer;

        private Animator animator;

        void Awake()
        {
            // Caso o Animator esteja no próprio objeto
            //animator = GetComponent<Animator>();

            // Caso o Animator esteja em um filho, use:
            //animator = GetComponentInChildren<Animator>();
        }

        void Start()
        {
            Destroy(gameObject, lifetime);
        }

        void FixedUpdate()
        {
            fireTimer -= Time.fixedDeltaTime;

            // Busca inimigos
            HurtBox target = FindTarget();

            if (target)
            {
                // Rotaciona em direção ao alvo
                direction = target.transform.position - transform.position;
                direction.y = 0;

                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
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
                //GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(firePoint.forward));
                // Aqui você pode adicionar dano, efeitos, etc

                originEXE = gameObject.transform.position;
                originEXE.y += 3f;

                //animator.Play("Shoots");

                new BulletAttack
                {
                    bulletCount = 1,
                    aimVector = direction,
                    origin = originEXE,
                    damage = 1f,
                    damageColorIndex = DamageColorIndex.Default,
                    damageType = DamageTypeCombo.GenericPrimary,
                    falloffModel = BulletAttack.FalloffModel.None,
                    maxDistance = 500,
                    force = 800,
                    hitMask = LayerIndex.CommonMasks.bullet,
                    minSpread = 0f,
                    maxSpread = 0f,
                    //isCrit = RollCrit(),
                    owner = gameObject,
                    //muzzleName = muzzleString,
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
            search.searchOrigin = transform.position;
            search.sortMode = BullseyeSearch.SortMode.Distance;
            search.filterByLoS = false;
            search.RefreshCandidates();

            return search.GetResults().FirstOrDefault();
        }
    }

}