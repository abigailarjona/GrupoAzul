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

    public void ActivarMenuPerdiste()
    {
        menuPerdiste.SetActive(true); // Activa el menú de pérdida
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