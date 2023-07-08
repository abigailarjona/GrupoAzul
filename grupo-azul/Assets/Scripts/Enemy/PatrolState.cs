using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class PatrolState : State
    {
        private readonly Animator _animator;
        private readonly NavMeshAgent _agent;
        private readonly Vector3 _initialPosition;
        private Vector3 _targetDestination;

        private const float PatrolRange = 10f;
        private float _stopDistance;
        private static readonly int Speed = Animator.StringToHash("Speed");

        public PatrolState(EnemyController enemyController) : base(enemyController)
        {
            _animator = enemyController.Animator;
            _agent = enemyController.NavMeshAgent;
            _initialPosition = enemyController.transform.position;
        }

        public override void OnStateEnter()
        {
            _agent.speed = 1f;
            _stopDistance = _agent.stoppingDistance + 0.25f;
            _targetDestination = GetRandomDestination();
            MoveToDestination();
        }

        public override void UpdateState()
        {
            // Si el hay un target en la esfera de detección y en el campo de vision pasar al estado de atauqe
            if (CheckForAttackTarget())
            {
                enemyController.ChangeState(enemyController.AttackState);
                return;
            }

            // Si el enemigo llego al punto de patrulla entrar al estado Idle
            if (_agent.remainingDistance <= _stopDistance)
            {
                enemyController.ChangeState(enemyController.IdleState);
            }

            MoveToDestination();
        }


        /// Comprobar si hay un objetivo de ataque en la esfera de detección y en el campo de visión
        private bool CheckForAttackTarget()
        {
            if (!enemyController.Target) return false;

            Transform enemyTransform = enemyController.transform;
            Vector3 targetDir = enemyController.Target.position - enemyTransform.position;
            float angle = Vector3.Angle(enemyTransform.forward, targetDir);
            return angle < 90;
        }

        private void MoveToDestination()
        {
            _agent.destination = _targetDestination;
            _animator.SetFloat(Speed, _agent.velocity.magnitude);
        }


        /// <summary>
        /// Obtener un nuevo punto de destino.
        /// </summary>
        /// <returns>Un Vector3 con una ubicación valida en el NavMeshSurface. Si no se encuentra una se devuelve la posición actual del NPC.</returns>
        private Vector3 GetRandomDestination()
        {
            Vector3 randomDirection = _targetDestination = Random.insideUnitSphere * PatrolRange;
            randomDirection += _initialPosition;
            Vector3 destination = enemyController.transform.position;
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, PatrolRange, 1))
            {
                destination = hit.position;
            }

            return destination;
        }
    }
}