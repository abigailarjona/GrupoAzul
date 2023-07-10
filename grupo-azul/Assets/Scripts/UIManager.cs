using System;
using System.Collections;
using HealthSystem;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject menuPausa; // Referencia al menú de pausa
    public GameObject menuHasGanado; // Referencia al menú Has Ganado
    public GameObject menuPerdiste; // Referencia al menú de "perdiste"
    public GameObject mostrarMensaje; // Referencia a la interfaz de usuario mostrar mensaje
    [SerializeField] private Inputs inputs;
    [SerializeField] private Image barraDeVida;

    public static Action<string> onShowMessage;
    public static Action onHideMessage;

    public void OnEnable()
    {
        onShowMessage += ActivarMostrarMensaje;
        onHideMessage += DesactivarMostrarMensaje;
        BarraVida.onDeath += OnDeath;
        BarraVida.onDamageReceived += OnDamageReceived;
    }

    public void OnDisable()
    {
        onShowMessage -= ActivarMostrarMensaje;
        onHideMessage -= DesactivarMostrarMensaje;
        BarraVida.onDeath -= OnDeath;
        BarraVida.onDamageReceived -= OnDamageReceived;
    }

    public void ActivarPausa()
    {
        inputs.CursorLocked = false; // Desactivar movimiento de la camara con el mouse
        Time.timeScale = 0; // Poner en pausa el juego
        menuPausa.SetActive(true); // Activa el menú de pausa
    }

    public void DesactivarPausa()
    {
        inputs.CursorLocked = true; // Activar movimiento de la camara con el mouse
        Time.timeScale = 1; // Reanudar el juego
        menuPausa.SetActive(false); // Desactiva el menú de pausa
    }

    public void Menu()
    {
        Time.timeScale = 1; //Activa la pantalla
        SceneManager.LoadScene("MainMenu"); // Va al menu principal
    }

    public void Salir()
    {
        Application.Quit(); //Sale del juego
    }

    // Actualiza la barra de vida de acuerdo al valor actual
    private void OnDamageReceived(float currentHealth, float maxHealth)
    {
        barraDeVida.fillAmount = currentHealth / maxHealth;
    }

    private void OnDeath()
    {
        StartCoroutine(ActivarMenuPerdiste());
    }

    public IEnumerator ActivarMenuPerdiste()
    {
        // Mostrar imagen Has muerto
        menuPerdiste.SetActive(true); // Activa el menú de pérdida
        yield return new WaitForSeconds(10f);

        // Recargar escena
        SceneManager.LoadScene("Scenario1");
    }

    public void ActivarMenuHasGanado()
    {
        menuHasGanado.SetActive(true);
    }

    public void ActivarMostrarMensaje(string mensaje)
    {
        Text msg = mostrarMensaje.GetComponentsInChildren<Text>()[0];
        msg.text = mensaje;
        mostrarMensaje.SetActive(true); // Activa la interfaz de usuario para mostrar el mensaje
    }

    public void DesactivarMostrarMensaje()
    {
        mostrarMensaje.SetActive(false); // Desactiva la interfaz de usuario de los items
    }
}