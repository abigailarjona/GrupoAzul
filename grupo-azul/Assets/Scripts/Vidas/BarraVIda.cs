using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVIda : MonoBehaviour
{
    public Image barraDeVida;               // Referencia al componente Image de la barra de vida
    public float vidaActual;                // Valor actual de la vida
    public float vidaInicial = 100f;        // Valor inicial de la vida
    public static BarraVIda instance;       // Instancia estática de la barra de vida
    public Animator animator;               // Referencia al componente Animator
    public RespawnController respawnController; // Referencia al script de RespawnController
    public float tiempoEspera = 3f;         // Tiempo de espera antes de llamar al respawn

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>(); // Obtener el componente Animator
    }

    void Update()
    {
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaInicial); // Asegura que el valor de vida no sea menor que 0 ni mayor que el valor inicial

        barraDeVida.fillAmount = vidaActual / vidaInicial; // Actualiza la barra de vida de acuerdo al valor actual

        if (vidaActual <= 0)
        {
            animator.SetBool("dead", true); // Activa la animación de muerte
            Invoke("CallRespawn", tiempoEspera); // Invoca el método CallRespawn después de un tiempo de espera
        }
    }

    // Método invocado después del tiempo de espera para llamar al respawn
    void CallRespawn()
    {
        respawnController.Respawn(); // Llama al método Respawn del RespawnController
    }

    // Restablece la vida a su valor inicial y desactiva la animación de muerte
    public void RestablecerVida()
    {
        vidaActual = vidaInicial;
        animator.SetBool("dead", false);
    }
}
