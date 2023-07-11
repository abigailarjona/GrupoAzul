using System;
using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace HealthSystem
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerController))]
    public class BarraVida : MonoBehaviour, IDamageable
    {
        public float vidaInicial = 100f; // Valor inicial de la vida
        public float tiempoEspera = 3f; // Tiempo de espera antes de llamar al respawn
        [SerializeField] private PlayerInput playerInput;

        private float _vidaActual; // Valor actual de la vida
        private PlayerController _playerController;
        private ShooterController _shooterController;
        private Animator _animator;
        private float _airTime; // Cantidad de tiempo en el aire
        private const float FallDamage = 25f;

        public static Action OnDeath;
        public static Action<float, float> OnDamageReceived;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerController = GetComponent<PlayerController>();
            _shooterController = GetComponent<ShooterController>();
            _vidaActual = vidaInicial;
        }

        private void Update()
        {
            HandleFallDamage();
        }

        private void HandleFallDamage()
        {
            if (!_playerController.Grounded)
            {
                _airTime += Time.deltaTime;
            }
            else
            {
                if (_airTime > 2f)
                {
                    float fallDamage = _airTime * FallDamage;
                    TakeDamage(transform, (int)fallDamage);
                }

                _airTime = 0f;
            }
        }

        // Corrutina para manejar la muerta y respawn del jugador
        private IEnumerator StartDyingAnimation()
        {
            // Desactivar componentes Controller del jugador
            _playerController.enabled = false;
            playerInput.enabled = false;

            // Reproducir y esperar a que termine la animación de muerte
            _animator.SetTrigger($"Dying");
            float animationLength = _animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animationLength + tiempoEspera);

            OnDeath?.Invoke();
        }

        public void TakeDamage(Transform attacker, int damageTaken)
        {
            if (_vidaActual <= 0) return;
            _vidaActual -= damageTaken;
            _vidaActual =
                Mathf.Clamp(_vidaActual, 0,
                    vidaInicial); // Asegura que el valor de vida no sea menor que 0 ni mayor que el valor inicial

            OnDamageReceived?.Invoke(_vidaActual, vidaInicial);
            CheckIfDead();
        }

        private void CheckIfDead()
        {
            if (_vidaActual > 0) return;
            StartCoroutine(StartDyingAnimation());
        }
    }
}