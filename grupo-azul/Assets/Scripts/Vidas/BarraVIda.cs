using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image barraDeVida; // Referencia al componente Image de la barra de vida
    public float vidaActual; // Valor actual de la vida
    public float vidaInicial = 100f; // Valor inicial de la vida
    public static BarraVida instance; // Instancia estática de la barra de vida
    public Animator animator; // Referencia al componente Animator
    public RespawnController respawnController; // Referencia al script de RespawnController
    public float tiempoEspera = 3f; // Tiempo de espera antes de llamar al respawn
    [SerializeField] private Vector3 respawnPosition;
    public PlayerController PlayerController;

    private bool _isAlive = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>(); // Obtener el componente Animator
        if (animator == null)
            Debug.LogError("Animator is missing");
    }

    private void Update()
    {
        vidaActual =
            Mathf.Clamp(vidaActual, 0,vidaInicial); // Asegura que el valor de vida no sea menor que 0 ni mayor que el valor inicial

        barraDeVida.fillAmount = vidaActual / vidaInicial; // Actualiza la barra de vida de acuerdo al valor actual

        if (vidaActual <= 0 && _isAlive)
        {
            _isAlive = false;
            StartCoroutine(Die());
            
        }
    }

    // Corrutina para manejar la muerta y respawn del jugador
    private IEnumerator Die()
    {
        animator.SetTrigger($"Dying");
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLength + tiempoEspera);

        // Reproducir la animación de transición antes del respawn


        const float tiempoRespawn = 3f;
        yield return new WaitForSeconds(tiempoRespawn);

        SceneManager.LoadScene("SampleScene");
    }

    // Método invocado después del tiempo de espera para llamar al respawn
    private IEnumerator CallRespawn()
    {
        yield return respawnController.Respawn(); // Llama al método Respawn del RespawnController
    }

    // Restablecer la vida a su valor inicial y desactivar la animación de muerte
    public void RestablecerVida()
    {
        vidaActual = vidaInicial;
    }
}