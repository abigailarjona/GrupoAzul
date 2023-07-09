using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorCollision : MonoBehaviour
{
    public UIManager manager; // Referencia al UIManager

    private bool estaEnRango = false; // Indica si el jugador está dentro del rango de interacción
    private int numerito = 0; // Variable de control para los ítems

    public GameObject item1; // Referencia al ítem 1
    public GameObject item2; // Referencia al ítem 2
    public GameObject item3; // Referencia al ítem 3
    public GameObject item4; // Referencia al ítem 4
    public GameObject item5; // Referencia al ítem 5
    public GameObject item6; // Referencia al ítem 6
    public GameObject item7; // Referencia al ítem 7
    public GameObject item8; // Referencia al ítem 8
    public GameObject item9; // Referencia al ítem 9

    void Update()
    {
        if (estaEnRango)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                numerito++; // Incrementa el valor de numerito cuando se presiona la tecla E
            }
        }

        switch (numerito)
        {
            case 0:
                // No se activa ningún ítem
                break;

            case 1:
                item4.SetActive(false); // Activa el ítem 1
                item7.SetActive(true); // Activa el ítem 4
                break;

            case 2:
                item5.SetActive(false); // Activa el ítem 2
                item8.SetActive(true); // Activa el ítem 5
                break;

            case 3:
                item6.SetActive(false); // Activa el ítem 3
                item9.SetActive(true); // Activa el ítem 6
                break;

            default:
                // No se activa ningún ítem
                break;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Enemigo"))
        {
            manager.ActivarMenuPerdiste(); // Activa el menú de pérdida si colisiona con un enemigo
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.transform.CompareTag("Componente"))
        {
            manager.ActivarMostrarMensaje(
                "Presiona \"E\" para dejar los objetos"); // Activa la interfaz de usuario de los ítems si permanece en el rango

            estaEnRango = true; // Indica que el jugador está en rango de interacción
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.transform.CompareTag("Componente"))
        {
            manager.DesactivarMostrarMensaje(); // Desactiva la interfaz de usuario de los ítems si sale del rango
        }
    }
}