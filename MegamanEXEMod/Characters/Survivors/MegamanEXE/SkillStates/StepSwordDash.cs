using EntityStates;
using MegamanEXEMod.Modules.BaseStates;
using MegamanEXEMod.Survivors.MegamanEXE;
using MegamanEXEMod.Survivors.MegamanEXE.Components;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using static RoR2.BulletAttack;

namespace MegamanEXEMod.Survivors.MegamanEXE.SkillStates
{
    public class StepSwordDash : BaseSkillState
    {

        public static float initialSpeedCoefficient = 5f;
        public static float finalSpeedCoefficient = 4f;
        public static float dodgeFOV = global::EntityStates.Commando.DodgeState.dodgeFOV;

        private float rollSpeed;
        private Vector3 forwardDirection;
        private Animator animator;
        private Vector3 previousPosition;

        private ChildLocator childLocator;

        public static float duration = 0.8f;

        public static float overlapSphereRadius = 1f;

        public override void OnEnter()
        {


            //AkSoundEngine.PostEvent(ZeroStaticValues.zDash, this.gameObject);

            animator = GetModelAnimator();
            characterBody.SetAimTimer(0.8f);
            Ray aimRay = GetAimRay();

            base.characterMotor.Motor.ForceUnground(0.1f);

            if (isAuthority && inputBank && characterDirection)
            {
                forwardDirection = aimRay.direction.normalized;
            }

            if (characterMotor && characterDirection)
            {
                characterMotor.velocity = forwardDirection.normalized * moveSpeedStat * initialSpeedCoefficient;
            }

            base.PlayAnimation("FullBody, Override", "StepSwordLoop", "attackSpeed", duration);

            base.OnEnter();
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            base.characterMotor.Motor.ForceUnground(0.1f);

            if (characterDirection) characterDirection.forward = forwardDirection;

            if (cameraTargetParams)
                cameraTargetParams.fovOverride = Mathf.Lerp(dodgeFOV, 60f, fixedAge / duration);


            if (characterMotor && characterDirection)
            {
                characterMotor.velocity = forwardDirection.normalized * moveSpeedStat * Mathf.Lerp(initialSpeedCoefficient, finalSpeedCoefficient, fixedAge / duration);
            }


            // Verifica colisão durante o dash
            if (isAuthority)
            {
                // Define o raio da colisão
                float radius = characterBody.radius + overlapSphereRadius;

                // Busca todos os colliders na camada de entidades
                Collider[] hitColliders = Physics.OverlapSphere(
                    base.transform.position,
                    radius,
                    LayerIndex.entityPrecise.mask
                );

                foreach (var collider in hitColliders)
                {
                    // Verifica se tem um HurtBox no collider
                    HurtBox hurtBox = collider.GetComponent<HurtBox>();

                    // Se é um inimigo (não é o próprio personagem)
                    if (hurtBox != null && hurtBox.healthComponent != null && hurtBox.healthComponent != base.healthComponent)
                    {
                        // ✅ Cancela o dash e troca para o estado de ataque
                        outer.SetNextState(new StepSwordAttack());

                        // Sai da função após encontrar o primeiro inimigo
                        return;
                    }
                }
            }



            if (isAuthority && fixedAge >= duration)
            {
                outer.SetNextStateToMain();
                return;
            }

        }

        public override void OnExit()
        {

            base.PlayAnimation("FullBody, Override", "DashEnd", "attackSpeed", duration);

            //On.RoR2.UI.HUD.Awake -= HUD_Awake;

            base.OnExit();
        }

        public override void OnSerialize(NetworkWriter writer)
        {
            base.OnSerialize(writer);
            writer.Write(forwardDirection);
        }

        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);
            forwardDirection = reader.ReadVector3();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }
    }
}