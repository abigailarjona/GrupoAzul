using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HealthSystem
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerController))]
    public class BarraVida : MonoBehaviour, IDamageable
    {
        public UIManager uiManager; // Referencia al componente Image de la barra de vida
        public Image barraDeVida; // Referencia al componente Image de la barra de vida
        public float vidaInicial = 100f; // Valor inicial de la vida
        public float tiempoEspera = 3f; // Tiempo de espera antes de llamar al respawn
        public static BarraVida instance; // Instancia estática de la barra de vida

        private float _vidaActual; // Valor actual de la vida
        private bool _isAlive = true;
        private PlayerController _playerController;
        private ShooterController _shooterController;
        private Animator _animator;
        private float _airTime; // Cantidad de tiempo en el aire
        private const float FallDamage = 25f;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            _animator = GetComponent<Animator>();
            _playerController = GetComponent<PlayerController>();
            _shooterController = GetComponent<ShooterController>();
            _vidaActual = vidaInicial;
        }

        private void Update()
        {
            _vidaActual =
                Mathf.Clamp(_vidaActual, 0,
                    vidaInicial); // Asegura que el valor de vida no sea menor que 0 ni mayor que el valor inicial

            barraDeVida.fillAmount = _vidaActual / vidaInicial; // Actualiza la barra de vida de acuerdo al valor actual
            if (_vidaActual <= 0 && _isAlive)
            {
                _isAlive = false;
                StartCoroutine(OnDeath());
                return;
            }

            // Comprobar daño por caida
            if (!_playerController.Grounded)
            {
                _airTime += Time.deltaTime;
            }
            else
            {
                if (_airTime > 2f)
                {
                    Debug.Log(_airTime * FallDamage);
                    _vidaActual -= _airTime * FallDamage;
                }

                _airTime = 0;
            }
        }

        // Corrutina para manejar la muerta y respawn del jugador
        private IEnumerator OnDeath()
        {
            // Desactivar componentes Controller del jugador
            _playerController.enabled = false;
            _shooterController.enabled = false;

            // Reproducir y esperar a que termine la animación de muerte
            _animator.SetTrigger($"Dying");
            float animationLength = _animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animationLength + tiempoEspera);

            // Mostrar imagen Has muerto
            uiManager.ActivarMenuPerdiste();
            yield return new WaitForSeconds(tiempoEspera);

            // Esperar y recargar escena
            yield return new WaitForSeconds(tiempoEspera);
            SceneManager.LoadScene("Scenario1");
        }

        public void TakeDamage(Transform attacker, int damageTaken)
        {
            _vidaActual -= damageTaken;
        }
    }
}