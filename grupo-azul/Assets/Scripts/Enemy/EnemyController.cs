using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float detectionRange;
        [SerializeField] private LayerMask detectionLayerMask;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] enemyNoises;
        [SerializeField] private EnemyWeapon weapon;

        private bool _isPlayingAudioClip;
        private Collider _capsuleCollider;
        private Rigidbody[] _ragdollRigidBodies;
        private float _detectionCheckAt;
        private const float DetectionFrequency = 1f;

        #region Properties

        public Animator Animator { get; private set; }
        public NavMeshAgent NavMeshAgent { get; private set; }
        public Transform Target { get; private set; }

        #endregion

        #region State Machine variables

        private State _currentState;
        public PatrolState PatrolState { get; private set; }
        public IdleState IdleState { get; private set; }
        public AttackState AttackState { get; private set; }

        #endregion

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            NavMeshAgent = GetComponent<NavMeshAgent>();
            _capsuleCollider = GetComponent<Collider>();
            _ragdollRigidBodies = GetComponentsInChildren<Rigidbody>();

            ToggleRagdollPhysics(false);

            // Inicializar posibles estados del NPC
            IdleState = new IdleState(this);
            PatrolState = new PatrolState(this);
            AttackState = new AttackState(this);
        }

        private void Start()
        {
            ChangeState(PatrolState);
        }

        private void Update()
        {
            _currentState?.UpdateState();

            if (_isPlayingAudioClip) return;
            _isPlayingAudioClip = true;
            StartCoroutine(PlaySound());
        }

        private void FixedUpdate()
        {
            if (Time.time < _detectionCheckAt + DetectionFrequency) return;

            // Detectar objetivos de ataque cercanos    
            _detectionCheckAt = Time.time;
            Collider[] targetColliders = new Collider[1];
            int size = Physics.OverlapSphereNonAlloc(transform.position, detectionRange, targetColliders,
                detectionLayerMask);
            Target = size > 0 ? targetColliders[0].transform : null;
        }

        /// <summary>
        /// Motodo OnHurt se ejecuta cuando el npc recibe daño. Pasa el npc al estado de Ataque.
        /// </summary>
        public void OnHurt(Transform attacker)
        {
            Target = attacker;
            if (_currentState != AttackState)
                ChangeState(AttackState);
            StartCoroutine(PlaySound());
            _currentState?.UpdateState();
        }

        /// <summary>
        /// Metodo <c>OnDeath</c> inicia la animación de muerte y destruye los componentes de audio y movimiento.
        /// </summary>
        public void OnDeath()
        {
            Destroy(_capsuleCollider);
            ToggleRagdollPhysics(true);
            audioSource.Stop();
            Destroy(this);
        }

        /// <summary>
        /// Metodo <c>OnAttack</c> inicia la animación de ataque y devuelve un Float con la duración de la misma.
        /// </summary>
        public float OnAttack()
        {
            weapon.Attack();
            return weapon.GetAttackCooldown();
        }

        public void ChangeState(State newState)
        {
            _currentState?.OnStateExit();
            _currentState = newState;
            _currentState.OnStateEnter();
        }

        /// <summary>
        /// Metodo <c>ToggleRagdollPhysics</c> Activa o desactiva los componentes Animator y NavMeshAgent que interfieren con las físicas Ragdoll del NPC.
        /// </summary>
        private void ToggleRagdollPhysics(bool isActivated)
        {
            NavMeshAgent.enabled = !isActivated;
            Animator.enabled = !isActivated;
            foreach (Rigidbody rb in _ragdollRigidBodies)
            {
                rb.isKinematic = !isActivated;
            }
        }

        private IEnumerator PlaySound()
        {
            int index = Random.Range(0, enemyNoises.Length);
            AudioClip audioClip = enemyNoises[index];
            audioSource.clip = audioClip;
            audioSource.Play();
            yield return new WaitForSeconds(audioClip.length + 3f);
            _isPlayingAudioClip = false;
        }
    }
}