using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class AttackState : State
    {
        private readonly Animator _animator;
        private readonly NavMeshAgent _agent;


        private Vector3 _lastKnownPos;
        private Vector3 _closestPointToLastKnowPos;
        private float _lastAttackedAt;
        private float _attackCooldown;
        private float _attackRange;

        private static readonly int Speed = Animator.StringToHash("Speed");

        public AttackState(EnemyController enemyController) : base(enemyController)
        {
            _agent = enemyController.NavMeshAgent;
            _animator = enemyController.Animator;
        }

        public override void OnStateEnter()
        {
            _agent.speed = 4f;
            _lastKnownPos = enemyController.Target.position;

            _lastAttackedAt = Time.time;
            _attackCooldown = 0f;
            _attackRange = 1f;

            MoveToDestination();
        }

        public override void UpdateState()
        {
            // Comprobar si el npc esta atacando
            bool isAttacking = Time.time < _lastAttackedAt + _attackCooldown;

            // Comprobar si el objetivo de ataque esta fuero del rango de detección
            if (!enemyController.Target)
            {
                // Obtener el punto mas cercano a la ultima posicion conocida del target
                if (_closestPointToLastKnowPos == Vector3.zero)
                {
                    _closestPointToLastKnowPos = GetClosestPointInNavMeshSurface(_lastKnownPos);
                    _agent.destination = _closestPointToLastKnowPos;
                }

                if (Vector3.SqrMagnitude(_closestPointToLastKnowPos - enemyController.transform.position) > 1f)
                    _animator.SetFloat(Speed, _agent.velocity.magnitude);
                else
                    enemyController.ChangeState(enemyController.IdleState);
                return;
            }

            // Si esta atacando rotar hacia el target
            if (isAttacking)
            {
                Vector3 targetDirection = enemyController.Target.position - enemyController.transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
                enemyController.transform.rotation = Quaternion.Slerp(enemyController.transform.rotation, lookRotation,
                    Time.deltaTime * 30f);
                return;
            }

            _closestPointToLastKnowPos = Vector3.zero;
            MoveToDestination();

            // Comprobar si el target esta en rango de ataque
            bool targetInAttackRange =
                Vector3.SqrMagnitude(enemyController.Target.position - enemyController.transform.position) <
                _attackRange * _attackRange;
            if (!targetInAttackRange) return;

            // Iniciar animacion de ataque
            _agent.destination = enemyController.transform.position;
            _animator.SetFloat(Speed, 0f);
            _lastAttackedAt = Time.time;
            _attackCooldown = enemyController.OnAttack();
        }

        private void MoveToDestination()
        {
            if (enemyController.Target)
                _lastKnownPos = enemyController.Target.position;
            _agent.destination = _lastKnownPos;
            _animator.SetFloat(Speed, _agent.velocity.magnitude);
        }

        private Vector3 GetClosestPointInNavMeshSurface(Vector3 pos)
        {
            Vector3 closestPos = NavMesh.SamplePosition(pos, out NavMeshHit hit, 10f, NavMesh.AllAreas)
                ? hit.position
                : enemyController.transform.position;
            return closestPos;
        }
    }
}