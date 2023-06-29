using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject menuPausa;    // Referencia al menú de pausa
    public GameObject menuPerdiste; // Referencia al menú de "perdiste"
    public GameObject itemsUI;      // Referencia a la interfaz de usuario de los items

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) || Input.GetKey("p"))
        {
            ActivarPausa();     // Activa el menú de pausa cuando se presiona la tecla Escape o la tecla p
        }
    }

    public void ActivarPausa()
    {
        Time.timeScale = 0; //Pone en pausa el juego
        menuPausa.SetActive(true);    // Activa el menú de pausa
    }

    public void DesactivarPausa()
    {
        Time.timeScale = 1; //Reanuda el juego
        menuPausa.SetActive(false);   // Desactiva el menú de pausa
    }

    public void Menu()
    {
        Time.timeScale = 1;//Activa la pantalla
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

    public void ActivarMenuItems()
    {
        itemsUI.SetActive(true);      // Activa la interfaz de usuario de los items
    }

    public void DesactivarMenuItems()
    {
        itemsUI.SetActive(false);     // Desactiva la interfaz de usuario de los items
    }
}

