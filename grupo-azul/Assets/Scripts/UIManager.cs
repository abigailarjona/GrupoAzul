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
    public GameObject menuPrimeraParteSuperada; // Referencia al menú de primara parte completada
    public GameObject menuHasGanado; // Referencia al menú Has Ganado
    public GameObject menuPerdiste; // Referencia al menú de "perdiste"
    public GameObject mostrarMensaje; // Referencia a la interfaz de usuario mostrar mensaje
    [SerializeField] private Inputs inputs;
    [SerializeField] private Image barraDeVida;

    public static Action<string> ShowMessage;
    public static Action HideMessage;

    public void OnEnable()
    {
        ShowMessage += UIManager_MostrarMensaje;
        HideMessage += UIManager_OcultarMensaje;
        BarraVida.OnDeath += BarraVida_OnDeath;
        BarraVida.OnDamageReceived += BarrarVida_OnDamageReceived;
    }

    public void OnDisable()
    {
        ShowMessage -= UIManager_MostrarMensaje;
        HideMessage -= UIManager_OcultarMensaje;
        BarraVida.OnDeath -= BarraVida_OnDeath;
        BarraVida.OnDamageReceived -= BarrarVida_OnDamageReceived;
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
    private void BarrarVida_OnDamageReceived(float currentHealth, float maxHealth)
    {
        barraDeVida.fillAmount = currentHealth / maxHealth;
    }

    private void BarraVida_OnDeath()
    {
        StartCoroutine(MostrarPerdiste());
    }

    public IEnumerator MostrarPerdiste()
    {
        // Mostrar imagen Has muerto
        menuPerdiste.SetActive(true);
        yield return new WaitForSeconds(10f);

        // Recargar escena
        SceneManager.LoadScene("Scenario1");
    }

    public void ToggleFirstPartCompleted(bool value)
    {
        menuPrimeraParteSuperada.SetActive(value);
    }


    public void MostrarHasGanado()
    {
        menuHasGanado.SetActive(true);
    }

    private void UIManager_MostrarMensaje(string mensaje)
    {
        Text msg = mostrarMensaje.GetComponentsInChildren<Text>()[0];
        msg.text = mensaje;
        mostrarMensaje.SetActive(true);
    }

    private void UIManager_OcultarMensaje()
    {
        mostrarMensaje.SetActive(false);
    }
}