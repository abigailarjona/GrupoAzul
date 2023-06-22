using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject menuPausa;    // Referencia al menú de pausa
    public GameObject menuPerdiste; // Referencia al menú de "perdiste"
    public GameObject itemsUI;      // Referencia a la interfaz de usuario de los items

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActivarPausa();     // Activa el menú de pausa cuando se presiona la tecla Escape
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            DesactivarPausa();  // Desactiva el menú de pausa cuando se suelta la tecla Escape
        }
    }

    public void ActivarPausa()
    {
        menuPausa.SetActive(true);    // Activa el menú de pausa
    }

    public void DesactivarPausa()
    {
        menuPausa.SetActive(false);   // Desactiva el menú de pausa
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

