using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class IdleState : State
    {
        private readonly Animator _animator;
        private readonly NavMeshAgent _agent;
        private float _idleTimer;

        private static readonly int Speed = Animator.StringToHash("Speed");

        public IdleState(EnemyController enemyController) : base(enemyController)
        {
            _animator = enemyController.Animator;
            _agent = enemyController.NavMeshAgent;
        }

        public override void OnStateEnter()
        {
            _idleTimer = Random.Range(0, 15);
        }

        public override void UpdateState()
        {
            if (CheckForAttackTarget())
            {
                // Si el hay un target en la esfera de detección y en el campo de vision pasar al estado de atauqe
                enemyController.ChangeState(enemyController.AttackState);
                return;
            }

            // Controlar que el enemigo se mantenga en Idle una cierta cantidad de tiempo
            if (_idleTimer <= 0)
            {
                enemyController.ChangeState(enemyController.PatrolState);
                return;
            }

            _idleTimer -= Time.deltaTime;
            _animator.SetFloat(Speed, _agent.velocity.magnitude);
        }

        /** Comprobar si hay un objetivo de ataque en la esfera de detección y en el campo de visión */
        private bool CheckForAttackTarget()
        {
            // Comprobar que haya un target en la esfera de detección
            if (!enemyController.Target) return false;

            // Comprobar que el target este en el angulo de visión
            Transform enemyTransform = enemyController.transform;
            Vector3 targetDir = enemyController.Target.position - enemyTransform.position;
            float angle = Vector3.Angle(enemyTransform.forward, targetDir);
            return !(angle > 90);
        }
    }
}